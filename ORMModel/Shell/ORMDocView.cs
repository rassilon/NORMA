using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.EnterpriseTools.Shell;
using Microsoft.VisualStudio.EnterpriseTools.Validation.UI;
using Microsoft.VisualStudio.Shell;
using Northface.Tools.ORM;
using Northface.Tools.ORM.ObjectModel;
using Northface.Tools.ORM.ShapeModel;
namespace Northface.Tools.ORM.Shell
{
	/// <summary>
	/// Valid commands
	/// </summary>
	[Flags]
	public enum ORMDesignerCommands
	{
		/// <summary>
		/// Commands not set
		/// </summary>
		None = 0,
		/// <summary>
		/// Deletion of one or more object types is enabled
		/// </summary>
		DeleteObjectType = 1,
		/// <summary>
		/// Deletion of one or more fact types is enabled
		/// </summary>
		DeleteFactType = 2,
		/// <summary>
		/// Deletion of one or more constraints is enabled
		/// </summary>
		DeleteConstraint = 4,
		/// <summary>
		/// Mask field representing individual delete commands
		/// </summary>
		Delete = DeleteObjectType | DeleteFactType | DeleteConstraint,
		// Update the multiselect command filter constants in ORMDesignerDocView
		// when new commands are added
	}

	/// <summary>
	/// DocView designed to contain a single ORM Diagram
	/// </summary>
	[CLSCompliant(false)]
	public partial class ORMDesignerDocView : SingleDiagramDocView
	{
		#region Member variables
		private ORMDesignerCommands myEnabledCommands;
		private ORMDesignerCommands myVisibleCommands;
		private const ORMDesignerCommands EnabledSimpleMultiSelectCommandFilter = ORMDesignerCommands.Delete;
		private const ORMDesignerCommands EnabledComplexMultiSelectCommandFilter = ORMDesignerCommands.Delete;
		#endregion // Member variables
		#region Construction/destruction
		/// <summary>
		/// Standard DocView constructor, called by the editor factory
		/// </summary>
		/// <param name="docData">DocData</param>
		/// <param name="serviceProvider">IServiceProvider</param>
		public ORMDesignerDocView(DocData docData, IServiceProvider serviceProvider) : base(docData, serviceProvider)
		{
		}
		#endregion // Construction/destruction
		#region Base overrides
		/// <summary>
		/// Get the default context menu for this view
		/// </summary>
		protected override System.ComponentModel.Design.CommandID ContextMenuId
		{
			get
			{
				return default(System.ComponentModel.Design.CommandID);
			}
		}

		/// <summary>
		/// String indicating the toolbox tab name that should be selected when this view gets focus.
		/// </summary>
		protected override string DefaultToolboxTabName
		{
			get
			{
				return ResourceStrings.ToolboxDefaultTabName;
			}
		}


		/// <summary>
		/// Handle right-clicks on the diagram
		/// </summary>
		/// <param name="mouseArgs"></param>
		protected override void OnContextMenuRequested(DiagramPointEventArgs mouseArgs)
		{
			// myVisibleCommands and myEnabledCommands will be set when the selection is changed
			if (0 != (myVisibleCommands & myEnabledCommands))
			{
				DiagramClientView clientView = mouseArgs.DiagramClientView;
				// Get the mouse point (relative to the diagram's position), and convert it to a point on the screen
				System.Drawing.Point emulateClickPoint = clientView.PointToScreen(clientView.WorldToDevice(mouseArgs.MousePosition));
				this.MenuService.ShowContextMenu(ORMDesignerCommandIds.ViewContextMenu, emulateClickPoint.X, emulateClickPoint.Y);
			}
			else
			{
				mouseArgs.Handled = true;
			}
		}


