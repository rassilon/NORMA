using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.EnterpriseTools.Shell;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.ArtifactMapper;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Northface.Tools.ORM.ObjectModel;
using Northface.Tools.ORM.ShapeModel;
#if ATTACHELEMENTPROVIDERS
using Northface.Tools.ORM.DocumentSynchronization;
#endif // ATTACHELEMENTPROVIDERS
namespace Northface.Tools.ORM.Shell
{
	/// <summary>
	/// DocData object for the ORM Designer editor
	/// </summary>
	public partial class ORMDesignerDocData : ModelingDocData
	{
		#region Member variables
		#endregion // Member variables
		#region Construction/destruction
		/// <summary>
		/// Standard DocData constructor, called by the editor factory
		/// </summary>
		/// <param name="serviceProvider">IServiceProvider</param>
		/// <param name="editorFactory">EditorFactory</param>
		public ORMDesignerDocData(IServiceProvider serviceProvider, EditorFactory editorFactory) : base(serviceProvider, editorFactory)
		{
		}
		#endregion // Construction/destruction
		#region Base overrides
		/// <summary>
		/// UNDONE: Temporary code for load failure debugging. Remove.
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="isReload"></param>
		/// <returns></returns>
		protected override int LoadDocData(string fileName, bool isReload)
		{
			int retVal;
			try
			{
				retVal = base.LoadDocData(fileName, isReload);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.StackTrace.ToString(), ex.Message);
				throw;
			}
			return retVal;
		}
		/// <summary>
		/// Return array of types of the substores used by the designer
		/// </summary>
		/// <returns></returns>
		protected override System.Type[] GetSubStores(object storeKey)
		{
			if (storeKey == PrimaryStoreKey)
			{
				return new System.Type[] { typeof(Microsoft.VisualStudio.Modeling.Diagrams.CoreDesignSurface),
									   typeof(ORMMetaModel),
									   typeof(ORMShapeModel)};
			}
			return null;
		}
		/// <summary>
		/// Load a file
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="isReload"></param>
		protected override void Load(string fileName, bool isReload)
		{
			if (fileName == null)
				return;

			Store store = this.Store;
			Diagram diagram = null;
			if (fileName.EndsWith(@"\default.orm", true, CultureInfo.CurrentCulture))
			{
				#region Generate Test Object Model
				ORMModel model = ORMModel.CreateORMModel(store);
				model.Name = "Model1";

				// Create a ValueType
				ObjectType valType = ObjectType.CreateObjectType(store);
				valType.Name = "ValueType1";
				valType.Model = model;
				valType.DataType = DataType.CreateDataType(store);
				valType.DataType.Name = "foo";

				// Create an EntityType
				ObjectType entType = ObjectType.CreateObjectType(store);
				entType.Name = "EntityType1";
				entType.Model = model;

				// Create a FactType
				FactType fact = FactType.CreateFactType(store);
				fact.Name = "Fact1";
				fact.Model = model; // Also do after roles are added to test shape generation
				RoleMoveableCollection roles = fact.RoleCollection;
				Role role = Role.CreateRole(store);
				role.Name = "Role1";
				role.RolePlayer = valType;
				roles.Add(role);

				role = Role.CreateRole(store);
				role.Name = "Role2";
				role.RolePlayer = entType;
				roles.Add(role);

				Reading read = Reading.CreateReading(store);
				fact.ReadingCollection.Add(read);
				read.RoleCollection.Add(fact.RoleCollection[0]);
				read.RoleCollection.Add(fact.RoleCollection[1]);
				read.Text = "{0} has/is of {1}";

				// Create an objectified fact type with one role
				FactType nestedFact = FactType.CreateFactType(store);
				nestedFact.Name = "ObjectifiedFact1";
				nestedFact.Model = model; // Also do after roles are added to test shape generation

				ObjectType nestingType = ObjectType.CreateObjectType(store);
				nestingType.Name = "ObjectifyFact1";
				nestingType.Model = model;
				roles = nestedFact.RoleCollection;
				role = Role.CreateRole(store);
				role.Name = "Role1";
				roles.Add(role);

				nestingType.NestedFactType = nestedFact;
				role.RolePlayer = entType;

				//ExternalUniquenessConstraint euc = ExternalUniquenessConstraint.CreateExternalUniquenessConstraint(store);
				ExclusionConstraint euc = ExclusionConstraint.CreateExclusionConstraint(store);
				euc.Name = "Constraint1";
				euc.Model = model;
				ExternalConstraintRoleSetMoveableCollection roleSets = euc.RoleSetCollection;
				ExternalConstraintRoleSet roleSet = ExternalConstraintRoleSet.CreateExternalConstraintRoleSet(store);
				roleSet.RoleCollection.Add(fact.RoleCollection[1]);
				roleSets.Add(roleSet);
				roleSet = ExternalConstraintRoleSet.CreateExternalConstraintRoleSet(store);
				roleSets.Add(roleSet);
				roleSet.RoleCollection.Add(nestedFact.RoleCollection[0]);

				// UNDONE: Need to verify that this ordering is handled as well
				//fact.Model = model; // Done earlier to test shape generation
				#endregion // Generate Test Object Model
				#region Generate Test Diagram
				diagram = ORMDiagram.CreateORMDiagram(store);
				diagram.Associate((ModelElement)store.ElementDirectory.GetElements(ORMModel.MetaClassGuid)[0]);
				#endregion // Generate Test Diagram
			}
			else
			{
				Synchronize();
				using (FileStream stream = File.OpenRead(fileName))
				{
					if (stream.Length > 1)
					{
						(new ORMSerializer(store)).Load(stream);
						IList diagrams = store.ElementDirectory.GetElements(ORMDiagram.MetaClassGuid);
						if (diagrams.Count == 0)
						{
							diagram = ORMDiagram.CreateORMDiagram(store);
						}
						else
						{
							diagram = (Diagram)diagrams[0];
						}
					}
				}
			}

			Debug.Assert(diagram != null, "Diagram has not been set");

			// Make sure all views are connected to the (single) diagram
			// A more realistic scenario would be to have views for each diagram or a user's persisted set of views
			foreach (ORMDesignerDocView docView in this.DocViews)
			{
				docView.InitializeView(diagram, this);
			}

			this.SetFileName(fileName);

			// Make sure all of the shapes are set up correctly
			diagram.PerformShapeAnchoringRule();
		}

