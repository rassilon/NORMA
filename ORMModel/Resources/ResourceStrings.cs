using System;
using System.Diagnostics;
using Northface.Tools.ORM.ObjectModel;
using Northface.Tools.ORM.ShapeModel;
using System.Resources;
namespace Northface.Tools.ORM
{
	/// <summary>
	/// A constant list of strings corresponding to resource identifiers
	/// in the resource files for all models. Any resource id referenced
	/// directly in non-spit code should be duplicated here.
	/// </summary>
	internal static class ResourceStrings
	{
		#region Supported Resource Managers
		/// <summary>
		/// Recognized resource managers
		/// </summary>
		private enum ResourceManagers
		{
			/// <summary>
			/// IMS-managed resource file for the core object model
			/// </summary>
			ObjectModel,
			/// <summary>
			/// IMS-managed resource file for the shape object model
			/// </summary>
			ShapeModel,
			/// <summary>
			/// Standalone resource file for the core model
			/// </summary>
			Model,
			/// <summary>
			/// Standalone resource file for the diagram
			/// </summary>
			Diagram,
		}
		#endregion // Supported Resource Managers
		#region Non-IMS ResourceManagers
		private static object myLockObject;
		private static object LockObject
		{
			get
			{
				if (myLockObject == null)
				{
					System.Threading.Interlocked.CompareExchange(ref myLockObject, new object(), null);
				}
				return myLockObject;
			}
		}
		private static ResourceManager myDiagramResourceManager;
		private static ResourceManager DiagramResourceManager
		{
			get
			{
				if (myDiagramResourceManager == null)
				{
					lock (LockObject)
					{
						if (myDiagramResourceManager == null)
						{
							myDiagramResourceManager = LoadResourceManagerForType(typeof(ORMDiagram));
						}
					}
				}
				return myDiagramResourceManager;
			}
		}
		private static ResourceManager LoadResourceManagerForType(Type type)
		{
			return new ResourceManager(type.FullName, type.Assembly);
		}

