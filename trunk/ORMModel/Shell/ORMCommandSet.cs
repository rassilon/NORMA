using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.VisualStudio.EnterpriseTools.Shell;
using Microsoft.VisualStudio.EnterpriseTools.Validation.UI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Neumont.Tools.ORM;
using Neumont.Tools.ORM.ObjectModel;
using Neumont.Tools.ORM.ShapeModel;

namespace Neumont.Tools.ORM.Shell
{	
	public partial class ORMDesignerDocView
	{
		/// <summary>
		/// Create a command set for this type of view. Should be called
		/// once when the package loads
		/// </summary>
		/// <param name="serviceProvider">IServiceProvider</param>
		/// <returns></returns>
		public static object CreateCommandSet(IServiceProvider serviceProvider)
		{
			return new ORMDesignerCommandSet(serviceProvider);
		}
		/// <summary>
		/// Command objects for the ORMDesignerDocView
		/// </summary>
		[CLSCompliant(false)]
		protected class ORMDesignerCommandSet : MarshalByRefObject, IDisposable
		{
			private IMenuCommandService myMenuService;
			private IMonitorSelectionService myMonitorSelection;
			private IServiceProvider myServiceProvider;
			private ModelElementLocator myElementLocator;

			/// <summary>
			/// Commands
			/// </summary>
			private MenuCommand[] myCommands;

			/// <summary>
			/// ORMDesignerCommandSet constructor
			/// </summary>
			/// <param name="serviceProvider"></param>
			public ORMDesignerCommandSet(IServiceProvider serviceProvider)
			{
				myServiceProvider = serviceProvider;
				// add view ORM Model Explorer commands in the top-level menu.
				// These do not need a status handler (always enabled when the application designer is 
				// active), so we don't need an EFTMenuCommand
				MenuCommand menuCommand = new MenuCommand(new EventHandler(OnMenuViewORMModelExplorer), ORMDesignerCommandIds.ViewModelExplorer);
				menuCommand.Supported = false;
				MenuService.AddCommand(menuCommand);

				#region Array of menu commands
				myCommands = new MenuCommand[]
				{
				// Commands
#if DEBUG
				new MenuCommand(
				new EventHandler(OnMenuDebugViewStore),
				ORMDesignerCommandIds.DebugViewStore),
#endif // DEBUG
				new DynamicStatusMenuCommand(
				new EventHandler(OnStatusReferenceModesWindow),
				new EventHandler(OnMenuReferenceModesWindow),
				ORMDesignerCommandIds.ViewReferenceModeEditor),

				new DynamicStatusMenuCommand(
				new EventHandler(OnStatusDelete),
				new EventHandler(OnMenuDelete),				
				StandardCommands.Delete)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusReadingsWindow),
				new EventHandler(OnMenuReadingsWindow),
				ORMDesignerCommandIds.ViewReadingEditor)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusInsertRole),
				new EventHandler(OnMenuInsertRoleBefore),
				ORMDesignerCommandIds.InsertRoleBefore)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusInsertRole),
				new EventHandler(OnMenuInsertRoleAfter),
				ORMDesignerCommandIds.InsertRoleAfter)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusFactEditorWindow),
				new EventHandler(OnMenuFactEditorWindow),
				ORMDesignerCommandIds.ViewFactEditor)

				// Constraint editing commands				
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusActivateRoleSequence),
				new EventHandler(OnMenuActivateRoleSequence),
				ORMDesignerCommandIds.ViewActivateRoleSequence)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusDeleteRowSequence),
				new EventHandler(OnMenuDeleteRowSequence),
				ORMDesignerCommandIds.ViewDeleteRoleSequence)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusMoveRoleSequenceUp),
				new EventHandler(OnMenuMoveRoleSequenceUp),
				ORMDesignerCommandIds.ViewMoveRoleSequenceUp)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusMoveRoleSequenceDown),
				new EventHandler(OnMenuMoveRoleSequenceDown),
				ORMDesignerCommandIds.ViewMoveRoleSequenceDown)
				,new DynamicStatusMenuCommand(
				new EventHandler(OnStatusEditExternalConstraint),
				new EventHandler(OnMenuEditExternalConstraint),
				ORMDesignerCommandIds.ViewEditExternalConstraint)
			};
				#endregion
				AddCommands(myCommands);


			}

			/// <summary>
			/// Called to add a set of commands. This should be called
			/// by Initialize.
			/// </summary>
			/// <param name="commands">Commands to add</param>
			protected virtual void AddCommands(MenuCommand[] commands)
			{
				IMenuCommandService menuService = MenuService; // Use the accessor to force creation
				if (menuService != null)
				{
					int count = commands.Length;
					for (int i = 0; i < count; ++i)
					{
						menuService.AddCommand(commands[i]);
					}
				}
			}
			/// <summary>
			/// Called to remove a set of commands. This should be called
			/// by Dispose.
			/// </summary>
			/// <param name="commands">Commands to add</param>
			protected virtual void RemoveCommands(MenuCommand[] commands)
			{

				IMenuCommandService menuService = myMenuService;
				if (menuService != null)
				{
					int count = commands.Length;
					for (int i = 0; i < count; ++i)
					{
						menuService.RemoveCommand(commands[i]);
					}
				}
			}
			/// <summary>
			/// Cleanup code 
			/// </summary>
			public void Dispose()
			{
				if (myCommands != null)
				{
					RemoveCommands(myCommands);
				}
				myMenuService = null;
				myMonitorSelection = null;
				myServiceProvider = null;
				myCommands = null;
				myElementLocator = null;
			}

			/// <summary>
			/// Show the ORM Model Explorer
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			protected void OnMenuViewORMModelExplorer(object sender, EventArgs e)
			{
				ORMDesignerPackage.BrowserWindow.Show();
			}

