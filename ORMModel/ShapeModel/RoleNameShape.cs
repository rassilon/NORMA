#region Common Public License Copyright Notice
/**************************************************************************\
* Neumont Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright � Neumont University. All rights reserved.                     *
* Copyright � Matthew Curland. All rights reserved.                        *
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Neumont.Tools.ORM.ObjectModel;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Neumont.Tools.ORM.Shell;
using Microsoft.VisualStudio.Modeling;
using System.Collections;
using System.Globalization;

namespace Neumont.Tools.ORM.ShapeModel
{
	public partial class RoleNameShape : ISelectionContainerFilter, IProxyDisplayProvider
	{
		#region Member Variables
		private static AutoSizeTextField myTextField;

		/// <summary>
		/// A brush used to draw the value range text
		/// </summary>
		protected static readonly StyleSetResourceId RoleNameTextBrush = new StyleSetResourceId("Neumont", "RoleNameTextBrush");
		#endregion // Member Variables
		#region Base overrides
		/// <summary>
		/// Sets the AutoSizeTextField to be added to the ShapeFieldCollection, is only run once
		/// </summary>
		/// <param name="fieldName">Non-localized name for the field</param>
		protected override AutoSizeTextField CreateAutoSizeTextField(string fieldName)
		{
			AutoSizeTextField newTextField = new RoleNameAutoSizeTextField(fieldName);
			newTextField.DefaultFocusable = true;
			newTextField.DefaultTextBrushId = RoleNameTextBrush;
			return newTextField;
		}
		/// <summary>
		/// Sets up the Brush to be used to draw the object and adds it to the StyleSet
		/// </summary>
		protected override void InitializeResources(StyleSet classStyleSet)
		{
			base.InitializeResources(classStyleSet);
			IORMFontAndColorService colorService = (Store as IORMToolServices).FontAndColorService;
			BrushSettings brushSettings = new BrushSettings();
			brushSettings.Color = colorService.GetForeColor(ORMDesignerColor.RoleName);
			classStyleSet.AddBrush(RoleNameTextBrush, DiagramBrushes.ShapeBackground, brushSettings);
		}
		/// <summary>
		/// Returns the guid for the object model name property
		/// </summary>
		protected override Guid AssociatedModelDomainPropertyId
		{
			get
			{
				return Role.NameDomainPropertyId;
			}
		}
		/// <summary>
		/// Gets and sets the AutoSizeTextField shape for this object
		/// </summary>
		protected override AutoSizeTextField TextShapeField
		{
			get
			{
				return myTextField;
			}
			set
			{
				Debug.Assert(myTextField == null);
				myTextField = value;
			}
		}
		/// <summary>
		/// Place a newly added role name shape
		/// </summary>
		/// <param name="parent">Parent FactTypeShape</param>
		/// <param name="createdDuringViewFixup">Whether this shape was created as part of a view fixup</param>
		public override void PlaceAsChildOf(NodeShape parent, bool createdDuringViewFixup)
		{
			FactTypeShape factShape = (FactTypeShape)parent;
			double x = -0.2;
			double y = -0.2;
			FactType factType = factShape.AssociatedFactType;
			// Cascades RoleNameShapes for facts that contain more than one role
			LinkedElementCollection<RoleBase> roles = factShape.DisplayedRoleOrder;
			int roleIndex = roles.IndexOf((RoleBase)ModelElement);
			if (roleIndex != -1)
			{
				x += roleIndex * 0.15;
				y -= roleIndex * 0.15;
			}
			Location = new PointD(x, y);
		}
		#endregion // Base overrides
		#region RoleNameShape specific
		/// <summary>
		/// Removes the RoleNameShape from the associated Role
		/// </summary>
		public static void RemoveRoleNameShapeFromRole(Role role)
		{
			LinkedElementCollection<PresentationElement> pels = PresentationViewsSubject.GetPresentation(role);
			int pelCount = pels.Count;
			for (int i = pelCount - 1; i >= 0; --i)
			{
				RoleNameShape pel = pels[i] as RoleNameShape;
				if (pel != null)
				{
					pel.Delete();
				}
			}
		}

		/// <summary>
		/// Sets the isVisible property for the given Role
		/// </summary>
		private static void SetRoleNameDisplay(Role role, bool shouldDisplay, bool shouldRemove)
		{
			if (!shouldRemove)
			{
				Diagram.FixUpDiagram(role.FactType, role);
			}
			foreach (PresentationElement element in PresentationViewsSubject.GetPresentation(role))
			{
				RoleNameShape rns = element as RoleNameShape;
				if (rns != null)
				{
					if (shouldRemove)
					{
						RemoveRoleNameShapeFromRole(role);
					}
					else
					{
						if (shouldDisplay)
						{
							rns.Show();
						}
						else
						{
							rns.Hide();
							rns.Size = SizeD.Empty;
						}
					}
					break;
				}
			}
		}
		/// <summary>
		/// Sets the isVisible for each of the Roles in the given FactType
		/// </summary>
		public static void SetRoleNameDisplay(FactType fact)
		{
			bool shouldDisplay = false;
			bool shouldRemove = false;
			foreach (PresentationElement element in PresentationViewsSubject.GetPresentation(fact))
			{
				FactTypeShape fts = element as FactTypeShape;
				if (fts != null)
				{
					if (fts.DisplayRoleNames == DisplayRoleNames.UserDefault
						&& OptionsPage.CurrentRoleNameDisplay == RoleNameDisplay.On)
					{
						shouldDisplay = true;
					}
					else if (fts.DisplayRoleNames == DisplayRoleNames.On)
					{
						shouldDisplay = true;
					}
					else if (fts.DisplayRoleNames == DisplayRoleNames.Off)
					{
						shouldRemove = true;
					}
				}
			}
			foreach (RoleBase roleBase in fact.RoleCollection)
			{
				Role role = roleBase as Role;
				if (role != null && !string.IsNullOrEmpty(role.Name))
				{
					SetRoleNameDisplay(role, shouldDisplay, shouldRemove);
				}
			}
		}
		#endregion // RoleNameShape specific
		#region ISelectionContainerFilter Implementation
		/// <summary>
		/// Implements ISelectionContainerFilter.IncludeInSelectionContainer
		/// </summary>
		protected static bool IncludeInSelectionContainer
		{
			get
			{
				return false;
			}
		}
		bool ISelectionContainerFilter.IncludeInSelectionContainer
		{
			get
			{
				return IncludeInSelectionContainer;
			}
		}
		#endregion // ISelectionContainerFilter Implementation
		#region RoleNameAutoSizeTextField class
		/// <summary>
		/// Inherited AutoSizeTextField class so the display GetDisplayText could be overridden
		/// </summary>
		private sealed class RoleNameAutoSizeTextField : AutoSizeTextField
		{
			/// <summary>
			/// Create a new RoleNameAutoSizeTextField
			/// </summary>
			/// <param name="fieldName">Non-localized name for the field</param>
			public RoleNameAutoSizeTextField(string fieldName)
			    : base(fieldName)
			{
			}			
			/// <summary>
			/// Gets the text to display in the RoleNameShape and appends square brackets to beginning and end
			/// </summary>
			public sealed override string GetDisplayText(ShapeElement parentShape)
			{
				return string.Format(CultureInfo.InvariantCulture, "[{0}]", base.GetDisplayText(parentShape)); // UNDONE: Localize format string
			}
		}
		#endregion // RoleNameAutoSizeTextField class

		#region IProxyDisplayProvider Implementation
		/// <summary>
		/// Implements <see cref="IProxyDisplayProvider.ElementDisplayedAs"/>
		/// </summary>
		protected object ElementDisplayedAs(ModelElement element, ModelError forError)
		{
			Role role;
			FactType factType;
			//((Role)ModelElement).FactType == ((FactType)element).ImpliedByObjectification.NestedFactType
			if (null != (role = element as Role))
			{
				return ((FactTypeShape)ParentShape).GetDiagramItem(role);
			}
			else if (null != (factType = element as FactType))
			{
				Objectification objectification;
				if (null != (objectification = factType.ImpliedByObjectification) &&
					null != (role = (Role)ModelElement) &&
					role.FactType == objectification.NestedFactType)
				{
					return ((FactTypeShape)ParentShape).GetDiagramItem(role);
				}
			}
			return null;
		}
		object IProxyDisplayProvider.ElementDisplayedAs(ModelElement element, ModelError forError)
		{
			return ElementDisplayedAs(element, forError);
		}
		#endregion // IProxyDisplayProvider Implementation
	}
}
