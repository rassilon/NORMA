﻿#region Common Public License Copyright Notice
/**************************************************************************\
* Neumont Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright © Neumont University. All rights reserved.                     *
*                                                                          *
* The use and distribution terms for this software are covered by the      *
* Common Public License 1.0 (http://opensource.org/licenses/cpl) which     *
* can be found in the file CPL.txt at the root of this distribution.       *
* By using this software in any fashion, you are agreeing to be bound by   *
* the terms of this license.                                               *
*                                                                          *
* You must not remove this notice, or any other, from this software.       *
\**************************************************************************/
#endregion
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
namespace Neumont.Tools.ORM.Views.RelationalView
{
	/// <summary>
	/// DomainModel RelationalShapeDomainModel
	/// Relational View of ORM Model
	/// </summary>
	[DslModeling::ExtendsDomainModel("3EAE649F-E654-4D04-8289-C25D2C0322D8"/*Neumont.Tools.ORM.ObjectModel.ORMCoreDomainModel*/)]
	[DslModeling::ExtendsDomainModel("CD96AA55-FCBC-47D0-93F8-30D3DACC5FF7"/*Neumont.Tools.ORM.OIALModel.OIALMetaModel*/)]
	[DslDesign::DisplayNameResource("Neumont.Tools.ORM.Views.RelationalView.RelationalShapeDomainModel.DisplayName", typeof(global::Neumont.Tools.ORM.Views.RelationalView.RelationalShapeDomainModel), "Neumont.Tools.ORM.Views.RelationalView.GeneratedCode.DomainModelResx")]
	[DslDesign::DescriptionResource("Neumont.Tools.ORM.Views.RelationalView.RelationalShapeDomainModel.Description", typeof(global::Neumont.Tools.ORM.Views.RelationalView.RelationalShapeDomainModel), "Neumont.Tools.ORM.Views.RelationalView.GeneratedCode.DomainModelResx")]
	[DslModeling::DomainObjectId("0144a831-92d5-4c42-b7c5-99a5fa9d79df")]
	internal partial class RelationalShapeDomainModel : DslModeling::DomainModel
	{
		#region Constructor, domain model Id
	
		/// <summary>
		/// RelationalShapeDomainModel domain model Id.
		/// </summary>
		public static readonly global::System.Guid DomainModelId = new global::System.Guid(0x0144a831, 0x92d5, 0x4c42, 0xb7, 0xc5, 0x99, 0xa5, 0xfa, 0x9d, 0x79, 0xdf);
	
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="store">Store containing the domain model.</param>
		public RelationalShapeDomainModel(DslModeling::Store store)
			: base(store, DomainModelId)
		{
		}
		
		#endregion
		#region Domain model reflection
			