		/// <summary>
		/// Enable menu commands when the selection changes
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSelectionChanged(EventArgs e)
		{
			ORMDesignerCommands visibleCommands = ORMDesignerCommands.None;
			ORMDesignerCommands enabledCommands = ORMDesignerCommands.None;
			int count = SelectionCount;
			if (count != 0)
			{
				if (count > 1)
				{
					ORMDesignerCommands currentVisible = ORMDesignerCommands.None;
					ORMDesignerCommands currentEnabled = ORMDesignerCommands.None;
					visibleCommands = enabledCommands = EnabledSimpleMultiSelectCommandFilter; // UNDONE: state.IsCoercedSelectionMixed ? EnabledComplexMultiSelectCommandFilter : EnabledSimpleMultiSelectCommandFilter;
					// UNDONE: How do we get the state?
					//foreach (ModelElement mel in state.CoercedSelectionModelElements)
					foreach (ModelElement melIter in GetSelectedComponents())
					{
						ModelElement mel = melIter;
						PresentationElement pel = mel as PresentationElement;
						if (pel != null)
						{
							mel = pel.ModelElement;
						}
						if (mel != null)
						{
							SetCommandStatus(mel, out currentVisible, out currentEnabled);
							enabledCommands &= currentEnabled;
							visibleCommands &= currentVisible;
							if (enabledCommands == 0 && visibleCommands == 0)
							{
								break;
							}
						}
					}
				}
				else
				{
					// UNDONE: How do we get the state?
					//foreach (ModelElement mel in state.CoercedSelectionModelElements)
					foreach (ModelElement melIter in GetSelectedComponents())
					{
						ModelElement mel = melIter;
						PresentationElement pel = mel as PresentationElement;
						if (pel != null)
						{
							mel = pel.ModelElement;
						}
						if (mel != null)
						{
							SetCommandStatus(mel, out visibleCommands, out enabledCommands);
						}
					}
				}
			}
			myVisibleCommands = visibleCommands;
			myEnabledCommands = enabledCommands;
		}
		/// <summary>
		/// Determine which commands are visible and enabled for the
		/// current state of an individual given element.
		/// </summary>
		/// <param name="element">A single model element. Should be a backing object, not a presentation element.</param>
		/// <param name="visibleCommands">(output) The set of visible commands</param>
		/// <param name="enabledCommands">(output) The set of enabled commands</param>
		protected virtual void SetCommandStatus(ModelElement element, out ORMDesignerCommands visibleCommands, out ORMDesignerCommands enabledCommands)
		{
			enabledCommands = ORMDesignerCommands.None;
			visibleCommands = ORMDesignerCommands.None;
			if (element is FactType)
			{
				visibleCommands = enabledCommands = ORMDesignerCommands.DeleteFactType;
			}
			else if (element is ObjectType)
			{
				visibleCommands = enabledCommands = ORMDesignerCommands.DeleteObjectType;
			}
			else if (element is Constraint)
			{
				visibleCommands = enabledCommands = ORMDesignerCommands.DeleteConstraint;
			}
			else if (element is ORMModel)
			{
				visibleCommands = ORMDesignerCommands.Delete;
			}
		}
		
		/// <summary>
		/// Check the current status of the requested command. This is called frequently, and is
		/// static to enable placing the null check inside this function.
		/// </summary>
		/// <param name="sender">A MenuCommand or OleMenuCommand</param>
		/// <param name="docView">The view to test</param>
		/// <param name="commandFlag">The command to check for enabled</param>
		protected static void OnStatusCommand(object sender, ORMDesignerDocView docView, ORMDesignerCommands commandFlag)
		{
			MenuCommand command = sender as MenuCommand;
			Debug.Assert(command != null);
			if (docView != null)
			{
				command.Visible = 0 != (commandFlag & docView.myVisibleCommands);
				command.Enabled = 0 != (commandFlag & docView.myEnabledCommands);
				if (0 != (commandFlag & ORMDesignerCommands.Delete))
				{
					docView.SetDeleteCommandText((OleMenuCommand)command);
				}
			}
		}