		/// <summary>
		/// Saves the model in Store format
		/// </summary>
		/// <param name="fileName"></param>
		protected override void Save(string fileName)
		{
			if (fileName == null || fileName.EndsWith(@"\default.orm", true, CultureInfo.CurrentCulture))
				return;

			// sync the model to any artifacts.
			Synchronize();

			// Save it out.
			using (FileStream fileStream = File.Create(fileName))
			{
				(new ORMSerializer(Store)).Save(fileStream);
			}
		}

		/// <summary>
		/// Called to populate the Filter field in the Save As... dialog.
		/// </summary>
		protected override string FormatList
		{
			get
			{
				string formatList = "ORM Diagram (*.orm)|*.orm|";
				return formatList.Replace("|", "\n");
			}
		}
#if ATTACHELEMENTPROVIDERS
		/// <summary>
		/// UNDONE: Attach element providers
		/// </summary>
		/// <param name="store">The store being loaded</param>
		/// <param name="storeKey">The key for the store in the docdata. Handles PrimaryStoreKey.</param>
		/// <returns></returns>
		protected override ElementProvider[] GetElementProviders(Store store, object storeKey)
		{
			if (storeKey == PrimaryStoreKey)
			{
				//return new ElementProvider[] { new ORMElementProvider(store) };
			}
			return base.GetElementProviders(store, storeKey);
		}
		/// <summary>
		/// Continually synchronize the primary store with the element provider
		/// </summary>
		/// <param name="storeKey">The store key in the docdata. Handles PrimaryStoreKey.</param>
		public override bool GetContinuousSynchronization(object storeKey)
		{
			if (storeKey == PrimaryStoreKey)
			{
				return true;
			}
			return base.GetContinuousSynchronization(storeKey);
		}
#endif // ATTACHELEMENTPROVIDERS
		/// <summary>
		/// Set the document scope to ProjectScope for the element provider mechanism
		/// </summary>
		protected override IArtifactScope DocumentScope
		{
			get { return this.ProjectScope; }
		}
		/// <summary>
		/// Attach model events. Adds NamedElementDictionary handling
		/// to the document's primary store.
		/// </summary>
		protected override void AddPostLoadModelingEventHandlers()
		{
			NamedElementDictionary.AttachedEventHandlers(Store);
		}
		/// <summary>
		/// Attach event handlers to populate the task list
		/// </summary>
		protected override void AddPreLoadModelingEventHandlers()
		{
			AddErrorReportingEvents();
		}
		/// <summary>
		/// Detach model events. Adds NamedElementDictionary handling
		/// to the document's primary store.
		/// </summary>
		protected override void RemoveModelingEventHandlers()
		{
			NamedElementDictionary.DetachEventHandlers(Store);
			RemoveErrorReportingEvents();
		}
		/// <summary>
		/// Clear out the task provider
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (myTaskProvider != null)
			{
				myTaskProvider.RemoveAllTasks();
				myTaskProvider = null;
			}
			base.Dispose(disposing);
		}
		/// <summary>
		/// Support the default/only (GUID_NULL) view
		/// </summary>
		/// <param name="logicalView">A view guid to test</param>
		/// <returns>true for an empty guid</returns>
		public override bool SupportsLogicalView(Guid logicalView)
		{
			if (logicalView == Guid.Empty)
			{
				return true;
			}
			return base.SupportsLogicalView(logicalView);
		}
		#endregion // Base overrides
		#region Error reporting
		private void AddErrorReportingEvents()
		{
			Store store = Store;
			MetaDataDirectory dataDirectory = store.MetaDataDirectory;
			EventManagerDirectory eventDirectory = store.EventManagerDirectory;
			MetaClassInfo classInfo = dataDirectory.FindMetaRelationship(ModelHasError.MetaRelationshipGuid);

			eventDirectory.ElementAdded.Add(classInfo, new ElementAddedEventHandler(ErrorAddedEvent));

			classInfo = dataDirectory.FindMetaClass(ModelError.MetaClassGuid);
			eventDirectory.ElementRemoved.Add(classInfo, new ElementRemovedEventHandler(ErrorRemovedEvent));
			eventDirectory.ElementAttributeChanged.Add(classInfo, new ElementAttributeChangedEventHandler(ErrorChangedEvent));
		}
		private void RemoveErrorReportingEvents()
		{
			Store store = Store;
			MetaDataDirectory dataDirectory = store.MetaDataDirectory;
			EventManagerDirectory eventDirectory = store.EventManagerDirectory;
			MetaClassInfo classInfo = dataDirectory.FindMetaRelationship(ModelHasError.MetaRelationshipGuid);

			eventDirectory.ElementAdded.Remove(classInfo, new ElementAddedEventHandler(ErrorAddedEvent));

			classInfo = dataDirectory.FindMetaClass(ModelError.MetaClassGuid);
			eventDirectory.ElementRemoved.Remove(classInfo, new ElementRemovedEventHandler(ErrorRemovedEvent));
			eventDirectory.ElementAttributeChanged.Remove(classInfo, new ElementAttributeChangedEventHandler(ErrorChangedEvent));
		}
		private void ErrorAddedEvent(object sender, ElementAddedEventArgs e)
		{
			ModelHasError link = e.ModelElement as ModelHasError;
			ModelError error = link.ErrorCollection;
			IORMToolTaskProvider provider = (this as IORMToolServices).TaskProvider;
			IORMToolTaskItem newTask = provider.CreateTask();
			newTask.ElementLocator = error as IRepresentModelElements;
			newTask.Text = error.Name;
			Debug.Assert(error.TaskData == null);
			error.TaskData = newTask;
			provider.AddTask(newTask);
		}
		private void ErrorRemovedEvent(object sender, ElementRemovedEventArgs e)
		{
			ModelError error = e.ModelElement as ModelError;
			IORMToolTaskItem taskData = error.TaskData as IORMToolTaskItem;
			if (taskData != null)
			{
				error.TaskData = null;
				(this as IORMToolServices).TaskProvider.RemoveTask(taskData);
			}
		}
		private void ErrorChangedEvent(object sender, ElementAttributeChangedEventArgs e)
		{
			ModelError error = e.ModelElement as ModelError;
			IORMToolTaskItem taskData = error.TaskData as IORMToolTaskItem;
			if (taskData != null)
			{
				taskData.Text = error.Name;
			}
		}
		#endregion // Error reporting
	}
}