		private static ResourceManager myModelResourceManager;
		private static ResourceManager ModelResourceManager
		{
			get
			{
				if (myModelResourceManager == null)
				{
					lock (LockObject)
					{
						if (myModelResourceManager == null)
						{
							myModelResourceManager = LoadResourceManagerForType(typeof(ORMModel));
						}
					}
				}
				return myModelResourceManager;
			}
		}
		#endregion // Non-IMS ResourceManagers
		#region Helper functions
		private static string GetString(ResourceManagers manager, string resourceName)
		{
			ResourceManager resMgr = null;
			string retVal = null;
			switch (manager)
			{
				case ResourceManagers.ObjectModel:
					resMgr = ORMMetaModel.SingletonResourceManager;
					break;
				case ResourceManagers.ShapeModel:
					resMgr = ORMShapeModel.SingletonResourceManager;
					break;
				case ResourceManagers.Diagram:
					resMgr = DiagramResourceManager;
					break;
				case ResourceManagers.Model:
					resMgr = ModelResourceManager;
					break;
			}
			if (resMgr != null)
			{
				retVal = resMgr.GetString(resourceName);
			}
			Debug.Assert(retVal != null && retVal.Length > 0, "Unrecognized resource string: " + resourceName);
			return (retVal != null) ? retVal : String.Empty;
		}
		#endregion // Helper functions
		#region Public resource ids
		/// <summary>
		/// The identifier for the EntityType toolbox item
		/// </summary>
		public const string ToolboxEntityTypeItemId = "Toolbox.EntityType.Item.Id";
		/// <summary>
		/// The identifier for the ValueType toolbox item
		/// </summary>
		public const string ToolboxValueTypeItemId = "Toolbox.ValueType.Item.Id";
		/// <summary>
		/// The identifier for the UnaryFactType toolbox item
		/// </summary>
		public const string ToolboxUnaryFactTypeItemId = "Toolbox.UnaryFactType.Item.Id";
		/// <summary>
		/// The identifier for the BinaryFactType toolbox item
		/// </summary>
		public const string ToolboxBinaryFactTypeItemId = "Toolbox.BinaryFactType.Item.Id";
		/// <summary>
		/// The identifier for the TernaryFactType toolbox item
		/// </summary>
		public const string ToolboxTernaryFactTypeItemId = "Toolbox.TernaryFactType.Item.Id";
		/// <summary>
		/// The identifier for an ExternalUniquenessConstraint toolbox item
		/// </summary>
		public const string ToolboxExternalUniquenessConstraintItemId = "Toolbox.ExternalUniquenessConstraint.Item.Id";
		/// <summary>
		/// The identifier for an PreferredExternalUniquenessConstraint toolbox item
		/// </summary>
		public const string ToolboxPreferredExternalUniquenessConstraintItemId = "Toolbox.PreferredExternalUniquenessConstraint.Item.Id";
		/// <summary>
		/// The identifier for an ExclusionConstraint toolbox item
		/// </summary>
		public const string ToolboxExclusionConstraintItemId = "Toolbox.ExclusionConstraint.Item.Id";
		/// <summary>
		/// The identifier for an InclusiveOrConstraint toolbox item
		/// </summary>
		public const string ToolboxInclusiveOrConstraintItemId = "Toolbox.InclusiveOrConstraint.Item.Id";
		/// <summary>
		/// The identifier for an ExclusiveOrConstraint toolbox item
		/// </summary>
		public const string ToolboxExclusiveOrConstraintItemId = "Toolbox.ExclusiveOrConstraint.Item.Id";
		/// <summary>
		/// The identifier for an SubsetConstraint toolbox item
		/// </summary>
		public const string ToolboxSubsetConstraintItemId = "Toolbox.SubsetConstraint.Item.Id";
		/// <summary>
		/// The identifier for an EqualityConstraint toolbox item
		/// </summary>
		public const string ToolboxEqualityConstraintItemId = "Toolbox.EqualityConstraint.Item.Id";
		/// <summary>
		/// The identifier for an ExternalConstraintConnector toolbox item
		/// </summary>
		public const string ToolboxExternalConstraintConnectorItemId = "Toolbox.ExternalConstraintConnector.Item.Id";
		/// <summary>
		/// Category name for options page
		/// </summary>
		public const string OptionsPageCategoryAppearanceId = "OptionsPage.Category.Appearance";
		/// <summary>
		/// Description of the object type shape
		/// </summary>
		public const string OptionsPagePropertyObjectTypeShapeDescriptionId = "OptionsPage.Property.ObjectTypeShape.Description";
		/// <summary>
		/// Display name of the object type shape
		/// </summary>
		public const string OptionsPagePropertyObjectTypeShapeDisplayNameId = "OptionsPage.Property.ObjectTypeShape.DisplayName";
		/// <summary>
		/// Description of the objectified fact shape
		/// </summary>
		public const string OptionsPagePropertyObjectifiedShapeDescriptionId = "OptionsPage.Property.ObjectifiedShape.Description";
		/// <summary>
		/// Display name of the objectified fact shape
		/// </summary>
		public const string OptionsPagePropertyObjectifiedShapeDisplayNameId = "OptionsPage.Property.ObjectifiedShape.DisplayName";
		/// <summary>
		/// Description of the Mandatory Dot placement
		/// </summary>
		public const string OptionsPagePropertyMandatoryDotDescriptionId = "OptionsPage.Property.MandatoryDot.Description";
		/// <summary>
		/// Display name of the Mandatory Dot placement
		/// </summary>
		public const string OptionsPagePropertyMandatoryDotDisplayNameId = "OptionsPage.Property.MandatoryDot.DisplayName";
		#endregion // Public resource ids
		#region Private resource ids
		private const string ToolboxDefaultTabName_Id = "Toolbox.DefaultTabName";
		private const string ValueType_Id = "Northface.Tools.ORM.ObjectModel.ValueType";
		private const string EntityType_Id = "Northface.Tools.ORM.ObjectModel.EntityType";
		private const string FactType_Id = "Northface.Tools.ORM.ObjectModel.FactType";
		private const string ObjectifiedFactType_Id = "Northface.Tools.ORM.ObjectModel.ObjectifiedFactType";
		private const string RolePlayerPickerNullItemText_Id = "Northface.Tools.ORM.ObjectModel.Editors.RolePlayerPicker.NullItemText";
		private const string NestedFactTypePickerNullItemText_Id = "Northface.Tools.ORM.ObjectModel.Editors.NestedFactTypePicker.NullItemText";
		private const string NestingTypePickerNullItemText_Id = "Northface.Tools.ORM.ObjectModel.Editors.NestedFactTypePicker.NullItemText";
		private const string RoleDefaultNamePattern_Id = "Northface.Tools.ORM.ObjectModel.Role.DefaultNamePattern";
		private const string ExternalConstraintConnectActionInstructions_Id = "ExternalConstraintConnectAction.Instructions";
		private const string ModelBrowserWindowTitle_Id = "ORMModelBrowser.WindowTitle";
		private const string ModelExceptionReadingIsPrimaryToFalse_Id = "ModelException.Reading.IsPrimary.ReadOnlyWhenFalse";
		private const string ModelExceptionReadingTextChangeInvalid_Id = "ModelException.Reading.Text.InvalidText";
		private const string ModelExceptionFactAddReadingInvalidReadingText_Id = "ModelException.Fact.AddReading.InvalidReadingText";
		private const string CommandDeleteFactTypeText_Id = "Command.DeleteFactType.Text";
		private const string CommandDeleteObjectTypeText_Id = "Command.DeleteObjectType.Text";
		private const string ModelErrorConstraintHasTooFewRoleSetsText_Id = "ModelError.Constraint.TooFewRoleSets.Text";
		private const string ModelErrorConstraintHasTooManyRoleSetsText_Id = "ModelError.Constraint.TooManyRoleSets.Text";
		#endregion // Private resource ids
		#region Public accessor properties
		/// <summary>
		/// The tab name for default toolbox
		/// </summary>
		public static string ToolboxDefaultTabName
		{
			get
			{
				return GetString(ResourceManagers.ShapeModel, ToolboxDefaultTabName_Id);
			}
		}
		/// <summary>
		/// The display name used for an ObjectType when IsValueType is false
		/// </summary>
		public static string ValueType
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, ValueType_Id);
			}
		}
		/// <summary>
		/// The display name used for an ObjectType when IsValueType is true
		/// </summary>
		public static string EntityType
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, EntityType_Id);
			}
		}
		/// <summary>
		/// The display name used for a simple FactType
		/// </summary>
		public static string FactType
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, FactType_Id);
			}
		}
		/// <summary>
		/// The display name used for an objectified (nested) FactType
		/// </summary>
		public static string ObjectifiedFactType
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, ObjectifiedFactType_Id);
			}
		}
		/// <summary>
		/// The name displayed to represent null in the role player picker
		/// </summary>
		public static string RolePlayerPickerNullItemText
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, RolePlayerPickerNullItemText_Id);
			}
		}
		/// <summary>
		/// The name displayed to represent null in the nested fact type picker
		/// </summary>
		public static string NestedFactTypePickerNullItemText
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, NestedFactTypePickerNullItemText_Id);
			}
		}
		/// <summary>
		/// The name displayed to represent null in the nesting type picker
		/// </summary>
		public static string NestingTypePickerNullItemText
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, NestingTypePickerNullItemText_Id);
			}
		}
		/// <summary>
		/// The base name used to create a name for a new role. This is a format string,
		/// with {0} being the placeholder for the number placement.
		/// </summary>
		public static string RoleDefaultNamePattern
		{
			get
			{
				return GetString(ResourceManagers.ObjectModel, RoleDefaultNamePattern_Id);
			}
		}
		/// <summary>
		/// The instructions shown when creating an external constraint
		/// </summary>
		public static string ExternalConstraintConnectActionInstructions
		{
			get
			{
				return GetString(ResourceManagers.Diagram, ExternalConstraintConnectActionInstructions_Id);
			}
		}
		/// <summary>
		/// The window title for the model browser tool window
		/// </summary>
		public static string ModelBrowserWindowTitle
		{
			get
			{
				return GetString(ResourceManagers.Diagram, ModelBrowserWindowTitle_Id);
			}
		}
		/// <summary>
		/// The category name to display on the options pages
		/// </summary>
		public static string GetOptionsPageString(string resourceName)
		{
			return GetString(ResourceManagers.Diagram, resourceName);
		}
		/// <summary>
		/// The error message to return when an attempt is made to change the IsPrimary property of a 
		/// reading to false.
		/// </summary>
		public static string ModelExceptionReadingIsPrimaryToFalse
		{
			get
			{
				return GetString(ResourceManagers.Model, ModelExceptionReadingIsPrimaryToFalse_Id);
			}
		}
		/// <summary>
		/// The error message that is returned when attempting to add a new reading to a fact
		/// and the text is not valid.
		/// 
		/// It needs to have a number of place holders equal to the number of roles in the fact
		/// and they need to have their positions identified by number using the replacement
		/// syntax of String.Format. For example: "{0} has {1}"
		/// </summary>
		public static string ModelExceptionFactAddReadingInvalidReadingText
		{
			get
			{
				return GetString(ResourceManagers.Model, ModelExceptionFactAddReadingInvalidReadingText_Id);
			}
		}
		/// <summary>
		/// Error message thrown when the Text of a reading is changed to something that is
		/// not valid for the current state of the Reading.
		/// </summary>
		public static string ModelExceptionReadingTextChangeInvalid
		{
			get
			{
				return GetString(ResourceManagers.Model, ModelExceptionReadingTextChangeInvalid_Id);
			}
		}
		/// <summary>
		/// Model validation error shown when too few role sets are defined
		/// for a constraint. This is a frequent occurrence as external constraints
		/// are easily created in this state.
		/// </summary>
		public static string ModelErrorConstraintHasTooFewRoleSetsText
		{
			get
			{
				return GetString(ResourceManagers.Model, ModelErrorConstraintHasTooFewRoleSetsText_Id);
			}
		}
		/// <summary>
		/// Model validation error shown when too many role sets are defined
		/// for a constraint. This is an infrequent occurrence which should not
		/// be attainable via the UI, but should be possible with a hand edit
		/// of the model file.
		/// </summary>
		public static string ModelErrorConstraintHasTooManyRoleSetsText
		{
			get
			{
				return GetString(ResourceManagers.Model, ModelErrorConstraintHasTooManyRoleSetsText_Id);
			}
		}
		/// <summary>
		/// This text appears in the edit menu when fact types are selected in the diagram.
		/// </summary>
		public static string CommandDeleteFactTypeText
		{
			get
			{
				return GetString(ResourceManagers.Diagram, CommandDeleteFactTypeText_Id);
			}
		}
		/// <summary>
		/// This text appears in the edit menu when object types are selected in the diagram.
		/// </summary>
		public static string CommandDeleteObjectTypeText
		{
			get
			{
				return GetString(ResourceManagers.Diagram, CommandDeleteObjectTypeText_Id);
			}
		}
		#endregion // Public accessor properties
	}
}
