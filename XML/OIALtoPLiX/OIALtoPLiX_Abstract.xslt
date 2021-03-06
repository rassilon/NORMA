﻿<?xml version="1.0" encoding="utf-8"?>
<!--
	Neumont Object-Role Modeling Architect for Visual Studio

	Copyright © Neumont University. All rights reserved.
	Copyright © ORM Solutions, LLC. All rights reserved.

	The use and distribution terms for this software are covered by the
	Common Public License 1.0 (http://opensource.org/licenses/cpl) which
	can be found in the file CPL.txt at the root of this distribution.
	By using this software in any fashion, you are agreeing to be bound by
	the terms of this license.

	You must not remove this notice, or any other, from this software.
-->
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:exsl="http://exslt.org/common"
	xmlns:oial="http://schemas.neumont.edu/ORM/Abstraction/2007-06/Core"
	xmlns:plx="http://schemas.neumont.edu/CodeGeneration/PLiX"
	xmlns:prop="urn:schemas-orm-net:PLiX:CLI:Properties"
	exclude-result-prefixes="oial"
	extension-element-prefixes="exsl">
	
	<xsl:import href="OIALtoPLiX_GlobalSupportFunctions.xslt"/>
	<xsl:param name="ORM"/>
	<xsl:output method="xml" encoding="utf-8" media-type="text/xml" indent="yes"/>

	<xsl:variable name="OialModel" select="$ORM/oial:model"/>
	<xsl:variable name="PropertiesRoot" select="prop:AllProperties"/>

	<xsl:template match="/">
		<plx:root>
			<plx:namespaceImport name="System"/>
			<plx:namespaceImport name="System.Collections.Generic"/>
			<plx:namespaceImport name="System.Collections.ObjectModel"/>
			<plx:namespaceImport name="System.ComponentModel"/>
			<plx:namespaceImport name="System.Xml"/>
			<xsl:variable name="modelName" select="string($PropertiesRoot/@modelName)"/>
			<xsl:variable name="allProperties" select="$PropertiesRoot/prop:Properties"/>
			<xsl:choose>
				<xsl:when test="$DefaultNamespace">
					<plx:namespace name="{$DefaultNamespace}">
						<xsl:apply-templates select="$OialModel" mode="OIALtoPLiX_Abstract">
							<xsl:with-param name="ModelName" select="$modelName"/>
							<xsl:with-param name="AllProperties" select="$allProperties"/>
						</xsl:apply-templates>
					</plx:namespace>
				</xsl:when>
				<xsl:otherwise>
					<xsl:apply-templates select="$OialModel" mode="OIALtoPLiX_Abstract">
						<xsl:with-param name="ModelName" select="$modelName"/>
						<xsl:with-param name="AllProperties" select="$allProperties"/>
					</xsl:apply-templates>
				</xsl:otherwise>
			</xsl:choose>
		</plx:root>
	</xsl:template>
	<xsl:template name="GenerateCLSCompliantAttributeIfNecessary">
		<xsl:if test="$GenerateCLSCompliantAttribute">
			<xsl:variable name="dataTypeFragment">
				<xsl:choose>
					<xsl:when test="string-length(@dataTypeName)">
						<xsl:copy-of select="."/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:copy-of select="prop:DataType"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			<xsl:variable name="dataType" select="exsl:node-set($dataTypeFragment)/child::*"/>
			<xsl:choose>
				<xsl:when test="starts-with($dataType/@dataTypeName,'.u')">
					<plx:attribute dataTypeName="CLSCompliantAttribute">
						<plx:passParam>
							<plx:falseKeyword/>
						</plx:passParam>
					</plx:attribute>
				</xsl:when>
				<xsl:otherwise>
					<xsl:for-each select="$dataType/child::*">
						<xsl:call-template name="GenerateCLSCompliantAttributeIfNecessary"/>
					</xsl:for-each>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
	</xsl:template>

	<xsl:template match="oial:model" mode="OIALtoPLiX_Abstract">
		<xsl:param name="ModelName"/>
		<xsl:param name="AllProperties"/>
		<xsl:variable name="modelContextName" select="concat($ModelName, 'Context')"/>
		<xsl:variable name="model" select="."/>
		<xsl:variable name="conceptTypes" select="oial:conceptTypes/oial:conceptType"/>
		<xsl:variable name="assimilations" select="$conceptTypes/oial:children/oial:assimilatedConceptType"/>
		<plx:namespace name="{$ModelName}">

			<xsl:for-each select="$conceptTypes">
				<xsl:variable name="currentProperties" select="$AllProperties[@conceptTypeId=current()/@id]"/>
				<xsl:apply-templates select="." mode="GenerateAbstractClass">
					<xsl:with-param name="Model" select="$model"/>
					<xsl:with-param name="ConceptTypes" select="$conceptTypes"/>
					<xsl:with-param name="Assimilations" select="$assimilations"/>
					<xsl:with-param name="Properties" select="$currentProperties/prop:Property"/>
					<xsl:with-param name="AllProperties" select="$AllProperties"/>
					<xsl:with-param name="ClassNameAttribute" select="$currentProperties/@conceptTypeName"/>
					<xsl:with-param name="ModelContextName" select="$modelContextName"/>
				</xsl:apply-templates>
			</xsl:for-each>

			<plx:interface visibility="public" name="IHas{$modelContextName}">
				<plx:leadingInfo>
					<plx:pragma type="region" data="IHas{$modelContextName}"/>
				</plx:leadingInfo>
				<plx:trailingInfo>
					<plx:pragma type="closeRegion" data="IHas{$modelContextName}"/>
				</plx:trailingInfo>
				<xsl:copy-of select="$GeneratedCodeAttribute"/>
				<plx:property visibility="public" modifier="abstract" name="Context">
					<plx:returns dataTypeName="{$modelContextName}"/>
					<plx:get/>
				</plx:property>
			</plx:interface>
			<plx:interface visibility="public" name="I{$modelContextName}">
				<plx:leadingInfo>
					<plx:pragma type="region" data="I{$modelContextName}"/>
				</plx:leadingInfo>
				<plx:trailingInfo>
					<plx:pragma type="closeRegion" data="I{$modelContextName}"/>
				</plx:trailingInfo>
				<xsl:copy-of select="$GeneratedCodeAttribute"/>
				<xsl:call-template name="GenerateModelContextInterfaceLookupAndExternalConstraintEnforcementMembers">
					<xsl:with-param name="ConceptTypes" select="$conceptTypes"/>
					<xsl:with-param name="AllProperties" select="$AllProperties"/>
				</xsl:call-template>
				<xsl:for-each select="$conceptTypes">
					<xsl:variable name="currentProperties" select="$AllProperties[@conceptTypeId=current()/@id]"/>
					<xsl:apply-templates select="." mode="GenerateModelContextInterfaceObjectMethods">
						<xsl:with-param name="Model" select="$model"/>
						<xsl:with-param name="ClassName" select="string($currentProperties/@conceptTypeName)"/>
						<!-- This intentionally ignores identity fields. -->
						<xsl:with-param name="Properties" select="$currentProperties/prop:Property"/>
					</xsl:apply-templates>
				</xsl:for-each>
			</plx:interface>
		</plx:namespace>
	</xsl:template>

	<xsl:template match="oial:conceptType" mode="GenerateAbstractClass">
		<xsl:param name="Model"/>
		<xsl:param name="ConceptTypes"/>
		<xsl:param name="Assimilations"/>
		<xsl:param name="Properties"/>
		<xsl:param name="AllProperties"/>
		<xsl:param name="ClassNameAttribute"/>
		<xsl:param name="ModelContextName"/>
		<xsl:variable name="className" select="string($ClassNameAttribute)"/>
		<xsl:variable name="classParamName" select="string($ClassNameAttribute/../@conceptTypeParamName)"/>
		<xsl:variable name="eventProperties" select="$Properties[not(@isCollection[.='true' or .=1])]"/>
		<plx:class visibility="public" modifier="abstract" partial="true" name="{$className}">
			<plx:leadingInfo>
				<plx:pragma type="region" data="{$className}"/>
			</plx:leadingInfo>
			<plx:trailingInfo>
				<plx:pragma type="closeRegion" data="{$className}"/>
			</plx:trailingInfo>
			<xsl:if test="$GenerateObjectDataSourceSupport">
				<plx:attribute dataTypeName="DataObjectAttribute"/>
			</xsl:if>
			<xsl:copy-of select="$GeneratedCodeAttribute"/>
			<xsl:copy-of select="$StructLayoutAttribute"/>
			<plx:implementsInterface dataTypeName="INotifyPropertyChanged"/>
			<plx:implementsInterface dataTypeName="IHas{$ModelContextName}"/>
			<plx:function visibility="protected" name=".construct"/>

			<plx:pragma type="region" data="{$className} INotifyPropertyChanged Implementation"/>
			<xsl:call-template name="GenerateINotifyPropertyChangedImplementation"/>
			<plx:pragma type="closeRegion" data="{$className} INotifyPropertyChanged Implementation"/>

			<xsl:if test="$eventProperties">
				<plx:pragma type="region" data="{$className} Property Change Events"/>
				<plx:field visibility="private" name="{$PrivateMemberPrefix}events" dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System"/>
				<plx:property visibility="private" name="Events">
					<plx:returns dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System"/>
					<plx:get>
						<xsl:choose>
							<xsl:when test="$SynchronizeEventAddRemove">
								<plx:local name="localEvents" dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System"/>
								<plx:return>
									<plx:inlineStatement dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
										<plx:nullFallbackOperator>
											<plx:left>
												<plx:inlineStatement dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
													<plx:assign>
														<plx:left>
															<plx:nameRef type="local" name="localEvents"/>
														</plx:left>
														<plx:right>
															<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}events"/>
														</plx:right>
													</plx:assign>
												</plx:inlineStatement>
											</plx:left>
											<plx:right>
												<plx:inlineStatement dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
													<plx:nullFallbackOperator>
														<plx:left>
															<plx:callStatic type="methodCall" name="CompareExchange" dataTypeName="Interlocked" dataTypeQualifier="System.Threading">
																<plx:passMemberTypeParam dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System"/>
																<plx:passParam type="inOut">
																	<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}events"/>
																</plx:passParam>
																<plx:passParam>
																	<plx:inlineStatement dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
																		<plx:assign>
																			<plx:left>
																				<plx:nameRef type="local" name="localEvents"/>
																			</plx:left>
																			<plx:right>
																				<plx:callNew dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
																					<plx:passParam>
																						<plx:value type="i4" data="{count($eventProperties) * 2}"/>
																					</plx:passParam>
																				</plx:callNew>
																			</plx:right>
																		</plx:assign>
																	</plx:inlineStatement>
																</plx:passParam>
																<plx:passParam>
																	<plx:nullKeyword/>
																</plx:passParam>
															</plx:callStatic>
														</plx:left>
														<plx:right>
															<plx:nameRef type="local" name="localEvents"/>
														</plx:right>
													</plx:nullFallbackOperator>
												</plx:inlineStatement>
											</plx:right>
										</plx:nullFallbackOperator>
									</plx:inlineStatement>
								</plx:return>
							</xsl:when>
							<xsl:otherwise>
								<plx:return>
									<plx:inlineStatement dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
										<plx:nullFallbackOperator>
											<plx:left>
												<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}events"/>
											</plx:left>
											<plx:right>
												<plx:inlineStatement dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
													<plx:assign>
														<plx:left>
															<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}events"/>
														</plx:left>
														<plx:right>
															<plx:callNew dataTypeIsSimpleArray="true" dataTypeName="Delegate" dataTypeQualifier="System">
																<plx:passParam>
																	<plx:value type="i4" data="{count($eventProperties) * 2}"/>
																</plx:passParam>
															</plx:callNew>
														</plx:right>
													</plx:assign>
												</plx:inlineStatement>
											</plx:right>
										</plx:nullFallbackOperator>
									</plx:inlineStatement>
								</plx:return>
							</xsl:otherwise>
						</xsl:choose>
					</plx:get>
				</plx:property>
				<xsl:if test="$SynchronizeEventAddRemove">
					<xsl:call-template name="GenerateInterlockedDelegateMethod">
						<xsl:with-param name="MethodName" select="'Combine'"/>
					</xsl:call-template>
					<xsl:call-template name="GenerateInterlockedDelegateMethod">
						<xsl:with-param name="MethodName" select="'Remove'"/>
					</xsl:call-template>
				</xsl:if>
				<xsl:apply-templates select="$eventProperties" mode="GeneratePropertyChangeEvents">
					<xsl:with-param name="ClassName" select="$className"/>
				</xsl:apply-templates>
				<plx:pragma type="closeRegion" data="{$className} Property Change Events"/>
			</xsl:if>

			<plx:pragma type="region" data="{$className} Abstract Properties"/>
			<plx:property visibility="public" modifier="abstract" name="Context">
				<plx:interfaceMember memberName="Context" dataTypeName="IHas{$ModelContextName}"/>
				<plx:returns dataTypeName="{$ModelContextName}"/>
				<plx:get/>
			</plx:property>
			<xsl:apply-templates select="$Properties" mode="GenerateAbstractProperty"/>
			<plx:pragma type="closeRegion" data="{$className} Abstract Properties"/>

			<plx:pragma type="region" data="{$className} ToString Methods"/>
			<xsl:call-template name="GenerateToString">
				<xsl:with-param name="ClassName" select="$className"/>
				<xsl:with-param name="Properties" select="$Properties"/>
			</xsl:call-template>
			<plx:pragma type="closeRegion" data="{$className} ToString Methods"/>

			<xsl:variable name="referencedByAssimilations" select="$Assimilations[@ref=current()/@id]"/>
			<xsl:if test="$referencedByAssimilations">
				<xsl:variable name="ancestorAssimilationsFragment">
					<xsl:call-template name="GetAncestorAssimilations">
						<xsl:with-param name="Assimilations" select="$Assimilations"/>
						<xsl:with-param name="AssimilatedConceptTypeId" select="string(@id)"/>
					</xsl:call-template>
				</xsl:variable>
				<xsl:variable name="ancestorAssimilations" select="exsl:node-set($ancestorAssimilationsFragment)/child::*"/>
				<xsl:for-each select="$referencedByAssimilations/../..">
					<xsl:variable name="parentConceptTypeName" select="string($AllProperties[@conceptTypeId=current()/@id]/@conceptTypeName)"/>
					<plx:pragma type="region" data="{$className} Parent Support ({$parentConceptTypeName})"/>
					<xsl:variable name="conversionCallObjectFragment">
						<plx:nameRef type="parameter" name="{$classParamName}"/>
					</xsl:variable>
					<!-- Generate an implicit cast operator for the oial:conceptType elements that assimilate this oial:conceptType -->
					<xsl:call-template name="GenerateImplicitConversionOperators">
						<xsl:with-param name="Assimilations" select="$Assimilations"/>
						<xsl:with-param name="AncestorAssimilations" select="$ancestorAssimilations"/>
						<xsl:with-param name="AllProperties" select="$AllProperties"/>
						<xsl:with-param name="SourceClassName" select="$className"/>
						<xsl:with-param name="SourceClassParamName" select="$classParamName"/>
						<xsl:with-param name="DestinationClassName" select="$parentConceptTypeName"/>
						<xsl:with-param name="ConversionCallObject" select="exsl:node-set($conversionCallObjectFragment)/child::*"/>
					</xsl:call-template>
					<!-- Generate a virtual property for each of the parent's properties (except for the one for this class, because that would be awkward). -->
					<!-- Also exclude any properties that have the same name as a property in this class. -->
					<xsl:variable name="excludeNames" select="$ClassNameAttribute | $Properties/@conceptTypeName"/>
					<xsl:call-template name="GenerateVirtualPropertiesFromParents">
						<xsl:with-param name="ExcludeNames" select="$excludeNames"/>
						<xsl:with-param name="AllProperties" select="$AllProperties"/>
						<xsl:with-param name="Assimilations" select="$Assimilations"/>
						<xsl:with-param name="AncestorAssimilations" select="$ancestorAssimilations"/>
						<xsl:with-param name="ParentProperties" select="$AllProperties[@conceptTypeId=current()/@id]/prop:Property[not(@name=$excludeNames)]"/>
						<xsl:with-param name="PropertyCallObject">
							<plx:callInstance type="property" name="{$parentConceptTypeName}">
								<plx:callObject>
									<plx:thisKeyword/>
								</plx:callObject>
							</plx:callInstance>
						</xsl:with-param>
					</xsl:call-template>
					<plx:pragma type="closeRegion" data="{$className} Parent Support ({$parentConceptTypeName})"/>
				</xsl:for-each>
			</xsl:if>

			<xsl:variable name="childConceptTypes" select="$ConceptTypes[@id=current()/oial:children/oial:assimilatedConceptType/@ref]"/>
			<xsl:if test="$childConceptTypes">
				<xsl:variable name="multipleChildren" select="count($childConceptTypes) > 1"/>
				<xsl:if test="$multipleChildren">
					<plx:pragma type="region" data="{$className} Children Support"/>
				</xsl:if>
				<xsl:for-each select="$childConceptTypes">
					<!-- Generate an explicit cast operator for all oial:conceptType elements assimilated by this oial:conceptType -->
					<xsl:variable name="childConceptTypeNameElement" select="$AllProperties[@conceptTypeId=current()/@id]"/>
					<xsl:variable name="childConceptTypeName" select="string($childConceptTypeNameElement/@conceptTypeName)"/>
					<xsl:variable name="childConceptTypeParamName" select="string($childConceptTypeNameElement/@conceptTypeParamName)"/>
					<plx:pragma type="region" data="{$className} Child Support ({$childConceptTypeName})"/>
					<plx:operatorFunction type="castNarrow">
						<plx:param dataTypeName="{$className}" name="{$classParamName}"/>
						<plx:returns dataTypeName="{$childConceptTypeName}"/>
						<plx:branch>
							<plx:condition>
								<plx:binaryOperator type="identityEquality">
									<plx:left>
										<plx:cast type="exceptionCast" dataTypeName=".object">
											<plx:nameRef type="parameter" name="{$classParamName}"/>
										</plx:cast>
									</plx:left>
									<plx:right>
										<plx:nullKeyword/>
									</plx:right>
								</plx:binaryOperator>
							</plx:condition>
							<plx:return>
								<plx:nullKeyword/>
							</plx:return>
						</plx:branch>
						<plx:local name="{$childConceptTypeParamName}" dataTypeName="{$childConceptTypeName}"/>
						<plx:branch>
							<plx:condition>
								<plx:binaryOperator type="identityEquality">
									<plx:left>
										<plx:cast type="exceptionCast" dataTypeName=".object">
											<plx:inlineStatement dataTypeName="{$childConceptTypeName}">
												<plx:assign>
													<plx:left>
														<plx:nameRef type="local" name="{$childConceptTypeParamName}"/>
													</plx:left>
													<plx:right>
														<plx:callInstance type="property" name="{$childConceptTypeName}">
															<plx:callObject>
																<plx:nameRef type="parameter" name="{$classParamName}"/>
															</plx:callObject>
														</plx:callInstance>
													</plx:right>
												</plx:assign>
											</plx:inlineStatement>
										</plx:cast>
									</plx:left>
									<plx:right>
										<plx:nullKeyword/>
									</plx:right>
								</plx:binaryOperator>
							</plx:condition>
							<plx:throw>
								<plx:callNew dataTypeName="InvalidCastException"/>
							</plx:throw>
						</plx:branch>
						<plx:return>
							<plx:nameRef type="local" name="{$childConceptTypeParamName}"/>
						</plx:return>
					</plx:operatorFunction>
					<!-- Generate an explicit cast operator for for each of the child's children -->
					<xsl:variable name="conversionCallObjectFragment">
						<plx:cast type="exceptionCast" dataTypeName="{$childConceptTypeName}">
							<plx:nameRef type="parameter" name="{$classParamName}"/>
						</plx:cast>
					</xsl:variable>
					<xsl:for-each select="$ConceptTypes[@id=oial:children/oial:assimiliatedConceptType/@ref]">
						<xsl:call-template name="GenerateExplicitConversionOperators">
							<xsl:with-param name="ConceptTypes" select="$ConceptTypes"/>
							<xsl:with-param name="AllProperties" select="$AllProperties"/>
							<xsl:with-param name="SourceClassName" select="$className"/>
							<xsl:with-param name="SourceClassParamName" select="$classParamName"/>
							<xsl:with-param name="ConversionCallObject" select="exsl:node-set($conversionCallObjectFragment)/child::*"/>
						</xsl:call-template>
					</xsl:for-each>
					<plx:pragma type="closeRegion" data="{$className} Child Support ({$childConceptTypeName})"/>
				</xsl:for-each>
				<xsl:if test="$multipleChildren">
					<plx:pragma type="closeRegion" data="{$className} Children Support"/>
				</xsl:if>
			</xsl:if>
		</plx:class>
	</xsl:template>

	<xsl:template name="GenerateImplicitConversionOperators">
		<xsl:param name="Assimilations"/>
		<xsl:param name="AncestorAssimilations"/>
		<xsl:param name="AllProperties"/>
		<xsl:param name="SourceClassName"/>
		<xsl:param name="SourceClassParamName"/>
		<xsl:param name="DestinationClassName"/>
		<xsl:param name="ConversionCallObject"/>
		<xsl:variable name="conversionCodeFragment">
			<plx:callInstance type="property" name="{$DestinationClassName}">
				<plx:callObject>
					<xsl:copy-of select="$ConversionCallObject"/>
				</plx:callObject>
			</plx:callInstance>
		</xsl:variable>
		<xsl:variable name="conversionCode" select="exsl:node-set($conversionCodeFragment)/child::*"/>
		<plx:operatorFunction type="castWiden">
			<plx:param dataTypeName="{$SourceClassName}" name="{$SourceClassParamName}"/>
			<plx:returns dataTypeName="{$DestinationClassName}"/>
			<plx:branch>
				<plx:condition>
					<plx:binaryOperator type="identityEquality">
						<plx:left>
							<plx:cast type="exceptionCast" dataTypeName=".object">
								<plx:nameRef type="parameter" name="{$SourceClassParamName}"/>
							</plx:cast>
						</plx:left>
						<plx:right>
							<plx:nullKeyword/>
						</plx:right>
					</plx:binaryOperator>
				</plx:condition>
				<plx:return>
					<plx:nullKeyword/>
				</plx:return>
			</plx:branch>
			<plx:return>
				<xsl:copy-of select="$conversionCode"/>
			</plx:return>
		</plx:operatorFunction>
		<xsl:for-each select="$Assimilations[@ref=current()/@id][not(@id=$AncestorAssimilations[@alternateParentPath]/@id)]/../..">
			<xsl:call-template name="GenerateImplicitConversionOperators">
				<xsl:with-param name="Assimilations" select="$Assimilations"/>
				<xsl:with-param name="AncestorAssimilations" select="$AncestorAssimilations"/>
				<xsl:with-param name="AllProperties" select="$AllProperties"/>
				<xsl:with-param name="SourceClassName" select="$SourceClassName"/>
				<xsl:with-param name="SourceClassParamName" select="$SourceClassParamName"/>
				<xsl:with-param name="DestinationClassName" select="string($AllProperties[@conceptTypeId=current()/@id]/@conceptTypeName)"/>
				<xsl:with-param name="ConversionCallObject" select="$conversionCode"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>

	<xsl:template name="GenerateExplicitConversionOperators">
		<xsl:param name="ConceptTypes"/>
		<xsl:param name="AllProperties"/>
		<xsl:param name="SourceClassName"/>
		<xsl:param name="SourceClassParamName"/>
		<xsl:param name="ConversionCallObject"/>
		<xsl:variable name="destinationClassName" select="$AllProperties[@conceptTypeId=current()/@id]/@conceptTypeName"/>
		<xsl:variable name="conversionCodeFragment">
			<plx:cast type="exceptionCast" dataTypeName="{$destinationClassName}">
				<xsl:copy-of select="$ConversionCallObject"/>
			</plx:cast>
		</xsl:variable>
		<xsl:variable name="conversionCode" select="exsl:node-set($conversionCodeFragment)/child::*"/>
		<plx:operatorFunction type="castNarrow">
			<plx:param dataTypeName="{$SourceClassName}" name="{$SourceClassParamName}"/>
			<plx:returns dataTypeName="{$destinationClassName}"/>
			<plx:branch>
				<plx:condition>
					<plx:binaryOperator type="identityEquality">
						<plx:left>
							<plx:cast type="exceptionCast" dataTypeName=".object">
								<plx:nameRef type="parameter" name="{$SourceClassParamName}"/>
							</plx:cast>
						</plx:left>
						<plx:right>
							<plx:nullKeyword/>
						</plx:right>
					</plx:binaryOperator>
				</plx:condition>
				<plx:return>
					<plx:nullKeyword/>
				</plx:return>
			</plx:branch>
			<plx:return>
				<xsl:copy-of select="$conversionCode"/>
			</plx:return>
		</plx:operatorFunction>
		<xsl:for-each select="$ConceptTypes[@id=current()/oial:children/oial:assimilatedConceptType/@ref]">
			<xsl:call-template name="GenerateExplicitConversionOperators">
				<xsl:with-param name="ConceptTypes" select="$ConceptTypes"/>
				<xsl:with-param name="AllProperties" select="$AllProperties"/>
				<xsl:with-param name="SourceClassName" select="$SourceClassName"/>
				<xsl:with-param name="SourceClassParamName" select="$SourceClassParamName"/>
				<xsl:with-param name="ConversionCallObject" select="$conversionCode"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>

	<xsl:template name="GenerateVirtualPropertiesFromParents">
		<xsl:param name="ExcludeNames"/>
		<xsl:param name="AllProperties"/>
		<xsl:param name="Assimilations"/>
		<xsl:param name="AncestorAssimilations"/>
		<xsl:param name="ParentProperties"/>
		<xsl:param name="PropertyCallObject"/>
		<xsl:variable name="thisParentConceptTypeName" select="$AllProperties[@conceptTypeId=current()/@id]/@conceptTypeName"/>
		<xsl:for-each select="$ParentProperties[not((@childId | @reverseChildId)=$AncestorAssimilations/@id)]">
			<xsl:variable name="propertyName" select="@name"/>
			<plx:property visibility="public" modifier="virtual" name="{$propertyName}">
				<xsl:call-template name="GenerateCLSCompliantAttributeIfNecessary"/>
				<plx:returns>
					<xsl:copy-of select="prop:DataType/@*"/>
					<xsl:copy-of select="prop:DataType/child::*"/>
				</plx:returns>
				<plx:get>
					<plx:return>
						<plx:callInstance type="property" name="{$propertyName}">
							<plx:callObject>
								<xsl:copy-of select="$PropertyCallObject"/>
							</plx:callObject>
						</plx:callInstance>
					</plx:return>
				</plx:get>
				<xsl:if test="not(@isCollection[.='true' or .=1])">
					<plx:set>
						<plx:assign>
							<plx:left>
								<plx:callInstance type="property" name="{$propertyName}">
									<plx:callObject>
										<xsl:copy-of select="$PropertyCallObject"/>
									</plx:callObject>
								</plx:callInstance>
							</plx:left>
							<plx:right>
								<plx:valueKeyword/>
							</plx:right>
						</plx:assign>
					</plx:set>
				</xsl:if>
			</plx:property>
			<xsl:if test="not(@isCollection[.='true' or .=1])">
				<plx:event visibility="public" name="{$propertyName}Changing">
					<xsl:call-template name="GenerateCLSCompliantAttributeIfNecessary"/>
					<plx:param name="sender" dataTypeName=".object"/>
					<plx:param name="e" dataTypeName="PropertyChangingEventArgs">
						<plx:passTypeParam dataTypeName="{$thisParentConceptTypeName}"/>
						<plx:passTypeParam>
							<xsl:copy-of select="prop:DataType/@*"/>
							<xsl:copy-of select="prop:DataType/child::*"/>
						</plx:passTypeParam>
					</plx:param>
					<plx:explicitDelegateType dataTypeName="EventHandler"/>
					<plx:passTypeParam  dataTypeName="PropertyChangingEventArgs">
						<plx:passTypeParam dataTypeName="{$thisParentConceptTypeName}"/>
						<plx:passTypeParam>
							<xsl:copy-of select="prop:DataType/@*"/>
							<xsl:copy-of select="prop:DataType/child::*"/>
						</plx:passTypeParam>
					</plx:passTypeParam>
					<plx:onAdd>
						<plx:attachEvent>
							<plx:left>
								<plx:callInstance type="event" name="{$propertyName}Changing">
									<plx:callObject>
										<xsl:copy-of select="$PropertyCallObject"/>
									</plx:callObject>
								</plx:callInstance>
							</plx:left>
							<plx:right>
								<plx:valueKeyword/>
							</plx:right>
						</plx:attachEvent>
					</plx:onAdd>
					<plx:onRemove>
						<plx:detachEvent>
							<plx:left>
								<plx:callInstance type="event" name="{$propertyName}Changing">
									<plx:callObject>
										<xsl:copy-of select="$PropertyCallObject"/>
									</plx:callObject>
								</plx:callInstance>
							</plx:left>
							<plx:right>
								<plx:valueKeyword/>
							</plx:right>
						</plx:detachEvent>
					</plx:onRemove>
				</plx:event>
				<plx:event visibility="public" name="{$propertyName}Changed">
					<xsl:call-template name="GenerateCLSCompliantAttributeIfNecessary"/>
					<plx:param name="sender" dataTypeName=".object"/>
					<plx:param name="e" dataTypeName="PropertyChangedEventArgs">
						<plx:passTypeParam dataTypeName="{$thisParentConceptTypeName}"/>
						<plx:passTypeParam>
							<xsl:copy-of select="prop:DataType/@*"/>
							<xsl:copy-of select="prop:DataType/child::*"/>
						</plx:passTypeParam>
					</plx:param>
					<plx:explicitDelegateType dataTypeName="EventHandler"/>
					<plx:passTypeParam  dataTypeName="PropertyChangedEventArgs">
						<plx:passTypeParam dataTypeName="{$thisParentConceptTypeName}"/>
						<plx:passTypeParam>
							<xsl:copy-of select="prop:DataType/@*"/>
							<xsl:copy-of select="prop:DataType/child::*"/>
						</plx:passTypeParam>
					</plx:passTypeParam>
					<plx:onAdd>
						<plx:attachEvent>
							<plx:left>
								<plx:callInstance type="event" name="{$propertyName}Changed">
									<plx:callObject>
										<xsl:copy-of select="$PropertyCallObject"/>
									</plx:callObject>
								</plx:callInstance>
							</plx:left>
							<plx:right>
								<plx:valueKeyword/>
							</plx:right>
						</plx:attachEvent>
					</plx:onAdd>
					<plx:onRemove>
						<plx:detachEvent>
							<plx:left>
								<plx:callInstance type="event" name="{$propertyName}Changed">
									<plx:callObject>
										<xsl:copy-of select="$PropertyCallObject"/>
									</plx:callObject>
								</plx:callInstance>
							</plx:left>
							<plx:right>
								<plx:valueKeyword/>
							</plx:right>
						</plx:detachEvent>
					</plx:onRemove>
				</plx:event>
			</xsl:if>
		</xsl:for-each>
		<xsl:for-each select="$Assimilations[@ref=current()/@id][not(@id=$AncestorAssimilations[@alternateParentPath]/@id)]/../..">
			<xsl:variable name="parentParentConceptTypeNameAttribute" select="$AllProperties[@conceptTypeId=current()/@id]/@conceptTypeName"/>
			<xsl:variable name="excludeNames" select="$ExcludeNames | $parentParentConceptTypeNameAttribute | $ParentProperties/@conceptTypeName"/>
			<xsl:call-template name="GenerateVirtualPropertiesFromParents">
				<xsl:with-param name="ExcludeNames" select="$excludeNames"/>
				<xsl:with-param name="AllProperties" select="$AllProperties"/>
				<xsl:with-param name="Assimilations" select="$Assimilations"/>
				<xsl:with-param name="AncestorAssimilations" select="$AncestorAssimilations"/>
				<xsl:with-param name="ParentProperties" select="$AllProperties[@conceptTypeId=current()/@id]/prop:Property[not(@name=$excludeNames)]"/>
				<xsl:with-param name="PropertyCallObject">
					<plx:callInstance type="property" name="{$parentParentConceptTypeNameAttribute}">
						<plx:callObject>
							<xsl:copy-of select="$PropertyCallObject"/>
						</plx:callObject>
					</plx:callInstance>
				</xsl:with-param>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="GetAncestorAssimilations">
		<xsl:param name="Assimilations"/>
		<xsl:param name="AssimilatedConceptTypeId"/>
		<xsl:variable name="ancestorAssimilationsFragment">
			<xsl:call-template name="GetAncestorAssimilationsWorker">
				<xsl:with-param name="Assimilations" select="$Assimilations"/>
				<xsl:with-param name="AssimilatedConceptTypeId" select="$AssimilatedConceptTypeId"/>
			</xsl:call-template>
		</xsl:variable>
		<xsl:for-each select="exsl:node-set($ancestorAssimilationsFragment)/child::*">
			<xsl:copy>
				<xsl:copy-of select="@*"/>
				<xsl:if test="preceding-sibling::*/@parentConceptTypeId=@parentConceptTypeId">
					<xsl:attribute name="alternateParentPath">
						<xsl:text>true</xsl:text>
					</xsl:attribute>
				</xsl:if>
			</xsl:copy>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="GetAncestorAssimilationsWorker">
		<xsl:param name="Assimilations"/>
		<xsl:param name="AssimilatedConceptTypeId"/>
		<xsl:for-each select="$Assimilations[@ref=$AssimilatedConceptTypeId]">
			<oial:parentAssimilation id="{@id}" childConceptTypeId="{@ref}" parentConceptTypeId="{../../@id}"/>
			<xsl:call-template name="GetAncestorAssimilationsWorker">
				<xsl:with-param name="Assimilations" select="$Assimilations"/>
				<xsl:with-param name="AssimilatedConceptTypeId" select="string(../../@id)"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="prop:Property" mode="GenerateAbstractProperty">
		<plx:property visibility="public" modifier="abstract" name="{@name}" >
			<xsl:call-template name="GenerateCLSCompliantAttributeIfNecessary"/>
			<xsl:if test="$GenerateObjectDataSourceSupport">
				<!--
					TODO: Should we even be generating this for properties where @isCollection='true'?
					If so, do we still handle the 'isNullable' parameter the same way?
				-->
				<plx:attribute dataTypeName="DataObjectFieldAttribute">
					<plx:passParam>
						<plx:falseKeyword/>
					</plx:passParam>
					<plx:passParam>
						<plx:falseKeyword/>
					</plx:passParam>
					<plx:passParam>
						<xsl:choose>
							<xsl:when test="@mandatory='alethic'">
								<plx:falseKeyword/>
							</xsl:when>
							<xsl:otherwise>
								<plx:trueKeyword/>
							</xsl:otherwise>
						</xsl:choose>
					</plx:passParam>
				</plx:attribute>
			</xsl:if>
			<plx:returns>
				<xsl:copy-of select="prop:DataType/@*"/>
				<xsl:copy-of select="prop:DataType/child::*"/>
			</plx:returns>
			<plx:get/>
			<xsl:if test="not(@isCollection[.='true' or .=1])">
				<plx:set/>
			</xsl:if>
		</plx:property>
	</xsl:template>

	<xsl:template name="GenerateInterlockedDelegateMethod">
		<xsl:param name="MethodName"/>
		<plx:function visibility="private" modifier="static" name="InterlockedDelegate{$MethodName}">
			<plx:param type="inOut" name="location" dataTypeName="Delegate" dataTypeQualifier="System"/>
			<plx:param name="value" dataTypeName="Delegate" dataTypeQualifier="System"/>
			<plx:local name="currentHandler" dataTypeName="Delegate" dataTypeQualifier="System"/>
			<plx:loop checkCondition="before">
				<plx:condition>
					<plx:binaryOperator type="identityInequality">
						<plx:left>
							<plx:cast type="exceptionCast" dataTypeName=".object">
								<plx:callStatic type="methodCall" name="CompareExchange" dataTypeName="Interlocked" dataTypeQualifier="System.Threading">
									<plx:passMemberTypeParam dataTypeName="Delegate" dataTypeQualifier="System"/>
									<plx:passParam type="inOut">
										<plx:nameRef type="parameter" name="location"/>
									</plx:passParam>
									<plx:passParam>
										<plx:callStatic type="methodCall" name="{$MethodName}" dataTypeName="Delegate" dataTypeQualifier="System">
											<plx:passParam>
												<plx:inlineStatement dataTypeName="Delegate" dataTypeQualifier="System">
													<plx:assign>
														<plx:left>
															<plx:nameRef type="local" name="currentHandler"/>
														</plx:left>
														<plx:right>
															<plx:nameRef type="parameter" name="location"/>
														</plx:right>
													</plx:assign>
												</plx:inlineStatement>
											</plx:passParam>
											<plx:passParam>
												<plx:nameRef type="parameter" name="value"/>
											</plx:passParam>
										</plx:callStatic>
									</plx:passParam>
									<plx:passParam>
										<plx:nameRef type="local" name="currentHandler"/>
									</plx:passParam>
								</plx:callStatic>
							</plx:cast>
						</plx:left>
						<plx:right>
							<plx:cast type="exceptionCast" dataTypeName=".object">
								<plx:nameRef type="local" name="currentHandler"/>
							</plx:cast>
						</plx:right>
					</plx:binaryOperator>
				</plx:condition>
			</plx:loop>
		</plx:function>
	</xsl:template>
	
	<xsl:template match="prop:Property" mode="GeneratePropertyChangeEvents">
		<xsl:param name="ClassName"/>
		<xsl:variable name="EventIndex" select="(position()*2)-2"/>
		<xsl:call-template name="GeneratePropertyChangeEvent">
			<xsl:with-param name="ClassName" select="$ClassName"/>
			<xsl:with-param name="ChangeType" select="'Changing'"/>
			<xsl:with-param name="EventIndex" select="$EventIndex"/>
		</xsl:call-template>
		<xsl:call-template name="GeneratePropertyChangeEventRaiseMethod">
			<xsl:with-param name="ClassName" select="$ClassName"/>
			<xsl:with-param name="ChangeType" select="'Changing'"/>
			<xsl:with-param name="EventIndex" select="$EventIndex"/>
		</xsl:call-template>
		<xsl:call-template name="GeneratePropertyChangeEvent">
			<xsl:with-param name="ClassName" select="$ClassName"/>
			<xsl:with-param name="ChangeType" select="'Changed'"/>
			<xsl:with-param name="EventIndex" select="$EventIndex + 1"/>
		</xsl:call-template>
		<xsl:call-template name="GeneratePropertyChangeEventRaiseMethod">
			<xsl:with-param name="ClassName" select="$ClassName"/>
			<xsl:with-param name="ChangeType" select="'Changed'"/>
			<xsl:with-param name="EventIndex" select="$EventIndex + 1"/>
		</xsl:call-template>
	</xsl:template>
	
	<xsl:template name="GeneratePropertyChangeEvent">
		<xsl:param name="ClassName"/>
		<xsl:param name="ChangeType"/>
		<xsl:param name="EventIndex"/>
		<plx:event visibility="public" name="{@name}{$ChangeType}">
			<xsl:call-template name="GenerateCLSCompliantAttributeIfNecessary"/>
			<plx:param name="sender" dataTypeName=".object"/>
			<plx:param name="e" dataTypeName="Property{$ChangeType}EventArgs">
				<plx:passTypeParam dataTypeName="{$ClassName}"/>
				<plx:passTypeParam>
					<xsl:copy-of select="prop:DataType/@*"/>
					<xsl:copy-of select="prop:DataType/child::*"/>
				</plx:passTypeParam>
			</plx:param>
			<plx:explicitDelegateType dataTypeName="EventHandler"/>
			<plx:passTypeParam  dataTypeName="Property{$ChangeType}EventArgs">
				<plx:passTypeParam dataTypeName="{$ClassName}"/>
				<plx:passTypeParam>
					<xsl:copy-of select="prop:DataType/@*"/>
					<xsl:copy-of select="prop:DataType/child::*"/>
				</plx:passTypeParam>
			</plx:passTypeParam>
			<plx:onAdd>
				<xsl:call-template name="GetPropertyChangeEventOnAddCode">
					<xsl:with-param name="ClassName" select="$ClassName"/>
					<xsl:with-param name="EventIndex" select="$EventIndex"/>
				</xsl:call-template>
			</plx:onAdd>
			<plx:onRemove>				
				<xsl:call-template name="GetPropertyChangeEventOnRemoveCode">
					<xsl:with-param name="ClassName" select="$ClassName"/>
					<xsl:with-param name="EventIndex" select="$EventIndex"/>
				</xsl:call-template>
			</plx:onRemove>
		</plx:event>
	</xsl:template>

	<xsl:template name="GetPropertyChangeEventOnAddCode">
		<xsl:param name="ClassName"/>
		<xsl:param name="EventIndex"/>
		<plx:branch>
			<plx:condition>
				<plx:binaryOperator type="identityInequality">
					<plx:left>
						<plx:cast type="exceptionCast" dataTypeName=".object">
							<plx:valueKeyword/>
						</plx:cast>
					</plx:left>
					<plx:right>
						<plx:nullKeyword/>
					</plx:right>
				</plx:binaryOperator>
			</plx:condition>
			<xsl:choose>
				<xsl:when test="$SynchronizeEventAddRemove">
					<plx:callStatic type="methodCall" name="InterlockedDelegateCombine" dataTypeName="{$ClassName}">
						<plx:passParam type="inOut">
							<plx:callInstance type="arrayIndexer" name=".implied">
								<plx:callObject>
									<plx:callThis accessor="this" type="property" name="Events"/>
								</plx:callObject>
								<plx:passParam>
									<plx:value type="i4" data="{$EventIndex}"/>
								</plx:passParam>
							</plx:callInstance>
						</plx:passParam>
						<plx:passParam>
							<plx:valueKeyword/>
						</plx:passParam>
					</plx:callStatic>
				</xsl:when>
				<xsl:otherwise>
					<plx:local name="events" dataTypeName="Delegate" dataTypeQualifier="System" dataTypeIsSimpleArray="true"/>
					<plx:assign>
						<plx:left>
							<plx:callInstance type="arrayIndexer" name=".implied">
								<plx:callObject>
									<plx:inlineStatement dataTypeName="Delegate" dataTypeQualifier="System" dataTypeIsSimpleArray="true">
										<plx:assign>
											<plx:left>
												<plx:nameRef type="local" name="events"/>
											</plx:left>
											<plx:right>
												<plx:callThis accessor="this" type="property" name="Events"/>
											</plx:right>
										</plx:assign>
									</plx:inlineStatement>
								</plx:callObject>
								<plx:passParam>
									<plx:value type="i4" data="{$EventIndex}"/>
								</plx:passParam>
							</plx:callInstance>
						</plx:left>
						<plx:right>
							<plx:callStatic type="methodCall" name="Combine" dataTypeName="Delegate" dataTypeQualifier="System">
								<plx:passParam>
									<plx:callInstance type="arrayIndexer" name=".implied">
										<plx:callObject>
											<plx:nameRef type="local" name="events"/>
										</plx:callObject>
										<plx:passParam>
											<plx:value type="i4" data="{$EventIndex}"/>
										</plx:passParam>
									</plx:callInstance>
								</plx:passParam>
								<plx:passParam>
									<plx:valueKeyword/>
								</plx:passParam>
							</plx:callStatic>
						</plx:right>
					</plx:assign>
				</xsl:otherwise>
			</xsl:choose>
		</plx:branch>
	</xsl:template>

	<xsl:template name="GetPropertyChangeEventOnRemoveCode">
		<xsl:param name="ClassName"/>
		<xsl:param name="EventIndex"/>
		<plx:local name="events" dataTypeName="Delegate" dataTypeQualifier="System" dataTypeIsSimpleArray="true"/>
		<plx:branch>
			<plx:condition>
				<plx:binaryOperator type="booleanAnd">
					<plx:left>
						<plx:binaryOperator type="identityInequality">
							<plx:left>
								<plx:cast type="exceptionCast" dataTypeName=".object">
									<plx:valueKeyword/>
								</plx:cast>
							</plx:left>
							<plx:right>
								<plx:nullKeyword/>
							</plx:right>
						</plx:binaryOperator>
					</plx:left>
					<plx:right>
						<plx:binaryOperator type="identityInequality">
							<plx:left>
								<plx:cast type="exceptionCast" dataTypeName=".object">
									<plx:inlineStatement dataTypeName="Delegate" dataTypeQualifier="System" dataTypeIsSimpleArray="true">
										<plx:assign>
											<plx:left>
												<plx:nameRef type="local" name="events"/>
											</plx:left>
											<plx:right>
												<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}events"/>
											</plx:right>
										</plx:assign>
									</plx:inlineStatement>
								</plx:cast>
							</plx:left>
							<plx:right>
								<plx:nullKeyword/>
							</plx:right>
						</plx:binaryOperator>
					</plx:right>
				</plx:binaryOperator>
			</plx:condition>
			<xsl:choose>
				<xsl:when test="$SynchronizeEventAddRemove">
					<plx:callStatic type="methodCall" name="InterlockedDelegateRemove" dataTypeName="{$ClassName}">
						<plx:passParam type="inOut">
							<plx:callInstance type="arrayIndexer" name=".implied">
								<plx:callObject>
									<plx:nameRef type="local" name="events"/>
								</plx:callObject>
								<plx:passParam>
									<plx:value type="i4" data="{$EventIndex}"/>
								</plx:passParam>
							</plx:callInstance>
						</plx:passParam>
						<plx:passParam>
							<plx:valueKeyword/>
						</plx:passParam>
					</plx:callStatic>
				</xsl:when>
				<xsl:otherwise>
					<plx:assign>
						<plx:left>
							<plx:callInstance type="arrayIndexer" name=".implied">
								<plx:callObject>
									<plx:nameRef type="local" name="events"/>
								</plx:callObject>
								<plx:passParam>
									<plx:value type="i4" data="{$EventIndex}"/>
								</plx:passParam>
							</plx:callInstance>
						</plx:left>
						<plx:right>
							<plx:callStatic type="methodCall" name="Remove" dataTypeName="Delegate" dataTypeQualifier="System">
								<plx:passParam>
									<plx:callInstance type="arrayIndexer" name=".implied">
										<plx:callObject>
											<plx:nameRef type="local" name="events"/>
										</plx:callObject>
										<plx:passParam>
											<plx:value type="i4" data="{$EventIndex}"/>
										</plx:passParam>
									</plx:callInstance>
								</plx:passParam>
								<plx:passParam>
									<plx:valueKeyword/>
								</plx:passParam>
							</plx:callStatic>
						</plx:right>
					</plx:assign>
				</xsl:otherwise>
			</xsl:choose>
		</plx:branch>
	</xsl:template>

	<xsl:template name="GeneratePropertyChangeEventRaiseMethod">
		<xsl:param name="ClassName"/>
		<xsl:param name="ChangeType"/>
		<xsl:param name="EventIndex"/>
		<xsl:variable name="isChanging" select="$ChangeType='Changing'"/>
		<xsl:variable name="isChanged" select="$ChangeType='Changed'"/>
		<plx:function visibility="protected" name="On{@name}{$ChangeType}">
			<xsl:call-template name="GenerateCLSCompliantAttributeIfNecessary"/>
			<xsl:choose>
				<xsl:when test="$isChanging">
					<plx:param name="newValue">
						<xsl:copy-of select="prop:DataType/@*"/>
						<xsl:copy-of select="prop:DataType/child::*"/>
					</plx:param>
					<plx:returns dataTypeName=".boolean"/>
				</xsl:when>
				<xsl:when test="$isChanged">
					<plx:param name="oldValue">
						<xsl:copy-of select="prop:DataType/@*"/>
						<xsl:copy-of select="prop:DataType/child::*"/>
					</plx:param>
				</xsl:when>
			</xsl:choose>
			<plx:local name="events" dataTypeName="Delegate" dataTypeQualifier="System" dataTypeIsSimpleArray="true"/>
			<plx:local name="eventHandler" dataTypeName="EventHandler">
				<plx:passTypeParam dataTypeName="Property{$ChangeType}EventArgs">
					<plx:passTypeParam dataTypeName="{$ClassName}"/>
					<plx:passTypeParam>
						<xsl:copy-of select="prop:DataType/@*"/>
						<xsl:copy-of select="prop:DataType/child::*"/>
					</plx:passTypeParam>
				</plx:passTypeParam>
			</plx:local>
			<plx:branch>
				<plx:condition>
					<plx:binaryOperator type="booleanAnd">
						<plx:left>
							<plx:binaryOperator type="identityInequality">
								<plx:left>
									<plx:cast type="exceptionCast" dataTypeName=".object">
										<plx:inlineStatement dataTypeName="Delegate" dataTypeQualifier="System" dataTypeIsSimpleArray="true">
											<plx:assign>
												<plx:left>
													<plx:nameRef type="local" name="events"/>
												</plx:left>
												<plx:right>
													<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}events"/>
												</plx:right>
											</plx:assign>
										</plx:inlineStatement>
									</plx:cast>
								</plx:left>
								<plx:right>
									<plx:nullKeyword/>
								</plx:right>
							</plx:binaryOperator>
						</plx:left>
						<plx:right>
							<plx:binaryOperator type="identityInequality">
								<plx:left>
									<plx:cast type="exceptionCast" dataTypeName=".object">
										<plx:inlineStatement dataTypeName="EventHandler">
											<plx:passTypeParam dataTypeName="Property{$ChangeType}EventArgs">
												<plx:passTypeParam dataTypeName="{$ClassName}"/>
												<plx:passTypeParam>
													<xsl:copy-of select="prop:DataType/@*"/>
													<xsl:copy-of select="prop:DataType/child::*"/>
												</plx:passTypeParam>
											</plx:passTypeParam>
											<plx:assign>
												<plx:left>
													<plx:nameRef type="local" name="eventHandler"/>
												</plx:left>
												<plx:right>
													<plx:cast type="exceptionCast" dataTypeName="EventHandler">
														<plx:passTypeParam dataTypeName="Property{$ChangeType}EventArgs">
															<plx:passTypeParam dataTypeName="{$ClassName}"/>
															<plx:passTypeParam>
																<xsl:copy-of select="prop:DataType/@*"/>
																<xsl:copy-of select="prop:DataType/child::*"/>
															</plx:passTypeParam>
														</plx:passTypeParam>
														<plx:callInstance type="arrayIndexer" name=".implied">
															<plx:callObject>
																<plx:nameRef type="local" name="events"/>
															</plx:callObject>
															<plx:passParam>
																<plx:value type="i4" data="{$EventIndex}"/>
															</plx:passParam>
														</plx:callInstance>
													</plx:cast>
												</plx:right>
											</plx:assign>
										</plx:inlineStatement>
									</plx:cast>
								</plx:left>
								<plx:right>
									<plx:nullKeyword/>
								</plx:right>
							</plx:binaryOperator>
						</plx:right>
					</plx:binaryOperator>
					
				</plx:condition>
				<xsl:variable name="eventArgsCreationCodeFragment">
					<plx:callNew dataTypeName="Property{$ChangeType}EventArgs">
						<plx:passTypeParam dataTypeName="{$ClassName}"/>
						<plx:passTypeParam>
							<xsl:copy-of select="prop:DataType/@*"/>
							<xsl:copy-of select="prop:DataType/child::*"/>
						</plx:passTypeParam>
						<plx:passParam>
							<plx:thisKeyword/>
						</plx:passParam>
						<plx:passParam>
							<plx:string data="{@name}"/>
						</plx:passParam>
						<plx:passParam>
							<xsl:choose>
								<xsl:when test="$isChanging">
									<plx:callThis accessor="this" type="property" name="{@name}"/>
								</xsl:when>
								<xsl:when test="$isChanged">
									<plx:nameRef type="parameter" name="oldValue"/>
								</xsl:when>
							</xsl:choose>
						</plx:passParam>
						<plx:passParam>
							<xsl:choose>
								<xsl:when test="$isChanging">
									<plx:nameRef type="parameter" name="newValue"/>
								</xsl:when>
								<xsl:when test="$isChanged">
									<plx:callThis accessor="this" type="property" name="{@name}"/>
								</xsl:when>
							</xsl:choose>
						</plx:passParam>
					</plx:callNew>
				</xsl:variable>
				<xsl:variable name="eventArgsCreationCode" select="exsl:node-set($eventArgsCreationCodeFragment)/child::*"/>
				<xsl:choose>
					<xsl:when test="$isChanging">
						<plx:return>
							<plx:callStatic name="InvokeCancelableEventHandler" dataTypeName="EventHandlerUtility" type="methodCall">
								<plx:passMemberTypeParam dataTypeName="PropertyChangingEventArgs">
									<plx:passTypeParam dataTypeName="{$ClassName}"/>
									<plx:passTypeParam>
										<xsl:copy-of select="prop:DataType/@*"/>
										<xsl:copy-of select="prop:DataType/child::*"/>
									</plx:passTypeParam>
								</plx:passMemberTypeParam>
								<plx:passParam>
									<plx:nameRef type="local" name="eventHandler"/>
								</plx:passParam>
								<plx:passParam>
									<plx:thisKeyword/>
								</plx:passParam>
								<plx:passParam>
									<xsl:copy-of select="$eventArgsCreationCode"/>
								</plx:passParam>
							</plx:callStatic>
						</plx:return>
					</xsl:when>
					<xsl:when test="$isChanged">
						<xsl:choose>
							<xsl:when test="$RaiseEventsAsynchronously">
								<plx:callStatic type="methodCall" name="InvokeEventHandlerAsync" dataTypeName="EventHandlerUtility">
									<plx:passMemberTypeParam dataTypeName="PropertyChangedEventArgs">
										<plx:passTypeParam dataTypeName="{$ClassName}"/>
										<plx:passTypeParam>
											<xsl:copy-of select="prop:DataType/@*"/>
											<xsl:copy-of select="prop:DataType/child::*"/>
										</plx:passTypeParam>
									</plx:passMemberTypeParam>
									<plx:passParam>
										<plx:nameRef type="local" name="eventHandler"/>
									</plx:passParam>
									<plx:passParam>
										<plx:thisKeyword/>
									</plx:passParam>
									<plx:passParam>
										<xsl:copy-of select="$eventArgsCreationCode"/>
									</plx:passParam>
									<plx:passParam>
										<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}propertyChangedEventHandler"/>
									</plx:passParam>
								</plx:callStatic>
							</xsl:when>
							<xsl:otherwise>
								<plx:local name="e" dataTypeName="PropertyChangedEventArgs">
									<plx:passTypeParam dataTypeName="{$ClassName}"/>
									<plx:passTypeParam>
										<xsl:copy-of select="prop:DataType/@*"/>
										<xsl:copy-of select="prop:DataType/child::*"/>
									</plx:passTypeParam>
									<plx:initialize>
										<xsl:copy-of select="$eventArgsCreationCode"/>
									</plx:initialize>
								</plx:local>
								<plx:callInstance type="delegateCall" name="implied">
									<plx:callObject>
										<plx:nameRef type="local" name="eventHandler"/>
									</plx:callObject>
									<plx:passParam>
										<plx:thisKeyword/>
									</plx:passParam>
									<plx:passParam>
										<plx:nameRef type="local" name="e"/>
									</plx:passParam>
								</plx:callInstance>
								<plx:callThis accessor="this" type="methodCall" name="OnPropertyChanged">
									<plx:passParam>
										<plx:nameRef type="local" name="e"/>
									</plx:passParam>
								</plx:callThis>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
			</plx:branch>
			<xsl:choose>
				<xsl:when test="$isChanging">
					<plx:return>
						<plx:trueKeyword/>
					</plx:return>	
				</xsl:when>
				<xsl:when test="$isChanged">
					<plx:fallbackBranch>
						<plx:callThis accessor="this" type="methodCall" name="OnPropertyChanged">
							<plx:passParam>
								<plx:string data="{@name}"/>
							</plx:passParam>
						</plx:callThis>						
					</plx:fallbackBranch>
				</xsl:when>
			</xsl:choose>
		</plx:function>
	</xsl:template>
	
	<xsl:template name="GenerateINotifyPropertyChangedImplementation">
		<plx:field visibility="private" name="{$PrivateMemberPrefix}propertyChangedEventHandler" dataTypeName="PropertyChangedEventHandler"/>
		<plx:event visibility="privateInterfaceMember" name="PropertyChanged">
			<!-- Suppress the 'InterfaceMethodsShouldBeCallableByChildTypes' FxCop warning, since it is not applicable here. -->
			<!-- Child types call the property-specific notification methods, which in turn raise the INotifyPropertyChanged.PropertyChanged event. -->
			<xsl:call-template name="GenerateSuppressMessageAttribute">
				<xsl:with-param name="category" select="'Microsoft.Design'"/>
				<xsl:with-param name="checkId" select="'CA1033:InterfaceMethodsShouldBeCallableByChildTypes'"/>
			</xsl:call-template>
			<plx:interfaceMember memberName="PropertyChanged" dataTypeName="INotifyPropertyChanged"/>
			<plx:param name="sender" dataTypeName=".object"/>
			<plx:param name="e" dataTypeName="PropertyChangedEventArgs"/>
			<plx:explicitDelegateType dataTypeName="PropertyChangedEventHandler"/>
			<plx:onAdd>
				<xsl:call-template name="GetINotifyPropertyChangedImplementationEventOnAddRemoveCode">
					<xsl:with-param name="MethodName" select="'Combine'"/>
				</xsl:call-template>
			</plx:onAdd>
			<plx:onRemove>
				<xsl:call-template name="GetINotifyPropertyChangedImplementationEventOnAddRemoveCode">
					<xsl:with-param name="MethodName" select="'Remove'"/>
				</xsl:call-template>
			</plx:onRemove>
		</plx:event>
		<plx:function visibility="private" overload="{not($RaiseEventsAsynchronously)}" name="OnPropertyChanged">
			<plx:param name="propertyName" dataTypeName=".string"/>
			<plx:local name="eventHandler" dataTypeName="PropertyChangedEventHandler"/>
			<plx:branch>
				<plx:condition>
					<plx:binaryOperator type="identityInequality">
						<plx:left>
							<plx:cast type="exceptionCast" dataTypeName=".object">
								<plx:inlineStatement dataTypeName="PropertyChangedEventHandler">
									<plx:assign>
										<plx:left>
											<plx:nameRef type="local" name="eventHandler"/>
										</plx:left>
										<plx:right>
											<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}propertyChangedEventHandler"/>
										</plx:right>
									</plx:assign>
								</plx:inlineStatement>
							</plx:cast>
						</plx:left>
						<plx:right>
							<plx:nullKeyword/>
						</plx:right>
					</plx:binaryOperator>
				</plx:condition>
				<xsl:variable name="commonCallCodeFragment">
					<plx:passParam>
						<plx:thisKeyword/>
					</plx:passParam>
					<plx:passParam>
						<plx:callNew dataTypeName="PropertyChangedEventArgs">
							<plx:passParam>
								<plx:nameRef type="parameter" name="propertyName"/>
							</plx:passParam>
						</plx:callNew>
					</plx:passParam>
				</xsl:variable>
				<xsl:variable name="commonCallCode" select="exsl:node-set($commonCallCodeFragment)/child::*"/>
				<xsl:choose>
					<xsl:when test="$RaiseEventsAsynchronously">
						<plx:callStatic name="InvokeEventHandlerAsync" dataTypeName="EventHandlerUtility" type="methodCall">
							<plx:passParam>
								<plx:nameRef type="local" name="eventHandler"/>
							</plx:passParam>
							<xsl:copy-of select="$commonCallCode"/>
						</plx:callStatic>
					</xsl:when>
					<xsl:otherwise>
						<plx:callInstance type="delegateCall" name=".implied">
							<plx:callObject>
								<plx:nameRef type="local" name="eventHandler"/>
							</plx:callObject>
							<xsl:copy-of select="$commonCallCode"/>
						</plx:callInstance>
					</xsl:otherwise>
				</xsl:choose>
			</plx:branch>
		</plx:function>
		<xsl:if test="not($RaiseEventsAsynchronously)">
			<plx:function visibility="private" overload="true" name="OnPropertyChanged">
				<plx:param name="e" dataTypeName="PropertyChangedEventArgs"/>
				<plx:local name="eventHandler" dataTypeName="PropertyChangedEventHandler"/>
				<plx:branch>
					<plx:condition>
						<plx:binaryOperator type="identityInequality">
							<plx:left>
								<plx:cast type="exceptionCast" dataTypeName=".object">
									<plx:inlineStatement dataTypeName="PropertyChangedEventHandler">
										<plx:assign>
											<plx:left>
												<plx:nameRef type="local" name="eventHandler"/>
											</plx:left>
											<plx:right>
												<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}propertyChangedEventHandler"/>
											</plx:right>
										</plx:assign>
									</plx:inlineStatement>
								</plx:cast>
							</plx:left>
							<plx:right>
								<plx:nullKeyword/>
							</plx:right>
						</plx:binaryOperator>
					</plx:condition>
					<plx:callInstance type="delegateCall" name=".implied">
						<plx:callObject>
							<plx:nameRef type="local" name="eventHandler"/>
						</plx:callObject>
						<plx:passParam>
							<plx:thisKeyword/>
						</plx:passParam>
						<plx:passParam>
							<plx:nameRef type="parameter" name="e"/>
						</plx:passParam>
					</plx:callInstance>
				</plx:branch>
			</plx:function>	
		</xsl:if>
	</xsl:template>

	<xsl:template name="GetINotifyPropertyChangedImplementationEventOnAddRemoveCode">
		<xsl:param name="MethodName"/>
		<plx:branch>
			<plx:condition>
				<plx:binaryOperator type="identityInequality">
					<plx:left>
						<plx:cast type="exceptionCast" dataTypeName=".object">
							<plx:valueKeyword/>
						</plx:cast>
					</plx:left>
					<plx:right>
						<plx:nullKeyword/>
					</plx:right>
				</plx:binaryOperator>
			</plx:condition>
			<xsl:choose>
				<xsl:when test="$SynchronizeEventAddRemove">
					<plx:local name="currentHandler" dataTypeName="PropertyChangedEventHandler"/>
					<plx:loop checkCondition="before">
						<plx:condition>
							<plx:binaryOperator type="identityInequality">
								<plx:left>
									<plx:cast type="exceptionCast" dataTypeName=".object">
										<plx:callStatic type="methodCall" name="CompareExchange" dataTypeName="Interlocked" dataTypeQualifier="System.Threading">
											<plx:passMemberTypeParam dataTypeName="PropertyChangedEventHandler"/>
											<plx:passParam type="inOut">
												<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}propertyChangedEventHandler"/>
											</plx:passParam>
											<plx:passParam>
												<plx:cast type="exceptionCast" dataTypeName="PropertyChangedEventHandler">
													<plx:callStatic type="methodCall" name="{$MethodName}" dataTypeName="Delegate" dataTypeQualifier="System">
														<plx:passParam>
															<plx:inlineStatement dataTypeName="PropertyChangedEventHandler">
																<plx:assign>
																	<plx:left>
																		<plx:nameRef type="local" name="currentHandler"/>
																	</plx:left>
																	<plx:right>
																		<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}propertyChangedEventHandler"/>
																	</plx:right>
																</plx:assign>
															</plx:inlineStatement>
														</plx:passParam>
														<plx:passParam>
															<plx:valueKeyword/>
														</plx:passParam>
													</plx:callStatic>
												</plx:cast>
											</plx:passParam>
											<plx:passParam>
												<plx:nameRef type="local" name="currentHandler"/>
											</plx:passParam>
										</plx:callStatic>
									</plx:cast>
								</plx:left>
								<plx:right>
									<plx:cast type="exceptionCast" dataTypeName=".object">
										<plx:nameRef type="local" name="currentHandler"/>
									</plx:cast>
								</plx:right>
							</plx:binaryOperator>
						</plx:condition>
					</plx:loop>
				</xsl:when>
				<xsl:otherwise>
					<plx:assign>
						<plx:left>
							<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}propertyChangedEventHandler"/>
						</plx:left>
						<plx:right>
							<plx:cast type="exceptionCast" dataTypeName="PropertyChangedEventHandler">
								<plx:callStatic type="methodCall" name="Combine" dataTypeName="Delegate" dataTypeQualifier="System">
									<plx:passParam>
										<plx:callThis accessor="this" type="field" name="{$PrivateMemberPrefix}propertyChangedEventHandler"/>
									</plx:passParam>
									<plx:passParam>
										<plx:valueKeyword/>
									</plx:passParam>
								</plx:callStatic>
							</plx:cast>
						</plx:right>
					</plx:assign>
				</xsl:otherwise>
			</xsl:choose>
		</plx:branch>
	</xsl:template>

	<xsl:template name="GenerateToString">
		<xsl:param name="ClassName"/>
		<xsl:param name="Properties"/>
		<xsl:variable name="nonCollectionProperties" select="$Properties[not(@isCollection[.='true' or .=1])]"/>
		<plx:function visibility="public" modifier="override" overload="true" name="ToString">
			<plx:returns dataTypeName=".string"/>
			<plx:return>
				<plx:callThis accessor="this" type="methodCall" name="ToString">
					<plx:passParam>
						<plx:nullKeyword/>
					</plx:passParam>
				</plx:callThis>
			</plx:return>
		</plx:function>
		<plx:function visibility="public" modifier="virtual" overload="true" name="ToString">
			<plx:param name="provider" dataTypeName="IFormatProvider"/>
			<plx:returns dataTypeName=".string"/>
			<plx:return>
				<plx:callStatic name="Format" dataTypeName=".string">
					<plx:passParam>
						<plx:nameRef type="parameter" name="provider"/>
					</plx:passParam>
					<plx:passParam>
						<plx:string>
							<xsl:value-of select="concat($ClassName,'{0}{{{0}{1}')"/>
							<xsl:for-each select="$nonCollectionProperties">
								<xsl:value-of select="concat(@name,' = ')"/>
								<xsl:if test="not(@isCustomType[.='true' or .=1])">
									<xsl:value-of select="'&quot;'"/>
								</xsl:if>
								<xsl:value-of select="concat('{',position()+1,'}')"/>
								<xsl:if test="not(@isCustomType[.='true' or .=1])">
									<xsl:value-of select="'&quot;'"/>
								</xsl:if>
								<xsl:if test="not(position()=last())">
									<xsl:value-of select="',{0}{1}'"/>
								</xsl:if>
							</xsl:for-each>
							<xsl:value-of select="'{0}}}'"/>
						</plx:string>
					</plx:passParam>
					<plx:passParam>
						<plx:callStatic type="field" name="NewLine" dataTypeName="Environment"/>
					</plx:passParam>
					<plx:passParam>
						<plx:string>
							<xsl:text disable-output-escaping="yes">&amp;#x09;</xsl:text>
						</plx:string>
					</plx:passParam>
					<xsl:for-each select="$nonCollectionProperties">
						<plx:passParam>
							<xsl:choose>
								<xsl:when test="@isCustomType[.='true' or .=1]">
									<plx:string>TODO: Recursively call ToString for customTypes...</plx:string>
								</xsl:when>
								<xsl:otherwise>
									<plx:callThis accessor="this" type="property" name="{@name}"/>
								</xsl:otherwise>
							</xsl:choose>
						</plx:passParam>
					</xsl:for-each>
				</plx:callStatic>
			</plx:return>
		</plx:function>
	</xsl:template>

	<xsl:template name="GenerateModelContextInterfaceLookupAndExternalConstraintEnforcementMembers">
		<xsl:param name="ConceptTypes"/>
		<xsl:param name="AllProperties"/>
		<!-- TODO: This will break for uniquenessConstraint elements that reference conceptType. -->
		<xsl:variable name="uniquenessConstraints" select="$ConceptTypes/oial:uniquenessConstraints/oial:uniquenessConstraint"/>
		<xsl:for-each select="$uniquenessConstraints">
			<xsl:variable name="uniqueConceptTypePropertiesContainer" select="$AllProperties[@conceptTypeId=current()/../../@id]"/>
			<xsl:variable name="uniqueConceptTypeName" select="string($uniqueConceptTypePropertiesContainer/@conceptTypeName)"/>
			<xsl:variable name="uniqueConceptTypeParamName" select="string($uniqueConceptTypePropertiesContainer/@conceptTypeParamName)"/>
			<xsl:variable name="uniqueConceptTypeProperties" select="$uniqueConceptTypePropertiesContainer/prop:*"/>
			<xsl:variable name="paramsFragment">
				<xsl:for-each select="oial:uniquenessChild">
					<xsl:variable name="targetProperty" select="$uniqueConceptTypeProperties[@childId=current()/@ref]"/>
					<plx:param name="{$targetProperty/@paramName}">
						<xsl:choose>
							<xsl:when test="$targetProperty[self::prop:IdentityField]">
								<xsl:attribute name="dataTypeName">
									<xsl:text>.i4</xsl:text>
								</xsl:attribute>
							</xsl:when>
							<xsl:when test="$targetProperty[not(@isCustomType[.='true' or .=1])] and $targetProperty/@canBeNull[.='true' or .=1] and $targetProperty/prop:DataType/@dataTypeName='Nullable'">
								<xsl:copy-of select="$targetProperty/prop:DataType/plx:passTypeParam/@*"/>
								<xsl:copy-of select="$targetProperty/prop:DataType/plx:passTypeParam/child::*"/>
							</xsl:when>
							<xsl:otherwise>
								<xsl:copy-of select="$targetProperty/prop:DataType/@*"/>
								<xsl:copy-of select="$targetProperty/prop:DataType/child::*"/>
							</xsl:otherwise>
						</xsl:choose>
					</plx:param>
				</xsl:for-each>
			</xsl:variable>
			<!-- We ignore the uniqueness name here, which is rarely informative, in favor of a list of the property
			names used by the uniqueness constraint. -->
			<xsl:variable name="uniquenessSignatureFragment">
				<xsl:for-each select="oial:uniquenessChild">
					<xsl:if test="position()!=1">
						<xsl:text>And</xsl:text>
					</xsl:if>
					<xsl:value-of select="$uniqueConceptTypeProperties[@childId=current()/@ref]/@name"/>
				</xsl:for-each>
			</xsl:variable>
			<xsl:variable name="uniquenessSignature" select="string($uniquenessSignatureFragment)"/>
			<plx:function visibility="public" modifier="abstract" name="Get{$uniqueConceptTypeName}By{$uniquenessSignature}">
				<xsl:copy-of select="$paramsFragment"/>
				<plx:returns dataTypeName="{$uniqueConceptTypeName}"/>
			</plx:function>
			<plx:function visibility="public" modifier="abstract" name="TryGet{$uniqueConceptTypeName}By{$uniquenessSignature}">
				<xsl:copy-of select="$paramsFragment"/>
				<plx:param type="out" name="{$uniqueConceptTypeParamName}" dataTypeName="{$uniqueConceptTypeName}"/>
				<plx:returns dataTypeName=".boolean"/>
			</plx:function>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="oial:conceptType" mode="GenerateModelContextInterfaceObjectMethods">
		<xsl:param name="Model"/>
		<xsl:param name="ClassName"/>
		<xsl:param name="Properties"/>
		<plx:function visibility="public" modifier="abstract" name="Create{$ClassName}">
			<xsl:for-each select="$Properties[@mandatory='alethic']">
				<plx:param name="{@paramName}">
					<xsl:copy-of select="prop:DataType/@*"/>
					<xsl:copy-of select="prop:DataType/child::*"/>
				</plx:param>
			</xsl:for-each>
			<plx:returns dataTypeName="{$ClassName}"/>
		</plx:function>
		<plx:property visibility="public" modifier="abstract" name="{$ClassName}Collection">
			<plx:returns dataTypeName="IEnumerable">
				<plx:passTypeParam dataTypeName="{$ClassName}"/>
			</plx:returns>
			<plx:get/>
		</plx:property>
	</xsl:template>

</xsl:stylesheet>