#if DEBUG
			/// <summary>
			/// Show a debug window displaying the contents of the current store
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			protected void OnMenuDebugViewStore(object sender, EventArgs e)
			{
				Microsoft.VisualStudio.Modeling.Diagnostics.Debug.Assert(((ModelingDocData)CurrentORMView.DocData).Store);
			}
#endif // DEBUG

			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			private void OnStatusDelete(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.Delete);
			}

			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			private void OnMenuDelete(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					// call delete on the doc view
					docView.OnMenuDelete((sender as OleMenuCommand).Text);
				}
			}
			private void OnStatusReferenceModesWindow(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.DisplayCustomReferenceModeWindow);
			}
			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			private void OnStatusReadingsWindow(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.DisplayReadingsWindow);
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuReadingsWindow(object sender, EventArgs e)
			{
				ORMReadingEditorToolWindow editorWindow = ORMDesignerPackage.ReadingEditorWindow;
				editorWindow.Show();
			}
			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			private void OnStatusInsertRole(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.InsertRole);
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuInsertRoleAfter(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					// call delete on the doc view
					docView.OnMenuInsertRole(true);
				}
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuInsertRoleBefore(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					// call delete on the doc view
					docView.OnMenuInsertRole(false);
				}
			}
			#region External Constraint editing menu options
			#region Status queries
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuReferenceModesWindow(object sender, EventArgs e)
			{
				ORMDesignerPackage.ReferenceModeEditorWindow.Show();
			}

			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			private void OnStatusFactEditorWindow(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.DisplayFactEditorWindow);
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuFactEditorWindow(object sender, EventArgs e)
			{
				IVsWindowFrame editorWindow = ORMDesignerPackage.FactEditorWindow;
				editorWindow.Show();
			}
			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnStatusEditExternalConstraint(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.EditExternalConstraint);
			}
			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnStatusActivateRoleSequence(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.ActivateRoleSequence);
			}
			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnStatusDeleteRowSequence(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.DeleteRoleSequence);
			}
			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnStatusMoveRoleSequenceUp(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.MoveRoleSequenceUp);
			}
			/// <summary>
			/// Status callback
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnStatusMoveRoleSequenceDown(object sender, EventArgs e)
			{
				ORMDesignerDocView.OnStatusCommand(sender, CurrentORMView, ORMDesignerCommands.MoveRoleSequenceDown);
			}
			#endregion // Status queries
			#region Menu actions
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuEditExternalConstraint(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					docView.OnMenuEditExternalConstraint();
				}
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuActivateRoleSequence(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					docView.OnMenuActivateRoleSequence();
				}
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuDeleteRowSequence(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					// call delete on the doc view
					docView.OnMenuDeleteRoleSequence();
				}
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuMoveRoleSequenceUp(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					docView.OnMenuMoveRoleSequenceUp();
				}
			}
			/// <summary>
			/// Menu handler
			/// </summary>
			/// <param name="sender">Sender</param>
			/// <param name="e">Event args</param>
			protected void OnMenuMoveRoleSequenceDown(object sender, EventArgs e)
			{
				ORMDesignerDocView docView = CurrentORMView;
				if (docView != null)
				{
					docView.OnMenuMoveRoleSequenceDown();
				}
			}
			#endregion // Menu actions
			#endregion // External Constraint editing menu options
			/// <summary>
			/// Retrieve the menu service from the service provider
			/// specified in the constructor
			/// </summary>
			protected IMenuCommandService MenuService
			{
				get
				{
					IMenuCommandService menuService = myMenuService;
					if (menuService == null)
					{
						try
						{
							myMenuService = menuService = (IMenuCommandService)myServiceProvider.GetService(typeof(IMenuCommandService));
						}
						catch (InvalidCastException)
						{
							Debug.Assert(false, "CommandSet relies on the menu command service, which is unavailable.");
							throw;
						}
					}

					return menuService;
				}
			}
			/// <summary>
			/// Load the monitor selection service
			/// </summary>
			private IMonitorSelectionService MonitorSelection
			{
				get
				{
					IMonitorSelectionService monitorSelect = myMonitorSelection;
					if (monitorSelect == null)
					{
						myMonitorSelection = monitorSelect = (IMonitorSelectionService)myServiceProvider.GetService(typeof(IMonitorSelectionService));
					}
					return monitorSelect;
				}
			}

			/// <summary>
			/// An element locator. Used to navigate to items.
			/// </summary>
			/// <value></value>
			public ModelElementLocator ElementLocator
			{
				get
				{
					ModelElementLocator locator = myElementLocator;
					if (locator == null)
					{
						myElementLocator = locator = new ModelElementLocator((ModelingPackage)myServiceProvider);
					}
					return locator;
				}
			}
			/// <summary>
			/// Currently focused document
			/// </summary>
			protected ORMDesignerDocData CurrentData
			{
				get
				{
					return MonitorSelection.CurrentDocument as ORMDesignerDocData;
				}
			}

			/// <summary>
			/// Currently focused ORM document view
			/// </summary>
			protected ORMDesignerDocView CurrentORMView
			{
				get
				{
					return MonitorSelection.CurrentDocumentView as ORMDesignerDocView;
				}
			}
		}

		/// <summary>
		/// CommandIDs for the Application Designer package.
		/// </summary>
		public class ORMDesignerCommandIds
		{
			/// <summary>
			/// The global identifier for the command set used by the ORM designer.
			/// </summary>
			public static readonly Guid guidORMDesignerCommandSet = new Guid("7C51C000-1EAD-4b39-89B5-42BC9F49EA24");    // keep in sync with SatDll\PkgCmd.ctc

			#region CommandID objects for commands
#if DEBUG
			/// <summary>
			/// A command to view the current store contents in debug mode
			/// </summary>
			public static readonly CommandID DebugViewStore = new CommandID(guidORMDesignerCommandSet, cmdIdDebugViewStore);
#endif // DEBUG
			/// <summary>
			/// The ORM Model Explorer item on the view menu
			/// </summary>
			public static readonly CommandID ViewModelExplorer = new CommandID(guidORMDesignerCommandSet, cmdIdViewModelExplorer);
			/// <summary>
			/// The ORM Readings Window item on the fact type context menu
			/// </summary>
			public static readonly CommandID ViewReadingEditor = new CommandID(guidORMDesignerCommandSet, cmdIdViewReadingEditor);
			/// <summary>
			/// Insert a role after the selected role
			/// </summary>
			public static readonly CommandID InsertRoleAfter = new CommandID(guidORMDesignerCommandSet, cmdIdInsertRoleAfter);
			/// <summary>
			/// Insert a role before the selected role
			/// </summary>
			public static readonly CommandID InsertRoleBefore = new CommandID(guidORMDesignerCommandSet, cmdIdInsertRoleBefore);

			/// <summary>
			/// The Custom Reference Mode Editor Explorer item on the view menu
			/// </summary>
			public static readonly CommandID ViewReferenceModeEditor = new CommandID(guidORMDesignerCommandSet, cmdIdViewReferenceModeEditor);
			/// <summary>
			/// The ORM Fact Editor Window item on the fact type context menu
			/// </summary>
			public static readonly CommandID ViewFactEditor = new CommandID(guidORMDesignerCommandSet, cmdIdViewFactEditor);
			#endregion // CommandID objects for commands
			#region CommandID objects for menus
			/// <summary>
			/// The context menu for the diagram
			/// </summary>
			public static readonly CommandID ViewContextMenu = new CommandID(guidORMDesignerCommandSet, menuIdContextMenu);

			/// <summary>
			/// Available on any role belonging to the active RoleSequence in the active MCEC or SCEC.
			/// </summary>
			public static readonly CommandID ViewActivateRoleSequence = new CommandID(guidORMDesignerCommandSet, cmdIdActivateRoleSequence);

			/// <summary>
			/// Available on any role belonging to any RoleSequence in the active MCEC.
			/// </summary>
			public static readonly CommandID ViewDeleteRoleSequence = new CommandID(guidORMDesignerCommandSet, cmdIdDeleteRoleSequence);

			/// <summary>
			/// Available on any non-active external constraint.
			/// </summary>
			public static readonly CommandID ViewEditExternalConstraint = new CommandID(guidORMDesignerCommandSet, cmdIdEditExternalConstraint);

			/// <summary>
			/// Available on any role belonging to any RoleSequence in the active MCEC.
			/// </summary>
			public static readonly CommandID ViewMoveRoleSequenceUp = new CommandID(guidORMDesignerCommandSet, cmdIdMoveRoleSequenceUp);

			/// <summary>
			/// Available on any role belonging to any RoleSequence in the active MCEC.
			/// </summary>
			public static readonly CommandID ViewMoveRoleSequenceDown = new CommandID(guidORMDesignerCommandSet, cmdIdMoveRoleSequenceDown);
			#endregion //CommandID objects for menus

			#region cmdIds
			// IMPORTANT: keep these constants in sync with SatDll\PkgCmdID.h

#if DEBUG
			/// <summary>
			/// A command to view the current store contents in debug mode
			/// </summary>
			private const int cmdIdDebugViewStore = 0x28FF;
#endif // DEBUG
			/// <summary>
			/// The ORM Model Explorer item on the view menu
			/// </summary>
			private const int cmdIdViewModelExplorer = 0x2900;
			/// <summary>
			/// The ORM Readings Window item on the fact type context menu
			/// </summary>
			private const int cmdIdViewReadingEditor = 0x2901;
			/// <summary>
			/// View the reference mode editor
			/// </summary>
			private const int cmdIdViewReferenceModeEditor = 0x2902;
			/// <summary>
			/// Insert a role after the selected role
			/// </summary>
			private const int cmdIdInsertRoleAfter = 0x2903;
			/// <summary>
			/// Insert a role before the selected role
			/// </summary>
			private const int cmdIdInsertRoleBefore = 0x2904;
			/// <summary>
			/// The ORM Fact Editor Window item on the fact type context menu
			/// </summary>
			private const int cmdIdViewFactEditor = 0x2905;
			/// <summary>
			/// Available on any role belonging to the active RoleSequence in the active MCEC or SCEC.
			/// </summary>
			private const int cmdIdActivateRoleSequence = 0x2906;
			/// <summary>
			/// Available on any role belonging to any RoleSequence in the active MCEC.
			/// </summary>
			private const int cmdIdDeleteRoleSequence = 0x2907;
			/// <summary>
			/// Available on any non-active external constraint.
			/// </summary>
			private const int cmdIdEditExternalConstraint = 0x2908;
			/// <summary>
			/// Available on any role belonging to any RoleSequence in the active MCEC.
			/// </summary>
			private const int cmdIdMoveRoleSequenceUp = 0x2909;
			/// <summary>
			/// Available on any role belonging to any RoleSequence in the active MCEC.
			/// </summary>
			private const int cmdIdMoveRoleSequenceDown = 0x290A;

			/// <summary>
			/// The context menu for the diagram
			/// </summary>
			private const int menuIdContextMenu = 0x0100;
			#endregion

		}
	}
}