		/// <summary>
		/// Gets the list of generated domain model types (classes, rules, relationships).
		/// </summary>
		/// <returns>List of types.</returns>
		protected sealed override global::System.Type[] GetGeneratedDomainModelTypes()
		{
			return new global::System.Type[]
			{
				typeof(RelationalNamedElement),
				typeof(RelationalModel),
				typeof(Table),
				typeof(Column),
				typeof(Constraint),
				typeof(ForeignKey),
				typeof(UniquenessConstraint),
				typeof(RelationalModelHasTable),
				typeof(TableHasColumn),
				typeof(TableHasConstraint),
				typeof(ConstraintReferencesColumn),
				typeof(TableReferencesTable),
				typeof(TableReferenceHasForeignKey),
				typeof(TableReferencesConceptType),
				typeof(RelationalModelHasOIALModel),
				typeof(RelationalDiagram),
				typeof(ForeignKeyConnector),
				typeof(TableShape),
				typeof(global::Neumont.Tools.ORM.Views.RelationalView.FixUpDiagram),
				typeof(global::Neumont.Tools.ORM.Views.RelationalView.ConnectorRolePlayerChanged),
				typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemAddRule),
				typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemDeleteRule),
				typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemRolePlayerChangeRule),
				typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemRolePlayerPositionChangeRule),
				typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemChangeRule),
			};
		}
		/// <summary>
		/// Gets the list of generated domain properties.
		/// </summary>
		/// <returns>List of property data.</returns>
		protected sealed override DomainMemberInfo[] GetGeneratedDomainProperties()
		{
			return new DomainMemberInfo[]
			{
				new DomainMemberInfo(typeof(RelationalNamedElement), "Name", RelationalNamedElement.NameDomainPropertyId, typeof(RelationalNamedElement.NamePropertyHandler)),
				new DomainMemberInfo(typeof(RelationalModel), "DisplayDataTypes", RelationalModel.DisplayDataTypesDomainPropertyId, typeof(RelationalModel.DisplayDataTypesPropertyHandler)),
				new DomainMemberInfo(typeof(Column), "IsMandatory", Column.IsMandatoryDomainPropertyId, typeof(Column.IsMandatoryPropertyHandler)),
				new DomainMemberInfo(typeof(Column), "DataType", Column.DataTypeDomainPropertyId, typeof(Column.DataTypePropertyHandler)),
				new DomainMemberInfo(typeof(UniquenessConstraint), "IsPreferred", UniquenessConstraint.IsPreferredDomainPropertyId, typeof(UniquenessConstraint.IsPreferredPropertyHandler)),
			};
		}
		/// <summary>
		/// Gets the list of generated domain roles.
		/// </summary>
		/// <returns>List of role data.</returns>
		protected sealed override DomainRolePlayerInfo[] GetGeneratedDomainRoles()
		{
			return new DomainRolePlayerInfo[]
			{
				new DomainRolePlayerInfo(typeof(RelationalModelHasTable), "RelationalModel", RelationalModelHasTable.RelationalModelDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationalModelHasTable), "Table", RelationalModelHasTable.TableDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableHasColumn), "Table", TableHasColumn.TableDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableHasColumn), "Column", TableHasColumn.ColumnDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableHasConstraint), "Table", TableHasConstraint.TableDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableHasConstraint), "Constraint", TableHasConstraint.ConstraintDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConstraintReferencesColumn), "Constraint", ConstraintReferencesColumn.ConstraintDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConstraintReferencesColumn), "Column", ConstraintReferencesColumn.ColumnDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableReferencesTable), "Table", TableReferencesTable.TableDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableReferencesTable), "ReferencedTable", TableReferencesTable.ReferencedTableDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableReferenceHasForeignKey), "TableReferencesTable", TableReferenceHasForeignKey.TableReferencesTableDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableReferenceHasForeignKey), "ForeignKey", TableReferenceHasForeignKey.ForeignKeyDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableReferencesConceptType), "Table", TableReferencesConceptType.TableDomainRoleId),
				new DomainRolePlayerInfo(typeof(TableReferencesConceptType), "ConceptType", TableReferencesConceptType.ConceptTypeDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationalModelHasOIALModel), "RelationalModel", RelationalModelHasOIALModel.RelationalModelDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationalModelHasOIALModel), "OIALModel", RelationalModelHasOIALModel.OIALModelDomainRoleId),
			};
		}
		#endregion
		#region Factory methods
		private static global::System.Collections.Generic.Dictionary<global::System.Type, int> createElementMap;
	
		/// <summary>
		/// Creates an element of specified type.
		/// </summary>
		/// <param name="partition">Partition where element is to be created.</param>
		/// <param name="elementType">Element type which belongs to this domain model.</param>
		/// <param name="propertyAssignments">New element property assignments.</param>
		/// <returns>Created element.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public sealed override DslModeling::ModelElement CreateElement(DslModeling::Partition partition, global::System.Type elementType, DslModeling::PropertyAssignment[] propertyAssignments)
		{
			if (elementType == null) throw new global::System.ArgumentNullException("elementType");
	
			if (createElementMap == null)
			{
				createElementMap = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(10);
				createElementMap.Add(typeof(RelationalModel), 0);
				createElementMap.Add(typeof(Table), 1);
				createElementMap.Add(typeof(Column), 2);
				createElementMap.Add(typeof(ForeignKey), 3);
				createElementMap.Add(typeof(UniquenessConstraint), 4);
				createElementMap.Add(typeof(RelationalDiagram), 5);
				createElementMap.Add(typeof(ForeignKeyConnector), 6);
				createElementMap.Add(typeof(TableShape), 7);
			}
			int index;
			if (!createElementMap.TryGetValue(elementType, out index))
			{
				throw new global::System.ArgumentException("elementType is not recognized as a type of domain class which belongs to this domain model.");
			}
			switch (index)
			{
				case 0: return new RelationalModel(partition, propertyAssignments);
				case 1: return new Table(partition, propertyAssignments);
				case 2: return new Column(partition, propertyAssignments);
				case 3: return new ForeignKey(partition, propertyAssignments);
				case 4: return new UniquenessConstraint(partition, propertyAssignments);
				case 5: return new RelationalDiagram(partition, propertyAssignments);
				case 6: return new ForeignKeyConnector(partition, propertyAssignments);
				// A constructor was not generated for TableShape because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 7: return new TableShape(partition, propertyAssignments);
				default: return null;
			}
		}
	
		private static global::System.Collections.Generic.Dictionary<global::System.Type, int> createElementLinkMap;
	
		/// <summary>
		/// Creates an element link of specified type.
		/// </summary>
		/// <param name="partition">Partition where element is to be created.</param>
		/// <param name="elementLinkType">Element link type which belongs to this domain model.</param>
		/// <param name="roleAssignments">List of relationship role assignments for the new link.</param>
		/// <param name="propertyAssignments">New element property assignments.</param>
		/// <returns>Created element link.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public sealed override DslModeling::ElementLink CreateElementLink(DslModeling::Partition partition, global::System.Type elementLinkType, DslModeling::RoleAssignment[] roleAssignments, DslModeling::PropertyAssignment[] propertyAssignments)
		{
			if (elementLinkType == null) throw new global::System.ArgumentNullException("elementType");
			if (roleAssignments == null) throw new global::System.ArgumentNullException("roleAssignments");
	
			if (createElementLinkMap == null)
			{
				createElementLinkMap = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(8);
				createElementLinkMap.Add(typeof(RelationalModelHasTable), 0);
				createElementLinkMap.Add(typeof(TableHasColumn), 1);
				createElementLinkMap.Add(typeof(TableHasConstraint), 2);
				createElementLinkMap.Add(typeof(ConstraintReferencesColumn), 3);
				createElementLinkMap.Add(typeof(TableReferencesTable), 4);
				createElementLinkMap.Add(typeof(TableReferenceHasForeignKey), 5);
				createElementLinkMap.Add(typeof(TableReferencesConceptType), 6);
				createElementLinkMap.Add(typeof(RelationalModelHasOIALModel), 7);
			}
			int index;
			if (!createElementLinkMap.TryGetValue(elementLinkType, out index))
			{
				throw new global::System.ArgumentException("elementLinkType is not recognized as a type of domain relationship which belongs to this domain model.");
			}
			switch (index)
			{
				case 0: return new RelationalModelHasTable(partition, roleAssignments, propertyAssignments);
				case 1: return new TableHasColumn(partition, roleAssignments, propertyAssignments);
				case 2: return new TableHasConstraint(partition, roleAssignments, propertyAssignments);
				case 3: return new ConstraintReferencesColumn(partition, roleAssignments, propertyAssignments);
				case 4: return new TableReferencesTable(partition, roleAssignments, propertyAssignments);
				case 5: return new TableReferenceHasForeignKey(partition, roleAssignments, propertyAssignments);
				case 6: return new TableReferencesConceptType(partition, roleAssignments, propertyAssignments);
				case 7: return new RelationalModelHasOIALModel(partition, roleAssignments, propertyAssignments);
				default: return null;
			}
		}
		#endregion
		#region Resource manager
		
		private static global::System.Resources.ResourceManager resourceManager;
		
		/// <summary>
		/// The base name of this model's resources.
		/// </summary>
		public const string ResourceBaseName = "Neumont.Tools.ORM.Views.RelationalView.GeneratedCode.DomainModelResx";
		
		/// <summary>
		/// Gets the DomainModel's ResourceManager. If the ResourceManager does not already exist, then it is created.
		/// </summary>
		public override global::System.Resources.ResourceManager ResourceManager
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return RelationalShapeDomainModel.SingletonResourceManager;
			}
		}
	
		/// <summary>
		/// Gets the Singleton ResourceManager for this domain model.
		/// </summary>
		public static global::System.Resources.ResourceManager SingletonResourceManager
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				if (RelationalShapeDomainModel.resourceManager == null)
				{
					RelationalShapeDomainModel.resourceManager = new global::System.Resources.ResourceManager(ResourceBaseName, typeof(RelationalShapeDomainModel).Assembly);
				}
				return RelationalShapeDomainModel.resourceManager;
			}
		}
		#endregion
		#region Copy/Remove closures
		/// <summary>
		/// CopyClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter copyClosure;
		/// <summary>
		/// DeleteClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter removeClosure;
		/// <summary>
		/// Returns an IElementVisitorFilter that corresponds to the ClosureType.
		/// </summary>
		/// <param name="type">closure type</param>
		/// <param name="rootElements">collection of root elements</param>
		/// <returns>IElementVisitorFilter or null</returns>
		public override DslModeling::IElementVisitorFilter GetClosureFilter(DslModeling::ClosureType type, global::System.Collections.Generic.ICollection<DslModeling::ModelElement> rootElements)
		{
			switch (type)
			{
				case DslModeling::ClosureType.CopyClosure:
					return RelationalShapeDomainModel.CopyClosure;
				case DslModeling::ClosureType.DeleteClosure:
					return RelationalShapeDomainModel.DeleteClosure;
			}
			return base.GetClosureFilter(type, rootElements);
		}
		/// <summary>
		/// CopyClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter CopyClosure
		{
			get
			{
				// Incorporate all of the closures from the models we extend
				if (RelationalShapeDomainModel.copyClosure == null)
				{
					DslModeling::ChainingElementVisitorFilter copyFilter = new DslModeling::ChainingElementVisitorFilter();
					copyFilter.AddFilter(new RelationalShapeCopyClosure());
					copyFilter.AddFilter(new DslDiagrams::CoreDesignSurfaceCopyClosure());
					
					RelationalShapeDomainModel.copyClosure = copyFilter;
				}
				return RelationalShapeDomainModel.copyClosure;
			}
		}
		/// <summary>
		/// DeleteClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter DeleteClosure
		{
			get
			{
				// Incorporate all of the closures from the models we extend
				if (RelationalShapeDomainModel.removeClosure == null)
				{
					DslModeling::ChainingElementVisitorFilter removeFilter = new DslModeling::ChainingElementVisitorFilter();
					removeFilter.AddFilter(new RelationalShapeDeleteClosure());
					removeFilter.AddFilter(new DslDiagrams::CoreDesignSurfaceDeleteClosure());
		
					RelationalShapeDomainModel.removeClosure = removeFilter;
				}
				return RelationalShapeDomainModel.removeClosure;
			}
		}
		#endregion
		#region Diagram rule helpers
		/// <summary>
		/// Enables rules in this domain model related to diagram fixup for the given store.
		/// If diagram data will be loaded into the store, this method should be called first to ensure
		/// that the diagram behaves properly.
		/// </summary>
		public static void EnableDiagramRules(DslModeling::Store store)
		{
			if(store == null) throw new global::System.ArgumentNullException("store");
			
			DslModeling::RuleManager ruleManager = store.RuleManager;
			ruleManager.EnableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.FixUpDiagram));
			ruleManager.EnableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.ConnectorRolePlayerChanged));
			ruleManager.EnableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemAddRule));
			ruleManager.EnableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemDeleteRule));
			ruleManager.EnableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemRolePlayerChangeRule));
			ruleManager.EnableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemRolePlayerPositionChangeRule));
			ruleManager.EnableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemChangeRule));
		}
		
		/// <summary>
		/// Disables rules in this domain model related to diagram fixup for the given store.
		/// </summary>
		public static void DisableDiagramRules(DslModeling::Store store)
		{
			if(store == null) throw new global::System.ArgumentNullException("store");
			
			DslModeling::RuleManager ruleManager = store.RuleManager;
			ruleManager.DisableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.FixUpDiagram));
			ruleManager.DisableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.ConnectorRolePlayerChanged));
			ruleManager.DisableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemAddRule));
			ruleManager.DisableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemDeleteRule));
			ruleManager.DisableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemRolePlayerChangeRule));
			ruleManager.DisableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemRolePlayerPositionChangeRule));
			ruleManager.DisableRule(typeof(global::Neumont.Tools.ORM.Views.RelationalView.CompartmentItemChangeRule));
		}
		#endregion
	}
		
	#region Copy/Remove closure classes
	/// <summary>
	/// Remove closure visitor filter
	/// </summary>
	internal partial class RelationalShapeDeleteClosure : RelationalShapeDeleteClosureBase, DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RelationalShapeDeleteClosure() : base()
		{
		}
	}
	
	/// <summary>
	/// Base class for remove closure visitor filter
	/// </summary>
	internal partial class RelationalShapeDeleteClosureBase : DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// DomainRoles
		/// </summary>
		private global::System.Collections.Generic.Dictionary<global::System.Guid, bool> domainRoles;
		/// <summary>
		/// Constructor
		/// </summary>
		public RelationalShapeDeleteClosureBase()
		{
			#region Initialize DomainData Table
			DomainRoles.Add(global::Neumont.Tools.ORM.Views.RelationalView.RelationalModelHasTable.TableDomainRoleId, true);
			DomainRoles.Add(global::Neumont.Tools.ORM.Views.RelationalView.TableHasColumn.ColumnDomainRoleId, true);
			DomainRoles.Add(global::Neumont.Tools.ORM.Views.RelationalView.TableHasConstraint.ConstraintDomainRoleId, true);
			DomainRoles.Add(global::Neumont.Tools.ORM.Views.RelationalView.TableReferencesTable.TableDomainRoleId, true);
			DomainRoles.Add(global::Neumont.Tools.ORM.Views.RelationalView.TableReferenceHasForeignKey.TableReferencesTableDomainRoleId, true);
			DomainRoles.Add(global::Neumont.Tools.ORM.Views.RelationalView.TableReferencesConceptType.TableDomainRoleId, true);
			DomainRoles.Add(global::Neumont.Tools.ORM.Views.RelationalView.RelationalModelHasOIALModel.RelationalModelDomainRoleId, true);
			#endregion
		}
		/// <summary>
		/// Called to ask the filter if a particular relationship from a source element should be included in the traversal
		/// </summary>
		/// <param name="walker">ElementWalker that is traversing the model</param>
		/// <param name="sourceElement">Model Element playing the source role</param>
		/// <param name="sourceRoleInfo">DomainRoleInfo of the role that the source element is playing in the relationship</param>
		/// <param name="domainRelationshipInfo">DomainRelationshipInfo for the ElementLink in question</param>
		/// <param name="targetRelationship">Relationship in question</param>
		/// <returns>Yes if the relationship should be traversed</returns>
		public virtual DslModeling::VisitorFilterResult ShouldVisitRelationship(DslModeling::ElementWalker walker, DslModeling::ModelElement sourceElement, DslModeling::DomainRoleInfo sourceRoleInfo, DslModeling::DomainRelationshipInfo domainRelationshipInfo, DslModeling::ElementLink targetRelationship)
		{
			return DslModeling::VisitorFilterResult.Yes;
		}
		/// <summary>
		/// Called to ask the filter if a particular role player should be Visited during traversal
		/// </summary>
		/// <param name="walker">ElementWalker that is traversing the model</param>
		/// <param name="sourceElement">Model Element playing the source role</param>
		/// <param name="elementLink">Element Link that forms the relationship to the role player in question</param>
		/// <param name="targetDomainRole">DomainRoleInfo of the target role</param>
		/// <param name="targetRolePlayer">Model Element that plays the target role in the relationship</param>
		/// <returns></returns>
		public virtual DslModeling::VisitorFilterResult ShouldVisitRolePlayer(DslModeling::ElementWalker walker, DslModeling::ModelElement sourceElement, DslModeling::ElementLink elementLink, DslModeling::DomainRoleInfo targetDomainRole, DslModeling::ModelElement targetRolePlayer)
		{
			return this.DomainRoles.ContainsKey(targetDomainRole.Id) ? DslModeling::VisitorFilterResult.Yes : DslModeling::VisitorFilterResult.DoNotCare;
		}
		/// <summary>
		/// DomainRoles
		/// </summary>
		private global::System.Collections.Generic.Dictionary<global::System.Guid, bool> DomainRoles
		{
			get
			{
				if (this.domainRoles == null)
				{
					this.domainRoles = new global::System.Collections.Generic.Dictionary<global::System.Guid, bool>();
				}
				return this.domainRoles;
			}
		}
	
	}
	/// <summary>
	/// Copy closure visitor filter
	/// </summary>
	internal partial class RelationalShapeCopyClosure : RelationalShapeCopyClosureBase, DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RelationalShapeCopyClosure() : base()
		{
		}
	}
	/// <summary>
	/// Base class for copy closure visitor filter
	/// </summary>
	internal partial class RelationalShapeCopyClosureBase : DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// DomainRoles
		/// </summary>
		private global::System.Collections.Generic.Dictionary<global::System.Guid, bool> domainRoles;
		/// <summary>
		/// Constructor
		/// </summary>
		public RelationalShapeCopyClosureBase()
		{
			#region Initialize DomainData Table
			#endregion
		}
		/// <summary>
		/// Called to ask the filter if a particular relationship from a source element should be included in the traversal
		/// </summary>
		/// <param name="walker">ElementWalker traversing the model</param>
		/// <param name="sourceElement">Model Element playing the source role</param>
		/// <param name="sourceRoleInfo">DomainRoleInfo of the role that the source element is playing in the relationship</param>
		/// <param name="domainRelationshipInfo">DomainRelationshipInfo for the ElementLink in question</param>
		/// <param name="targetRelationship">Relationship in question</param>
		/// <returns>Yes if the relationship should be traversed</returns>
		public virtual DslModeling::VisitorFilterResult ShouldVisitRelationship(DslModeling::ElementWalker walker, DslModeling::ModelElement sourceElement, DslModeling::DomainRoleInfo sourceRoleInfo, DslModeling::DomainRelationshipInfo domainRelationshipInfo, DslModeling::ElementLink targetRelationship)
		{
			return this.DomainRoles.ContainsKey(sourceRoleInfo.Id) ? DslModeling::VisitorFilterResult.Yes : DslModeling::VisitorFilterResult.DoNotCare;
		}
		/// <summary>
		/// Called to ask the filter if a particular role player should be Visited during traversal
		/// </summary>
		/// <param name="walker">ElementWalker traversing the model</param>
		/// <param name="sourceElement">Model Element playing the source role</param>
		/// <param name="elementLink">Element Link that forms the relationship to the role player in question</param>
		/// <param name="targetDomainRole">DomainRoleInfo of the target role</param>
		/// <param name="targetRolePlayer">Model Element that plays the target role in the relationship</param>
		/// <returns></returns>
		public virtual DslModeling::VisitorFilterResult ShouldVisitRolePlayer(DslModeling::ElementWalker walker, DslModeling::ModelElement sourceElement, DslModeling::ElementLink elementLink, DslModeling::DomainRoleInfo targetDomainRole, DslModeling::ModelElement targetRolePlayer)
		{
			return this.DomainRoles.ContainsKey(targetDomainRole.Id) ? DslModeling::VisitorFilterResult.Yes : DslModeling::VisitorFilterResult.DoNotCare;
		}
		/// <summary>
		/// DomainRoles
		/// </summary>
		private global::System.Collections.Generic.Dictionary<global::System.Guid, bool> DomainRoles
		{
			get
			{
				if (this.domainRoles == null)
				{
					this.domainRoles = new global::System.Collections.Generic.Dictionary<global::System.Guid, bool>();
				}
				return this.domainRoles;
			}
		}
	
	}
	#endregion
		
}

