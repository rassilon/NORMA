using System;
using System.Collections.Generic;
using System.Text;
using Neumont.Tools.Modeling.Shell.DynamicSurveyTreeGrid;
using Neumont.Tools.ORM.ObjectModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.VirtualTreeGrid;
using Neumont.Tools.Modeling;
using System.Collections;
using Microsoft.VisualStudio.Modeling.Shell;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Neumont.Tools.Modeling.Design;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Neumont.Tools.ORM.ShapeModel;
using MSOLE = Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Neumont.Tools.ORM.Shell
{
	/// <summary>
	/// Tool window to contain survey tree control
	/// </summary>
	[Guid("DD2334C3-AFDB-4FC5-9E8A-17D19A8CC97A")]
	[CLSCompliant(false)]
	public partial class ORMModelBrowserToolWindow : ORMToolWindow, IORMSelectionContainer
	{
		private SurveyTreeContainer myTreeContainer;
		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="serviceProvider"></param>
		public ORMModelBrowserToolWindow(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}
		private ORMDesignerCommands myVisibleCommands;
		private ORMDesignerCommands myCheckedCommands;
		private ORMDesignerCommands myCheckableCommands;
		private ORMDesignerCommands myEnabledCommands;
		private object myCommandSet;

		#region MenuService, MonitorSelectionService, and SelectedNode properties
		private static bool myCommandsPopulated;
		/// <summary>
		/// returns the menu service and instantiates a new command set if none exists
		/// </summary>
		public override IMenuCommandService MenuService
		{
			get
			{
				IMenuCommandService retVal = base.MenuService;
				if (retVal != null && !myCommandsPopulated)
				{
					myCommandsPopulated = true;
					myCommandSet = new ORMModelBrowserCommandSet(ExternalServiceProvider, retVal);
				}
				return retVal;
			}
		}
		private object SelectedNode
		{
			get
			{
				VirtualTreeControl treeControl = myTreeContainer.TreeControl;
				int currentIndex = treeControl.CurrentIndex;
				if (currentIndex >= 0)
				{
					VirtualTreeItemInfo info = treeControl.Tree.GetItemInfo(currentIndex, 0, false);
					int options = 0;
					object trackingObject = info.Branch.GetObject(info.Row, 0, ObjectStyle.TrackingObject, ref options);
					return trackingObject;
				}
				return null;
			}
		}
		#endregion //MenuService, MonitorSelectionService, and SelectedNode properties
		#region Command handling for window
		/// <summary>
		/// sets up commands that should be enabled in the ORM Model Browser window
		/// </summary>
		/// <param name="sender">Menu Command</param>
		/// <param name="commandFlags">commands that are a part of the menu command</param>
		/// <param name="currentWindow">the currently selected window</param>
		protected static void OnStatusCommand(Object sender, ORMDesignerCommands commandFlags, ORMModelBrowserToolWindow currentWindow)
		{
			MenuCommand command = sender as MenuCommand;
			Debug.Assert(command != null);
			if (currentWindow != null)
			{
				command.Visible = 0 != (commandFlags & currentWindow.myVisibleCommands);
				command.Enabled = 0 != (commandFlags & currentWindow.myEnabledCommands);
				command.Checked = 0 != (commandFlags & currentWindow.myCheckedCommands);
				if (command.Visible)
				{
					if (0 != (commandFlags & (ORMDesignerCommands.Delete)))
					{
						currentWindow.SetDeleteCommandText((OleMenuCommand)command);
					}
				}
			}
		}
		/// <summary>
		/// the action to be taken when a delete command is issued on the ORM Model Browser window
		/// </summary>
		/// <param name="commandText">text of the current command</param>
		protected virtual void OnMenuDelete(String commandText)
		{
			object currentNode = SelectedNode;
			if (currentNode != null)
			{
				ModelElement selectedType = EditorUtility.ResolveContextInstance(currentNode, false) as ModelElement;
				if (0 != (myEnabledCommands & ORMDesignerCommands.Delete))//facts objects multi and single column external constraints
				{
					Store store = selectedType.Store;
					Debug.Assert(store != null);
					using (Transaction t = store.TransactionManager.BeginTransaction(commandText.Replace("&", "")))
					{
						if (!selectedType.IsDeleted)
						{
							Dictionary<object, object> contextinfo = t.TopLevelTransaction.Context.ContextInfo;
							LinkedElementCollection<PresentationElement> presentationElements = PresentationViewsSubject.GetPresentation(selectedType);
							foreach (PresentationElement o in presentationElements)
							{
								ObjectTypeShape objectShape;
								ObjectifiedFactTypeNameShape objectifiedShape;
								if ((null != (objectShape = o as ObjectTypeShape) && !objectShape.ExpandRefMode) ||
									(null != (objectifiedShape = o as ObjectifiedFactTypeNameShape) && !objectifiedShape.ExpandRefMode))
								{
									contextinfo[ObjectType.DeleteReferenceModeValueType] = null;
								}
							}
							presentationElements.Clear();
							selectedType.Delete();
						}
						if (t.HasPendingChanges)
						{
							t.Commit();
						}
					}
				}
			}
		}
		/// <summary>
		/// Place the element label editing mode
		/// </summary>
		protected virtual void OnMenuEditLabel()
		{
			myTreeContainer.TreeControl.BeginLabelEdit();
		}
		/// <summary>
		/// fires when ORM Browser Tool window has a selection change
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSelectionChanged(EventArgs e)
		{
			ORMDesignerCommands visibleCommands = ORMDesignerCommands.None;
			ORMDesignerCommands enabledCommands = ORMDesignerCommands.None;
			ORMDesignerCommands checkedCommands = ORMDesignerCommands.None;
			ORMDesignerCommands checkableCommands = ORMDesignerCommands.None;
			ORMDesignerCommands toleratedCommands = ORMDesignerCommands.None;
			ORMDesignerDocView currentDoc = CurrentDocumentView as ORMDesignerDocView;
			if (currentDoc != null)
			{
				object selectedNode = SelectedNode;
				if (selectedNode != null)
				{
					ModelElement selectedType = EditorUtility.ResolveContextInstance(selectedNode, false) as ModelElement;
					if (selectedType != null)
					{
						currentDoc.SetCommandStatus(selectedType, null, true, out visibleCommands, out enabledCommands, out checkableCommands, out checkedCommands, out toleratedCommands);
						// Add in label editing command
						ISurveyNode surveyNode = selectedType as ISurveyNode;
						if (surveyNode != null && surveyNode.IsSurveyNameEditable)
						{
							visibleCommands |= ORMDesignerCommands.EditLabel;
							enabledCommands |= ORMDesignerCommands.EditLabel;
						}
					}
				}
			}
			myVisibleCommands = visibleCommands;
			myEnabledCommands = enabledCommands;
			myCheckedCommands = checkedCommands & visibleCommands;
			myCheckableCommands = checkableCommands & visibleCommands & enabledCommands;
			base.OnSelectionChanged(e);
		}
		#region set command text
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
				case ORMDesignerCommands.DeleteRole:
					commandText = ResourceStrings.CommandDeleteRoleText;
					break;
				case ORMDesignerCommands.DeleteModelNote:
					commandText = ResourceStrings.CommandDeleteModelNoteText;
					break;
				case ORMDesignerCommands.DeleteModelNoteReference:
					commandText = ResourceStrings.CommandDeleteModelNoteReferenceText;
					break;
				default:
					commandText = null;
					break;
			}
			if (commandText == null && 0 != (myVisibleCommands & ORMDesignerCommands.DeleteAny))
			{
				commandText = ResourceStrings.CommandDeleteMultipleElementsText;
			}
			// Setting command.Text to null will pick up
			// the default command text
			command.Text = commandText;
		}
		#endregion //set command text
		#endregion //Command handling for window
		#region LoadWindow method

		/// <summary>
		/// Loads the SurveyTreeControl from the current document
		/// </summary>
		protected void LoadWindow()
		{
			SurveyTreeContainer treeContainer = myTreeContainer;
			if (treeContainer == null)
			{
				myTreeContainer = treeContainer = new SurveyTreeContainer();
				VirtualTreeControl treeControl = treeContainer.TreeControl;
				treeControl.SelectionChanged += new EventHandler(Tree_SelectionChanged);
				treeControl.ContextMenuInvoked += new ContextMenuEventHandler(Tree_ContextMenuInvoked);
				treeControl.LabelEditControlChanged += new EventHandler(Tree_LabelEditControlChanged);
			}
			
			ORMDesignerDocData currentDocument = this.CurrentDocument;
			treeContainer.Tree = (currentDocument != null) ? currentDocument.SurveyTree : null;
		}

		private void Tree_LabelEditControlChanged(object sender, EventArgs e)
		{
			ActiveInPlaceEditWindow = myTreeContainer.TreeControl.LabelEditControl;
		}
		private void Tree_ContextMenuInvoked(object sender, ContextMenuEventArgs e)
		{
			IMenuCommandService menuCommandService = MenuService;
			if (menuCommandService != null)
			{
				menuCommandService.ShowContextMenu(ORMDesignerDocView.ORMDesignerCommandIds.ViewContextMenu, e.X, e.Y);
			}
		}
		private void Tree_SelectionChanged(object sender, EventArgs e)
		{
			object selectedObject = SelectedNode;
			SetSelectedComponents((selectedObject != null) ? new object[]{selectedObject} : null);
		}
		#endregion //LoadWindow method
		#region ORMToolWindow overrides
		///// <summary>
		///// currently unimplemented, all events handled by tree directly
		///// </summary>
		/// <summary>
		/// Attaches custom <see cref="EventHandler{TEventArgs}"/>s to the <see cref="Store"/>.  This method must be overridden.
		/// </summary>
		/// <param name="store">The <see cref="Store"/> for which the <see cref="EventHandler{TEventArgs}"/>s should be managed.</param>
		/// <param name="eventManager">The <see cref="ModelingEventManager"/> used to manage the <see cref="EventHandler{TEventArgs}"/>s.</param>
		/// <param name="action">The <see cref="EventHandlerAction"/> that should be taken for the <see cref="EventHandler{TEventArgs}"/>s.</param>
		protected override void ManageEventHandlers(Microsoft.VisualStudio.Modeling.Store store, Neumont.Tools.Modeling.ModelingEventManager eventManager, Neumont.Tools.Modeling.EventHandlerAction action)
		{
			// Track Currently Executing Events
			eventManager.AddOrRemoveHandler(new EventHandler<ElementEventsBegunEventArgs>(ElementEventsBegunEvent), action);
			eventManager.AddOrRemoveHandler(new EventHandler<ElementEventsEndedEventArgs>(ElementEventsEndedEvent), action);
		}

		private void ElementEventsBegunEvent(object sender, ElementEventsBegunEventArgs e)
		{
			ITree tree = this.myTreeContainer.Tree;
			if (tree != null)
			{
				tree.DelayRedraw = true;
			}
		}

		private void ElementEventsEndedEvent(object sender, ElementEventsEndedEventArgs e)
		{
			ITree tree = this.myTreeContainer.Tree;
			if (tree != null)
			{
				tree.DelayRedraw = false;
			}
		}

		/// <summary>
		/// called when document current selected document changes
		/// </summary>
		protected override void OnCurrentDocumentChanged()
		{
			base.OnCurrentDocumentChanged();
			LoadWindow();
		}
		/// <summary>
		/// returns string to be displayed as window title
		/// </summary>
		public override string WindowTitle
		{
			get
			{
				return ResourceStrings.ModelBrowserWindowTitle;
			}
		}
		/// <summary>
		/// returns int value for window icon
		/// </summary>
		protected override int BitmapResource //TODO: find correct bitmap resource to use
		{
			get
			{
				return 125;
			}
		}
		/// <summary>
		/// returns index for window icon
		/// </summary>
		protected override int BitmapIndex //TODO: find correct bitmap index to use
		{
			get
			{
				return 4;
			}
		}
		/// <summary>
		/// retuns the SurveyTreeControl this window contains
		/// </summary>
		public override System.Windows.Forms.IWin32Window Window
		{
			get
			{
				if (myTreeContainer == null)
				{
					LoadWindow();
				}
				return myTreeContainer;
			}
		}
		#endregion //ORMToolWindow overrides
	}
}
