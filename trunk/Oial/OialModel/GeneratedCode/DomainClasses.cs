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
namespace Neumont.Tools.Oial
{
	/// <summary>
	/// DomainClass OialModel
	/// </summary>
	[DslDesign::DisplayNameResource("Neumont.Tools.Oial.OialModel.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[DslDesign::DescriptionResource("Neumont.Tools.Oial.OialModel.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[global::System.CLSCompliant(true)]
	[global::System.Diagnostics.DebuggerDisplay("{GetType().Name,nq} (Name = {namePropertyStorage})")]
	[DslModeling::DomainObjectId("9ef66be3-c128-4642-9767-063244de2cef")]
	public sealed partial class OialModel : DslModeling::ModelElement
	{
		#region Constructors, domain class Id
	
		/// <summary>
		/// OialModel domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0x9ef66be3, 0xc128, 0x4642, 0x97, 0x67, 0x06, 0x32, 0x44, 0xde, 0x2c, 0xef);
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="store">Store where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public OialModel(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
			: this(store != null ? store.DefaultPartition : null, propertyAssignments)
		{
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public OialModel(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
		#region Name domain property code
		
		/// <summary>
		/// Name domain property Id.
		/// </summary>
		public static readonly global::System.Guid NameDomainPropertyId = new global::System.Guid(0x49990808, 0xb97e, 0x4a72, 0x8b, 0x26, 0x8a, 0x81, 0x65, 0xcf, 0x4d, 0xf5);
		
		/// <summary>
		/// Storage for Name
		/// </summary>
		private global::System.String namePropertyStorage = string.Empty;
		
		/// <summary>
		/// Gets or sets the value of Name domain property.
		/// Description for Neumont.Tools.Oial.OialModel.Name
		/// </summary>
		[DslDesign::DisplayNameResource("Neumont.Tools.Oial.OialModel/Name.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslDesign::DescriptionResource("Neumont.Tools.Oial.OialModel/Name.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslModeling::ElementName]
		[DslModeling::DomainObjectId("49990808-b97e-4a72-8b26-8a8165cf4df5")]
		public global::System.String Name
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return namePropertyStorage;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				NamePropertyHandler.Instance.SetValue(this, value);
			}
		}
		/// <summary>
		/// Value handler for the OialModel.Name domain property.
		/// </summary>
		internal sealed partial class NamePropertyHandler : DslModeling::DomainPropertyValueHandler<OialModel, global::System.String>
		{
			private NamePropertyHandler() { }
		
			/// <summary>
			/// Gets the singleton instance of the OialModel.Name domain property value handler.
			/// </summary>
			public static readonly NamePropertyHandler Instance = new NamePropertyHandler();
		
			/// <summary>
			/// Gets the Id of the OialModel.Name domain property.
			/// </summary>
			public sealed override global::System.Guid DomainPropertyId
			{
				[global::System.Diagnostics.DebuggerStepThrough]
				get
				{
					return NameDomainPropertyId;
				}
			}
			
			/// <summary>
			/// Gets a strongly-typed value of the property on specified element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <returns>Property value.</returns>
			public override sealed global::System.String GetValue(OialModel element)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
				return element.namePropertyStorage;
			}
		
			/// <summary>
			/// Sets property value on an element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <param name="newValue">New property value.</param>
			public override sealed void SetValue(OialModel element, global::System.String newValue)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
		
				global::System.String oldValue = GetValue(element);
				if (newValue != oldValue)
				{
					ValueChanging(element, oldValue, newValue);
					element.namePropertyStorage = newValue;
					ValueChanged(element, oldValue, newValue);
				}
			}
		}
		
		#endregion
		#region ConceptTypeCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of ConceptTypeCollection.
		/// Description for Neumont.Tools.Oial.OialModelHasConceptType.Model
		/// </summary>
		public DslModeling::LinkedElementCollection<ConceptType> ConceptTypeCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<ConceptType>(this, global::Neumont.Tools.Oial.OialModelHasConceptType.ModelDomainRoleId);
			}
		}
		#endregion
		#region InformationTypeFormatCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of InformationTypeFormatCollection.
		/// Description for Neumont.Tools.Oial.OialModelHasInformationTypeFormat.Model
		/// </summary>
		public DslModeling::LinkedElementCollection<InformationTypeFormat> InformationTypeFormatCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<InformationTypeFormat>(this, global::Neumont.Tools.Oial.OialModelHasInformationTypeFormat.ModelDomainRoleId);
			}
		}
		#endregion
		#region ElementGroupPrototype Merge methods
		/// <summary>
		/// Returns a value indicating whether the source element represented by the
		/// specified root ProtoElement can be added to this element.
		/// </summary>
		/// <param name="rootElement">
		/// The root ProtoElement representing a source element.  This can be null, 
		/// in which case the ElementGroupPrototype does not contain an ProtoElements
		/// and the code should inspect the ElementGroupPrototype context information.
		/// </param>
		/// <param name="elementGroupPrototype">The ElementGroupPrototype that contains the root ProtoElement.</param>
		/// <returns>true if the source element represented by the ProtoElement can be added to this target element.</returns>
		protected override bool CanMerge(DslModeling::ProtoElementBase rootElement, DslModeling::ElementGroupPrototype elementGroupPrototype)
		{
			if ( elementGroupPrototype == null ) throw new global::System.ArgumentNullException("elementGroupPrototype");
			
			if (rootElement != null)
			{
				DslModeling::DomainClassInfo rootElementDomainInfo = this.Partition.DomainDataDirectory.GetDomainClass(rootElement.DomainClassId);
				
				if (rootElementDomainInfo.IsDerivedFrom(global::Neumont.Tools.Oial.InformationTypeFormat.DomainClassId)) 
				{
					return true;
				}
				
				if (rootElementDomainInfo.IsDerivedFrom(global::Neumont.Tools.Oial.ConceptType.DomainClassId)) 
				{
					return true;
				}
			}
			return base.CanMerge(rootElement, elementGroupPrototype);
		}
		
		/// <summary>
		/// Called by the Merge process to create a relationship between 
		/// this target element and the specified source element. 
		/// Typically, a parent-child relationship is established
		/// between the target element (the parent) and the source element 
		/// (the child), but any relationship can be established.
		/// </summary>
		/// <param name="sourceElement">The element that is to be related to this model element.</param>
		/// <param name="elementGroup">The group of source ModelElements that have been rehydrated into the target store.</param>
		/// <remarks>
		/// This method is overriden to create the relationship between the target element and the specified source element.
		/// The base method does nothing.
		/// </remarks>
		protected override void MergeRelate(DslModeling::ModelElement sourceElement, DslModeling::ElementGroup elementGroup)
		{
			// In general, sourceElement is allowed to be null, meaning that the elementGroup must be parsed for special cases.
			// However this is not supported in generated code.  Use double-deriving on this class and then override MergeRelate completely if you 
			// need to support this case.
			if ( sourceElement == null ) throw new global::System.ArgumentNullException("sourceElement");
		
				
			global::Neumont.Tools.Oial.InformationTypeFormat sourceInformationTypeFormat1 = sourceElement as global::Neumont.Tools.Oial.InformationTypeFormat;
			if (sourceInformationTypeFormat1 != null)
			{
				// Create link for path OialModelHasInformationTypeFormat.InformationTypeFormatCollection
				this.InformationTypeFormatCollection.Add(sourceInformationTypeFormat1);

				return;
			}
				
			global::Neumont.Tools.Oial.ConceptType sourceConceptType2 = sourceElement as global::Neumont.Tools.Oial.ConceptType;
			if (sourceConceptType2 != null)
			{
				// Create link for path OialModelHasConceptType.ConceptTypeCollection
				this.ConceptTypeCollection.Add(sourceConceptType2);

				return;
			}
			// Fall through to base class if this class hasn't handled the merge.
			base.MergeRelate(sourceElement, elementGroup);
		}
		
		/// <summary>
		/// Performs operation opposite to MergeRelate - i.e. disconnects a given
		/// element from the current one (removes links created by MergeRelate).
		/// </summary>
		/// <param name="sourceElement">Element to be unmerged/disconnected.</param>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override void MergeDisconnect(DslModeling::ModelElement sourceElement)
		{
			if (sourceElement == null) throw new global::System.ArgumentNullException("sourceElement");
				
			global::Neumont.Tools.Oial.InformationTypeFormat sourceInformationTypeFormat1 = sourceElement as global::Neumont.Tools.Oial.InformationTypeFormat;
			if (sourceInformationTypeFormat1 != null)
			{
				// Delete link for path OialModelHasInformationTypeFormat.InformationTypeFormatCollection
				
				foreach (DslModeling::ElementLink link in global::Neumont.Tools.Oial.OialModelHasInformationTypeFormat.GetLinks((global::Neumont.Tools.Oial.OialModel)this, sourceInformationTypeFormat1))
				{
					// Delete the link, but without possible delete propagation to the element since it's moving to a new location.
					link.Delete(global::Neumont.Tools.Oial.OialModelHasInformationTypeFormat.ModelDomainRoleId, global::Neumont.Tools.Oial.OialModelHasInformationTypeFormat.InformationTypeFormatDomainRoleId);
				}

				return;
			}
				
			global::Neumont.Tools.Oial.ConceptType sourceConceptType2 = sourceElement as global::Neumont.Tools.Oial.ConceptType;
			if (sourceConceptType2 != null)
			{
				// Delete link for path OialModelHasConceptType.ConceptTypeCollection
				
				foreach (DslModeling::ElementLink link in global::Neumont.Tools.Oial.OialModelHasConceptType.GetLinks((global::Neumont.Tools.Oial.OialModel)this, sourceConceptType2))
				{
					// Delete the link, but without possible delete propagation to the element since it's moving to a new location.
					link.Delete(global::Neumont.Tools.Oial.OialModelHasConceptType.ModelDomainRoleId, global::Neumont.Tools.Oial.OialModelHasConceptType.ConceptTypeDomainRoleId);
				}

				return;
			}
			// Fall through to base class if this class hasn't handled the unmerge.
			base.MergeDisconnect(sourceElement);
		}
		#endregion
	}
}
namespace Neumont.Tools.Oial
{
	/// <summary>
	/// DomainClass ConceptType
	/// </summary>
	[DslDesign::DisplayNameResource("Neumont.Tools.Oial.ConceptType.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[DslDesign::DescriptionResource("Neumont.Tools.Oial.ConceptType.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[global::System.CLSCompliant(true)]
	[global::System.Diagnostics.DebuggerDisplay("{GetType().Name,nq} (Name = {namePropertyStorage})")]
	[DslModeling::DomainObjectId("a75df497-8d38-4841-aae3-341fd4ed234b")]
	public sealed partial class ConceptType : DslModeling::ModelElement
	{
		#region Constructors, domain class Id
	
		/// <summary>
		/// ConceptType domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0xa75df497, 0x8d38, 0x4841, 0xaa, 0xe3, 0x34, 0x1f, 0xd4, 0xed, 0x23, 0x4b);
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="store">Store where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public ConceptType(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
			: this(store != null ? store.DefaultPartition : null, propertyAssignments)
		{
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public ConceptType(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
		#region Name domain property code
		
		/// <summary>
		/// Name domain property Id.
		/// </summary>
		public static readonly global::System.Guid NameDomainPropertyId = new global::System.Guid(0x1193b616, 0xac5a, 0x4179, 0x86, 0x2f, 0x88, 0xe9, 0x24, 0x33, 0x10, 0xa4);
		
		/// <summary>
		/// Storage for Name
		/// </summary>
		private global::System.String namePropertyStorage = string.Empty;
		
		/// <summary>
		/// Gets or sets the value of Name domain property.
		/// Description for Neumont.Tools.Oial.ConceptType.Name
		/// </summary>
		[DslDesign::DisplayNameResource("Neumont.Tools.Oial.ConceptType/Name.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslDesign::DescriptionResource("Neumont.Tools.Oial.ConceptType/Name.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslModeling::ElementName]
		[DslModeling::DomainObjectId("1193b616-ac5a-4179-862f-88e9243310a4")]
		public global::System.String Name
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return namePropertyStorage;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				NamePropertyHandler.Instance.SetValue(this, value);
			}
		}
		/// <summary>
		/// Value handler for the ConceptType.Name domain property.
		/// </summary>
		internal sealed partial class NamePropertyHandler : DslModeling::DomainPropertyValueHandler<ConceptType, global::System.String>
		{
			private NamePropertyHandler() { }
		
			/// <summary>
			/// Gets the singleton instance of the ConceptType.Name domain property value handler.
			/// </summary>
			public static readonly NamePropertyHandler Instance = new NamePropertyHandler();
		
			/// <summary>
			/// Gets the Id of the ConceptType.Name domain property.
			/// </summary>
			public sealed override global::System.Guid DomainPropertyId
			{
				[global::System.Diagnostics.DebuggerStepThrough]
				get
				{
					return NameDomainPropertyId;
				}
			}
			
			/// <summary>
			/// Gets a strongly-typed value of the property on specified element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <returns>Property value.</returns>
			public override sealed global::System.String GetValue(ConceptType element)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
				return element.namePropertyStorage;
			}
		
			/// <summary>
			/// Sets property value on an element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <param name="newValue">New property value.</param>
			public override sealed void SetValue(ConceptType element, global::System.String newValue)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
		
				global::System.String oldValue = GetValue(element);
				if (newValue != oldValue)
				{
					ValueChanging(element, oldValue, newValue);
					element.namePropertyStorage = newValue;
					ValueChanged(element, oldValue, newValue);
				}
			}
		}
		
		#endregion
		#region Model opposite domain role accessor
		/// <summary>
		/// Gets or sets Model.
		/// Description for Neumont.Tools.Oial.OialModelHasConceptType.ConceptType
		/// </summary>
		public OialModel Model
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return DslModeling::DomainRoleInfo.GetLinkedElement(this, global::Neumont.Tools.Oial.OialModelHasConceptType.ConceptTypeDomainRoleId) as OialModel;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				DslModeling::DomainRoleInfo.SetLinkedElement(this, global::Neumont.Tools.Oial.OialModelHasConceptType.ConceptTypeDomainRoleId, value);
			}
		}
		#endregion
		#region UniquenessCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of UniquenessCollection.
		/// Description for Neumont.Tools.Oial.ConceptTypeHasUniqueness.ConceptType
		/// </summary>
		public DslModeling::LinkedElementCollection<Uniqueness> UniquenessCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<Uniqueness>(this, global::Neumont.Tools.Oial.ConceptTypeHasUniqueness.ConceptTypeDomainRoleId);
			}
		}
		#endregion
		#region InformationTypeFormatCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of InformationTypeFormatCollection.
		/// Description for Neumont.Tools.Oial.InformationType.ConceptType
		/// </summary>
		public DslModeling::LinkedElementCollection<InformationTypeFormat> InformationTypeFormatCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<InformationTypeFormat>(this, global::Neumont.Tools.Oial.InformationType.ConceptTypeDomainRoleId);
			}
		}
		#endregion
		#region ReferencedConceptTypeCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of ReferencedConceptTypeCollection.
		/// Description for
		/// Neumont.Tools.Oial.ConceptTypeReferencesConceptType.ReferencingConceptType
		/// </summary>
		public DslModeling::ReadOnlyLinkedElementCollection<ConceptType> ReferencedConceptTypeCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::ReadOnlyLinkedElementCollection<ConceptType>(this, global::Neumont.Tools.Oial.ConceptTypeReferencesConceptType.ReferencingConceptTypeDomainRoleId);
			}
		}
		#endregion
		#region RelatedConceptTypeCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of RelatedConceptTypeCollection.
		/// Description for
		/// Neumont.Tools.Oial.ConceptTypeRelatesToConceptType.RelatingConceptType
		/// </summary>
		public DslModeling::LinkedElementCollection<ConceptType> RelatedConceptTypeCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<ConceptType>(this, global::Neumont.Tools.Oial.ConceptTypeRelatesToConceptType.RelatingConceptTypeDomainRoleId);
			}
		}
		#endregion
		#region AssimilatedConceptTypeCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of AssimilatedConceptTypeCollection.
		/// Description for
		/// Neumont.Tools.Oial.ConceptTypeAssimilatesConceptType.AssimilatorConceptType
		/// </summary>
		public DslModeling::LinkedElementCollection<ConceptType> AssimilatedConceptTypeCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<ConceptType>(this, global::Neumont.Tools.Oial.ConceptTypeAssimilatesConceptType.AssimilatorConceptTypeDomainRoleId);
			}
		}
		#endregion
		#region AssimilatorConceptTypeCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of AssimilatorConceptTypeCollection.
		/// Description for
		/// Neumont.Tools.Oial.ConceptTypeAssimilatesConceptType.AssimilatedConceptType
		/// </summary>
		public DslModeling::LinkedElementCollection<ConceptType> AssimilatorConceptTypeCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<ConceptType>(this, global::Neumont.Tools.Oial.ConceptTypeAssimilatesConceptType.AssimilatedConceptTypeDomainRoleId);
			}
		}
		#endregion
	}
}
namespace Neumont.Tools.Oial
{
	/// <summary>
	/// DomainClass InformationTypeFormat
	/// </summary>
	[DslDesign::DisplayNameResource("Neumont.Tools.Oial.InformationTypeFormat.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[DslDesign::DescriptionResource("Neumont.Tools.Oial.InformationTypeFormat.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[global::System.CLSCompliant(true)]
	[global::System.Diagnostics.DebuggerDisplay("{GetType().Name,nq} (Name = {namePropertyStorage})")]
	[DslModeling::DomainObjectId("7eb62327-99a6-4543-be0d-8d4ced8c4f0e")]
	public partial class InformationTypeFormat : DslModeling::ModelElement
	{
		#region Constructors, domain class Id
	
		/// <summary>
		/// InformationTypeFormat domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0x7eb62327, 0x99a6, 0x4543, 0xbe, 0x0d, 0x8d, 0x4c, 0xed, 0x8c, 0x4f, 0x0e);
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="store">Store where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public InformationTypeFormat(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
			: this(store != null ? store.DefaultPartition : null, propertyAssignments)
		{
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public InformationTypeFormat(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
		#region Name domain property code
		
		/// <summary>
		/// Name domain property Id.
		/// </summary>
		public static readonly global::System.Guid NameDomainPropertyId = new global::System.Guid(0xe84be6bf, 0x9799, 0x4c58, 0xb5, 0x8b, 0x88, 0xda, 0x7d, 0xc7, 0x2f, 0xa0);
		
		/// <summary>
		/// Storage for Name
		/// </summary>
		private global::System.String namePropertyStorage = string.Empty;
		
		/// <summary>
		/// Gets or sets the value of Name domain property.
		/// Description for Neumont.Tools.Oial.InformationTypeFormat.Name
		/// </summary>
		[DslDesign::DisplayNameResource("Neumont.Tools.Oial.InformationTypeFormat/Name.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslDesign::DescriptionResource("Neumont.Tools.Oial.InformationTypeFormat/Name.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslModeling::ElementName]
		[DslModeling::DomainObjectId("e84be6bf-9799-4c58-b58b-88da7dc72fa0")]
		public global::System.String Name
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return namePropertyStorage;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				NamePropertyHandler.Instance.SetValue(this, value);
			}
		}
		/// <summary>
		/// Value handler for the InformationTypeFormat.Name domain property.
		/// </summary>
		internal sealed partial class NamePropertyHandler : DslModeling::DomainPropertyValueHandler<InformationTypeFormat, global::System.String>
		{
			private NamePropertyHandler() { }
		
			/// <summary>
			/// Gets the singleton instance of the InformationTypeFormat.Name domain property value handler.
			/// </summary>
			public static readonly NamePropertyHandler Instance = new NamePropertyHandler();
		
			/// <summary>
			/// Gets the Id of the InformationTypeFormat.Name domain property.
			/// </summary>
			public sealed override global::System.Guid DomainPropertyId
			{
				[global::System.Diagnostics.DebuggerStepThrough]
				get
				{
					return NameDomainPropertyId;
				}
			}
			
			/// <summary>
			/// Gets a strongly-typed value of the property on specified element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <returns>Property value.</returns>
			public override sealed global::System.String GetValue(InformationTypeFormat element)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
				return element.namePropertyStorage;
			}
		
			/// <summary>
			/// Sets property value on an element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <param name="newValue">New property value.</param>
			public override sealed void SetValue(InformationTypeFormat element, global::System.String newValue)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
		
				global::System.String oldValue = GetValue(element);
				if (newValue != oldValue)
				{
					ValueChanging(element, oldValue, newValue);
					element.namePropertyStorage = newValue;
					ValueChanged(element, oldValue, newValue);
				}
			}
		}
		
		#endregion
		#region Model opposite domain role accessor
		/// <summary>
		/// Gets or sets Model.
		/// Description for
		/// Neumont.Tools.Oial.OialModelHasInformationTypeFormat.InformationTypeFormat
		/// </summary>
		public OialModel Model
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return DslModeling::DomainRoleInfo.GetLinkedElement(this, global::Neumont.Tools.Oial.OialModelHasInformationTypeFormat.InformationTypeFormatDomainRoleId) as OialModel;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				DslModeling::DomainRoleInfo.SetLinkedElement(this, global::Neumont.Tools.Oial.OialModelHasInformationTypeFormat.InformationTypeFormatDomainRoleId, value);
			}
		}
		#endregion
		#region ConceptTypeCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of ConceptTypeCollection.
		/// Description for Neumont.Tools.Oial.InformationType.InformationTypeFormat
		/// </summary>
		public DslModeling::LinkedElementCollection<ConceptType> ConceptTypeCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<ConceptType>(this, global::Neumont.Tools.Oial.InformationType.InformationTypeFormatDomainRoleId);
			}
		}
		#endregion
	}
}
namespace Neumont.Tools.Oial
{
	/// <summary>
	/// DomainClass Uniqueness
	/// </summary>
	[DslDesign::DisplayNameResource("Neumont.Tools.Oial.Uniqueness.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[DslDesign::DescriptionResource("Neumont.Tools.Oial.Uniqueness.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
	[global::System.CLSCompliant(true)]
	[global::System.Diagnostics.DebuggerDisplay("{GetType().Name,nq} (Name = {namePropertyStorage})")]
	[DslModeling::DomainObjectId("0af67f1f-66d6-4c2b-b85c-d556894ac300")]
	public sealed partial class Uniqueness : DslModeling::ModelElement
	{
		#region Constructors, domain class Id
	
		/// <summary>
		/// Uniqueness domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0x0af67f1f, 0x66d6, 0x4c2b, 0xb8, 0x5c, 0xd5, 0x56, 0x89, 0x4a, 0xc3, 0x00);
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="store">Store where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public Uniqueness(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
			: this(store != null ? store.DefaultPartition : null, propertyAssignments)
		{
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		public Uniqueness(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
		#region Name domain property code
		
		/// <summary>
		/// Name domain property Id.
		/// </summary>
		public static readonly global::System.Guid NameDomainPropertyId = new global::System.Guid(0x82013a44, 0x5bae, 0x43af, 0x84, 0x72, 0xa5, 0x34, 0x73, 0xad, 0xac, 0x7e);
		
		/// <summary>
		/// Storage for Name
		/// </summary>
		private global::System.String namePropertyStorage = string.Empty;
		
		/// <summary>
		/// Gets or sets the value of Name domain property.
		/// Description for Neumont.Tools.Oial.Uniqueness.Name
		/// </summary>
		[DslDesign::DisplayNameResource("Neumont.Tools.Oial.Uniqueness/Name.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslDesign::DescriptionResource("Neumont.Tools.Oial.Uniqueness/Name.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslModeling::ElementName]
		[DslModeling::DomainObjectId("82013a44-5bae-43af-8472-a53473adac7e")]
		public global::System.String Name
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return namePropertyStorage;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				NamePropertyHandler.Instance.SetValue(this, value);
			}
		}
		/// <summary>
		/// Value handler for the Uniqueness.Name domain property.
		/// </summary>
		internal sealed partial class NamePropertyHandler : DslModeling::DomainPropertyValueHandler<Uniqueness, global::System.String>
		{
			private NamePropertyHandler() { }
		
			/// <summary>
			/// Gets the singleton instance of the Uniqueness.Name domain property value handler.
			/// </summary>
			public static readonly NamePropertyHandler Instance = new NamePropertyHandler();
		
			/// <summary>
			/// Gets the Id of the Uniqueness.Name domain property.
			/// </summary>
			public sealed override global::System.Guid DomainPropertyId
			{
				[global::System.Diagnostics.DebuggerStepThrough]
				get
				{
					return NameDomainPropertyId;
				}
			}
			
			/// <summary>
			/// Gets a strongly-typed value of the property on specified element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <returns>Property value.</returns>
			public override sealed global::System.String GetValue(Uniqueness element)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
				return element.namePropertyStorage;
			}
		
			/// <summary>
			/// Sets property value on an element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <param name="newValue">New property value.</param>
			public override sealed void SetValue(Uniqueness element, global::System.String newValue)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
		
				global::System.String oldValue = GetValue(element);
				if (newValue != oldValue)
				{
					ValueChanging(element, oldValue, newValue);
					element.namePropertyStorage = newValue;
					ValueChanged(element, oldValue, newValue);
				}
			}
		}
		
		#endregion
		#region IsPreferred domain property code
		
		/// <summary>
		/// IsPreferred domain property Id.
		/// </summary>
		public static readonly global::System.Guid IsPreferredDomainPropertyId = new global::System.Guid(0xfeef8b5e, 0x13bc, 0x4c0b, 0x8b, 0xd4, 0xff, 0xfc, 0x4a, 0x37, 0x64, 0xc0);
		
		/// <summary>
		/// Storage for IsPreferred
		/// </summary>
		private global::System.Boolean isPreferredPropertyStorage;
		
		/// <summary>
		/// Gets or sets the value of IsPreferred domain property.
		/// Description for Neumont.Tools.Oial.Uniqueness.IsPreferred
		/// </summary>
		[DslDesign::DisplayNameResource("Neumont.Tools.Oial.Uniqueness/IsPreferred.DisplayName", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslDesign::DescriptionResource("Neumont.Tools.Oial.Uniqueness/IsPreferred.Description", typeof(global::Neumont.Tools.Oial.OialDomainModel), "Neumont.Tools.Oial.GeneratedCode.OialDomainModelResx")]
		[DslModeling::DomainObjectId("feef8b5e-13bc-4c0b-8bd4-fffc4a3764c0")]
		public global::System.Boolean IsPreferred
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return isPreferredPropertyStorage;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				IsPreferredPropertyHandler.Instance.SetValue(this, value);
			}
		}
		/// <summary>
		/// Value handler for the Uniqueness.IsPreferred domain property.
		/// </summary>
		internal sealed partial class IsPreferredPropertyHandler : DslModeling::DomainPropertyValueHandler<Uniqueness, global::System.Boolean>
		{
			private IsPreferredPropertyHandler() { }
		
			/// <summary>
			/// Gets the singleton instance of the Uniqueness.IsPreferred domain property value handler.
			/// </summary>
			public static readonly IsPreferredPropertyHandler Instance = new IsPreferredPropertyHandler();
		
			/// <summary>
			/// Gets the Id of the Uniqueness.IsPreferred domain property.
			/// </summary>
			public sealed override global::System.Guid DomainPropertyId
			{
				[global::System.Diagnostics.DebuggerStepThrough]
				get
				{
					return IsPreferredDomainPropertyId;
				}
			}
			
			/// <summary>
			/// Gets a strongly-typed value of the property on specified element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <returns>Property value.</returns>
			public override sealed global::System.Boolean GetValue(Uniqueness element)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
				return element.isPreferredPropertyStorage;
			}
		
			/// <summary>
			/// Sets property value on an element.
			/// </summary>
			/// <param name="element">Element which owns the property.</param>
			/// <param name="newValue">New property value.</param>
			public override sealed void SetValue(Uniqueness element, global::System.Boolean newValue)
			{
				if (element == null) throw new global::System.ArgumentNullException("element");
		
				global::System.Boolean oldValue = GetValue(element);
				if (newValue != oldValue)
				{
					ValueChanging(element, oldValue, newValue);
					element.isPreferredPropertyStorage = newValue;
					ValueChanged(element, oldValue, newValue);
				}
			}
		}
		
		#endregion
		#region ConceptType opposite domain role accessor
		/// <summary>
		/// Gets or sets ConceptType.
		/// Description for Neumont.Tools.Oial.ConceptTypeHasUniqueness.Uniqueness
		/// </summary>
		public ConceptType ConceptType
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return DslModeling::DomainRoleInfo.GetLinkedElement(this, global::Neumont.Tools.Oial.ConceptTypeHasUniqueness.UniquenessDomainRoleId) as ConceptType;
			}
			[global::System.Diagnostics.DebuggerStepThrough]
			set
			{
				DslModeling::DomainRoleInfo.SetLinkedElement(this, global::Neumont.Tools.Oial.ConceptTypeHasUniqueness.UniquenessDomainRoleId, value);
			}
		}
		#endregion
		#region ConceptTypeChildCollection opposite domain role accessor
		/// <summary>
		/// Gets a list of ConceptTypeChildCollection.
		/// Description for Neumont.Tools.Oial.UniquenessIncludesConceptTypeChild.Uniqueness
		/// </summary>
		public DslModeling::LinkedElementCollection<ConceptTypeChild> ConceptTypeChildCollection
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return new DslModeling::LinkedElementCollection<ConceptTypeChild>(this, global::Neumont.Tools.Oial.UniquenessIncludesConceptTypeChild.UniquenessDomainRoleId);
			}
		}
		#endregion
	}
}