		/// <summary>
		/// Set the menu's text for the delete command
		/// </summary>
		/// <param name="command">OleMenuCommand</param>
		protected virtual void SetDeleteCommandText(OleMenuCommand command)
		{
			Debug.Assert(command != null);
			string commandText;
			switch (myVisibleCommands & ORMDesignerCommands.Delete)
			{
				case ORMDesignerCommands.DeleteObjectType:
					commandText = ResourceStrings.CommandDeleteObjectTypeText;
					break;
				case ORMDesignerCommands.DeleteFactType:
					commandText = ResourceStrings.CommandDeleteFactTypeText;
					break;
				case ORMDesignerCommands.DeleteConstraint:
					commandText = ResourceStrings.CommandDeleteConstraintText;
					break;
				default:
					commandText = null;
					break;
			}
			if (commandText != null)
			{
				command.Text = commandText;
			}
		}
		/// <summary>
		/// UNDONE: Temporary workaround for DSLTools SDK bug.
		/// OnClose is throwing when ModelingDocStore.CreateVSHost
		/// attempts to cast to the wrong class type.
		/// </summary>
		/// <param name="pgrfSaveOptions"></param>
		/// <returns></returns>
		public override int OnClose(ref uint pgrfSaveOptions)
		{
			return 0;
		}
		#endregion // Base overrides
		#region ORMDesignerDocView Specific
		/// <summary>
		/// Called by ORMDesignerDocData during Load
		/// </summary>
		/// <param name="diagram">The diagram object. Passed to the base class.</param>
		/// <param name="document">ORMDesignerDocData</param>
		public void InitializeView(Diagram diagram, ORMDesignerDocData document)
		{
			// Important to set this value via the Diagram member on SingleDiagramDocView as the 
			// side effects are vital to the diagram being hooked to the view correctly.
			base.Diagram = diagram;

			// Make sure we get a closing notification so we can clear the
			// selected components
			document.DocumentClosing += new EventHandler(DocumentClosing);
		}
		private void DocumentClosing(object sender, EventArgs e)
		{
			(sender as DocData).DocumentClosing -= new EventHandler(DocumentClosing);
			SetSelectedComponents(new object[]{});
		}
		/// <summary>
		/// Execute the delete command
		/// </summary>
		protected virtual void OnMenuDelete()
		{
			int count = SelectionCount;
			if (count > 0)
			{
				ModelingDocData docData = this.DocData as ModelingDocData;
				Debug.Assert(docData != null);

				Store store = docData.Store;
				Debug.Assert(store != null);

				Diagram d = null;
				using (Transaction t = store.TransactionManager.BeginTransaction("delete"))
				{
					bool testRefModeCollapse = 0 != (myEnabledCommands & ORMDesignerCommands.DeleteObjectType);

					// account for multiple selection
					foreach (object selectedObject in GetSelectedComponents())
					{
						ShapeElement pel = selectedObject as ShapeElement; // just the shape
						if (pel != null)
						{
							//UNDONE: Check if the object shape was in expanded mode
							Northface.Tools.ORM.ShapeModel.ObjectTypeShape objectShape;
							if (testRefModeCollapse &&
								null != (objectShape = pel as Northface.Tools.ORM.ShapeModel.ObjectTypeShape)&& false/*) &&
								!objectShape.ExpandRefMode*/
								)
							{
								t.TopLevelTransaction.Context.ContextInfo[ObjectType.DeleteReferenceModeValueType] = null;
							}
							if (d == null)
							{
								d = pel.Diagram;
								(docData.QueuedSelection as IList).Add(d);
							}

							// Get the actual object inside the pel before
							// removing the pel.
							ModelElement mel = pel.ModelElement;

							// Remove the actual object in the model
							if (mel != null)
							{
								// get rid of all visual shapes corresponding to this
								// model element. pel removal is done in the PresentationLinkRemoved rule
								mel.PresentationRolePlayers.Clear();

								// Get rid of the model element
								mel.Remove();
							}
						}
					}

					if (t.HasPendingChanges)
					{
						t.Commit();
					}
				}

				if (d != null)
				{
					// Clearing the selection selects the diagram
					CurrentDesigner.Selection.Clear();
				}
			}
		}
		/// <summary>
		/// Get the element locator associate with this view.
		/// The locator is used to jump to a specific element.
		/// </summary>
		public static ModelElementLocator ElementLocator
		{
			get
			{
				// The element locator available from the command
				// set associate with the current package.
				ORMDesignerCommandSet commandSet = ORMDesignerPackage.CommandSet as ORMDesignerCommandSet;
				return (commandSet != null) ? commandSet.ElementLocator : null;
			}
		}
		#endregion // ORMDesignerDocView Specific
	}
}
