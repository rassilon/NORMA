﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Neumont.Tools.ORM.Shell;
using Neumont.Tools.ORM.ObjectModel;

// Common Public License Copyright Notice
// /**************************************************************************\
// * Neumont Object-Role Modeling Architect for Visual Studio                 *
// *                                                                          *
// * Copyright © Neumont University. All rights reserved.                     *
// *                                                                          *
// * The use and distribution terms for this software are covered by the      *
// * Common Public License 1.0 (http://opensource.org/licenses/cpl) which     *
// * can be found in the file CPL.txt at the root of this distribution.       *
// * By using this software in any fashion, you are agreeing to be bound by   *
// * the terms of this license.                                               *
// *                                                                          *
// * You must not remove this notice, or any other, from this software.       *
// \**************************************************************************/

namespace Neumont.Tools.ORM.ObjectModel
{
	#region ORMCoreDomainModel model serialization
	partial class ORMCoreDomainModel : IORMCustomSerializedDomainModel
	{
		/// <summary>
		/// The default XmlNamespace associated with the 'ORMCoreDomainModel' extension model
		/// </summary>
		public static readonly string XmlNamespace = "http://schemas.neumont.edu/ORM/2006-04/ORMCore";
		/// <summary>
		/// Implements IORMCustomSerializedDomainModel.DefaultElementPrefix
		/// </summary>
		protected static string DefaultElementPrefix
		{
			get
			{
				return "orm";
			}
		}
		string IORMCustomSerializedDomainModel.DefaultElementPrefix
		{
			get
			{
				return DefaultElementPrefix;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedDomainModel.GetCustomElementNamespaces
		/// </summary>
		protected static string[,] GetCustomElementNamespaces()
		{
			string[,] ret = new string[1, 3];
			ret[0, 0] = "orm";
			ret[0, 1] = "http://schemas.neumont.edu/ORM/2006-04/ORMCore";
			ret[0, 2] = "ORM2Core.xsd";
			return ret;
		}
		string[,] IORMCustomSerializedDomainModel.GetCustomElementNamespaces()
		{
			return GetCustomElementNamespaces();
		}
		private Dictionary<DomainClassInfo, object> myCustomSerializationOmissions;
		private static Dictionary<DomainClassInfo, object> BuildCustomSerializationOmissions(Store store)
		{
			Dictionary<DomainClassInfo, object> retVal = new Dictionary<DomainClassInfo, object>();
			DomainDataDirectory dataDir = store.DomainDataDirectory;
			retVal[dataDir.FindDomainRelationship(FactConstraint.DomainClassId)] = null;
			retVal[dataDir.FindDomainRelationship(FactSetComparisonConstraint.DomainClassId)] = null;
			retVal[dataDir.FindDomainRelationship(ExternalRoleConstraint.DomainClassId)] = null;
			retVal[dataDir.FindDomainRelationship(RoleInstance.DomainClassId)] = null;
			retVal[dataDir.FindDomainRelationship(ModelNoteReferencesModelElement.DomainClassId)] = null;
			retVal[dataDir.FindDomainClass(IntrinsicReferenceMode.DomainClassId)] = null;
			retVal[dataDir.FindDomainRelationship(Microsoft.VisualStudio.Modeling.Diagrams.PresentationViewsSubject.DomainClassId)] = null;
			return retVal;
		}
		private static Dictionary<string, Guid> myClassNameMap;
		private static Collection<string> myValidNamespaces;
		/// <summary>
		/// Implements IORMCustomSerializedDomainModel.ShouldSerializeDomainClass
		/// </summary>
		protected bool ShouldSerializeDomainClass(Store store, DomainClassInfo classInfo)
		{
			Dictionary<DomainClassInfo, object> omissions = this.myCustomSerializationOmissions;
			if (omissions == null)
			{
				omissions = ORMCoreDomainModel.BuildCustomSerializationOmissions(store);
				this.myCustomSerializationOmissions = omissions;
			}
			return !(omissions.ContainsKey(classInfo));
		}
		bool IORMCustomSerializedDomainModel.ShouldSerializeDomainClass(Store store, DomainClassInfo classInfo)
		{
			return this.ShouldSerializeDomainClass(store, classInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedDomainModel.GetRootElementClasses
		/// </summary>
		protected static Guid[] GetRootElementClasses()
		{
			return new Guid[]{
				ORMModel.DomainClassId};
		}
		Guid[] IORMCustomSerializedDomainModel.GetRootElementClasses()
		{
			return GetRootElementClasses();
		}
		/// <summary>
		/// Implements IORMCustomSerializedDomainModel.MapRootElement
		/// </summary>
		protected static Guid MapRootElement(string xmlNamespace, string elementName)
		{
			if ((elementName == "ORMModel") && (xmlNamespace == "http://schemas.neumont.edu/ORM/2006-04/ORMCore"))
			{
				return ORMModel.DomainClassId;
			}
			return default(Guid);
		}
		Guid IORMCustomSerializedDomainModel.MapRootElement(string xmlNamespace, string elementName)
		{
			return MapRootElement(xmlNamespace, elementName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedDomainModel.MapClassName
		/// </summary>
		protected static Guid MapClassName(string xmlNamespace, string elementName)
		{
			Collection<string> validNamespaces = ORMCoreDomainModel.myValidNamespaces;
			Dictionary<string, Guid> classNameMap = ORMCoreDomainModel.myClassNameMap;
			if (validNamespaces == null)
			{
				validNamespaces = new Collection<string>();
				validNamespaces.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore");
				ORMCoreDomainModel.myValidNamespaces = validNamespaces;
			}
			if (classNameMap == null)
			{
				classNameMap = new Dictionary<string, Guid>();
				classNameMap.Add("ORMModelElement", ORMModelElement.DomainClassId);
				classNameMap.Add("ORMNamedElement", ORMNamedElement.DomainClassId);
				classNameMap.Add("ORMModel", ORMModel.DomainClassId);
				classNameMap.Add("Note", Note.DomainClassId);
				classNameMap.Add("ModelNote", ModelNote.DomainClassId);
				classNameMap.Add("EntityType", ObjectType.DomainClassId);
				classNameMap.Add("ValueType", ObjectType.DomainClassId);
				classNameMap.Add("ObjectifiedType", ObjectType.DomainClassId);
				classNameMap.Add("ValueTypeInstance", ValueTypeInstance.DomainClassId);
				classNameMap.Add("EntityTypeInstance", EntityTypeInstance.DomainClassId);
				classNameMap.Add("EntityTypeRoleInstance", EntityTypeRoleInstance.DomainClassId);
				classNameMap.Add("CustomReferenceMode", CustomReferenceMode.DomainClassId);
				classNameMap.Add("ValueTypeHasDataType", ValueTypeHasDataType.DomainClassId);
				classNameMap.Add("DataType", DataType.DomainClassId);
				classNameMap.Add("ReferenceModeKind", ReferenceModeKind.DomainClassId);
				classNameMap.Add("BaseValueConstraint", ValueConstraint.DomainClassId);
				classNameMap.Add("ValueConstraint", ValueTypeValueConstraint.DomainClassId);
				classNameMap.Add("RoleValueConstraint", RoleValueConstraint.DomainClassId);
				classNameMap.Add("ValueRange", ValueRange.DomainClassId);
				classNameMap.Add("Fact", FactType.DomainClassId);
				classNameMap.Add("ImpliedFact", FactType.DomainClassId);
				classNameMap.Add("FactTypeInstance", FactTypeInstance.DomainClassId);
				classNameMap.Add("FactTypeRoleInstance", FactTypeRoleInstance.DomainClassId);
				classNameMap.Add("Expression", Expression.DomainClassId);
				classNameMap.Add("DerivationExpression", FactTypeDerivationExpression.DomainClassId);
				classNameMap.Add("SubtypeFact", SubtypeFact.DomainClassId);
				classNameMap.Add("Objectification", Objectification.DomainClassId);
				classNameMap.Add("ReadingOrder", ReadingOrder.DomainClassId);
				classNameMap.Add("Reading", Reading.DomainClassId);
				classNameMap.Add("Role", Role.DomainClassId);
				classNameMap.Add("RoleProxy", RoleProxy.DomainClassId);
				classNameMap.Add("SetComparisonConstraint", SetComparisonConstraint.DomainClassId);
				classNameMap.Add("RoleSequence", SetComparisonConstraintRoleSequence.DomainClassId);
				classNameMap.Add("SetConstraint", SetConstraint.DomainClassId);
				classNameMap.Add("MandatoryConstraint", MandatoryConstraint.DomainClassId);
				classNameMap.Add("FrequencyConstraint", FrequencyConstraint.DomainClassId);
				classNameMap.Add("UniquenessConstraint", UniquenessConstraint.DomainClassId);
				classNameMap.Add("EqualityConstraint", EqualityConstraint.DomainClassId);
				classNameMap.Add("ExclusionConstraint", ExclusionConstraint.DomainClassId);
				classNameMap.Add("RingConstraint", RingConstraint.DomainClassId);
				classNameMap.Add("ConstraintDuplicateNameError", ConstraintDuplicateNameError.DomainClassId);
				classNameMap.Add("ObjectTypeDuplicateNameError", ObjectTypeDuplicateNameError.DomainClassId);
				classNameMap.Add("EntityTypeRequiresReferenceSchemeError", EntityTypeRequiresReferenceSchemeError.DomainClassId);
				classNameMap.Add("ExternalConstraintRoleSequenceArityMismatchError", ExternalConstraintRoleSequenceArityMismatchError.DomainClassId);
				classNameMap.Add("ImpliedInternalUniquenessConstraintError", ImpliedInternalUniquenessConstraintError.DomainClassId);
				classNameMap.Add("FactTypeRequiresInternalUniquenessConstraintError", FactTypeRequiresInternalUniquenessConstraintError.DomainClassId);
				classNameMap.Add("FactTypeRequiresReadingError", FactTypeRequiresReadingError.DomainClassId);
				classNameMap.Add("FrequencyConstraintMinMaxError", FrequencyConstraintMinMaxError.DomainClassId);
				classNameMap.Add("MinValueMismatchError", MinValueMismatchError.DomainClassId);
				classNameMap.Add("MaxValueMismatchError", MaxValueMismatchError.DomainClassId);
				classNameMap.Add("ValueRangeOverlapError", ValueRangeOverlapError.DomainClassId);
				classNameMap.Add("RingConstraintTypeNotSpecifiedError", RingConstraintTypeNotSpecifiedError.DomainClassId);
				classNameMap.Add("CompatibleValueTypeInstanceValueError", CompatibleValueTypeInstanceValueError.DomainClassId);
				classNameMap.Add("TooFewEntityTypeRoleInstancesError", TooFewEntityTypeRoleInstancesError.DomainClassId);
				classNameMap.Add("TooFewFactTypeRoleInstancesError", TooFewFactTypeRoleInstancesError.DomainClassId);
				classNameMap.Add("PopulationMandatoryError", PopulationMandatoryError.DomainClassId);
				classNameMap.Add("PopulationUniquenessError", PopulationUniquenessError.DomainClassId);
				classNameMap.Add("TooFewReadingRolesError", TooFewReadingRolesError.DomainClassId);
				classNameMap.Add("TooFewRoleSequencesError", TooFewRoleSequencesError.DomainClassId);
				classNameMap.Add("TooManyReadingRolesError", TooManyReadingRolesError.DomainClassId);
				classNameMap.Add("TooManyRoleSequencesError", TooManyRoleSequencesError.DomainClassId);
				classNameMap.Add("DataTypeNotSpecifiedError", DataTypeNotSpecifiedError.DomainClassId);
				classNameMap.Add("EqualityImpliedByMandatoryError", EqualityImpliedByMandatoryError.DomainClassId);
				classNameMap.Add("NMinusOneError", NMinusOneError.DomainClassId);
				classNameMap.Add("ImplicationError", ImplicationError.DomainClassId);
				classNameMap.Add("ContradictionError", ContradictionError.DomainClassId);
				classNameMap.Add("ExclusionContradictsEqualityError", ExclusionContradictsEqualityError.DomainClassId);
				classNameMap.Add("ObjectTypeRequiresPrimarySupertypeError", ObjectTypeRequiresPrimarySupertypeError.DomainClassId);
				classNameMap.Add("PreferredIdentifierRequiresMandatoryError", PreferredIdentifierRequiresMandatoryError.DomainClassId);
				classNameMap.Add("CompatibleSupertypesError", CompatibleSupertypesError.DomainClassId);
				classNameMap.Add("CompatibleRolePlayerTypeError", CompatibleRolePlayerTypeError.DomainClassId);
				classNameMap.Add("RolePlayerRequiredError", RolePlayerRequiredError.DomainClassId);
				classNameMap.Add("FrequencyConstraintContradictsInternalUniquenessConstraintError", FrequencyConstraintContradictsInternalUniquenessConstraintError.DomainClassId);
				ORMCoreDomainModel.myClassNameMap = classNameMap;
			}
			if (validNamespaces.Contains(xmlNamespace) && classNameMap.ContainsKey(elementName))
			{
				return classNameMap[elementName];
			}
			return default(Guid);
		}
		Guid IORMCustomSerializedDomainModel.MapClassName(string xmlNamespace, string elementName)
		{
			return MapClassName(xmlNamespace, elementName);
		}
	}
	#endregion // ORMCoreDomainModel model serialization
	#region ORMModelElement serialization
	partial class ORMModelElement : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return ORMCustomSerializedElementSupportedOperations.ChildElementInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ORMModelElement.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ret = new ORMCustomSerializedChildElementInfo[1];
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Extensions", null, ORMCustomSerializedElementWriteStyle.Element, null, ORMModelElementHasExtensionElement.ExtensionDomainRoleId);
				ORMModelElement.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				throw new NotSupportedException();
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				return null;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ORMModelElement.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ORMModelElementHasExtensionElement.ExtensionDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Extensions||", match);
				ORMModelElement.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal);
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			return default(Guid);
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.ShouldSerialize
		/// </summary>
		protected static bool ShouldSerialize()
		{
			return true;
		}
		bool IORMCustomSerializedElement.ShouldSerialize()
		{
			return ShouldSerialize();
		}
	}
	#endregion // ORMModelElement serialization
	#region ORMNamedElement serialization
	partial class ORMNamedElement : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.None;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
	}
	#endregion // ORMNamedElement serialization
	#region ORMModel serialization
	partial class ORMModel : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.ElementInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ORMModel.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 8];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 8);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Objects", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasObjectType.ObjectTypeDomainRoleId);
				ret[1] = new ORMCustomSerializedChildElementInfo(null, "Facts", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasFactType.FactTypeDomainRoleId);
				ret[2] = new ORMCustomSerializedChildElementInfo(null, "Constraints", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasSetComparisonConstraint.SetComparisonConstraintDomainRoleId, ModelHasSetConstraint.SetConstraintDomainRoleId);
				ret[3] = new ORMCustomSerializedChildElementInfo(null, "DataTypes", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasDataType.DataTypeDomainRoleId);
				ret[4] = new ORMCustomSerializedChildElementInfo(null, "CustomReferenceModes", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasReferenceMode.ReferenceModeDomainRoleId);
				ret[5] = new ORMCustomSerializedChildElementInfo(null, "ModelNotes", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasModelNote.NoteDomainRoleId);
				ret[6] = new ORMCustomSerializedChildElementInfo(null, "ModelErrors", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasError.ErrorDomainRoleId);
				ret[7] = new ORMCustomSerializedChildElementInfo(null, "ReferenceModeKinds", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelHasReferenceModeKind.ReferenceModeKindDomainRoleId);
				ORMModel.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "ORMModel", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasObjectType.ObjectTypeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasFactType.FactTypeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasSetComparisonConstraint.SetComparisonConstraintDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasSetConstraint.SetConstraintDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasDataType.DataTypeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 3;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasReferenceMode.ReferenceModeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 4;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasModelNote.NoteDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 5;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasError.ErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 6;
				domainRole = domainDataDirectory.FindDomainRole(ModelHasReferenceModeKind.ReferenceModeKindDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 7;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = ORMModel.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					ORMModel.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ORMModel.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ModelHasObjectType.ObjectTypeDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Objects||", match);
				match.InitializeRoles(ModelHasFactType.FactTypeDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Facts||", match);
				match.InitializeRoles(ModelHasSetComparisonConstraint.SetComparisonConstraintDomainRoleId, ModelHasSetConstraint.SetConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Constraints||", match);
				match.InitializeRoles(ModelHasDataType.DataTypeDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|DataTypes||", match);
				match.InitializeRoles(ModelHasReferenceMode.ReferenceModeDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|CustomReferenceModes||", match);
				match.InitializeRoles(ModelHasModelNote.NoteDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ModelNotes||", match);
				match.InitializeRoles(ModelHasError.ErrorDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ModelErrors||", match);
				match.InitializeRoles(ModelHasReferenceModeKind.ReferenceModeKindDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ReferenceModeKinds||", match);
				ORMModel.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ORMModel serialization
	#region Note serialization
	partial class Note : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.PropertyInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == Note.TextDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = Note.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeAttribute(Note.TextDomainPropertyId, null);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Text", match);
				Note.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // Note serialization
	#region ModelNote serialization
	partial class ModelNote : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ModelNote.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "ReferencedBy", null, ORMCustomSerializedElementWriteStyle.Element, null, ModelNoteReferencesFactType.ElementDomainRoleId, ModelNoteReferencesObjectType.ElementDomainRoleId);
				ModelNote.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ModelNoteReferencesFactType.ElementDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "FactType", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == ModelNoteReferencesObjectType.ElementDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ObjectType", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ModelNote.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ModelNoteReferencesFactType.ElementDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ReferencedBy|http://schemas.neumont.edu/ORM/2006-04/ORMCore|FactType", match);
				match.InitializeRoles(ModelNoteReferencesObjectType.ElementDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ReferencedBy|http://schemas.neumont.edu/ORM/2006-04/ORMCore|ObjectType", match);
				ModelNote.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ModelNote serialization
	#region ObjectType serialization
	partial class ObjectType : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.ElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles))));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ObjectType.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 4];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 4);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Notes", null, ORMCustomSerializedElementWriteStyle.Element, null, ObjectTypeHasNote.NoteDomainRoleId);
				ret[1] = new ORMCustomSerializedChildElementInfo(null, "PlayedRoles", null, ORMCustomSerializedElementWriteStyle.Element, null, ObjectTypePlaysRole.PlayedRoleDomainRoleId);
				ret[2] = new ORMCustomSerializedChildElementInfo(null, "ValueRestriction", null, ORMCustomSerializedElementWriteStyle.Element, null, ValueTypeHasValueConstraint.ValueConstraintDomainRoleId);
				ret[3] = new ORMCustomSerializedChildElementInfo(null, "Instances", null, ORMCustomSerializedElementWriteStyle.Element, null, ValueTypeHasValueTypeInstance.ValueTypeInstanceDomainRoleId, EntityTypeHasEntityTypeInstance.EntityTypeInstanceDomainRoleId);
				ObjectType.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				string name = "EntityType";
				if (this.IsValueType)
				{
					name = "ValueType";
				}
				else if (this.NestedFactType != null)
				{
					name = "ObjectifiedType";
				}
				return new ORMCustomSerializedElementInfo(null, name, null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == ObjectType.ReferenceModeStringDomainPropertyId)
			{
				if (this.IsValueType)
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, "_ReferenceMode", null, true, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (domainPropertyInfo.Id == ObjectType.IsIndependentDomainPropertyId)
			{
				if (!(this.IsIndependent))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (domainPropertyInfo.Id == ObjectType.IsExternalDomainPropertyId)
			{
				if (!(this.IsExternal))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (domainPropertyInfo.Id == ObjectType.IsPersonalDomainPropertyId)
			{
				if (!(this.IsPersonal))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueTypeHasDataType.DataTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ConceptualDataType", null, ORMCustomSerializedElementWriteStyle.PrimaryLinkElement, null);
			}
			if (roleId == ObjectTypePlaysRole.PlayedRoleDomainRoleId)
			{
				string name = "Role";
				if (DomainRoleInfo.GetRolePlayer(elementLink, ObjectTypePlaysRole.PlayedRoleDomainRoleId) is SubtypeMetaRole)
				{
					name = "SubtypeMetaRole";
				}
				else if (DomainRoleInfo.GetRolePlayer(elementLink, ObjectTypePlaysRole.PlayedRoleDomainRoleId) is SupertypeMetaRole)
				{
					name = "SupertypeMetaRole";
				}
				return new ORMCustomSerializedElementInfo(null, name, null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == EntityTypeHasPreferredIdentifier.PreferredIdentifierDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "PreferredIdentifier", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == Objectification.NestedFactTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "NestedPredicate", null, ORMCustomSerializedElementWriteStyle.PrimaryLinkElement, null);
			}
			if (roleId == ObjectTypeHasEntityTypeRequiresReferenceSchemeError.ReferenceSchemeErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ObjectTypeHasDuplicateNameError.DuplicateNameErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ObjectTypeHasObjectTypeRequiresPrimarySupertypeError.ObjectTypeRequiresPrimarySupertypeErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ObjectTypeHasPreferredIdentifierRequiresMandatoryError.PreferredIdentifierRequiresMandatoryErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ObjectTypeHasCompatibleSupertypesError.CompatibleSupertypesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ModelNoteReferencesObjectType.NoteDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypeHasNote.NoteDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypePlaysRole.PlayedRoleDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				domainRole = domainDataDirectory.FindDomainRole(ValueTypeHasDataType.DataTypeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(ValueTypeHasValueConstraint.ValueConstraintDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 3;
				domainRole = domainDataDirectory.FindDomainRole(EntityTypeHasPreferredIdentifier.PreferredIdentifierDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 4;
				domainRole = domainDataDirectory.FindDomainRole(Objectification.NestedFactTypeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 5;
				domainRole = domainDataDirectory.FindDomainRole(ValueTypeHasValueTypeInstance.ValueTypeInstanceDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 6;
				domainRole = domainDataDirectory.FindDomainRole(EntityTypeHasEntityTypeInstance.EntityTypeInstanceDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 6;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypeHasEntityTypeRequiresReferenceSchemeError.ReferenceSchemeErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 7;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypeHasDuplicateNameError.DuplicateNameErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 8;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypeHasObjectTypeRequiresPrimarySupertypeError.ObjectTypeRequiresPrimarySupertypeErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 9;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypeHasPreferredIdentifierRequiresMandatoryError.PreferredIdentifierRequiresMandatoryErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 10;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypeHasCompatibleSupertypesError.CompatibleSupertypesErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 11;
				domainRole = domainDataDirectory.FindDomainRole(ModelNoteReferencesObjectType.NoteDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 12;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = ObjectType.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					ObjectType.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ObjectType.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ValueTypeHasDataType.DataTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ConceptualDataType", match);
				match.InitializeRoles(EntityTypeHasPreferredIdentifier.PreferredIdentifierDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|PreferredIdentifier", match);
				match.InitializeRoles(Objectification.NestedFactTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|NestedPredicate", match);
				match.InitializeRoles(ObjectTypePlaysRole.PlayedRoleDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|PlayedRoles|http://schemas.neumont.edu/ORM/2006-04/ORMCore|Role", match);
				match.InitializeRoles(ObjectTypeHasNote.NoteDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Notes||", match);
				match.InitializeRoles(ValueTypeHasValueConstraint.ValueConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueRestriction||", match);
				match.InitializeRoles(ValueTypeHasValueTypeInstance.ValueTypeInstanceDomainRoleId, EntityTypeHasEntityTypeInstance.EntityTypeInstanceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Instances||", match);
				ObjectType.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = ObjectType.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("_ReferenceMode", ObjectType.ReferenceModeStringDomainPropertyId);
				customSerializedAttributes.Add("IsIndependent", ObjectType.IsIndependentDomainPropertyId);
				customSerializedAttributes.Add("IsExternal", ObjectType.IsExternalDomainPropertyId);
				customSerializedAttributes.Add("IsPersonal", ObjectType.IsPersonalDomainPropertyId);
				ObjectType.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // ObjectType serialization
	#region ValueTypeInstance serialization
	partial class ValueTypeInstance : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "ValueTypeInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == ValueTypeInstance.ValueDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == EntityTypeRoleInstance.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeRoleInstance.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ValueTypeInstanceHasCompatibleValueTypeInstanceValueError.CompatibleValueTypeInstanceValueErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ObjectTypeInstanceHasPopulationMandatoryError.PopulationMandatoryErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ValueTypeInstance.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeAttribute(ValueTypeInstance.ValueDomainPropertyId, null);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Value", match);
				ValueTypeInstance.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ValueTypeInstance serialization
	#region EntityTypeInstance serialization
	partial class EntityTypeInstance : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.ElementInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = EntityTypeInstance.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "RoleInstances", null, ORMCustomSerializedElementWriteStyle.Element, null, EntityTypeInstanceHasRoleInstance.RoleInstanceDomainRoleId);
				EntityTypeInstance.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "EntityTypeInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == EntityTypeInstanceHasRoleInstance.RoleInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "EntityTypeRoleInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == EntityTypeRoleInstance.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeRoleInstance.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == EntityTypeInstanceHasTooFewEntityTypeRoleInstancesError.TooFewEntityTypeRoleInstancesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ObjectTypeInstanceHasPopulationMandatoryError.PopulationMandatoryErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = EntityTypeInstance.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(EntityTypeInstanceHasRoleInstance.RoleInstanceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleInstances|http://schemas.neumont.edu/ORM/2006-04/ORMCore|EntityTypeRoleInstance", match);
				EntityTypeInstance.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // EntityTypeInstance serialization
	#region EntityTypeRoleInstance serialization
	partial class EntityTypeRoleInstance : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				throw new NotSupportedException();
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == EntityTypeInstanceHasRoleInstance.EntityTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				return null;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return default(ORMCustomSerializedElementMatch);
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			return default(Guid);
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.ShouldSerialize
		/// </summary>
		protected static bool ShouldSerialize()
		{
			return true;
		}
		bool IORMCustomSerializedElement.ShouldSerialize()
		{
			return ShouldSerialize();
		}
	}
	#endregion // EntityTypeRoleInstance serialization
	#region CustomReferenceMode serialization
	partial class CustomReferenceMode : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles | ORMCustomSerializedElementSupportedOperations.MixedTypedAttributes)));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == CustomReferenceMode.CustomFormatStringDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ReferenceModeHasReferenceModeKind.KindDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Kind", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(ReferenceModeHasReferenceModeKind.KindDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = CustomReferenceMode.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					CustomReferenceMode.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = CustomReferenceMode.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ReferenceModeHasReferenceModeKind.KindDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Kind", match);
				match.InitializeAttribute(CustomReferenceMode.CustomFormatStringDomainPropertyId, null);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|CustomFormatString", match);
				CustomReferenceMode.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // CustomReferenceMode serialization
	#region ValueTypeHasDataType serialization
	partial class ValueTypeHasDataType : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				throw new NotSupportedException();
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == ValueTypeHasDataType.ScaleDomainPropertyId)
			{
				if (rolePlayedInfo.Id == ValueTypeHasDataType.ValueTypeDomainRoleId)
				{
					return new ORMCustomSerializedPropertyInfo(null, "Scale", null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueTypeHasUnspecifiedDataTypeError.DataTypeNotSpecifiedErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				return null;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return default(ORMCustomSerializedElementMatch);
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = ValueTypeHasDataType.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("Scale", ValueTypeHasDataType.ScaleDomainPropertyId);
				ValueTypeHasDataType.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			customSerializedAttributes.TryGetValue(key, out rVal);
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.ShouldSerialize
		/// </summary>
		protected static bool ShouldSerialize()
		{
			return true;
		}
		bool IORMCustomSerializedElement.ShouldSerialize()
		{
			return ShouldSerialize();
		}
	}
	#endregion // ValueTypeHasDataType serialization
	#region DataType serialization
	partial class DataType : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueTypeHasDataType.ValueTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.ShouldSerialize
		/// </summary>
		protected new bool ShouldSerialize()
		{
			return DomainRoleInfo.GetAllElementLinks(this).Count > 1;
		}
		bool IORMCustomSerializedElement.ShouldSerialize()
		{
			return this.ShouldSerialize();
		}
	}
	#endregion // DataType serialization
	#region ReferenceModeKind serialization
	partial class ReferenceModeKind : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "ReferenceModeKind", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == ReferenceModeKind.NameDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ReferenceModeHasReferenceModeKind.ReferenceModeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
	}
	#endregion // ReferenceModeKind serialization
	#region ValueConstraint serialization
	partial class ValueConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.ElementInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ValueConstraint.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "ValueRanges", null, ORMCustomSerializedElementWriteStyle.Element, null, ValueConstraintHasValueRange.ValueRangeDomainRoleId);
				ValueConstraint.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "BaseValueConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueConstraintHasDuplicateNameError.DuplicateNameErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ValueConstraintHasValueRangeOverlapError.ValueRangeOverlapErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ValueConstraint.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ValueConstraintHasValueRange.ValueRangeDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueRanges||", match);
				ValueConstraint.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ValueConstraint serialization
	#region ValueTypeValueConstraint serialization
	partial class ValueTypeValueConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.ElementInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "ValueConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
	}
	#endregion // ValueTypeValueConstraint serialization
	#region RoleValueConstraint serialization
	partial class RoleValueConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.ElementInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "RoleValueConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
	}
	#endregion // RoleValueConstraint serialization
	#region ValueRange serialization
	partial class ValueRange : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueRangeHasMinValueMismatchError.MinValueMismatchErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ValueRangeHasMaxValueMismatchError.MaxValueMismatchErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
	}
	#endregion // ValueRange serialization
	#region FactType serialization
	partial class FactType : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.ElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles))));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = FactType.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 6];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 6);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Notes", null, ORMCustomSerializedElementWriteStyle.Element, null, FactTypeHasNote.NoteDomainRoleId);
				ret[1] = new ORMCustomSerializedChildElementInfo(null, "FactRoles", null, ORMCustomSerializedElementWriteStyle.Element, null, FactTypeHasRole.RoleDomainRoleId);
				ret[2] = new ORMCustomSerializedChildElementInfo(null, "ReadingOrders", null, ORMCustomSerializedElementWriteStyle.Element, null, FactTypeHasReadingOrder.ReadingOrderDomainRoleId);
				ret[3] = new ORMCustomSerializedChildElementInfo(null, "InternalConstraints", null, ORMCustomSerializedElementWriteStyle.Element, null, FactSetConstraint.SetConstraintDomainRoleId);
				ret[4] = new ORMCustomSerializedChildElementInfo(null, "DerivationRule", null, ORMCustomSerializedElementWriteStyle.Element, null, FactTypeHasDerivationExpression.DerivationRuleDomainRoleId);
				ret[5] = new ORMCustomSerializedChildElementInfo(null, "Instances", null, ORMCustomSerializedElementWriteStyle.Element, null, FactTypeHasFactTypeInstance.FactTypeInstanceDomainRoleId);
				FactType.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				string name = "Fact";
				if (this.ImpliedByObjectification != null)
				{
					name = "ImpliedFact";
				}
				return new ORMCustomSerializedElementInfo(null, name, null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == FactType.IsExternalDomainPropertyId)
			{
				if (!(this.IsExternal))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactSetConstraint.SetConstraintDomainRoleId)
			{
				string name = null;
				ORMCustomSerializedElementWriteStyle writeStyle = ORMCustomSerializedElementWriteStyle.NotWritten;
				if (ConstraintType.InternalUniqueness == ((IConstraint)DomainRoleInfo.GetRolePlayer(elementLink, FactSetConstraint.SetConstraintDomainRoleId)).ConstraintType)
				{
					name = "UniquenessConstraint";
					writeStyle = ORMCustomSerializedElementWriteStyle.Element;
				}
				else if (ConstraintType.SimpleMandatory == ((IConstraint)DomainRoleInfo.GetRolePlayer(elementLink, FactSetConstraint.SetConstraintDomainRoleId)).ConstraintType)
				{
					name = "MandatoryConstraint";
					writeStyle = ORMCustomSerializedElementWriteStyle.Element;
				}
				return new ORMCustomSerializedElementInfo(null, name, null, writeStyle, null);
			}
			if (roleId == ObjectificationImpliesFactType.ImpliedByObjectificationDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ImpliedByObjectification", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == Objectification.NestingTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeHasFactTypeRequiresInternalUniquenessConstraintError.InternalUniquenessConstraintRequiredErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeHasFactTypeRequiresReadingError.ReadingRequiredErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeHasImpliedInternalUniquenessConstraintError.ImpliedInternalUniquenessConstraintErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeHasFrequencyConstraintContradictsInternalUniquenessConstraintError.FrequencyConstraintContradictsInternalUniquenessConstraintErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ModelNoteReferencesFactType.NoteDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasNote.NoteDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasRole.RoleDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasReadingOrder.ReadingOrderDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(FactSetConstraint.SetConstraintDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 3;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasDerivationExpression.DerivationRuleDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 4;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasFactTypeInstance.FactTypeInstanceDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 5;
				domainRole = domainDataDirectory.FindDomainRole(ObjectificationImpliesFactType.ImpliedByObjectificationDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 6;
				domainRole = domainDataDirectory.FindDomainRole(Objectification.NestingTypeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 7;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasFactTypeRequiresInternalUniquenessConstraintError.InternalUniquenessConstraintRequiredErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 8;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasFactTypeRequiresReadingError.ReadingRequiredErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 9;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasImpliedInternalUniquenessConstraintError.ImpliedInternalUniquenessConstraintErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 10;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeHasFrequencyConstraintContradictsInternalUniquenessConstraintError.FrequencyConstraintContradictsInternalUniquenessConstraintErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 11;
				domainRole = domainDataDirectory.FindDomainRole(ModelNoteReferencesFactType.NoteDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 12;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = FactType.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					FactType.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = FactType.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectificationImpliesFactType.ImpliedByObjectificationDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ImpliedByObjectification", match);
				match.InitializeRoles(FactTypeHasNote.NoteDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Notes||", match);
				match.InitializeRoles(FactTypeHasRole.RoleDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|FactRoles||", match);
				match.InitializeRoles(FactTypeHasReadingOrder.ReadingOrderDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ReadingOrders||", match);
				match.InitializeRoles(FactSetConstraint.SetConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|InternalConstraints||", match);
				match.InitializeRoles(FactTypeHasDerivationExpression.DerivationRuleDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|DerivationRule||", match);
				match.InitializeRoles(FactTypeHasFactTypeInstance.FactTypeInstanceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Instances||", match);
				FactType.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = FactType.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("IsExternal", FactType.IsExternalDomainPropertyId);
				FactType.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // FactType serialization
	#region FactTypeInstance serialization
	partial class FactTypeInstance : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.ElementInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = FactTypeInstance.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "RoleInstances", null, ORMCustomSerializedElementWriteStyle.Element, null, FactTypeInstanceHasRoleInstance.RoleInstanceDomainRoleId);
				FactTypeInstance.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "FactTypeInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactTypeInstanceHasRoleInstance.RoleInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "FactTypeRoleInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == EntityTypeRoleInstance.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeRoleInstance.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactTypeInstanceHasTooFewFactTypeRoleInstancesError.TooFewFactTypeRoleInstancesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = FactTypeInstance.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(FactTypeInstanceHasRoleInstance.RoleInstanceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleInstances|http://schemas.neumont.edu/ORM/2006-04/ORMCore|FactTypeRoleInstance", match);
				FactTypeInstance.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // FactTypeInstance serialization
	#region FactTypeRoleInstance serialization
	partial class FactTypeRoleInstance : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				throw new NotSupportedException();
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactTypeInstanceHasRoleInstance.FactTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				return null;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return default(ORMCustomSerializedElementMatch);
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			return default(Guid);
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.ShouldSerialize
		/// </summary>
		protected static bool ShouldSerialize()
		{
			return true;
		}
		bool IORMCustomSerializedElement.ShouldSerialize()
		{
			return ShouldSerialize();
		}
	}
	#endregion // FactTypeRoleInstance serialization
	#region Expression serialization
	partial class Expression : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.MixedTypedAttributes);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == Expression.LanguageDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
			}
			if (domainPropertyInfo.Id == Expression.BodyDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = Expression.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeAttribute(Expression.BodyDomainPropertyId, null);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Body", match);
				Expression.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // Expression serialization
	#region FactTypeDerivationExpression serialization
	partial class FactTypeDerivationExpression : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.ElementInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "DerivationExpression", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
	}
	#endregion // FactTypeDerivationExpression serialization
	#region SubtypeFact serialization
	partial class SubtypeFact : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ElementInfo | ORMCustomSerializedElementSupportedOperations.PropertyInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "SubtypeFact", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == SubtypeFact.IsPrimaryDomainPropertyId)
			{
				if (!(this.IsPrimary))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = SubtypeFact.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("IsPrimary", SubtypeFact.IsPrimaryDomainPropertyId);
				SubtypeFact.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // SubtypeFact serialization
	#region Objectification serialization
	partial class Objectification : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			throw new NotSupportedException();
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				throw new NotSupportedException();
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == Objectification.IsImpliedDomainPropertyId)
			{
				if (!(this.IsImplied))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectificationImpliesFactType.ImpliedFactTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ImpliedByObjectification", null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				return null;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return default(ORMCustomSerializedElementMatch);
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = Objectification.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("IsImplied", Objectification.IsImpliedDomainPropertyId);
				Objectification.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			customSerializedAttributes.TryGetValue(key, out rVal);
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.ShouldSerialize
		/// </summary>
		protected static bool ShouldSerialize()
		{
			return true;
		}
		bool IORMCustomSerializedElement.ShouldSerialize()
		{
			return ShouldSerialize();
		}
	}
	#endregion // Objectification serialization
	#region ReadingOrder serialization
	partial class ReadingOrder : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ReadingOrder.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 2];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 2);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Readings", null, ORMCustomSerializedElementWriteStyle.Element, null, ReadingOrderHasReading.ReadingDomainRoleId);
				ret[1] = new ORMCustomSerializedChildElementInfo(null, "RoleSequence", null, ORMCustomSerializedElementWriteStyle.Element, null, ReadingOrderHasRole.RoleDomainRoleId);
				ReadingOrder.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ReadingOrderHasRole.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Role", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(ReadingOrderHasReading.ReadingDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(ReadingOrderHasRole.RoleDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = ReadingOrder.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					ReadingOrder.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ReadingOrder.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ReadingOrderHasRole.RoleDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleSequence|http://schemas.neumont.edu/ORM/2006-04/ORMCore|Role", match);
				match.InitializeRoles(ReadingOrderHasReading.ReadingDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Readings||", match);
				ReadingOrder.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ReadingOrder serialization
	#region Reading serialization
	partial class Reading : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.MixedTypedAttributes));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == Reading.TextDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, "Data", null, false, ORMCustomSerializedAttributeWriteStyle.Element, null);
			}
			if (domainPropertyInfo.Id == Reading.LanguageDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ReadingHasTooManyRolesError.TooManyRolesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ReadingHasTooFewRolesError.TooFewRolesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = Reading.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeAttribute(Reading.TextDomainPropertyId, null);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Data", match);
				Reading.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // Reading serialization
	#region Role serialization
	partial class Role : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles)));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = Role.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 2];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 2);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "ValueRestriction", null, ORMCustomSerializedElementWriteStyle.Element, null, RoleHasValueConstraint.ValueConstraintDomainRoleId);
				ret[1] = new ORMCustomSerializedChildElementInfo(null, "RoleInstances", null, ORMCustomSerializedElementWriteStyle.Element, null, EntityTypeRoleInstance.ObjectTypeInstanceDomainRoleId, FactTypeRoleInstance.ObjectTypeInstanceDomainRoleId);
				Role.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == Role.IsMandatoryDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, "_IsMandatory", null, true, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (domainPropertyInfo.Id == Role.MultiplicityDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, "_Multiplicity", null, true, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectTypePlaysRole.RolePlayerDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "RolePlayer", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == EntityTypeRoleInstance.ObjectTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "EntityTypeRoleInstance", null, ORMCustomSerializedElementWriteStyle.PrimaryLinkElement, null);
			}
			if (roleId == FactTypeRoleInstance.ObjectTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "FactTypeRoleInstance", null, ORMCustomSerializedElementWriteStyle.PrimaryLinkElement, null);
			}
			if (roleId == RoleProxyHasRole.ProxyDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ConstraintRoleSequenceHasRole.ConstraintRoleSequenceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == ReadingOrderHasRole.ReadingOrderDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == RoleHasRolePlayerRequiredError.RolePlayerRequiredErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(ObjectTypePlaysRole.RolePlayerDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(RoleHasValueConstraint.ValueConstraintDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				domainRole = domainDataDirectory.FindDomainRole(EntityTypeRoleInstance.ObjectTypeInstanceDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(FactTypeRoleInstance.ObjectTypeInstanceDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(RoleProxyHasRole.ProxyDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 3;
				domainRole = domainDataDirectory.FindDomainRole(ConstraintRoleSequenceHasRole.ConstraintRoleSequenceDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 4;
				domainRole = domainDataDirectory.FindDomainRole(ReadingOrderHasRole.ReadingOrderDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 5;
				domainRole = domainDataDirectory.FindDomainRole(RoleHasRolePlayerRequiredError.RolePlayerRequiredErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 6;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = Role.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					Role.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = Role.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectTypePlaysRole.RolePlayerDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|RolePlayer", match);
				match.InitializeRoles(true, EntityTypeRoleInstance.ObjectTypeInstanceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleInstances|http://schemas.neumont.edu/ORM/2006-04/ORMCore|EntityTypeRoleInstance", match);
				match.InitializeRoles(true, FactTypeRoleInstance.ObjectTypeInstanceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleInstances|http://schemas.neumont.edu/ORM/2006-04/ORMCore|FactTypeRoleInstance", match);
				match.InitializeRoles(RoleHasValueConstraint.ValueConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueRestriction||", match);
				Role.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = Role.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("_IsMandatory", Role.IsMandatoryDomainPropertyId);
				customSerializedAttributes.Add("_Multiplicity", Role.MultiplicityDomainPropertyId);
				Role.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // Role serialization
	#region RoleProxy serialization
	partial class RoleProxy : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == RoleProxyHasRole.TargetRoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Role", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == ReadingOrderHasRole.ReadingOrderDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = RoleProxy.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(RoleProxyHasRole.TargetRoleDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Role", match);
				RoleProxy.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // RoleProxy serialization
	#region SetComparisonConstraint serialization
	partial class SetComparisonConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles)));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = SetComparisonConstraint.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "RoleSequences", null, ORMCustomSerializedElementWriteStyle.Element, null, SetComparisonConstraintHasRoleSequence.RoleSequenceDomainRoleId);
				SetComparisonConstraint.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == SetComparisonConstraint.ModalityDomainPropertyId)
			{
				if (ConstraintModality.Alethic == this.Modality)
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasCompatibleRolePlayerTypeError.CompatibleRolePlayerTypeErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetComparisonConstraintHasTooFewRoleSequencesError.TooFewRoleSequencesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetComparisonConstraintHasTooManyRoleSequencesError.TooManyRoleSequencesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetComparisonConstraintHasExternalConstraintRoleSequenceArityMismatchError.ArityMismatchErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetComparisonConstraintHasImplicationError.ImplicationErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetComparisonConstraintHasContradictionError.ContradictionErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasRoleSequence.RoleSequenceDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasCompatibleRolePlayerTypeError.CompatibleRolePlayerTypeErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasTooFewRoleSequencesError.TooFewRoleSequencesErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasTooManyRoleSequencesError.TooManyRoleSequencesErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 3;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasExternalConstraintRoleSequenceArityMismatchError.ArityMismatchErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 4;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasImplicationError.ImplicationErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 5;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasContradictionError.ContradictionErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 6;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = SetComparisonConstraint.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					SetComparisonConstraint.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = SetComparisonConstraint.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasRoleSequence.RoleSequenceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleSequences||", match);
				SetComparisonConstraint.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = SetComparisonConstraint.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("Modality", SetComparisonConstraint.ModalityDomainPropertyId);
				SetComparisonConstraint.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // SetComparisonConstraint serialization
	#region SetComparisonConstraintRoleSequence serialization
	partial class SetComparisonConstraintRoleSequence : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles)));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedElementInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo CustomSerializedElementInfo
		{
			get
			{
				return new ORMCustomSerializedElementInfo(null, "RoleSequence", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.CustomSerializedElementInfo
		{
			get
			{
				return this.CustomSerializedElementInfo;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == SetComparisonConstraintRoleSequence.NameDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ConstraintRoleSequenceHasRole.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Role", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(ConstraintRoleSequenceHasRole.RoleDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = SetComparisonConstraintRoleSequence.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					SetComparisonConstraintRoleSequence.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = SetComparisonConstraintRoleSequence.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ConstraintRoleSequenceHasRole.RoleDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Role", match);
				SetComparisonConstraintRoleSequence.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // SetComparisonConstraintRoleSequence serialization
	#region SetConstraint serialization
	partial class SetConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles)));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = SetConstraint.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "RoleSequence", null, ORMCustomSerializedElementWriteStyle.Element, null, ConstraintRoleSequenceHasRole.RoleDomainRoleId);
				SetConstraint.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == SetConstraint.ModalityDomainPropertyId)
			{
				if (ConstraintModality.Alethic == this.Modality)
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ConstraintRoleSequenceHasRole.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Role", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == SetConstraintHasCompatibleRolePlayerTypeError.CompatibleRolePlayerTypeErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetConstraintHasTooFewRoleSequencesError.TooFewRoleSequencesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetConstraintHasTooManyRoleSequencesError.TooManyRoleSequencesErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FactSetConstraint.FactTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetConstraintHasImplicationError.ImplicationErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(ConstraintRoleSequenceHasRole.RoleDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(SetConstraintHasCompatibleRolePlayerTypeError.CompatibleRolePlayerTypeErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				domainRole = domainDataDirectory.FindDomainRole(SetConstraintHasTooFewRoleSequencesError.TooFewRoleSequencesErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 2;
				domainRole = domainDataDirectory.FindDomainRole(SetConstraintHasTooManyRoleSequencesError.TooManyRoleSequencesErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 3;
				domainRole = domainDataDirectory.FindDomainRole(FactSetConstraint.FactTypeDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 4;
				domainRole = domainDataDirectory.FindDomainRole(SetConstraintHasImplicationError.ImplicationErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 5;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = SetConstraint.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					SetConstraint.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = SetConstraint.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ConstraintRoleSequenceHasRole.RoleDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleSequence|http://schemas.neumont.edu/ORM/2006-04/ORMCore|Role", match);
				SetConstraint.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = SetConstraint.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("Modality", SetConstraint.ModalityDomainPropertyId);
				SetConstraint.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // SetConstraint serialization
	#region MandatoryConstraint serialization
	partial class MandatoryConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == MandatoryConstraint.IsSimpleDomainPropertyId)
			{
				if (!(this.IsSimple))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == MandatoryConstraintHasPopulationMandatoryError.PopulationMandatoryErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = MandatoryConstraint.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("IsSimple", MandatoryConstraint.IsSimpleDomainPropertyId);
				MandatoryConstraint.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // MandatoryConstraint serialization
	#region FrequencyConstraint serialization
	partial class FrequencyConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FrequencyConstraintHasFrequencyConstraintMinMaxError.FrequencyConstraintMinMaxErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == FrequencyConstraintHasFrequencyConstraintInvalidatedByInternalUniquenessConstraintError.FrequencyConstraintContradictsInternalUniquenessConstraintErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
	}
	#endregion // FrequencyConstraint serialization
	#region UniquenessConstraint serialization
	partial class UniquenessConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles));
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == UniquenessConstraint.IsInternalDomainPropertyId)
			{
				if (!(this.IsInternal))
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == EntityTypeHasPreferredIdentifier.PreferredIdentifierForDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "PreferredIdentifierFor", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == UniquenessConstraintHasNMinusOneError.NMinusOneErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(EntityTypeHasPreferredIdentifier.PreferredIdentifierForDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(UniquenessConstraintHasNMinusOneError.NMinusOneErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = UniquenessConstraint.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					UniquenessConstraint.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = UniquenessConstraint.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(EntityTypeHasPreferredIdentifier.PreferredIdentifierForDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|PreferredIdentifierFor", match);
				UniquenessConstraint.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = UniquenessConstraint.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("IsInternal", UniquenessConstraint.IsInternalDomainPropertyId);
				UniquenessConstraint.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // UniquenessConstraint serialization
	#region EqualityConstraint serialization
	partial class EqualityConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == EqualityConstraintHasEqualityImpliedByMandatoryError.EqualityImpliedByMandatoryErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (roleId == SetComparisonConstraintHasExclusionContradictsEqualityError.ExclusionContradictsEqualityErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(EqualityConstraintHasEqualityImpliedByMandatoryError.EqualityImpliedByMandatoryErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasExclusionContradictsEqualityError.ExclusionContradictsEqualityErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 1;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = EqualityConstraint.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					EqualityConstraint.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
	}
	#endregion // EqualityConstraint serialization
	#region ExclusionConstraint serialization
	partial class ExclusionConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.LinkInfo | ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasExclusionContradictsEqualityError.ExclusionContradictsEqualityErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static IComparer<DomainRoleInfo> myCustomSortChildComparer;
		private sealed class CustomSortChildComparer : IComparer<DomainRoleInfo>
		{
			private readonly Dictionary<string, int> myRoleOrderDictionary;
			private IComparer<DomainRoleInfo> myBaseComparer;
			public CustomSortChildComparer(Store store, IComparer<DomainRoleInfo> baseComparer)
			{
				this.myBaseComparer = baseComparer;
				DomainDataDirectory domainDataDirectory = store.DomainDataDirectory;
				Dictionary<string, int> roleOrderDictionary = new Dictionary<string, int>();
				DomainRoleInfo domainRole;
				domainRole = domainDataDirectory.FindDomainRole(SetComparisonConstraintHasExclusionContradictsEqualityError.ExclusionContradictsEqualityErrorDomainRoleId).OppositeDomainRole;
				roleOrderDictionary[string.Concat(domainRole.DomainRelationship.ImplementationClass.FullName, ".", domainRole.Name)] = 0;
				this.myRoleOrderDictionary = roleOrderDictionary;
			}
			int IComparer<DomainRoleInfo>.Compare(DomainRoleInfo x, DomainRoleInfo y)
			{
				if (this.myBaseComparer != null)
				{
					int baseOpinion = this.myBaseComparer.Compare(x, y);
					if (0 != baseOpinion)
					{
						return baseOpinion;
					}
				}
				int xPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(x.DomainRelationship.ImplementationClass.FullName, ".", x.Name), out xPos)))
				{
					xPos = int.MaxValue;
				}
				int yPos;
				if (!(this.myRoleOrderDictionary.TryGetValue(string.Concat(y.DomainRelationship.ImplementationClass.FullName, ".", y.Name), out yPos)))
				{
					yPos = int.MaxValue;
				}
				return xPos.CompareTo(yPos);
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		/// </summary>
		protected new IComparer<DomainRoleInfo> CustomSerializedChildRoleComparer
		{
			get
			{
				IComparer<DomainRoleInfo> retVal = ExclusionConstraint.myCustomSortChildComparer;
				if (null == retVal)
				{
					IComparer<DomainRoleInfo> baseComparer = null;
					if (0 != (ORMCustomSerializedElementSupportedOperations.CustomSortChildRoles & base.SupportedCustomSerializedOperations))
					{
						baseComparer = base.CustomSerializedChildRoleComparer;
					}
					retVal = new CustomSortChildComparer(this.Store, baseComparer);
					ExclusionConstraint.myCustomSortChildComparer = retVal;
				}
				return retVal;
			}
		}
		IComparer<DomainRoleInfo> IORMCustomSerializedElement.CustomSerializedChildRoleComparer
		{
			get
			{
				return this.CustomSerializedChildRoleComparer;
			}
		}
	}
	#endregion // ExclusionConstraint serialization
	#region RingConstraint serialization
	partial class RingConstraint : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == RingConstraint.RingTypeDomainPropertyId)
			{
				return new ORMCustomSerializedPropertyInfo(null, "Type", null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == RingConstraintHasRingConstraintTypeNotSpecifiedError.RingConstraintTypeNotSpecifiedErrorDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, null, null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = RingConstraint.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("Type", RingConstraint.RingTypeDomainPropertyId);
				RingConstraint.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // RingConstraint serialization
	#region ConstraintDuplicateNameError serialization
	partial class ConstraintDuplicateNameError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ConstraintDuplicateNameError.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Constraints", null, ORMCustomSerializedElementWriteStyle.Element, null, SetComparisonConstraintHasDuplicateNameError.SetComparisonConstraintDomainRoleId, SetConstraintHasDuplicateNameError.SetConstraintDomainRoleId, ValueConstraintHasDuplicateNameError.ValueConstraintDomainRoleId);
				ConstraintDuplicateNameError.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasDuplicateNameError.SetComparisonConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetComparisonConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == SetConstraintHasDuplicateNameError.SetConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == ValueConstraintHasDuplicateNameError.ValueConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ValueConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ConstraintDuplicateNameError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasDuplicateNameError.SetComparisonConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Constraints|http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetComparisonConstraint", match);
				match.InitializeRoles(SetConstraintHasDuplicateNameError.SetConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Constraints|http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetConstraint", match);
				match.InitializeRoles(ValueConstraintHasDuplicateNameError.ValueConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Constraints|http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueConstraint", match);
				ConstraintDuplicateNameError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ConstraintDuplicateNameError serialization
	#region ObjectTypeDuplicateNameError serialization
	partial class ObjectTypeDuplicateNameError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ObjectTypeDuplicateNameError.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Objects", null, ORMCustomSerializedElementWriteStyle.Element, null, ObjectTypeHasDuplicateNameError.ObjectTypeDomainRoleId);
				ObjectTypeDuplicateNameError.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectTypeHasDuplicateNameError.ObjectTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Object", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ObjectTypeDuplicateNameError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectTypeHasDuplicateNameError.ObjectTypeDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Objects|http://schemas.neumont.edu/ORM/2006-04/ORMCore|Object", match);
				ObjectTypeDuplicateNameError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ObjectTypeDuplicateNameError serialization
	#region EntityTypeRequiresReferenceSchemeError serialization
	partial class EntityTypeRequiresReferenceSchemeError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectTypeHasEntityTypeRequiresReferenceSchemeError.ObjectTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "EntityType", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = EntityTypeRequiresReferenceSchemeError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectTypeHasEntityTypeRequiresReferenceSchemeError.ObjectTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|EntityType", match);
				EntityTypeRequiresReferenceSchemeError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // EntityTypeRequiresReferenceSchemeError serialization
	#region ExternalConstraintRoleSequenceArityMismatchError serialization
	partial class ExternalConstraintRoleSequenceArityMismatchError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasExternalConstraintRoleSequenceArityMismatchError.ConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Constraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ExternalConstraintRoleSequenceArityMismatchError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasExternalConstraintRoleSequenceArityMismatchError.ConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Constraint", match);
				ExternalConstraintRoleSequenceArityMismatchError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ExternalConstraintRoleSequenceArityMismatchError serialization
	#region ImpliedInternalUniquenessConstraintError serialization
	partial class ImpliedInternalUniquenessConstraintError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactTypeHasImpliedInternalUniquenessConstraintError.FactTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Fact", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ImpliedInternalUniquenessConstraintError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(FactTypeHasImpliedInternalUniquenessConstraintError.FactTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Fact", match);
				ImpliedInternalUniquenessConstraintError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ImpliedInternalUniquenessConstraintError serialization
	#region FactTypeRequiresInternalUniquenessConstraintError serialization
	partial class FactTypeRequiresInternalUniquenessConstraintError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactTypeHasFactTypeRequiresInternalUniquenessConstraintError.FactTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Fact", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = FactTypeRequiresInternalUniquenessConstraintError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(FactTypeHasFactTypeRequiresInternalUniquenessConstraintError.FactTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Fact", match);
				FactTypeRequiresInternalUniquenessConstraintError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // FactTypeRequiresInternalUniquenessConstraintError serialization
	#region FactTypeRequiresReadingError serialization
	partial class FactTypeRequiresReadingError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactTypeHasFactTypeRequiresReadingError.FactTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Fact", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = FactTypeRequiresReadingError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(FactTypeHasFactTypeRequiresReadingError.FactTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Fact", match);
				FactTypeRequiresReadingError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // FactTypeRequiresReadingError serialization
	#region FrequencyConstraintMinMaxError serialization
	partial class FrequencyConstraintMinMaxError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FrequencyConstraintHasFrequencyConstraintMinMaxError.FrequencyConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "FrequencyConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = FrequencyConstraintMinMaxError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(FrequencyConstraintHasFrequencyConstraintMinMaxError.FrequencyConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|FrequencyConstraint", match);
				FrequencyConstraintMinMaxError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // FrequencyConstraintMinMaxError serialization
	#region MinValueMismatchError serialization
	partial class MinValueMismatchError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueRangeHasMinValueMismatchError.ValueRangeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ValueRange", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = MinValueMismatchError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ValueRangeHasMinValueMismatchError.ValueRangeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueRange", match);
				MinValueMismatchError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // MinValueMismatchError serialization
	#region MaxValueMismatchError serialization
	partial class MaxValueMismatchError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueRangeHasMaxValueMismatchError.ValueRangeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ValueRange", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = MaxValueMismatchError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ValueRangeHasMaxValueMismatchError.ValueRangeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueRange", match);
				MaxValueMismatchError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // MaxValueMismatchError serialization
	#region ValueRangeOverlapError serialization
	partial class ValueRangeOverlapError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueConstraintHasValueRangeOverlapError.ValueConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ValueConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ValueRangeOverlapError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ValueConstraintHasValueRangeOverlapError.ValueConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueConstraint", match);
				ValueRangeOverlapError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ValueRangeOverlapError serialization
	#region RingConstraintTypeNotSpecifiedError serialization
	partial class RingConstraintTypeNotSpecifiedError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == RingConstraintHasRingConstraintTypeNotSpecifiedError.RingConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "RingConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = RingConstraintTypeNotSpecifiedError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(RingConstraintHasRingConstraintTypeNotSpecifiedError.RingConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|RingConstraint", match);
				RingConstraintTypeNotSpecifiedError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // RingConstraintTypeNotSpecifiedError serialization
	#region CompatibleValueTypeInstanceValueError serialization
	partial class CompatibleValueTypeInstanceValueError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueTypeInstanceHasCompatibleValueTypeInstanceValueError.ValueTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ValueTypeInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = CompatibleValueTypeInstanceValueError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ValueTypeInstanceHasCompatibleValueTypeInstanceValueError.ValueTypeInstanceDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ValueTypeInstance", match);
				CompatibleValueTypeInstanceValueError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // CompatibleValueTypeInstanceValueError serialization
	#region TooFewEntityTypeRoleInstancesError serialization
	partial class TooFewEntityTypeRoleInstancesError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == EntityTypeInstanceHasTooFewEntityTypeRoleInstancesError.EntityTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "EntityTypeInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = TooFewEntityTypeRoleInstancesError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(EntityTypeInstanceHasTooFewEntityTypeRoleInstancesError.EntityTypeInstanceDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|EntityTypeInstance", match);
				TooFewEntityTypeRoleInstancesError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // TooFewEntityTypeRoleInstancesError serialization
	#region TooFewFactTypeRoleInstancesError serialization
	partial class TooFewFactTypeRoleInstancesError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactTypeInstanceHasTooFewFactTypeRoleInstancesError.FactTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "FactTypeInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = TooFewFactTypeRoleInstancesError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(FactTypeInstanceHasTooFewFactTypeRoleInstancesError.FactTypeInstanceDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|FactTypeInstance", match);
				TooFewFactTypeRoleInstancesError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // TooFewFactTypeRoleInstancesError serialization
	#region PopulationMandatoryError serialization
	partial class PopulationMandatoryError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectTypeInstanceHasPopulationMandatoryError.ObjectTypeInstanceDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ObjectTypeInstance", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == MandatoryConstraintHasPopulationMandatoryError.MandatoryConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "MandatoryConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = PopulationMandatoryError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectTypeInstanceHasPopulationMandatoryError.ObjectTypeInstanceDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ObjectTypeInstance", match);
				match.InitializeRoles(MandatoryConstraintHasPopulationMandatoryError.MandatoryConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|MandatoryConstraint", match);
				PopulationMandatoryError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // PopulationMandatoryError serialization
	#region PopulationUniquenessError serialization
	partial class PopulationUniquenessError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.ChildElementInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = PopulationUniquenessError.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "RoleInstances", null, ORMCustomSerializedElementWriteStyle.Element, null, RoleInstanceHasPopulationUniquenessError.RoleInstanceDomainRoleId);
				PopulationUniquenessError.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = PopulationUniquenessError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(RoleInstanceHasPopulationUniquenessError.RoleInstanceDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|RoleInstances||", match);
				PopulationUniquenessError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // PopulationUniquenessError serialization
	#region TooFewReadingRolesError serialization
	partial class TooFewReadingRolesError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ReadingHasTooFewRolesError.ReadingDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Reading", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = TooFewReadingRolesError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ReadingHasTooFewRolesError.ReadingDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Reading", match);
				TooFewReadingRolesError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // TooFewReadingRolesError serialization
	#region TooFewRoleSequencesError serialization
	partial class TooFewRoleSequencesError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasTooFewRoleSequencesError.SetComparisonConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetComparisonConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == SetConstraintHasTooFewRoleSequencesError.SetConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = TooFewRoleSequencesError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasTooFewRoleSequencesError.SetComparisonConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetComparisonConstraint", match);
				match.InitializeRoles(SetConstraintHasTooFewRoleSequencesError.SetConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetConstraint", match);
				TooFewRoleSequencesError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // TooFewRoleSequencesError serialization
	#region TooManyReadingRolesError serialization
	partial class TooManyReadingRolesError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ReadingHasTooManyRolesError.ReadingDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Reading", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = TooManyReadingRolesError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ReadingHasTooManyRolesError.ReadingDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Reading", match);
				TooManyReadingRolesError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // TooManyReadingRolesError serialization
	#region TooManyRoleSequencesError serialization
	partial class TooManyRoleSequencesError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasTooManyRoleSequencesError.SetComparisonConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetComparisonConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == SetConstraintHasTooManyRoleSequencesError.SetConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = TooManyRoleSequencesError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasTooManyRoleSequencesError.SetComparisonConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetComparisonConstraint", match);
				match.InitializeRoles(SetConstraintHasTooManyRoleSequencesError.SetConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetConstraint", match);
				TooManyRoleSequencesError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // TooManyRoleSequencesError serialization
	#region DataTypeNotSpecifiedError serialization
	partial class DataTypeNotSpecifiedError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ValueTypeHasUnspecifiedDataTypeError.ValueTypeHasDataTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ConceptualDataType", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = DataTypeNotSpecifiedError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ValueTypeHasUnspecifiedDataTypeError.ValueTypeHasDataTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ConceptualDataType", match);
				DataTypeNotSpecifiedError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // DataTypeNotSpecifiedError serialization
	#region EqualityImpliedByMandatoryError serialization
	partial class EqualityImpliedByMandatoryError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == EqualityConstraintHasEqualityImpliedByMandatoryError.EqualityConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "EqualityConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = EqualityImpliedByMandatoryError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(EqualityConstraintHasEqualityImpliedByMandatoryError.EqualityConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|EqualityConstraint", match);
				EqualityImpliedByMandatoryError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // EqualityImpliedByMandatoryError serialization
	#region NMinusOneError serialization
	partial class NMinusOneError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == UniquenessConstraintHasNMinusOneError.ConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "UniquenessConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = NMinusOneError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(UniquenessConstraintHasNMinusOneError.ConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|UniquenessConstraint", match);
				NMinusOneError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // NMinusOneError serialization
	#region ImplicationError serialization
	partial class ImplicationError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasImplicationError.SetComparisonConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetComparisonConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == SetConstraintHasImplicationError.SetConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ImplicationError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasImplicationError.SetComparisonConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetComparisonConstraint", match);
				match.InitializeRoles(SetConstraintHasImplicationError.SetConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetConstraint", match);
				ImplicationError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ImplicationError serialization
	#region ContradictionError serialization
	partial class ContradictionError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasContradictionError.SetComparisonConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetComparisonConstraint", null, ORMCustomSerializedElementWriteStyle.NotWritten, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
	}
	#endregion // ContradictionError serialization
	#region ExclusionContradictsEqualityError serialization
	partial class ExclusionContradictsEqualityError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.ChildElementInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		private static ORMCustomSerializedChildElementInfo[] myCustomSerializedChildElementInfo;
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedChildElementInfo
		/// </summary>
		protected new ORMCustomSerializedChildElementInfo[] GetCustomSerializedChildElementInfo()
		{
			ORMCustomSerializedChildElementInfo[] ret = ExclusionContradictsEqualityError.myCustomSerializedChildElementInfo;
			if (ret == null)
			{
				ORMCustomSerializedChildElementInfo[] baseInfo = null;
				int baseInfoCount = 0;
				if (0 != (ORMCustomSerializedElementSupportedOperations.ChildElementInfo & base.SupportedCustomSerializedOperations))
				{
					baseInfo = base.GetCustomSerializedChildElementInfo();
					if (baseInfo != null)
					{
						baseInfoCount = baseInfo.Length;
					}
				}
				ret = new ORMCustomSerializedChildElementInfo[baseInfoCount + 1];
				if (baseInfoCount != 0)
				{
					baseInfo.CopyTo(ret, 1);
				}
				ret[0] = new ORMCustomSerializedChildElementInfo(null, "Constraints", null, ORMCustomSerializedElementWriteStyle.Element, null, SetComparisonConstraintHasExclusionContradictsEqualityError.SetComparisonConstraintDomainRoleId);
				ExclusionContradictsEqualityError.myCustomSerializedChildElementInfo = ret;
			}
			return ret;
		}
		ORMCustomSerializedChildElementInfo[] IORMCustomSerializedElement.GetCustomSerializedChildElementInfo()
		{
			return this.GetCustomSerializedChildElementInfo();
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasExclusionContradictsEqualityError.SetComparisonConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetComparisonConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ExclusionContradictsEqualityError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasExclusionContradictsEqualityError.SetComparisonConstraintDomainRoleId);
				childElementMappings.Add("http://schemas.neumont.edu/ORM/2006-04/ORMCore|Constraints|http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetComparisonConstraint", match);
				ExclusionContradictsEqualityError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ExclusionContradictsEqualityError serialization
	#region ObjectTypeRequiresPrimarySupertypeError serialization
	partial class ObjectTypeRequiresPrimarySupertypeError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectTypeHasObjectTypeRequiresPrimarySupertypeError.ObjectTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ObjectType", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = ObjectTypeRequiresPrimarySupertypeError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectTypeHasObjectTypeRequiresPrimarySupertypeError.ObjectTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ObjectType", match);
				ObjectTypeRequiresPrimarySupertypeError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // ObjectTypeRequiresPrimarySupertypeError serialization
	#region PreferredIdentifierRequiresMandatoryError serialization
	partial class PreferredIdentifierRequiresMandatoryError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectTypeHasPreferredIdentifierRequiresMandatoryError.ObjectTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ObjectType", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = PreferredIdentifierRequiresMandatoryError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectTypeHasPreferredIdentifierRequiresMandatoryError.ObjectTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ObjectType", match);
				PreferredIdentifierRequiresMandatoryError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // PreferredIdentifierRequiresMandatoryError serialization
	#region CompatibleSupertypesError serialization
	partial class CompatibleSupertypesError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == ObjectTypeHasCompatibleSupertypesError.ObjectTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "ObjectType", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = CompatibleSupertypesError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(ObjectTypeHasCompatibleSupertypesError.ObjectTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|ObjectType", match);
				CompatibleSupertypesError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // CompatibleSupertypesError serialization
	#region CompatibleRolePlayerTypeError serialization
	partial class CompatibleRolePlayerTypeError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | (ORMCustomSerializedElementSupportedOperations.PropertyInfo | ORMCustomSerializedElementSupportedOperations.LinkInfo);
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedPropertyInfo
		/// </summary>
		protected new ORMCustomSerializedPropertyInfo GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			if (domainPropertyInfo.Id == CompatibleRolePlayerTypeError.ColumnDomainPropertyId)
			{
				if (this.SetConstraint != null)
				{
					return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.NotWritten, null);
				}
				return new ORMCustomSerializedPropertyInfo(null, null, null, false, ORMCustomSerializedAttributeWriteStyle.Attribute, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.PropertyInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
			}
			return ORMCustomSerializedPropertyInfo.Default;
		}
		ORMCustomSerializedPropertyInfo IORMCustomSerializedElement.GetCustomSerializedPropertyInfo(DomainPropertyInfo domainPropertyInfo, DomainRoleInfo rolePlayedInfo)
		{
			return this.GetCustomSerializedPropertyInfo(domainPropertyInfo, rolePlayedInfo);
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == SetComparisonConstraintHasCompatibleRolePlayerTypeError.SetComparisonConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetComparisonConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == SetConstraintHasCompatibleRolePlayerTypeError.SetConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "SetConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = CompatibleRolePlayerTypeError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(SetComparisonConstraintHasCompatibleRolePlayerTypeError.SetComparisonConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetComparisonConstraint", match);
				match.InitializeRoles(SetConstraintHasCompatibleRolePlayerTypeError.SetConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|SetConstraint", match);
				CompatibleRolePlayerTypeError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
		private static Dictionary<string, Guid> myCustomSerializedAttributes;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapAttribute
		/// </summary>
		protected new Guid MapAttribute(string xmlNamespace, string attributeName)
		{
			Dictionary<string, Guid> customSerializedAttributes = CompatibleRolePlayerTypeError.myCustomSerializedAttributes;
			if (customSerializedAttributes == null)
			{
				customSerializedAttributes = new Dictionary<string, Guid>();
				customSerializedAttributes.Add("Column", CompatibleRolePlayerTypeError.ColumnDomainPropertyId);
				CompatibleRolePlayerTypeError.myCustomSerializedAttributes = customSerializedAttributes;
			}
			Guid rVal;
			string key = attributeName;
			if (xmlNamespace.Length != 0)
			{
				key = string.Concat(xmlNamespace, "|", attributeName);
			}
			if (!(customSerializedAttributes.TryGetValue(key, out rVal)))
			{
				rVal = base.MapAttribute(xmlNamespace, attributeName);
			}
			return rVal;
		}
		Guid IORMCustomSerializedElement.MapAttribute(string xmlNamespace, string attributeName)
		{
			return this.MapAttribute(xmlNamespace, attributeName);
		}
	}
	#endregion // CompatibleRolePlayerTypeError serialization
	#region RolePlayerRequiredError serialization
	partial class RolePlayerRequiredError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == RoleHasRolePlayerRequiredError.RoleDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Role", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = RolePlayerRequiredError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(RoleHasRolePlayerRequiredError.RoleDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Role", match);
				RolePlayerRequiredError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // RolePlayerRequiredError serialization
	#region FrequencyConstraintContradictsInternalUniquenessConstraintError serialization
	partial class FrequencyConstraintContradictsInternalUniquenessConstraintError : IORMCustomSerializedElement
	{
		/// <summary>
		/// Implements IORMCustomSerializedElement.SupportedCustomSerializedOperations
		/// </summary>
		protected new ORMCustomSerializedElementSupportedOperations SupportedCustomSerializedOperations
		{
			get
			{
				return base.SupportedCustomSerializedOperations | ORMCustomSerializedElementSupportedOperations.LinkInfo;
			}
		}
		ORMCustomSerializedElementSupportedOperations IORMCustomSerializedElement.SupportedCustomSerializedOperations
		{
			get
			{
				return this.SupportedCustomSerializedOperations;
			}
		}
		/// <summary>
		/// Implements IORMCustomSerializedElement.GetCustomSerializedLinkInfo
		/// </summary>
		protected new ORMCustomSerializedElementInfo GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			Guid roleId = rolePlayedInfo.Id;
			if (roleId == FactTypeHasFrequencyConstraintContradictsInternalUniquenessConstraintError.FactTypeDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "Fact", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (roleId == FrequencyConstraintHasFrequencyConstraintInvalidatedByInternalUniquenessConstraintError.FrequencyConstraintDomainRoleId)
			{
				return new ORMCustomSerializedElementInfo(null, "FrequencyConstraint", null, ORMCustomSerializedElementWriteStyle.Element, null);
			}
			if (0 != (ORMCustomSerializedElementSupportedOperations.LinkInfo & base.SupportedCustomSerializedOperations))
			{
				return base.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
			}
			return ORMCustomSerializedElementInfo.Default;
		}
		ORMCustomSerializedElementInfo IORMCustomSerializedElement.GetCustomSerializedLinkInfo(DomainRoleInfo rolePlayedInfo, ElementLink elementLink)
		{
			return this.GetCustomSerializedLinkInfo(rolePlayedInfo, elementLink);
		}
		private static Dictionary<string, ORMCustomSerializedElementMatch> myChildElementMappings;
		/// <summary>
		/// Implements IORMCustomSerializedElement.MapChildElement
		/// </summary>
		protected new ORMCustomSerializedElementMatch MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			Dictionary<string, ORMCustomSerializedElementMatch> childElementMappings = FrequencyConstraintContradictsInternalUniquenessConstraintError.myChildElementMappings;
			if (childElementMappings == null)
			{
				childElementMappings = new Dictionary<string, ORMCustomSerializedElementMatch>();
				ORMCustomSerializedElementMatch match = new ORMCustomSerializedElementMatch();
				match.InitializeRoles(FactTypeHasFrequencyConstraintContradictsInternalUniquenessConstraintError.FactTypeDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|Fact", match);
				match.InitializeRoles(FrequencyConstraintHasFrequencyConstraintInvalidatedByInternalUniquenessConstraintError.FrequencyConstraintDomainRoleId);
				childElementMappings.Add("||http://schemas.neumont.edu/ORM/2006-04/ORMCore|FrequencyConstraint", match);
				FrequencyConstraintContradictsInternalUniquenessConstraintError.myChildElementMappings = childElementMappings;
			}
			ORMCustomSerializedElementMatch rVal;
			if (!(childElementMappings.TryGetValue(string.Concat(containerNamespace, "|", containerName, "|", elementNamespace, "|", elementName), out rVal)))
			{
				rVal = base.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
			}
			return rVal;
		}
		ORMCustomSerializedElementMatch IORMCustomSerializedElement.MapChildElement(string elementNamespace, string elementName, string containerNamespace, string containerName)
		{
			return this.MapChildElement(elementNamespace, elementName, containerNamespace, containerName);
		}
	}
	#endregion // FrequencyConstraintContradictsInternalUniquenessConstraintError serialization
}
