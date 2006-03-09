#region Common Public License Copyright Notice
/**************************************************************************\
* Neumont Object-Role Modeling Architect for Visual Studio                 *
*                                                                          *
* Copyright � Neumont University. All rights reserved.                     *
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Neumont.Tools.ORM.ObjectModel;
using Neumont.Tools.ORM.Shell;
using Neumont.Tools.ORM.Framework;

namespace Neumont.Tools.ORM.ShapeModel
{
	public partial class SubtypeLink
	{
		#region Customize appearance
		//The Resource ID's for the given subtype drawing type.
		/// <summary>
		/// resource id for pen to draw non primary subtype facts that are using the normal pen
		/// </summary>
		protected static readonly StyleSetResourceId NonPrimaryNormalResource = new StyleSetResourceId("Neumont", "NonPrimarySupertypeLinkNormalResource");
		/// <summary>
		/// resource id for pen to draw non primary subtype facts that are using the sticky pen
		/// </summary>
		protected static readonly StyleSetResourceId NonPrimaryStickyResource = new StyleSetResourceId("Neumont", "NonPrimarySupertypeLinkStickyResource");
		/// <summary>
		/// resource id for pen to draw non primary subtype facts that are using the active pen
		/// </summary>
		protected static readonly StyleSetResourceId NonPrimaryActiveResource = new StyleSetResourceId("Neumont", "NonPrimarySupertypeLinkActiveResource");
		/// <summary>
		/// Change the outline pen to a thin black line for all instances
		/// of this shape.
		/// </summary>
		/// <param name="classStyleSet">The style set to modify</param>
		protected override void InitializeResources(StyleSet classStyleSet)
		{
			IORMFontAndColorService colorService = (this.Store as IORMToolServices).FontAndColorService;
			Color lineColor = colorService.GetForeColor(ORMDesignerColor.Constraint);
			Color stickyColor = colorService.GetBackColor(ORMDesignerColor.ActiveConstraint);
			Color activeColor = colorService.GetBackColor(ORMDesignerColor.RolePicker);
			PenSettings penSettings = new PenSettings();
			penSettings.Width = 1.8F / 72.0F; // 1.8 Point. 0 Means 1 pixel, but should only be used for non-printed items
			penSettings.Alignment = PenAlignment.Center;
			penSettings.Color = lineColor;
			classStyleSet.OverridePen(DiagramPens.ConnectionLine, penSettings);
			//Supporting Dashed subtypefacts when not primary
			penSettings.DashStyle = DashStyle.Dash;
			classStyleSet.AddPen(NonPrimaryNormalResource, DiagramPens.ConnectionLine, penSettings);
			penSettings.DashStyle = DashStyle.Solid;
			penSettings.Color = stickyColor;
			classStyleSet.AddPen(ORMDiagram.StickyBackgroundResource, DiagramPens.ConnectionLine, penSettings);

			penSettings.DashStyle = DashStyle.Dash;
			classStyleSet.AddPen(NonPrimaryStickyResource, DiagramPens.ConnectionLine, penSettings);
			penSettings.DashStyle = DashStyle.Solid;
			penSettings.Color = activeColor;
			classStyleSet.AddPen(ORMDiagram.ActiveBackgroundResource, DiagramPens.ConnectionLine, penSettings);

			penSettings.DashStyle = DashStyle.Dash;
			classStyleSet.AddPen(NonPrimaryActiveResource, DiagramPens.ConnectionLine, penSettings);
			penSettings.DashStyle = DashStyle.Solid;

			penSettings = new PenSettings();
			penSettings.Width = 1.4F / 72.0F; // Soften the arrow a bit
			penSettings.Color = lineColor;
			classStyleSet.OverridePen(DiagramPens.ConnectionLineDecorator, penSettings);
			penSettings.Color = stickyColor;
			classStyleSet.AddPen(ORMDiagram.StickyConnectionLineDecoratorResource, DiagramPens.ConnectionLineDecorator, penSettings);
			penSettings.Color = activeColor;
			classStyleSet.AddPen(ORMDiagram.ActiveConnectionLineDecoratorResource, DiagramPens.ConnectionLineDecorator, penSettings);
			BrushSettings brushSettings = new BrushSettings();
			brushSettings.Color = lineColor;
			classStyleSet.OverrideBrush(DiagramBrushes.ConnectionLineDecorator, brushSettings);
			brushSettings.Color = stickyColor;
			classStyleSet.AddBrush(ORMDiagram.StickyConnectionLineDecoratorResource, DiagramBrushes.ConnectionLineDecorator, brushSettings);
			brushSettings.Color = activeColor;
			classStyleSet.AddBrush(ORMDiagram.ActiveConnectionLineDecoratorResource, DiagramBrushes.ConnectionLineDecorator, brushSettings);
		}
		/// <summary>
		/// Specifies the three different color styles used to draw
		/// a subtype link
		/// </summary>
		private enum DrawColorStyle
		{
			/// <summary>
			/// Draw as a normal constraint
			/// </summary>
			Normal,
			/// <summary>
			/// Draw as an active part of a sticky object
			/// </summary>
			Sticky,
			/// <summary>
			/// Draw as a currently selected item in an active
			/// constraint editing operation
			/// </summary>
			Active,
		}
		private DrawColorStyle ColorStyle
		{
			get
			{
				ORMDiagram diagram = Diagram as ORMDiagram;
				ExternalConstraintConnectAction action = diagram.ExternalConstraintConnectAction;
				IConstraint testConstraint = action.ActiveConstraint;
				IList<Role> selectedRoles = null;
				if (testConstraint == null)
				{
					IStickyObject sticky = diagram.StickyObject;
					if (sticky != null)
					{
						ExternalConstraintShape shape = sticky as ExternalConstraintShape;
						if (shape != null)
						{
							testConstraint = shape.AssociatedConstraint;
						}
					}
				}
				else
				{
					selectedRoles = action.SelectedRoleCollection;
				}
				if (testConstraint != null)
				{
					SubtypeFact associatedSubtype = AssociatedSubtypeFact;
					if (null != selectedRoles && selectedRoles.Contains(associatedSubtype.SupertypeRole))
					{
						return DrawColorStyle.Active;
					}
					else
					{
						FactTypeMoveableCollection facts = null;
						switch (testConstraint.ConstraintStorageStyle)
						{
							case ConstraintStorageStyle.SingleColumnExternalConstraint:
								facts = ((SingleColumnExternalConstraint)testConstraint).FactTypeCollection;
								break;
							case ConstraintStorageStyle.MultiColumnExternalConstraint:
								facts = ((MultiColumnExternalConstraint)testConstraint).FactTypeCollection;
								break;
						}
						if (facts != null && facts.Contains(AssociatedSubtypeFact))
						{
							return DrawColorStyle.Sticky;
						}
					}
				}
				return DrawColorStyle.Normal;
			}
		}
		/// <summary>
		/// A filled arrow decorator drawn with sticky pens and brushes
		/// </summary>
		private class StickyFilledArrowDecorator : DecoratorFilledArrow
		{
			public static readonly LinkDecorator Decorator = new StickyFilledArrowDecorator();
			public override StyleSetResourceId BrushId
			{
				get
				{
					return ORMDiagram.StickyConnectionLineDecoratorResource;
				}
			}
			public override StyleSetResourceId PenId
			{
				get
				{
					return ORMDiagram.StickyConnectionLineDecoratorResource;
				}
			}
		}
		/// <summary>
		/// A filled arrow decorator drawn with active pens and brushes
		/// </summary>
		private class ActiveFilledArrowDecorator : DecoratorFilledArrow
		{
			public static readonly LinkDecorator Decorator = new ActiveFilledArrowDecorator();
			public override StyleSetResourceId BrushId
			{
				get
				{
					return ORMDiagram.ActiveConnectionLineDecoratorResource;
				}
			}
			public override StyleSetResourceId PenId
			{
				get
				{
					return ORMDiagram.ActiveConnectionLineDecoratorResource;
				}
			}
		}
		/// <summary>
		/// Draw an arrow on the subtype end
		/// </summary>
		public override LinkDecorator DecoratorTo
		{
			get
			{
				DrawColorStyle style = ColorStyle;
				switch (style)
				{
					case DrawColorStyle.Sticky:
						return StickyFilledArrowDecorator.Decorator;
					case DrawColorStyle.Active:
						return ActiveFilledArrowDecorator.Decorator;
					default:
						Debug.Assert(style == DrawColorStyle.Normal);
						return LinkDecorator.DecoratorFilledArrow;
				}
			}
			set
			{
			}
		}
		/// <summary>
		/// Change the connection line pen if the subtype is sticky or
		/// a selected role in an active constraint
		/// </summary>
		public override StyleSetResourceId OutlinePenId
		{
			get
			{
				SubtypeFact associatedFact = AssociatedSubtypeFact;
				if (associatedFact != null)
				{
					bool isPrimary = associatedFact.IsPrimary;
					DrawColorStyle style = ColorStyle;
					switch (style)
					{
						case DrawColorStyle.Sticky:
							if (isPrimary)
							{
								return ORMDiagram.StickyBackgroundResource;
							}
							else
							{
								return NonPrimaryStickyResource;
							}
						case DrawColorStyle.Active:
							if (isPrimary)
							{
								return ORMDiagram.ActiveBackgroundResource;
							}
							else
							{
								return NonPrimaryActiveResource;
							}
						default:
							Debug.Assert(style == DrawColorStyle.Normal);
							if (isPrimary)
							{
								return DiagramPens.ConnectionLine;
							}
							else
							{
								return NonPrimaryNormalResource;
							}
					}
				}
				else
				{
					return DiagramPens.ConnectionLine;
				}
			}
		}
		/// <summary>
		/// Subtype links need to be selectable to enable readings, etc
		/// </summary>
		public override bool CanSelect
		{
			get
			{
				return true;
			}
		}
		/// <summary>
		/// Get a geometry we can click on
		/// </summary>
		public override ShapeGeometry ShapeGeometry
		{
			get
			{
				return ObliqueBinaryLinkShapeGeometry.ShapeGeometry;
			}
		}
		#endregion // Customize appearance
		#region SubtypeLink specific
		/// <summary>
		/// Get the ObjectTypePlaysRole link associated with this link shape
		/// </summary>
		public SubtypeFact AssociatedSubtypeFact
		{
			get
			{
				return ModelElement as SubtypeFact;
			}
		}
		/// <summary>
		/// Configuring this link after it has been added to the diagram
		/// </summary>
		/// <param name="diagram">The parent diagram</param>
		public override void ConfiguringAsChildOf(ORMDiagram diagram)
		{
			// If we're already connected then walk away
			if (FromShape == null && ToShape == null)
			{
				SubtypeFact subtypeFact = AssociatedSubtypeFact;
				ObjectType subType = subtypeFact.Subtype;
				ObjectType superType = subtypeFact.Supertype;
				FactType nestedSubFact = subType.NestedFactType;
				FactType nestedSuperFact = superType.NestedFactType;
				NodeShape fromShape;
				NodeShape toShape;
				if (null != (toShape = diagram.FindShapeForElement((nestedSuperFact == null) ? superType as ModelElement : nestedSuperFact) as NodeShape) &&
					null != (fromShape = diagram.FindShapeForElement((nestedSubFact == null) ? subType as ModelElement : nestedSubFact) as NodeShape))
				{
					Connect(fromShape, toShape);
				}
			}
		}
		#endregion // SubtypeLink specific
		#region Store Event Handlers
		/// <summary>
		/// Attach event handlers to the store
		/// </summary>
		public static void AttachEventHandlers(Store store)
		{
			MetaDataDirectory dataDirectory = store.MetaDataDirectory;
			EventManagerDirectory eventDirectory = store.EventManagerDirectory;

			MetaAttributeInfo attributeInfo = dataDirectory.FindMetaAttribute(SubtypeFact.IsPrimaryMetaAttributeGuid);
			eventDirectory.ElementAttributeChanged.Add(attributeInfo, new ElementAttributeChangedEventHandler(IsPrimaryChangedEvent));
		}
		/// <summary>
		/// Detach event handlers from the store
		/// </summary>
		public static void DetachEventHandlers(Store store)
		{
			MetaDataDirectory dataDirectory = store.MetaDataDirectory;
			EventManagerDirectory eventDirectory = store.EventManagerDirectory;

			MetaAttributeInfo attributeInfo = dataDirectory.FindMetaAttribute(SubtypeFact.IsPrimaryMetaAttributeGuid);
			eventDirectory.ElementAttributeChanged.Remove(attributeInfo, new ElementAttributeChangedEventHandler(IsPrimaryChangedEvent));
		}
		/// <summary>
		/// Event handler for IsPrimary property on the associated subtype fact
		/// </summary>
		private static void IsPrimaryChangedEvent(object sender, ElementAttributeChangedEventArgs e)
		{
			SubtypeFact fact;
			if (null != (fact = e.ModelElement as SubtypeFact))
			{
				if (!fact.IsRemoved)
				{
					foreach (PresentationElement pel in fact.AssociatedPresentationElements)
					{
						SubtypeLink linkShape;
						if (null != (linkShape = pel as SubtypeLink))
						{
							linkShape.Invalidate(true);
						}
					}
				}
			}
		}
		#endregion // Store Event Handlers
		#region Accessibility Properties
		/// <summary>
		/// Return the localized accessible name for the link
		/// </summary>
		public override string AccessibleName
		{
			get
			{
				return ResourceStrings.SubtypeLinkAccessibleName;
			}
		}
		/// <summary>
		/// Return the localized accessible description
		/// </summary>
		public override string AccessibleDescription
		{
			get
			{
				return ResourceStrings.SubtypeLinkAccessibleDescription;
			}
		}
		#endregion // Accessibility Properties
	}
	public partial class ORMShapeModel
	{
		#region  DisplaySubtypeLinkFixupListener
		/// <summary>
		/// A fixup class to display subtype links
		/// </summary>
		private class DisplaySubtypeLinkFixupListener : DeserializationFixupListener<ModelHasFactType>
		{
			/// <summary>
			/// Create a new DisplaySubtypeLinkFixupListener
			/// </summary>
			public DisplaySubtypeLinkFixupListener()
				: base((int)ORMDeserializationFixupPhase.AddImplicitPresentationElements)
			{
			}
			/// <summary>
			/// Add subtype links when possible
			/// </summary>
			/// <param name="element">An ModelHasFactType instance</param>
			/// <param name="store">The context store</param>
			/// <param name="notifyAdded">The listener to notify if elements are added during fixup</param>
			protected override void ProcessElement(ModelHasFactType element, Store store, INotifyElementAdded notifyAdded)
			{
				SubtypeFact subTypeFact = element.FactTypeCollection as SubtypeFact;
				if (subTypeFact != null)
				{
					ORMModel model = subTypeFact.Model;
					ObjectType rolePlayer = subTypeFact.Subtype;
					FactType nestedFact = rolePlayer.NestedFactType;
					if (nestedFact != null)
					{
						Diagram.FixUpDiagram(model, nestedFact);
						Diagram.FixUpDiagram(nestedFact, rolePlayer);
					}
					else
					{
						Diagram.FixUpDiagram(model, rolePlayer);
					}
					rolePlayer = subTypeFact.Supertype;
					nestedFact = rolePlayer.NestedFactType;
					if (nestedFact != null)
					{
						Diagram.FixUpDiagram(model, nestedFact);
						Diagram.FixUpDiagram(nestedFact, rolePlayer);
					}
					else
					{
						Diagram.FixUpDiagram(model, rolePlayer);
					}
					Diagram.FixUpDiagram(model, subTypeFact);
				}
			}
		}
		#endregion // DisplaySubtypeLinkFixupListener class
	}
}