using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Diagrams.GraphObject;
using Northface.Tools.ORM.ObjectModel;
using Northface.Tools.ORM.Shell;

namespace Northface.Tools.ORM.ShapeModel
{
	#region ConstraintDisplayPosition enum
	/// <summary>
	/// Determines where internal constraints are drawn
	/// on a facttype
	/// </summary>
	[CLSCompliant(true)]
	public enum ConstraintDisplayPosition
	{
		/// <summary>
		/// Draw the constraints above the role boxes
		/// </summary>
		Top,
		/// <summary>
		/// Draw the constraints below the role boxes
		/// </summary>
		Bottom
	}
	#endregion ConstraintDisplayPosition enum
	#region FactTypeShape class
	public partial class FactTypeShape : ICustomShapeFolding, IModelErrorActivation
	{
		#region ConstraintBoxRoleActivity enum
		/// <summary>
		/// The activity of a role in a ConstraintBox
		/// </summary>
		protected enum ConstraintBoxRoleActivity
		{
			/// <summary>
			/// The role is inactive
			/// </summary>
			Inactive,
			/// <summary>
			/// The role is active
			/// </summary>
			Active,
			/// <summary>
			/// The role is, technically speaking, not supposed to be in this box.  Only used for binary fact internal constraint compression.
			/// </summary>
			NotInBox
		}
		#endregion // ConstraintBoxRoleActivity enum
		#region ConstraintBox struct
		/// <summary>
		/// Defines a box to contain the constraint.
		/// </summary>
		protected struct ConstraintBox
		{
			#region Member Variables
			/// <summary>
			/// The bounding box to use.
			/// </summary>
			private RectangleD myBounds;
			/// <summary>
			/// The type of constraint contained is this box.
			/// </summary>
			private ConstraintType myConstraintType;
			/// <summary>
			/// Roles relative to the current order of the roles
			/// on the facr for which this constraint applies.
			/// </summary>
			private ConstraintBoxRoleActivity[] myActiveRoles;
			/// <summary>
			/// The constraint object this box is for.
			/// </summary>
			private IFactConstraint myFactConstraint;
			/// <summary>
			/// The cached role collection
			/// </summary>
			private IList<Role> myRoleCollection;
			#endregion // Member Variables
			#region Constructors
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="factConstraint">A reference to the original constraint that this ConstraintBox is based on.</param>
			/// <param name="factRoleCount">The number of roles for the context fact.</param>
			[CLSCompliant(false)]
			public ConstraintBox(IFactConstraint factConstraint, int factRoleCount)
			{
				// UNDONE: These asserts are crashing the ExternalConstraintConnectAction when RoleSequences are being edited.  Find out why.
//				Debug.Assert(factConstraint != null);
//				Debug.Assert(factRoleCount > 0 && factRoleCount >= factConstraint.RoleCollection.Count);
				myBounds = new RectangleD();
				IConstraint constraint = factConstraint.Constraint;
				myConstraintType = constraint.ConstraintType;
				myActiveRoles = new ConstraintBoxRoleActivity[factRoleCount];
				myFactConstraint = factConstraint;
				myRoleCollection = null;
			}
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="factConstraint">A reference to the original constraint that this ConstraintBox is based on.</param>
			/// <param name="roleActivity">A representation of the factConstraint's role activity within the fact.</param>
			[CLSCompliant(false)]
			public ConstraintBox(IFactConstraint factConstraint, ConstraintBoxRoleActivity[] roleActivity)
			{
				if (!object.ReferenceEquals(roleActivity, PreDefinedConstraintBoxRoleActivities_FullySpanning) && !object.ReferenceEquals(roleActivity,PreDefinedConstraintBoxRoleActivities_AntiSpanning))
				{
					int roleActivityCount = roleActivity.Length;
					Debug.Assert(factConstraint != null);
					Debug.Assert(roleActivityCount > 0 && roleActivityCount >= factConstraint.RoleCollection.Count);
					myBounds = new RectangleD();
					IConstraint constraint = factConstraint.Constraint;
					myConstraintType = constraint.ConstraintType;
					myActiveRoles = roleActivity;
					myFactConstraint = factConstraint;
					myRoleCollection = null;
				}
				else
				{
					myBounds = new RectangleD();
					IConstraint constraint = factConstraint.Constraint;
					myConstraintType = constraint.ConstraintType;
					myActiveRoles = roleActivity;
					myFactConstraint = factConstraint;
					myRoleCollection = null;
				}
			}
			#endregion // Constructors
			#region Accessor Properties
			/// <summary>
			/// The bounding box to use.
			/// </summary>
			public RectangleD Bounds
			{
				get
				{
					return myBounds;
				}
				set
				{
					myBounds = value;
				}
			}
			/// <summary>
			/// The type of constraint contained is this box.
			/// </summary>
			public ConstraintType ConstraintType
			{
				get
				{
					return myConstraintType;
				}
			}
			/// <summary>
			/// Roles relative to the current order of the roles
			/// on the facr for which this constraint applies.
			/// </summary>
			public ConstraintBoxRoleActivity[] ActiveRoles
			{
				get
				{
					return myActiveRoles;
				}
			}
			/// <summary>
			/// The constraint object this box is for.
			/// </summary>
			[CLSCompliant(false)]
			public IFactConstraint FactConstraint
			{
				get
				{
					return myFactConstraint;
				}
			}
			/// <summary>
			/// A (cached) reference to the fact constraint's role collection
			/// </summary>
			[CLSCompliant(false)]
			public IList<Role> RoleCollection
			{
				get
				{
					IList<Role> roles = myRoleCollection;
					if (roles == null)
					{
						myRoleCollection = roles = myFactConstraint.RoleCollection;
					}
					return roles;
				}
			}
			/// <summary>
			/// Tests if this constraint is a fully spanning constraint.
			/// </summary>
			/// <value>True if the constraint is fully spanning.</value>
			public bool IsSpanning
			{
				get
				{
					return object.ReferenceEquals(myActiveRoles, PreDefinedConstraintBoxRoleActivities_FullySpanning);
				}
			}
			/// <summary>
			/// Tests if this constraint is undefined (AntiSpanning).
			/// </summary>
			/// <value>True if the constraint is undefined.</value>
			public bool IsAntiSpanning
			{
				get
				{
					return object.ReferenceEquals(myActiveRoles, PreDefinedConstraintBoxRoleActivities_AntiSpanning);
				}
			}
			/// <summary>
			/// Tests if this constraint is valid in combination with the other existing constraints on the fact type.
			/// </summary>
			/// <value>True if the constraint is valid.</value>
			public bool IsValid
			{
				get
				{
					//UNDONE: Test if this constraint is valid on the fact type
					if (IsAntiSpanning)
					{
						return false;
					}
					return true;
				}
			}
			#endregion // Accessor Properties
			#region Array sorting code
			/// <summary>
			/// Sort the constraint boxes and place non-displayed constraints
			/// at the end of the array. Return the number of boxes that
			/// actually need displaying.
			/// </summary>
			/// <param name="boxes">An existing array of constraint boxes
			/// created with the parametrized constructor</param>
			/// <returns>The number of significant boxes</returns>
			public static int OrderConstraintBoxes(ConstraintBox[] boxes)
			{
				Array.Sort(boxes, Compare);
				int fullBoxCount = boxes.Length;
				int significantBoxCount = 0;
				int i;
				for (i = 0; i < fullBoxCount; ++i)
				{
					if (IsConstraintTypeVisible(boxes[i].ConstraintType))
					{
						// UNDONE: Possibly add more code here, needs to
						// match algorithm in Compare
					}
					else
					{
						// All insignificant ones are sorted to the end
						significantBoxCount = i;
						break;
					}
				}
				if (i == fullBoxCount)
				{
					significantBoxCount = fullBoxCount;
				}
				return significantBoxCount;
			}
			/// <summary>
			/// Compares two ConstraintBoxes
			/// </summary>
			/// <param name="c1">First ConstraintBox to compare.</param>
			/// <param name="c2">Second ConstraintBox to compare.</param>
			/// <returns>Value indicating the relative order of the ConstraintBoxes. -1 if c1 &lt; c2, 0 if c1 == c2, 1 if c1 &gt; c2</returns>
			private static int Compare(ConstraintBox c1, ConstraintBox c2)
			{
				// Order the constraints, bringing preferred uniqueness constraints to the top of 
				// internal uniqueness constraint.  Internal constraints will be on the bottom of
				// external constraints.
				ConstraintType ct1 = c1.ConstraintType;
				ConstraintType ct2 = c2.ConstraintType;
				int retVal = 0;

				if (ct1 != ct2)
				{
					int ctOrder1 = RelativeSortPosition(ct1);
					int ctOrder2 = RelativeSortPosition(ct2);
					if (ctOrder1 < ctOrder2)
					{
						retVal = -1;
					}
					else if (ctOrder1 > ctOrder2)
					{
						retVal = 1;
					}
				}
				else if (IsConstraintTypeVisible(ct1))
				{
					// If one of them is the preferred identifier, it rises to the top.
					//if (c1.Constraint.PreferredIdentifierFor.
					// else
					//	{
					// Constraints with more roles sink to the bottom.
					int c1RoleCount = c1.RoleCollection.Count;
					int c2RoleCount = c2.RoleCollection.Count;
					if (c1RoleCount < c2RoleCount)
					{
						retVal = -1;
					}
					else if (c1RoleCount > c2RoleCount)
					{
						retVal = 1;
					}
				}

				return retVal;
			}
			/// <summary>
			/// Helper function for Compare to determine
			/// the relative order of different constraint types.
			/// </summary>
			/// <param name="constraintType">ConstraintType value</param>
			/// <returns>Relative numbers (the exact values should not matter).</returns>
			private static int RelativeSortPosition(ConstraintType constraintType)
			{
				int retVal = 0;
				switch (constraintType)
				{
					case ConstraintType.InternalUniqueness:
						retVal = 0;
						break;
					case ConstraintType.ExternalUniqueness:
					case ConstraintType.DisjunctiveMandatory:
					case ConstraintType.Ring:
					case ConstraintType.Equality:
					case ConstraintType.Exclusion:
					case ConstraintType.Frequency:
					case ConstraintType.SimpleMandatory:
					case ConstraintType.Subset:
					default:
						retVal = 1;
						break;
				}
				return retVal;
			}
			/// <summary>
			/// Is the constraint type ever visible to the ConstraintBox walking
			/// algorithm? A true return here does not guarantee that a specific constraint
			/// instance of this type is visible, only that constraints of this type can
			/// be visible.
			/// </summary>
			/// <param name="constraintType">ConstraintType value</param>
			/// <returns>true if the constraint can be drawn visibly</returns>
			private static bool IsConstraintTypeVisible(ConstraintType constraintType)
			{
				switch (constraintType)
				{
					case ConstraintType.InternalUniqueness:
						return true;
				}
				return false;
			}
			#endregion // Array sorting code
		}
		#endregion // ConstraintBox struct
		#region Pre-defined ConstraintBoxRoleActivity arrays
		// Used for the WalkConstraints method.  Having these static arrays is very
		// useful for saving time allocating arrays every time something is hit tested.
		/// <summary>
		/// A ConstraintBoxRoleActivity[] for a fully-spanning uniqueness constraint.
		/// </summary>
		private static readonly ConstraintBoxRoleActivity[] PreDefinedConstraintBoxRoleActivities_FullySpanning = new ConstraintBoxRoleActivity[0] {};
		/// <summary>
		/// A ConstraintBoxRoleActivity[] for an undefined uniqueness constraint.
		/// </summary>
		private static readonly ConstraintBoxRoleActivity[] PreDefinedConstraintBoxRoleActivities_AntiSpanning = new ConstraintBoxRoleActivity[0] {};
		/// <summary>
		/// A ConstraintBoxRoleActivity[] for an n-1 binary fact with the first role active.
		/// </summary>
		private static readonly ConstraintBoxRoleActivity[] PreDefinedConstraintBoxRoleActivities_BinaryLeft = new ConstraintBoxRoleActivity[2] { ConstraintBoxRoleActivity.Active, ConstraintBoxRoleActivity.NotInBox };
		/// <summary>
		/// A ConstraintBoxRoleActivity[] for an n-1 binary fact with the second role active.
		/// </summary>
		private static readonly ConstraintBoxRoleActivity[] PreDefinedConstraintBoxRoleActivities_BinaryRight = new ConstraintBoxRoleActivity[2] { ConstraintBoxRoleActivity.NotInBox, ConstraintBoxRoleActivity.Active };
		/// <summary>
		/// A ConstraintBoxRoleActivity[] for an n-1 ternary fact with the first and second roles active.
		/// </summary>
		private static readonly ConstraintBoxRoleActivity[] PreDefinedConstraintBoxRoleActivities_TernaryLeft = new ConstraintBoxRoleActivity[3] { ConstraintBoxRoleActivity.Active, ConstraintBoxRoleActivity.Active, ConstraintBoxRoleActivity.Inactive };
		/// <summary>
		/// A ConstraintBoxRoleActivity[] for an n-1 ternary fact with the first and third roles active.
		/// </summary>
		private static readonly ConstraintBoxRoleActivity[] PreDefinedConstraintBoxRoleActivities_TernaryCenter = new ConstraintBoxRoleActivity[3] { ConstraintBoxRoleActivity.Active, ConstraintBoxRoleActivity.Inactive, ConstraintBoxRoleActivity.Active };
		/// <summary>
		/// A ConstraintBoxRoleActivity[] for an n-1 ternary fact with the second and third roles active.
		/// </summary>
		private static readonly ConstraintBoxRoleActivity[] PreDefinedConstraintBoxRoleActivities_TernaryRight = new ConstraintBoxRoleActivity[3] { ConstraintBoxRoleActivity.Inactive, ConstraintBoxRoleActivity.Active, ConstraintBoxRoleActivity.Active };
		#endregion //Pre-defined ConstraintBoxRoleActivity arrays
		#region WalkConstraintBoxes implementation
		/// <summary>
		/// Do something within the bounds you're given.  This may include
		/// painting, hit testing, highlighting, etc.
		/// </summary>
		/// <param name="constraintBox">The constraint that is being described</param>
		/// <returns>bool</returns>
		protected delegate bool VisitConstraintBox(ref ConstraintBox constraintBox);

		/// <summary>
		/// Determines the bounding boxes of all the constraints associated with the FactType,
		/// then passes those bounding boxes into the delegate.  Specifically, it will pass in
		/// the bouding box, the number of roles in the box, a boolean[] telling the method
		/// which roles are active for the constraint, and the constraint type.
		/// </summary>
		/// <param name="parentShape">The FactTypeShape that the ConstraintShape is associated with.</param>
		/// <param name="shapeField">The ShapeField whose bounds define the space that the ConstraintBoxes will be built in.</param>
		/// <param name="boxUser">The VisitConstraintBox delegate that will use the ConstraintBoxes produced by WalkConstraintBoxes.</param>
		protected static void WalkConstraintBoxes(ShapeElement parentShape, ShapeField shapeField, VisitConstraintBox boxUser)
		{
			WalkConstraintBoxes(parentShape, shapeField.GetBounds(parentShape), boxUser);
		}

		/// <summary>
		/// Determines the bounding boxes of all the constraints associated with the FactType,
		/// then passes those bounding boxes into the delegate.  Specifically, it will pass in
		/// the bouding box, the number of roles in the box, a boolean[] telling the method
		/// which roles are active for the constraint, and the constraint type.
		/// </summary>
		/// <param name="parentShape">The FactTypeShape that the ConstraintShape is associated with.</param>
		/// <param name="fullBounds">The bounds the rectangles need to fit in.  Pass RectangleD.Empty if unknown.</param>
		/// <param name="boxUser">The VisitConstraintBox delegate that will use the ConstraintBoxes 
		/// produced by WalkConstraintBoxes.</param>
		protected static void WalkConstraintBoxes(ShapeElement parentShape, RectangleD fullBounds, VisitConstraintBox boxUser)
		{
			// initialize variables
			FactTypeShape parentFactTypeShape = parentShape as FactTypeShape;
			FactType parentFact = parentFactTypeShape.AssociatedFactType;
			RoleMoveableCollection factRoles = parentFact.RoleCollection;
			int factRoleCount = factRoles.Count;
			if (fullBounds.IsEmpty)
			{
				fullBounds = new RectangleD(0, 0, RoleBoxWidth, 0);
			}

			// First, gather the various constraints that are associated with the parent FactTypeShape.
			//
			ICollection<IFactConstraint> factConstraints = parentFact.FactConstraintCollection;
			int fullConstraintCount = factConstraints.Count;
			ConstraintBox[] constraintBoxes = new ConstraintBox[fullConstraintCount];

			if (fullConstraintCount != 0)
			{
				// Constraints hasn't been filled before it's used later in the code.
				int currentConstraintIndex = 0;
				foreach (IFactConstraint factConstraint in factConstraints)
				{
					IList<Role> constraintRoles = factConstraint.RoleCollection;
					int constraintRoleCount = constraintRoles.Count;
					#region Optimized ConstraintRoleBox assignments
					// Optimization time: If we're dealing with binary or ternary constraints,
					// use the pre-defined ConstraintBoxRoleActivity collections.  This saves
					// on allocating tons of arrays every time the constraints are drawn or hit tested.
					ConstraintBoxRoleActivity[] predefinedActivityRoles = null;
					if (constraintRoleCount == factRoleCount)
					{
						predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_FullySpanning;
					}
					else if (constraintRoleCount == 0)
					{
						predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_AntiSpanning;
					}
					else
					{
						switch (factRoleCount)
						{
							#region Binary fact type
							case 2:
								switch (constraintRoleCount)
								{
									case 1:
										int roleIndex = factRoles.IndexOf(constraintRoles[0]);
										Debug.Assert(roleIndex != -1); // This violates the IFactConstraint contract
										if (roleIndex == 0)
										{
											predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_BinaryLeft;
										}
										else if (roleIndex == 1)
										{
											predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_BinaryRight;
										}
										break;
								}
								break;
							#endregion // Binary fact type
							#region Ternary fact type
							case 3:
								switch (constraintRoleCount)
								{
									case 2:
										int roleIndex0 = factRoles.IndexOf(constraintRoles[0]);
										int roleIndex1 = factRoles.IndexOf(constraintRoles[1]);
										Debug.Assert(roleIndex0 != -1); // This violates the IFactConstraint contract
										Debug.Assert(roleIndex1 != -1); // This violates the IFactConstraint contract
										switch (roleIndex0)
										{
											case 0:
												if (roleIndex1 == 1)
												{
													predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_TernaryLeft;
												}
												else if (roleIndex1 == 2)
												{
													predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_TernaryCenter;
												}
												break;
											case 1:
												if (roleIndex1 == 0)
												{
													predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_TernaryLeft;
												}
												else if (roleIndex1 == 2)
												{
													predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_TernaryRight;
												}
												break;
											case 2:
												if (roleIndex1 == 0)
												{
													predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_TernaryCenter;
												}
												else if (roleIndex1 == 1)
												{
													predefinedActivityRoles = PreDefinedConstraintBoxRoleActivities_TernaryRight;
												}
												break;
										}
										break;
								}
								break;
							#endregion // Ternary fact type
						}
					}
					#endregion // Optimized ConstraintRoleBox assignments
					#region Manual ConstraintRoleBox assignment
					if (predefinedActivityRoles != null)
					{
						constraintBoxes[currentConstraintIndex] = new ConstraintBox(factConstraint, predefinedActivityRoles);
					}
					else
					{
						// The original code, now used for handling fact types with 4 or more roles
						// or fact types that are irregular. 
						ConstraintBox currentBox = new ConstraintBox(factConstraint, factRoleCount);

						// The constraint is not a fully-spanning constraint.  We must now
						// determine if the hole is between active roles.  This is important
						// mainly for drawing, to determine if a dashed line needs to be drawn
						// to connect the solid lines over the active roles of the constraint.

						// UNDONE: This assert is crashing the ExternalConstraintConnectAction when RoleSequences are being edited.  Find out why.
//						Debug.Assert(constraintRoleCount < factRoleCount); // Should be predefined otherwise
						ConstraintBoxRoleActivity[] activeRoles = currentBox.ActiveRoles;
						// UNDONE: This assert is crashing the ExternalConstraintConnectAction when RoleSequences are being edited.  Find out why.
//						Debug.Assert(activeRoles.Length == factRoleCount);
						// Walk the fact's roles, and for each role that is found in this constraint
						// mark the role as active in the constraintBox.roleActive array.  
						for (int i = 0; i < constraintRoleCount; ++i)
						{
							int roleIndex = factRoles.IndexOf(constraintRoles[i]);
							Debug.Assert(roleIndex != -1); // This violates the IFactConstraint contract
							activeRoles[roleIndex] = ConstraintBoxRoleActivity.Active;
						}
						if (factRoleCount == 2)
						{
							for (int i = 0; i < factRoleCount; ++i)
							{
								if (activeRoles[i] != ConstraintBoxRoleActivity.Active)
								{
									activeRoles[i] = ConstraintBoxRoleActivity.NotInBox;
								}
							}
						}
						constraintBoxes[currentConstraintIndex] = currentBox;
					}
					#endregion // Manual ConstraintRoleBox assignment
					++currentConstraintIndex;
				}
				int significantConstraintCount = ConstraintBox.OrderConstraintBoxes(constraintBoxes);

				// Walk the constraintBoxes array and assign a physical location to each constraint box,
				double constraintHeight = ConstraintHeight;
				double constraintWidth = fullBounds.Width / (double)factRoleCount;
				fullBounds.Height = constraintHeight;
				int heightLeft = 0;
				int heightRight = 0;
				int lastUncompressedConstraint = 0;
				double initialBottom = fullBounds.Bottom;
				#region Compressing the ConstraintRoleBoxes of binary fact types.
				if (factRoleCount == 2 && significantConstraintCount <= 2)
				{
					for (int i = significantConstraintCount - 1; i >= 0; --i)
					{
						ConstraintBox box = constraintBoxes[i];
						box.Bounds = fullBounds;
						RectangleD bounds = box.Bounds;

						ConstraintBoxRoleActivity[] activeRoles = box.ActiveRoles;
						if (activeRoles.Length == 2)
						{
							if (activeRoles[0] == ConstraintBoxRoleActivity.NotInBox)
							{
								bounds.X = bounds.X + constraintWidth;
								bounds.Width = bounds.Width - constraintWidth;

								if (heightLeft > 0)
								{
									bounds.Y = initialBottom - ((double)lastUncompressedConstraint * constraintHeight);
									--heightLeft;
								}
								else if (heightRight++ == 0)
								{
									lastUncompressedConstraint = i;
								}
							}
							else if (activeRoles[1] == ConstraintBoxRoleActivity.NotInBox)
							{
								bounds.Width = bounds.Width - constraintWidth;

								if (heightRight > 0)
								{
									bounds.Y = initialBottom - ((double)lastUncompressedConstraint * constraintHeight);
									--heightRight;
								}
								else if (heightLeft++ == 0)
								{
									lastUncompressedConstraint = i;
								}
							}
						}
						box.Bounds = bounds;
						if (!boxUser(ref box))
						{
							break;
						}
						fullBounds.Offset(0, constraintHeight);
					}
				}
				#endregion // Compressing the ConstraintRoleBoxes of binary fact types.
				// Unaries, ternaries and n-aries do not need to have 
				// their internal uniqueness constraints compressed.
				// This will also run if a binary has too many constraints.
				else
				{
					int i;
					int j;
					if (parentFactTypeShape.ConstraintDisplayPosition == ConstraintDisplayPosition.Top)
					{
						// walk the constraints from top to bottom
						i = significantConstraintCount - 1;
						j = -1;
					}
					else 
					{
						// walk the constraints from bottom to top
						i = 0;
						j = 1;
					}
					for (; i >= 0 && i < significantConstraintCount; i += j)
					{
						ConstraintBox box = constraintBoxes[i];
						box.Bounds = fullBounds;
						if (!boxUser(ref box))
						{
							break;
						}
						fullBounds.Offset(0, constraintHeight);
					}
				}
			}
		}
		#endregion // WalkConstraintBoxes implementation
		#region Size Constants
		private const double RoleBoxHeight = 0.11;
		private const double RoleBoxWidth = 0.16;
		private const double NestedFactHorizontalMargin = 0.2;
		private const double NestedFactVerticalMargin = 0.075;
		private const double ConstraintHeight = 0.07;
		#endregion // Size Constants
		#region SpacerShapeField : ShapeField
		/// <summary>
		/// Creates a shape to properly align the other shapefields within the FactTypeShape.
		/// </summary>
		private class SpacerShapeField : ShapeField
		{
			/// <summary>
			/// Construct a default SpacerShapeField
			/// </summary>
			public SpacerShapeField()
			{
				DefaultFocusable = false;
				DefaultSelectable = false;
				DefaultVisibility = false;
			}

			/// <summary>
			/// Width is that of NestedFactHorizontalMargin if parentShape is objectified; otherwise, zero.
			/// </summary>
			/// <returns>NestedFactHorizontalMargin if objectified; otherwise, 0.</returns>
			public override double GetMinimumWidth(ShapeElement parentShape)
			{
				FactTypeShape factShape = parentShape as FactTypeShape;
				if (factShape.IsObjectified)
					return NestedFactHorizontalMargin;
				else
					return 0;
			}

			/// <summary>
			/// Width is that of NestedFactVerticalMargin if parentShape is objectified; otherwise, zero.
			/// </summary>
			/// <returns>NestedFactVerticalMargin if objectified; otherwise, 0.</returns>
			public override double GetMinimumHeight(ShapeElement parentShape)
			{
				FactTypeShape factShape = parentShape as FactTypeShape;
				if (factShape.IsObjectified)
				{
					return NestedFactVerticalMargin;
				}
				else
				{
					// UNDONE: At the moment the pen width is a constant
					// value, so this should just return a constant.
					StyleSet styleSet = parentShape.StyleSet;
					Pen pen = styleSet.GetPen(InternalFactConstraintPen);
					return (Double)(pen.Width / 2);
				}
			}

			// Nothing to paint for the spacer. So, no DoPaint override needed.

		}
		#endregion // SpacerShapeField class
		#region SpacerSubField class
		private class SpacerSubField : ShapeSubField
		{
			#region Member Variables
			private Role myAssociatedRole;
			#endregion // Member Variables
			#region Construction
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="associatedRole">The role that this SpacerSubField is associated with.</param>
			public SpacerSubField(Role associatedRole)
			{
				Debug.Assert(associatedRole != null);
				myAssociatedRole = associatedRole;
			}
			#endregion // Construction
			#region Required ShapeSubField overrides
			/// <summary>
			/// Returns true if the fields have the same associated role
			/// </summary>
			public override bool SubFieldEquals(object obj)
			{
				SpacerSubField compareTo;
				if (null != (compareTo = obj as SpacerSubField))
				{
					return myAssociatedRole == compareTo.myAssociatedRole;
				}
				return false;
			}
			/// <summary>
			/// Returns the hash code for the associated role
			/// </summary>
			public override int SubFieldHashCode
			{
				get
				{
					return myAssociatedRole.GetHashCode();
				}
			}
			/// <summary>
			/// A spacer sub field is never selectable, return false regardless of parameters
			/// </summary>
			/// <returns>false</returns>
			public override bool GetSelectable(ShapeElement parentShape, ShapeField parentField)
			{
				return false;
			}
			/// <summary>
			/// A spacer sub field is never focusable, return false regardless of parameters
			/// </summary>
			/// <returns>false</returns>
			public override bool GetFocusable(ShapeElement parentShape, ShapeField parentField)
			{
				return false;
			}
			/// <summary>
			/// Returns bounds based on the size of the parent shape
			/// and the NestedFactVerticalMargin
			/// </summary>
			/// <param name="parentShape">The containing FactTypeShape</param>
			/// <param name="parentField">The containing shape field</param>
			/// <returns>The vertical slice for this role</returns>
			public override RectangleD GetBounds(ShapeElement parentShape, ShapeField parentField)
			{
				RectangleD retVal = parentField.GetBounds(parentShape);
				retVal.Height = NestedFactVerticalMargin;
				return retVal;
			}
			#endregion // Required ShapeSubField overrides
		}
		#endregion // SpacerSubField class
		#region ConstraintShapeField : ShapeField
		private class ConstraintShapeField : ShapeField
		{
			private ConstraintDisplayPosition myConstraintPosition;

			public ConstraintShapeField(ConstraintDisplayPosition constraintPosition)
			{
				DefaultFocusable = true;
				DefaultSelectable = true;
				DefaultVisibility = true;
				myConstraintPosition = constraintPosition;
			}

			/// <summary>
			/// Checks if constraint field is visible.
			/// </summary>
			/// <param name="parentShape">The parent FactTypeShape.</param>
			/// <returns>True if the constraint position of this ConstraintShapeField matches the selected constraint position of the FactTypeShape.</returns>
			public override bool GetVisible(ShapeElement parentShape)
			{
				FactTypeShape factTypeShape = parentShape as FactTypeShape;
				return factTypeShape.ConstraintDisplayPosition == myConstraintPosition;
			}
			/// <summary>
			/// Find the constraint sub shape at this location
			/// </summary>
			/// <param name="point">The point being hit-tested.</param>
			/// <param name="parentShape">The current ShapeField that the mouse is over.</param>
			/// <param name="diagramHitTestInfo">The DiagramHitTestInfo to which the ConstraintSubShapField
			/// will be added if the mouse is over it.</param>
			public override void DoHitTest(PointD point, ShapeElement parentShape, DiagramHitTestInfo diagramHitTestInfo)
			{
				ForHitTest hitTest = new ForHitTest(point, parentShape, this, diagramHitTestInfo);
				FactTypeShape.WalkConstraintBoxes(parentShape, this, hitTest.TestForHit);
			}

			/// <summary>
			/// Handles hit test of the constraint
			/// </summary>
			private class ForHitTest
			{
				private PointD myPoint;
				private ShapeElement myShapeElement;
				private ConstraintShapeField myConstraintShapeField;
				private DiagramHitTestInfo myDiagramHitTestInfo;

				public ForHitTest(PointD point, ShapeElement parentShape, ConstraintShapeField shapeField, DiagramHitTestInfo diagramHitTestInfo)
				{
					myPoint = point;
					myShapeElement = parentShape;
					myConstraintShapeField = shapeField;
					myDiagramHitTestInfo = diagramHitTestInfo;
				}

				/// <summary>
				/// Tests if a specific constraint is at this location.
				/// </summary>
				/// <param name="constraintBox">The constraint to look for</param>
				/// <returns>true</returns>
				public bool TestForHit(ref ConstraintBox constraintBox)
				{
					RectangleD fullBounds = constraintBox.Bounds;
					if (fullBounds.Contains(myPoint))
					{
						IFactConstraint factConstraint = constraintBox.FactConstraint;
						myDiagramHitTestInfo.HitDiagramItem = new DiagramItem(myShapeElement, myConstraintShapeField, new ConstraintSubField(factConstraint.Constraint));
						return false; // Don't continue, we got our item
					}
					return true;
				}
			}

			/// <summary>
			/// Get the minimum width of the ConstraintShapeField.
			/// </summary>
			/// <param name="parentShape">The FactTypeShape that this ConstraintShapeField is associated with.</param>
			/// <returns>The width of the ConstraintShapeField.</returns>
			public override double GetMinimumWidth(ShapeElement parentShape)
			{
				FactTypeShape parent = parentShape as FactTypeShape;
				return parent.RolesShape.GetMinimumWidth(parentShape);
			}

			/// <summary>
			/// Get the minimum height of the ConstraintShapeField.
			/// </summary>
			/// <param name="parentShape">The FactTypeShape that this ConstraintShapeField is associated with.</param>
			/// <returns>The height of the ConstraintShapeField.</returns>
			public override double GetMinimumHeight(ShapeElement parentShape)
			{
				FactTypeShape parent = parentShape as FactTypeShape;
				if (parent.ConstraintDisplayPosition != myConstraintPosition)
				{
					return 0;
				}

				return ForMinimumHeight.CalculateMinimumHeight(parentShape);
			}

			/// <summary>
			/// Helper class for GetMinimumHeight.
			/// </summary>
			private class ForMinimumHeight
			{
				private double minY = double.MaxValue;
				private double maxY = double.MinValue;
				private bool wasVisited = false;

				private ForMinimumHeight() { }
				public static double CalculateMinimumHeight(ShapeElement parentShape)
				{
					ForMinimumHeight fmh = new ForMinimumHeight();
					WalkConstraintBoxes(parentShape, RectangleD.Empty, fmh.VisitBox);
					return fmh.wasVisited ? fmh.maxY - fmh.minY : 0;
				}
				private bool VisitBox(ref ConstraintBox constraintBox)
				{
					wasVisited = true;
					RectangleD bounds = constraintBox.Bounds;
					minY = Math.Min(minY, bounds.Top);
					maxY = Math.Max(maxY, bounds.Bottom);
					return true;
				}
			}

			/// <summary>
			/// Paints the contstraints.
			/// </summary>
			/// <param name="e">DiagramPaintEventArgs with the Graphics object to draw to.</param>
			/// <param name="parentShape">ConstraintShapeField to draw to.</param>
			public override void DoPaint(DiagramPaintEventArgs e, ShapeElement parentShape)
			{
				ForDrawing draw = new ForDrawing(e, parentShape as FactTypeShape);
				FactTypeShape.WalkConstraintBoxes(parentShape, this, draw.DrawConstraint);
			}

			/// <summary>
			/// Helper class for DoPaint().  Handles drawing of the constraint.
			/// </summary>
			private class ForDrawing
			{
				private Graphics myGraphics;
				private HighlightedShapesCollection highlightedShapes;
				private FactTypeShape myParentShapeElement;
				private StyleSet styleSet;
				private float myGap;
				private Pen myConstraintPen;

				/// <summary>
				/// Constructor
				/// </summary>
				/// <param name="e">DiagramPaintEventArgs with the Graphics object to draw to.</param>
				/// <param name="parentShape">ConstraintShapeField to draw to.</param>
				public ForDrawing(DiagramPaintEventArgs e, FactTypeShape parentShape)
				{
					myGraphics = e.Graphics;
					highlightedShapes = e.View.HighlightedShapes;
					myParentShapeElement = parentShape;
					styleSet = myParentShapeElement.StyleSet;
					myConstraintPen = styleSet.GetPen(InternalFactConstraintPen);
					myGap = myConstraintPen.Width;
				}

				/// <summary>
				/// Does the actual drawing of a constraint.
				/// </summary>
				/// <param name="constraintBox">The constraint to draw.</param>
				/// <returns>False if constraint is not an internal uniqueness constraint; otherwise, true.</returns>
				public bool DrawConstraint(ref ConstraintBox constraintBox)
				{
					if (constraintBox.ConstraintType != ConstraintType.InternalUniqueness)
					{
						return false;
					}

					//default variables
					InternalUniquenessConstraintConnectAction activeInternalAction = ActiveInternalUniquenessConstraintConnectAction;
					IFactConstraint factConstraint = constraintBox.FactConstraint;
					IConstraint currentConstraint = factConstraint.Constraint;
					RectangleF boundsF = RectangleD.ToRectangleF(constraintBox.Bounds);
					float verticalPos = boundsF.Top + (float)(ConstraintHeight / 2);
					ConstraintBoxRoleActivity[] rolePosToDraw = constraintBox.ActiveRoles;
					int numRoles = rolePosToDraw.Length;
					float roleWidth = (float)FactTypeShape.RoleBoxWidth;
					bool drawConstraintPreffered = myParentShapeElement.ShouldDrawConstraintPreferred(currentConstraint);
					Color startColor = myConstraintPen.Color;
					DashStyle startDashStyle = myConstraintPen.DashStyle;

					//test if constraint is valid and apply appropriate pen
					if (!constraintBox.IsValid)
					{
						myConstraintPen.Color = ConstraintErrorForeColor;
					}
					if (constraintBox.IsAntiSpanning)
					{
						myConstraintPen.DashStyle = DashStyle.Dash;
					}

					// Draw active constraint highlight
					bool isActiveInternalConstraint = false;
					if (activeInternalAction != null)
					{
						InternalUniquenessConstraint activeInternalConstraint = activeInternalAction.SourceInternalUniquenessConstraint;
						InternalUniquenessConstraint targetConstraint = currentConstraint as InternalUniquenessConstraint;
						isActiveInternalConstraint = object.ReferenceEquals(activeInternalConstraint, targetConstraint);
						if(isActiveInternalConstraint){
							myParentShapeElement.DrawHighlight(myGraphics, boundsF, isActiveInternalConstraint, false);
							myConstraintPen.Color = myParentShapeElement.Diagram.StyleSet.GetPen(ORMDiagram.StickyForegroundResource).Color;
						}
					}

					// test for and draw highlights
					foreach (DiagramItem item in highlightedShapes)
					{
						if (object.ReferenceEquals(myParentShapeElement, item.Shape))
						{
							ConstraintSubField highlightedSubField = item.SubField as ConstraintSubField;
							if (highlightedSubField != null && highlightedSubField.AssociatedConstraint == currentConstraint)
							{
								myParentShapeElement.DrawHighlight(myGraphics, boundsF, isActiveInternalConstraint, true);
								myConstraintPen.Color = ORMDiagram.ModifyLuminosity(myConstraintPen.Color);
								break;
							}
						}
					}

					float startPos = boundsF.Left, endPos = startPos;
					if (constraintBox.IsSpanning || constraintBox.IsAntiSpanning)
					{
						endPos = boundsF.Right;
						//draw fully spanning constraint
						DrawConstraintLine(myGraphics, myConstraintPen, startPos, endPos, verticalPos, drawConstraintPreffered);
					}
					else
					{
						bool constraintHasDrawn = false;
						int i = 0;
						ConstraintBoxRoleActivity currentActivity = rolePosToDraw[i];
						for (; i < numRoles; ++i)
						{
							ConstraintBoxRoleActivity currentBoxActivity = rolePosToDraw[i];
							if (currentActivity != currentBoxActivity)
							{
								//activity has changed; draw previous activity
								if (startPos != endPos && currentActivity != ConstraintBoxRoleActivity.NotInBox)
								{
									if (currentActivity == ConstraintBoxRoleActivity.Active)
									{
										myConstraintPen.DashStyle = startDashStyle;
										constraintHasDrawn = true;
									}
									else
									{
										Debug.Assert(currentActivity == ConstraintBoxRoleActivity.Inactive); // enforces if statement above
										myConstraintPen.DashStyle = DashStyle.Dash;
									}
									//draw constraint
									if(constraintHasDrawn)
									{
										DrawConstraintLine(myGraphics, myConstraintPen, startPos, endPos, verticalPos, drawConstraintPreffered);
									}
									startPos = endPos;
								}
								currentActivity = currentBoxActivity;
							}
							// move to next position
							if (currentActivity != ConstraintBoxRoleActivity.NotInBox)
							{
								endPos += roleWidth;
							}
							else if (boundsF.Width > roleWidth)
							{
								// this covers BinaryRights when not compressing constraints
								startPos += roleWidth;
								endPos = startPos;
							}
						}
						// set DashStyle to original setting (solid)
						if (myConstraintPen.DashStyle != startDashStyle)
						{
							myConstraintPen.DashStyle = startDashStyle;
						}
						//We've reached the end. Draw out any right constraints that may exist.
						if (endPos > startPos && currentActivity == ConstraintBoxRoleActivity.Active)
						{
							DrawConstraintLine(myGraphics, myConstraintPen, startPos, endPos, verticalPos, drawConstraintPreffered);
						}
					}

					// set colors back to normal if they changed
					if (myConstraintPen.Color != startColor)
					{
						myConstraintPen.Color = startColor;
					}
					// set DashStyle to original setting (solid)
					if (myConstraintPen.DashStyle != startDashStyle)
					{
						myConstraintPen.DashStyle = startDashStyle;
					}

					return true;
				}

				/// <summary>
				/// Draws a regular constraint line
				/// </summary>
				/// <param name="g">The graphics object to draw to</param>
				/// <param name="pen">The pen to use</param>
				/// <param name="startPos">The x-coordinate of the left edge to draw at.</param>
				/// <param name="endPos">The x-coordinate of the right edge to draw at.</param>
				/// <param name="verticalPos">The y-coordinate to draw at.</param>
				/// <param name="preferred">Whether or not to draw the constraint as preffered.</param>
				private void DrawConstraintLine(Graphics g, Pen pen, float startPos, float endPos, float verticalPos, bool preferred)
				{
					float gap = myGap;
					if (preferred)
					{
						float vAdjust = gap * .75f;
						g.DrawLine(pen, startPos + gap, verticalPos - vAdjust, endPos - gap, verticalPos - vAdjust);
						g.DrawLine(pen, startPos + gap, verticalPos + vAdjust, endPos - gap, verticalPos + vAdjust);
					}
					else
					{
						g.DrawLine(pen, startPos + gap, verticalPos, endPos - gap, verticalPos);
					}
				}
			}
		}
		#endregion // ConstraintShapeField class
		#region ConstraintSubField class
		private class ConstraintSubField : ShapeSubField
		{
			public override void OnDoubleClick(DiagramPointEventArgs e)
			{
				DiagramClientView clientView = e.DiagramClientView;
				ORMDiagram diagram = clientView.Diagram as ORMDiagram;
				DiagramView activeView = diagram.ActiveDiagramView;
				InternalUniquenessConstraint iuc = AssociatedConstraint as InternalUniquenessConstraint;
				if (iuc != null)
				{
					// Move on to the selection action
					InternalUniquenessConstraintConnectAction iucca = diagram.InternalUniquenessConstraintConnectAction;
					ActiveInternalUniquenessConstraintConnectAction = iucca;
					RoleMoveableCollection roleColl = iuc.RoleCollection;
					FactTypeShape factShape = e.DiagramHitTestInfo.HitDiagramItem.Shape as FactTypeShape;
					if (roleColl.Count > 0)
					{
						IList<Role> iuccaRoles = iucca.SelectedRoleCollection;
						foreach (Role r in roleColl)
						{
							iuccaRoles.Add(r);
						}
					}
					factShape.Invalidate(true);
					iucca.ChainMouseAction(factShape, iuc, clientView);
				}
				e.Handled = true;
				base.OnDoubleClick(e);
			}
			#region Member variables
			private IConstraint myAssociatedConstraint;
			#endregion // Member variables
			#region Construction
			/// <summary>
			/// Default constructor
			/// </summary>
			/// <param name="associatedConstraint">The Constraint that this ConstraintSubfield will represent.</param>
			public ConstraintSubField(IConstraint associatedConstraint)
			{
				Debug.Assert(associatedConstraint != null);
				myAssociatedConstraint = associatedConstraint;
			}
			#endregion // Construction
			#region Required ShapeSubField overrides
			/// <summary>
			/// Returns true if the fields have the same associated role
			/// </summary>
			public override bool SubFieldEquals(object obj)
			{
				ConstraintSubField compareTo;
				if (null != (compareTo = obj as ConstraintSubField))
				{
					return myAssociatedConstraint == compareTo.myAssociatedConstraint;
				}
				return false;
			}
			/// <summary>
			/// Returns the hash code for the associated role
			/// </summary>
			public override int SubFieldHashCode
			{
				get
				{
					return myAssociatedConstraint.GetHashCode();
				}
			}
			/// <summary>
			/// A role sub field is always selectable, return true regardless of parameters
			/// </summary>
			/// <returns>true</returns>
			public override bool GetSelectable(ShapeElement parentShape, ShapeField parentField)
			{
				return true;
			}
			/// <summary>
			/// A role sub field is always focusable, return true regardless of parameters
			/// </summary>
			/// <returns>true</returns>
			public override bool GetFocusable(ShapeElement parentShape, ShapeField parentField)
			{
				return true;
			}
			/// <summary>
			/// Returns bounds based on the size of the parent shape
			/// and the RoleIndex of this shape
			/// </summary>
			/// <param name="parentShape">The containing FactTypeShape</param>
			/// <param name="parentField">The containing shape field</param>
			/// <returns>The vertical slice for this role</returns>
			public override RectangleD GetBounds(ShapeElement parentShape, ShapeField parentField)
			{
				return parentField.GetBounds(parentShape);
			}
			#endregion // Required ShapeSubField
			#region Accessor functions
			/// <summary>
			/// Get the Constraint element associated with this sub field
			/// </summary>
			public IConstraint AssociatedConstraint
			{
				get
				{
					return myAssociatedConstraint;
				}
			}
			#endregion // Accessor functions
		}
		#endregion // ConstraintSubField class
		#region RolesShapeField class
		private class RolesShapeField : ShapeField
		{
			/// <summary>
			/// Construct a default RolesShapeField (Visible, but not selectable or focusable)
			/// </summary>
			public RolesShapeField()
			{
				DefaultFocusable = false;
				DefaultSelectable = false;
				DefaultVisibility = true;
			}
			/// <summary>
			/// Find the role sub shape at this location
			/// </summary>
			/// <param name="point"></param>
			/// <param name="parentShape"></param>
			/// <param name="diagramHitTestInfo"></param>
			public override void DoHitTest(PointD point, ShapeElement parentShape, DiagramHitTestInfo diagramHitTestInfo)
			{
				RectangleD fullBounds = GetBounds(parentShape);
				if (fullBounds.Contains(point))
				{
					FactType factType = (parentShape as FactTypeShape).AssociatedFactType;
					RoleMoveableCollection roles = factType.RoleCollection;
					int roleCount = roles.Count;
					if (roleCount != 0)
					{
						int roleIndex = Math.Min((int)((point.X - fullBounds.Left) * roleCount / fullBounds.Width), roleCount - 1);
						diagramHitTestInfo.HitDiagramItem = new DiagramItem(parentShape, this, new RoleSubField(roles[roleIndex]));
					}
				}
			}
			/// <summary>
			/// Get the minimum width of this RolesShapeField.
			/// </summary>
			/// <param name="parentShape">The FactTypeShape associated with this RolesShapeField.</param>
			/// <returns>The width of this RolesShapeField.</returns>
			public override double GetMinimumWidth(ShapeElement parentShape)
			{
				return FactTypeShape.RoleBoxWidth * Math.Max(1, (parentShape as FactTypeShape).AssociatedFactType.RoleCollection.Count);
			}
			/// <summary>
			/// Get the minimum height of this RolesShapeField.
			/// </summary>
			/// <param name="parentShape">The FactTypeShape associated with this RolesShapeField.</param>
			/// <returns>The height of this RolesShapeField.</returns>
			public override double GetMinimumHeight(ShapeElement parentShape)
			{
				return FactTypeShape.RoleBoxHeight;
			}
			/// <summary>
			/// Paint the RolesShapeField
			/// </summary>
			/// <param name="e">DiagramPaintEventArgs with the Graphics object to draw to.</param>
			/// <param name="parentShape">FactTypeShape to draw to.</param>
			public override void DoPaint(DiagramPaintEventArgs e, ShapeElement parentShape)
			{
				FactTypeShape parentFactShape = parentShape as FactTypeShape;
				FactType factType = parentFactShape.AssociatedFactType;
				RoleMoveableCollection roles = factType.RoleCollection;
				int roleCount = roles.Count;
				bool objectified = factType.NestingType != null;
				if (roleCount > 0 || objectified)
				{
					int highlightRoleBox = -1;
					foreach (DiagramItem item in e.View.HighlightedShapes)
					{
						if (object.ReferenceEquals(parentShape, item.Shape))
						{
							RoleSubField roleField = item.SubField as RoleSubField;
							if (roleField != null)
							{
								highlightRoleBox = roleField.RoleIndex;
								break;
							}
						}
					}
					RectangleD bounds = GetBounds(parentShape);
					Graphics g = e.Graphics;
					double offsetBy = bounds.Width / roleCount;
					float offsetByF = (float)offsetBy;
					double lastX = bounds.Left;
					StyleSet styleSet = parentShape.StyleSet;
					Pen pen = styleSet.GetPen(FactTypeShape.RoleBoxResource);
					int activeRoleIndex;
					float top = (float)bounds.Top;
					float bottom = (float)bounds.Bottom;
					float height = (float)bounds.Height;
					ExternalConstraintConnectAction activeExternalAction = ActiveExternalConstraintConnectAction;
					InternalUniquenessConstraintConnectAction activeInternalAction = ActiveInternalUniquenessConstraintConnectAction;
					ORMDiagram currentDiagram = parentFactShape.Diagram as ORMDiagram;
					StringFormat stringFormat = null;
					Font connectActionFont = null;
					Brush connectActionBrush = null;
					Font constraintSequenceFont = null;
					Brush constraintSequenceBrush = null;
					bool highlightThisRole = false;
					try
					{
						for (int i = 0; i < roleCount; ++i)
						{
							float lastXF = (float)lastX;
							RectangleF roleBounds = new RectangleF(lastXF, top, offsetByF, height);
							highlightThisRole = (i == highlightRoleBox);
							Role currentRole = roles[i];

							// There is an active ExternalConstraintConnectAction, and this role is currently in the action's role set.
							if ((activeExternalAction != null) &&
								(-1 != (activeRoleIndex = activeExternalAction.GetActiveRoleIndex(currentRole))))
							{
								// There is an active ExternalConstraintConnectAction, and this role is currently in the action's role set.
								DrawHighlight(g, styleSet, roleBounds, highlightThisRole);
								if (stringFormat == null)
								{
									stringFormat = new StringFormat();
									stringFormat.LineAlignment = StringAlignment.Center;
									stringFormat.Alignment = StringAlignment.Center;
								}
								if (connectActionFont == null)
								{
									connectActionFont = styleSet.GetFont(DiagramFonts.CommentText);
								}
								if (connectActionBrush == null)
								{
									connectActionBrush = styleSet.GetBrush(RolePickerForeground);
								}
								g.DrawString((activeRoleIndex + 1).ToString(), connectActionFont, connectActionBrush, roleBounds, stringFormat);
							}
							// There is an active InternalUniquenessConstraintConnectAction, and this role is currently in the action's role set.
							else if (activeInternalAction != null && -1 != (activeRoleIndex = activeInternalAction.GetActiveRoleIndex(currentRole)))
							{
								// There is an active InternalUniquenessConstraintConnectAction, and this role is currently in the action's role set.
								DrawHighlight(g, styleSet, roleBounds, highlightThisRole);
							}
							else if (null != currentDiagram)
							{
								// Current diagram is an ORMDiagram.
								#region Handling StickyObject highlighting and selection
								ExternalConstraintShape stickyConstraintShape;
								IConstraint stickyConstraint;
								// The active StickyObject for the diagram is an ExternalConstraintShape
								if (null != (stickyConstraintShape = currentDiagram.StickyObject as ExternalConstraintShape)
									&& null != (stickyConstraint = stickyConstraintShape.AssociatedConstraint))
								{
									ConstraintRoleSequence sequence = null;
									RoleMoveableCollection roleCollection = null;
									bool roleIsInStickyObject = false;

									// Test to see if the diagram's StickyObject (which is an IConstraint) contains a reference to this role.
									foreach (ConstraintRoleSequence c in currentRole.ConstraintRoleSequenceCollection)
									{
										if (object.ReferenceEquals(c.Constraint, stickyConstraint))
										{
											sequence = c;
											roleCollection = sequence.RoleCollection;
											roleIsInStickyObject = true;
											break;
										}
									}

									// This role is in the diagram's StickyObject.
									if (roleIsInStickyObject)
									{
										// We need to find out if this role is in one of the role sequences being edited, or if it's just selected.
										parentFactShape.DrawHighlight(g, roleBounds, true, highlightThisRole);
										Debug.Assert(sequence != null);
										MultiColumnExternalConstraint mcec;
										SingleColumnExternalConstraint scec;
										bool drawIndexNumbers = false;
										string s = "";

										if (activeExternalAction == null)
										{
											drawIndexNumbers = true;
										}
										else
										{
											if (activeExternalAction.InitialRoles.IndexOf(currentRole) < 0)
											{
												drawIndexNumbers = true;
											}
										}
										if (drawIndexNumbers)
										{
											if (null != (mcec = stickyConstraint as MultiColumnExternalConstraint))
											{
												MultiColumnExternalConstraintRoleSequenceMoveableCollection sequenceCollection = mcec.RoleSequenceCollection;
												int y = 0;
												int sequenceCollectionCount = sequenceCollection.Count;
												for (int x = 0; x < sequenceCollectionCount; ++x)
												{
													y = sequenceCollection[x].RoleCollection.IndexOf(currentRole);
													if (y >= 0)
													{
														// Show 1-based position of the role in the MCEC.
														s = string.Concat(s, (x + 1).ToString(), ".", (y + 1).ToString());
														break;
													}
												}
											}
											else if (null != (scec = stickyConstraint as SingleColumnExternalConstraint))
											{
												s = (scec.RoleCollection.IndexOf(currentRole) + 1).ToString();
											}

											if (stringFormat == null)
											{
												stringFormat = new StringFormat();
												stringFormat.LineAlignment = StringAlignment.Center;
												stringFormat.Alignment = StringAlignment.Center;
											}
											if (constraintSequenceFont == null)
											{
												constraintSequenceFont = styleSet.GetFont(RoleBoxResource);
											}
											if (constraintSequenceBrush == null)
											{
												constraintSequenceBrush = currentDiagram.StyleSet.GetBrush(ORMDiagram.StickyForegroundResource);
											}
											g.DrawString(s, constraintSequenceFont, constraintSequenceBrush, roleBounds, stringFormat);
										}
									}
									else if (highlightThisRole)
									{
										parentFactShape.DrawHighlight(g, roleBounds, false, true);
									}
								}
								#endregion // Handling StickyObject highlighting and selection
								else if (highlightThisRole)
								{
									parentFactShape.DrawHighlight(g, roleBounds, false, true);
								}
							}

							// Draw the line between the role boxes
							if (i != 0)
							{
								g.DrawLine(pen, lastXF, top, lastXF, bottom);
							}
							lastX += offsetBy;
						}
					}
					finally
					{
						if (stringFormat != null)
						{
							stringFormat.Dispose();
						}
						if (constraintSequenceFont != null)
						{
							constraintSequenceFont.Dispose();
						}
						if (connectActionFont != null)
						{
							connectActionFont.Dispose();
						}
					}
					// Draw the outside border of the role boxes
					RectangleF boundsF = RectangleD.ToRectangleF(bounds);
					g.DrawRectangle(pen, boundsF.Left, boundsF.Top, boundsF.Width, boundsF.Height);
				}
			}
			/// <summary>
			/// Draws a role highlight.
			/// </summary>
			/// <param name="g">The Graphics object to draw to.</param>
			/// <param name="styleSet">The StyleSet of the shape we are drawing to.</param>
			/// <param name="bounds">The bounds to draw as the highlight.</param>
			/// <param name="active">Boolean indicating whether or not to draw highlight as active (ex: the mouse is currently over this highlight).</param>
			protected void DrawHighlight(Graphics g, StyleSet styleSet, RectangleF bounds, bool active)
			{
				Brush brush = styleSet.GetBrush(RoleBoxResource);
				Color startColor;
				SolidBrush coloredBrush = null;
				if (!SystemInformation.HighContrast && active)
				{
					coloredBrush = brush as SolidBrush;
					if (coloredBrush != null)
					{
						startColor = coloredBrush.Color;
						coloredBrush.Color = ORMDiagram.ModifyLuminosity(coloredBrush.Color);
					}
				}
				g.FillRectangle(brush, bounds);
				if (coloredBrush != null)
				{
					coloredBrush.Color = startColor;
				}
			}
		}
		#endregion // RolesShapeField class
		#region RoleSubField class
		private class RoleSubField : ShapeSubField
		{
			#region Member variables
			private Role myAssociatedRole;
			#endregion // Member variables
			#region Construction
			public RoleSubField(Role associatedRole)
			{
				Debug.Assert(associatedRole != null);
				myAssociatedRole = associatedRole;
			}
			#endregion // Construction
			#region Required ShapeSubField overrides
			/// <summary>
			/// Returns true if the fields have the same associated role
			/// </summary>
			public override bool SubFieldEquals(object obj)
			{
				RoleSubField compareTo;
				if (null != (compareTo = obj as RoleSubField))
				{
					return myAssociatedRole == compareTo.myAssociatedRole;
				}
				return false;
			}
			/// <summary>
			/// Returns the hash code for the associated role
			/// </summary>
			public override int SubFieldHashCode
			{
				get
				{
					return myAssociatedRole.GetHashCode();
				}
			}
			/// <summary>
			/// A role sub field is always selectable, return true regardless of parameters
			/// </summary>
			/// <returns>true</returns>
			public override bool GetSelectable(ShapeElement parentShape, ShapeField parentField)
			{
				return true;
			}
			/// <summary>
			/// A role sub field is always focusable, return true regardless of parameters
			/// </summary>
			/// <returns>true</returns>
			public override bool GetFocusable(ShapeElement parentShape, ShapeField parentField)
			{
				return true;
			}
			/// <summary>
			/// Returns bounds based on the size of the parent shape
			/// and the RoleIndex of this shape
			/// </summary>
			/// <param name="parentShape">The containing FactTypeShape</param>
			/// <param name="parentField">The containing shape field</param>
			/// <returns>The vertical slice for this role</returns>
			public override RectangleD GetBounds(ShapeElement parentShape, ShapeField parentField)
			{
				RectangleD retVal = parentField.GetBounds(parentShape);
				RoleMoveableCollection roles = myAssociatedRole.FactType.RoleCollection;
				retVal.Width /= roles.Count;
				int roleIndex = roles.IndexOf(myAssociatedRole);
				if (roleIndex > 0)
				{
					retVal.Offset(roleIndex * retVal.Width, 0);
				}
				return retVal;
			}
			#endregion // Required ShapeSubField
			#region DragDrop support
			public override MouseAction GetPotentialMouseAction(MouseButtons mouseButtons, PointD point, DiagramHitTestInfo hitTestInfo)
			{
				if (mouseButtons == MouseButtons.Left)
				{
					return ((ORMDiagram)hitTestInfo.DiagramClientView.Diagram).RoleDragPendingAction;
				}
				return base.GetPotentialMouseAction(mouseButtons, point, hitTestInfo);
			}
			#endregion // DragDrop support
			#region Accessor functions
			/// <summary>
			/// Get the Role element associated with this sub field
			/// </summary>
			public Role AssociatedRole
			{
				get
				{
					return myAssociatedRole;
				}
			}
			/// <summary>
			/// Returns the index of the associated Role element in its
			/// containing collection.
			/// </summary>
			public int RoleIndex
			{
				get
				{
					Debug.Assert(myAssociatedRole != null && !myAssociatedRole.IsRemoved);
					return myAssociatedRole.FactType.RoleCollection.IndexOf(myAssociatedRole);
				}
			}
			#endregion // Accessor functions
		}
		#endregion // RoleSubField class
		#region Member Variables
		private static RolesShapeField myRolesShapeField = null;
		private static ConstraintShapeField myTopConstraintShapeField = null;
		private static ConstraintShapeField myBottomConstraintShapeField = null;
		/// <summary>
		/// Pen to draw a role box outline
		/// </summary>
		protected static readonly StyleSetResourceId RoleBoxResource = new StyleSetResourceId("Northface", "RoleBoxResource");
		/// <summary>
		/// Brush to draw the foreground text for a role picker  
		/// </summary>
		protected static readonly StyleSetResourceId RolePickerForeground = new StyleSetResourceId("Northface", "RolePickerForeground");
		/// <summary>
		/// Pen to draw the active part of an internal uniqueness constraint.
		/// </summary>
		protected static readonly StyleSetResourceId InternalFactConstraintPen = new StyleSetResourceId("Northface", "InternalFactConstraintPen");
		/// <summary>
		/// The color to use when drawing constraint errors.
		/// </summary>
		protected static readonly Color ConstraintErrorForeColor = ORMDesignerPackage.FontAndColorService.GetForeColor(ORMDesignerColor.ConstraintError);
		private static ExternalConstraintConnectAction myActiveExternalConstraintConnectAction;
		private static InternalUniquenessConstraintConnectAction myActiveInternalUniquenessConstraintConnectAction;
		#endregion // Member Variables
		#region RoleSubField integration
		/// <summary>
		/// Get the role corresponding to the given subField
		/// </summary>
		/// <param name="shapeField">The containing shape field (will always be the RolesShapeField)</param>
		/// <param name="subField">A RoleSubField</param>
		/// <returns>A Role element</returns>
		public override ICollection GetSubFieldRepresentedElements(ShapeField shapeField, ShapeSubField subField)
		{
			RoleSubField roleField;
			if (null != (roleField = subField as RoleSubField))
			{
				return new ModelElement[] { roleField.AssociatedRole };
			}
			ConstraintSubField constraintSubField;
			if (null != (constraintSubField = subField as ConstraintSubField))
			{
				return new ModelElement[] { (ModelElement)constraintSubField.AssociatedConstraint };
			}
			return null;
		}
		/// <summary>
		/// The roles shape field is the default and only shape field inside
		/// a FactType shape.
		/// </summary>
		public override ShapeField DefaultShapeField
		{
			get
			{
				return myRolesShapeField;
			}
		}
		#endregion // RoleSubField integration
		#region Customize appearance
		/// <summary>
		/// Standard method to draw a consistent highlight within the FactTypeShape.
		/// </summary>
		/// <param name="g">The Graphics object to draw to.</param>
		/// <param name="bounds">The bounds of the highlight to draw.</param>
		/// <param name="isStuck">Bool indicating if the object to draw the highlight on
		/// is currently the "sticky" object.</param>
		/// <param name="isHighlighted">Bool indicating if object should be drawn highlighted.</param>
		protected void DrawHighlight(Graphics g, RectangleF bounds, bool isStuck, bool isHighlighted)
		{
			Brush brush;
			if (isStuck)
			{
				brush = Diagram.StyleSet.GetBrush(ORMDiagram.StickyBackgroundResource);
			}
			else
			{
				brush = StyleSet.GetBrush(DiagramBrushes.ShapeBackground);
			}
			Color startColor;
			SolidBrush coloredBrush = null;
			if (!SystemInformation.HighContrast && isHighlighted)
			{
				coloredBrush = brush as SolidBrush;
				if (coloredBrush != null)
				{
					startColor = coloredBrush.Color;
					coloredBrush.Color = ORMDiagram.ModifyLuminosity(coloredBrush.Color);
				}
			}
			g.FillRectangle(brush, bounds);
			if (coloredBrush != null)
			{
				coloredBrush.Color = startColor;
			}
		}
		/// <summary>
		/// Set to true. Enables role highlighting
		/// </summary>
		public override bool HasSubFieldHighlighting
		{
			get
			{
				return true;
			}
		}
		/// <summary>
		/// Set the default size for this object. This value is basically
		/// ignored because the size is ultimately based on the contained
		/// text, but it needs to be set.
		/// </summary>
		public override SizeD DefaultSize
		{
			get
			{
				return new SizeD(.7, .35);
			}
		}
		/// <summary>
		/// Change the outline pen to a thin black line for all instances
		/// of this shape.
		/// </summary>
		/// <param name="classStyleSet">The style set to modify</param>
		protected override void InitializeResources(StyleSet classStyleSet)
		{
			ORMDesignerFontsAndColors fontsAndColors = ORMDesignerPackage.FontAndColorService;
			Color constraintForeColor = fontsAndColors.GetForeColor(ORMDesignerColor.Constraint);
			Color rolePickerForeColor = fontsAndColors.GetForeColor(ORMDesignerColor.RolePicker);
			Color rolePickerBackColor = fontsAndColors.GetBackColor(ORMDesignerColor.RolePicker);

			BrushSettings brushSettings = new BrushSettings();
			brushSettings.Color = rolePickerForeColor;
			classStyleSet.AddBrush(RolePickerForeground, DiagramBrushes.DiagramBackground, brushSettings);

			brushSettings.Color = rolePickerBackColor;
			classStyleSet.AddBrush(RoleBoxResource, DiagramBrushes.DiagramBackground, brushSettings);

			PenSettings penSettings = new PenSettings();
			penSettings.Color = SystemColors.WindowText;
			penSettings.Width = 1.0F / 72.0F; // 1 Point. 0 Means 1 pixel, but should only be used for non-printed items
			penSettings.Alignment = PenAlignment.Center;
			classStyleSet.AddPen(RoleBoxResource, DiagramPens.ShapeOutline, penSettings);

			penSettings.Color = constraintForeColor;
			classStyleSet.AddPen(InternalFactConstraintPen, DiagramPens.ShapeOutline, penSettings);

			FontSettings fontSettings = new FontSettings();
			fontSettings.Size = 5f / 72f; // 5 Point.
			classStyleSet.AddFont(RoleBoxResource, DiagramFonts.CommentText, fontSettings);
		}
		/// <summary>
		/// Use the rolebox outline pen unless we're objectified
		/// </summary>
		public override StyleSetResourceId OutlinePenId
		{
			get
			{
				return IsObjectified ? DiagramPens.ShapeOutline : RoleBoxResource;
			}
		}
		/// <summary>
		/// Create our one placeholder shape field, which fills the whole shape
		/// and contains our role boxes.
		/// </summary>
		/// <param name="shapeFields">Per-class collection of shape fields</param>
		protected override void InitializeShapeFields(ShapeFieldCollection shapeFields)
		{
			base.InitializeShapeFields(shapeFields);

			// Initialize fields
			RolesShapeField field = new RolesShapeField();
			ConstraintShapeField topConstraintField = new ConstraintShapeField(ConstraintDisplayPosition.Top);
			ConstraintShapeField bottomConstraintField = new ConstraintShapeField(ConstraintDisplayPosition.Bottom);
			SpacerShapeField spacer = new SpacerShapeField();

			// Add all shapes before modifying anchoring behavior
			shapeFields.Add(spacer);
			shapeFields.Add(topConstraintField);
			shapeFields.Add(bottomConstraintField);
			shapeFields.Add(field);

			// Modify anchoring behavior
			AnchoringBehavior bottomConstraintAnchor = bottomConstraintField.AnchoringBehavior;
			bottomConstraintAnchor.CenterHorizontally();
			bottomConstraintAnchor.SetTopAnchor(field, 1);

			AnchoringBehavior anchor = field.AnchoringBehavior;
			anchor.CenterHorizontally();
			anchor.SetTopAnchor(topConstraintField, 1);

			AnchoringBehavior topConstraintAnchor = topConstraintField.AnchoringBehavior;
			topConstraintAnchor.CenterHorizontally();
			topConstraintAnchor.SetTopAnchor(spacer, 1);

			AnchoringBehavior spacerAnchor = spacer.AnchoringBehavior;
			spacerAnchor.CenterHorizontally();

			// Do not modify set edge anchors in this case. Edge anchors
			// force the bounds of the text field to the size of the parent,
			// we want it the other way around.

			Debug.Assert(myRolesShapeField == null); // Only called once
			myRolesShapeField = field;

			Debug.Assert(myTopConstraintShapeField == null); // Only called once
			myTopConstraintShapeField = topConstraintField;

			Debug.Assert(myBottomConstraintShapeField == null); // Only called once
			myBottomConstraintShapeField = bottomConstraintField;
		}
		/// <summary>
		/// The shape field used to display roles
		/// </summary>
		protected ShapeField RolesShape
		{
			get
			{
				return myRolesShapeField;
			}
		}
		/// <summary>
		/// Highlight region surrounding the roles box if
		/// it is objectified
		/// </summary>
		/// <value>True if the fact type is nested</value>
		public override bool HasHighlighting
		{
			get
			{
				return IsObjectified;
			}
		}
		/// <summary>
		/// Show an outline around the fact type only
		/// if it is objectified.
		/// </summary>
		/// <value>True if the fact type is nested</value>
		public override bool HasOutline
		{
			get
			{
				return IsObjectified;
			}
		}
		/// <summary>
		/// Set the content size of the FactTypeShape
		/// </summary>
		protected override SizeD ContentSize
		{
			get
			{
				// Margin is used to adjust the width and height of the content to incorporate the
				// width of the pen being used and prevent the pen from being cropped at the edges
				// of the content.
				double margin = this.StyleSet.GetPen(FactTypeShape.RoleBoxResource).Width;
				SizeD retVal = SizeD.Empty;
				ShapeField rolesShape = RolesShape;
				if (rolesShape != null)
				{
					double width, height;
					width = rolesShape.GetMinimumWidth(this) + margin;
					height = rolesShape.GetMinimumHeight(this) + margin;
					if (IsObjectified)
					{
						height += myTopConstraintShapeField.GetMinimumHeight(this) + myBottomConstraintShapeField.GetMinimumHeight(this);
					}
					else
					{
						if (this.ConstraintDisplayPosition == ConstraintDisplayPosition.Top)
						{
							height += myTopConstraintShapeField.GetMinimumHeight(this);
						}
						else
						{
							height += myBottomConstraintShapeField.GetMinimumHeight(this);
						}
					}
					retVal = new SizeD(width, height);
				}
				return retVal;
			}
		}
		/// <summary>
		/// Size to ContentSize plus some margin padding if we're a nested fact type.
		/// </summary>
		public override void AutoResize()
		{
			SizeD contentSize = ContentSize;
			if (!contentSize.IsEmpty)
			{
				if (IsObjectified)
				{
					contentSize.Width += NestedFactHorizontalMargin + NestedFactHorizontalMargin;
					contentSize.Height += NestedFactVerticalMargin + NestedFactVerticalMargin;
				}
			}
			Size = contentSize;
		}
		/// <summary>
		/// Called during a transaction when a new constraint
		/// is added or removed that is associated with this fact.
		/// </summary>
		/// <param name="constraint">The newly added or removed constraint</param>
		public void ConstraintSetChanged(IConstraint constraint)
		{
			Debug.Assert(Store.TransactionManager.InTransaction);
			bool resize = false;
			switch (constraint.ConstraintType)
			{
				case ConstraintType.InternalUniqueness:
					resize = true;
					break;
			}
			if (resize)
			{
				AutoResize();
				Invalidate(true);
			}
		}
		/// <summary>
		/// Return different shapes for objectified versus non-objectified fact types.
		/// The actual shape is controlled by the tools options page.
		/// </summary>
		public override ShapeGeometry ShapeGeometry
		{
			get
			{
				// If the fact is objectified, get the current setting from the options
				// page for how to draw the shape
				if (IsObjectified)
				{
					ShapeGeometry useShape;
					switch (Shell.OptionsPage.CurrentObjectifiedFactShape)
					{
						case Shell.ObjectifiedFactShape.HardRectangle:
							useShape = CustomFoldRectangleShapeGeometry.ShapeGeometry;
							break;
						case Shell.ObjectifiedFactShape.SoftRectangle:
						default:
							useShape = CustomFoldRoundedRectangleShapeGeometry.ShapeGeometry;
							break;
					}
					return useShape;
				}
				else
				{
					// Just draw a rectangle if the fact IS NOT objectified
					return CustomFoldRectangleShapeGeometry.ShapeGeometry;
				}
			}
		}
		/// <summary>
		/// Add a shape element linked to this parent to display the name
		/// of the objectifying type
		/// </summary>
		/// <param name="element">ModelElement of type ObjectType</param>
		/// <returns>true</returns>
		protected override bool ShouldAddShapeForElement(ModelElement element)
		{
			Debug.Assert(
					(element is ObjectType && ((ObjectType)element).NestedFactType == AssociatedFactType)
					|| (element is ReadingOrder && ((ReadingOrder)element).FactType == AssociatedFactType)
				);
			ReadingOrder ord;
			if (null != (ord = element as ReadingOrder))
			{
				//prevent reading orders that are different from the currently displayed one
				//from being added to the view.
				if (!object.ReferenceEquals(FactType.FindMatchingReadingOrder(AssociatedFactType), ord))
				{
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// An object type is displayed as an ObjectTypeShape unless it is
		/// objectified, in which case we display it as an ObjectifiedFactTypeNameShape
		/// </summary>
		/// <param name="element">The element to test. Expecting an ObjectType.</param>
		/// <param name="shapeTypes">The choice of shape types</param>
		/// <returns></returns>
		protected override MetaClassInfo ChooseShape(ModelElement element, IList shapeTypes)
		{
			Guid classId = element.MetaClassId;
			if (classId == ObjectType.MetaClassGuid)
			{
				return ORMDiagram.ChooseShapeTypeForObjectType((ObjectType)element, shapeTypes);
			}
			Debug.Assert(false); // We're only expecting an ObjectType here
			return base.ChooseShape(element, shapeTypes);
		}
		/// <summary>
		/// Make an ObjectifiedFactTypeNameShape a relative child element
		/// </summary>
		/// <param name="childShape"></param>
		/// <returns></returns>
		protected override RelationshipType ChooseRelationship(ShapeElement childShape)
		{
			Debug.Assert(childShape is ObjectifiedFactTypeNameShape || childShape is ReadingShape);
			return RelationshipType.Relative;
		}
		#endregion // Customize appearance
		#region Customize property display
		#region Reusable helper class for custom property descriptor creation
		/// <summary>
		/// A helper class to enable an object to be displayed as expandable,
		/// and have one string attribute specified as an editable string.
		/// </summary>
		private abstract class ExpandableStringConverter : ExpandableObjectConverter
		{
			/// <summary>
			/// Allow conversion from a string
			/// </summary>
			/// <param name="context">ITypeDescriptorContext</param>
			/// <param name="sourceType">Type</param>
			/// <returns>true for a string type</returns>
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string);
			}
			/// <summary>
			/// Allow conversion to a string. Note that the base class
			/// handles the ConvertTo function for us.
			/// </summary>
			/// <param name="context">ITypeDescriptorContext</param>
			/// <param name="destinationType">Type</param>
			/// <returns>true for a stirng type</returns>
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string);
			}
			/// <summary>
			/// Convert from a string to the specified string
			/// meta attribute on the context element.
			/// </summary>
			/// <param name="context">ITypeDescriptorContext</param>
			/// <param name="culture">CultureInfo</param>
			/// <param name="value">New value for the attribute</param>
			/// <returns>context.Instance for a string value, defers to base otherwise</returns>
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string stringValue = value as string;
				if (stringValue != null)
				{
					object instance = context.Instance;
					ModelElement element = ConvertContextToElement(context);
					if (element != null)
					{
						MetaAttributeInfo attrInfo = element.Store.MetaDataDirectory.FindMetaAttribute(PrimaryStringAttributeId);
						// This will recurse when the property descriptor is changed because the
						// transaction close will refresh the property browser. Make sure we don't
						// fire a second SetValue here so we only get one item on the undo stack.
						if (stringValue != (string)element.GetAttributeValue(attrInfo))
						{
							// We want exactly the same result as achieved by setting
							// the property directly in the property grid, so create a property
							// descriptor to do the actual work of setting the property inside
							// a transaction.
							element.CreatePropertyDescriptor(attrInfo, element).SetValue(element, stringValue);
						}
					}
					return instance;
				}
				else
				{
					return base.ConvertFrom(context, culture, value);
				}
			}
			/// <summary>
			/// Override to retrieve the ModelElement to modify from the context
			/// information.
			/// </summary>
			/// <param name="context">ITypeDescriptorContext</param>
			/// <returns>ModelElement</returns>
			protected abstract ModelElement ConvertContextToElement(ITypeDescriptorContext context);
			/// <summary>
			/// Override to specify the string property to represent
			/// as the string value for the object. Defaults to
			/// NamedElement.NameMetaAttributeGuid.
			/// </summary>
			/// <value></value>
			protected virtual Guid PrimaryStringAttributeId
			{
				get
				{
					return NamedElement.NameMetaAttributeGuid;
				}
			}
		}
		/// <summary>
		/// A property descriptor implementation to
		/// use a ModelElement as an attribute
		/// in the property grid. Use with a realized
		/// ExpandableStringConverter instance to create
		/// an expandable property with an editable text field.
		/// </summary>
		private class HeaderDescriptor : PropertyDescriptor
		{
			private ModelElement myWrappedElement;
			private TypeConverter myConverter;
			/// <summary>
			/// Create a descriptor for the specified element and
			/// type converter.
			/// </summary>
			/// <param name="wrapElement">ModelElement</param>
			/// <param name="converter">TypeConverter (can be null)</param>
			public HeaderDescriptor(ModelElement wrapElement, TypeConverter converter) : base(wrapElement.GetComponentName(), new Attribute[]{})
			{
				myWrappedElement = wrapElement;
				myConverter = converter;
			}
			/// <summary>
			/// Return the converter specified in the constructor
			/// </summary>
			public override TypeConverter Converter
			{
				get
				{
					return myConverter;
				}
			}
			/// <summary>
			/// Use the underlying class name as the display name
			/// </summary>
			public override string DisplayName
			{
				get { return myWrappedElement.GetClassName(); }
			}
			/// <summary>
			/// Return this object as the component type
			/// </summary>
			public override Type ComponentType
			{
				get { return typeof(HeaderDescriptor); }
			}
			/// <summary>
			/// Returns false
			/// </summary>
			public override bool IsReadOnly
			{
				get { return false; }
			}
			/// <summary>
			/// Specify the type of the wrapped element
			/// as the PropertyType
			/// </summary>
			public override Type PropertyType
			{
				get { return myWrappedElement.GetType(); }
			}
			/// <summary>
			/// Disallow resetting the value
			/// </summary>
			/// <param name="component">object</param>
			/// <returns>false</returns>
			public override bool CanResetValue(object component)
			{
				return false;
			}
			/// <summary>
			/// Return the wrapped element as the property value
			/// </summary>
			/// <param name="component">object (ignored)</param>
			/// <returns>wrapElement value specified in constructor</returns>
			public override object GetValue(object component)
			{
				return myWrappedElement;
			}
			/// <summary>
			/// Do not serialize
			/// </summary>
			/// <param name="component"></param>
			/// <returns></returns>
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}
			/// <summary>
			/// Do not reset
			/// </summary>
			/// <param name="component"></param>
			public override void ResetValue(object component)
			{
			}
			/// <summary>
			/// Do nothing. All value setting in this case
			/// is done by the type converter.
			/// </summary>
			/// <param name="component">object</param>
			/// <param name="value">object</param>
			public override void SetValue(object component, object value)
			{
			}
		}
		#endregion //Reusable helper class for custom property descriptor creation
		#region Nested FactType-specific type converters
		/// <summary>
		/// A type converter for showing the raw fact type
		/// as an expandable property in a nested fact type.
		/// </summary>
		private class ObjectifiedFactPropertyConverter : ExpandableStringConverter
		{
			public static readonly TypeConverter Converter = new ObjectifiedFactPropertyConverter();
			private ObjectifiedFactPropertyConverter() { }
			/// <summary>
			/// Convert from a FactTypeShape to a FactType
			/// </summary>
			/// <param name="context">ITypeDescriptorContext</param>
			/// <returns></returns>
			protected override ModelElement ConvertContextToElement(ITypeDescriptorContext context)
			{
				FactTypeShape shape = context.Instance as FactTypeShape;
				FactType factType;
				if (null != (shape = context.Instance as FactTypeShape) &&
					null != (factType = shape.AssociatedFactType))
				{
					return factType;
				}
				return null;
			}
		}
		/// <summary>
		/// A type converter for showing the nesting type
		/// as an expandable property in a nested fact type.
		/// </summary>
		private class ObjectifyingEntityTypePropertyConverter : ExpandableStringConverter
		{
			public static readonly TypeConverter Converter = new ObjectifyingEntityTypePropertyConverter();
			private ObjectifyingEntityTypePropertyConverter() { }
			/// <summary>
			/// Convert from a FactTypeShape to the nesting EntityType
			/// </summary>
			/// <param name="context">ITypeDescriptorContext</param>
			/// <returns></returns>
			protected override ModelElement ConvertContextToElement(ITypeDescriptorContext context)
			{
				FactTypeShape shape = context.Instance as FactTypeShape;
				FactType factType;
				if (null != (shape = context.Instance as FactTypeShape) &&
					null != (factType = shape.AssociatedFactType))
				{
					return factType.NestingType;
				}
				return null;
			}
		}
		#endregion // Nested FactType-specific type converters
		/// <summary>
		/// Show selected properties from the nesting type and the
		/// fact type for an objectified type, as well as expandable
		/// nodes for each of the underlying instances.
		/// </summary>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			FactType factType = AssociatedFactType;
			ObjectType nestingType = (factType == null) ? null : factType.NestingType;
			if (nestingType != null)
			{
				MetaDataDirectory metaDir = factType.Store.MetaDataDirectory;
				return new PropertyDescriptorCollection(new PropertyDescriptor[]{
					this.CreatePropertyDescriptor(metaDir.FindMetaAttribute(FactTypeShape.ConstraintDisplayPositionMetaAttributeGuid), this),
					nestingType.CreatePropertyDescriptor(metaDir.FindMetaAttribute(NamedElement.NameMetaAttributeGuid), nestingType),
					nestingType.CreatePropertyDescriptor(metaDir.FindMetaAttribute(ObjectType.IsIndependentMetaAttributeGuid), nestingType),
					new HeaderDescriptor(factType, ObjectifiedFactPropertyConverter.Converter),
					new HeaderDescriptor(nestingType, ObjectifyingEntityTypePropertyConverter.Converter),
					});
			}
			else
			{
				return base.GetProperties(attributes);
			}
		}
		#endregion // Customize property display
		#region ICustomShapeFolding implementation
		/// <summary>
		/// Implements ICustomShapeFolding.CalculateConnectionPoint
		/// </summary>
		/// <param name="oppositeShape">The opposite shape we're connecting to</param>
		/// <returns>The point to connect to. May be internal to the object, or on the boundary.</returns>
		protected PointD CalculateConnectionPoint(NodeShape oppositeShape)
		{
			ObjectTypeShape objectShape;
			FactTypeShape factShape;
			ExternalConstraintShape constraintShape;
			FactType factType = null;
			ObjectType objectType = null;
			int factRoleCount = 0;
			int roleIndex = -1;
			bool attachBeforeRole = false; // If true, attach before roleIndex, not in the middle of it
			if (null != (factShape = oppositeShape as FactTypeShape))
			{
				FactType oppositeFactType = factShape.AssociatedFactType;
				if (oppositeFactType != null)
				{
					factType = AssociatedFactType;
					objectType = oppositeFactType.NestingType;
				}
			}
			else if (null != (objectShape = oppositeShape as ObjectTypeShape))
			{
				factType = AssociatedFactType;
				objectType = objectShape.AssociatedObjectType;
			}
			else if (null != (constraintShape = oppositeShape as ExternalConstraintShape))
			{
				IConstraint constraint = constraintShape.AssociatedConstraint;
				factType = AssociatedFactType;
				if (factType != null)
				{
					SingleColumnExternalConstraint scec;
					MultiColumnExternalConstraint mcec;
					IList factConstraints = null;
					IList<Role> roles = null;
					if (null != (scec = constraint as SingleColumnExternalConstraint))
					{
						factConstraints = scec.GetElementLinks(SingleColumnExternalFactConstraint.SingleColumnExternalConstraintCollectionMetaRoleGuid);
					}
					else if (null != (mcec = constraint as MultiColumnExternalConstraint))
					{
						factConstraints = mcec.GetElementLinks(MultiColumnExternalFactConstraint.MultiColumnExternalConstraintCollectionMetaRoleGuid);
					}
					if (factConstraints != null)
					{
						int factConstraintCount = factConstraints.Count;
						for (int i = 0; i < factConstraintCount; ++i)
						{
							IFactConstraint factConstraint = (IFactConstraint)factConstraints[i];
							if (object.ReferenceEquals(factConstraint.FactType, factType))
							{
								roles = factConstraint.RoleCollection;
								break;
							}
						}
						if (roles != null)
						{
							RoleMoveableCollection factRoles = factType.RoleCollection;
							factRoleCount = factRoles.Count;

							switch (roles.Count)
							{
								case 1:
									roleIndex = factRoles.IndexOf(roles[0]);
									break;
								case 2:
									int index1 = factRoles.IndexOf(roles[0]);
									int index2 = factRoles.IndexOf(roles[1]);
									if (Math.Abs(index1 - index2) > 1)
									{
										goto default;
									}
									roleIndex = (index1 + index2 + 1) / 2;
									attachBeforeRole = true;
									break;
								default:
									// UNDONE: This is where the constraint box walking needs to
									// come into play. We need to draw subfields that span multiple roles
									// as well as determining where they connect. Just connect to the middle
									// for now to indicate a problem.
									RectangleD factBox = myRolesShapeField.GetBounds(this); // This finds the role box for both objectified and simple fact types
									factBox.Offset(AbsoluteBoundingBox.Location);
									return factBox.Center;
							}
						}
					}
				}
			}
			if (factType != null && objectType != null)
			{
				RoleMoveableCollection roles = factType.RoleCollection;
				factRoleCount = roles.Count;
				Role role = null;
				for (int i = 0; i < factRoleCount; ++i)
				{
					role = (Role)roles[i];
					if (object.ReferenceEquals(role.RolePlayer, objectType))
					{
						// UNDONE: Note that this where the data passed to DoFoldToShape
						// is insufficient. Unless we're given the specific link object
						// we're dealing with, there is no way to tell which role we're
						// on when the role player is shared by multiple roles.
						roleIndex = i;
						break;
					}
				}
			}
			if (roleIndex != -1)
			{
				PointD objCenter = oppositeShape.AbsoluteCenter;
				RectangleD factBox = myRolesShapeField.GetBounds(this); // This finds the role box for both objectified and simple fact types
				factBox.Offset(AbsoluteBoundingBox.Location);

				// Decide whether top or bottom works best
				double finalY = (Math.Abs(objCenter.Y - factBox.Top) <= Math.Abs(objCenter.Y - factBox.Bottom)) ? factBox.Top : factBox.Bottom;

				// Find the left/right position
				double roleWidth = factBox.Width / factRoleCount;
				double finalX = factBox.Left + roleWidth * (roleIndex + (attachBeforeRole ? 0 : .5));

				if (!attachBeforeRole)
				{
					// If we're the first or last (or both) role, then
					// prefer an edge attach point.
					PointD testCenter = PointD.Empty;
					if (factRoleCount == 1)
					{
						testCenter = factBox.Center;
					}
					else if (roleIndex == 0)
					{
						if (objCenter.X < factBox.Left)
						{
							testCenter = new PointD(factBox.Left + roleWidth * .5, factBox.Center.Y);
						}
					}
					else if (roleIndex == (factRoleCount - 1))
					{
						if (objCenter.X > factBox.Right)
						{
							testCenter = new PointD(factBox.Right - roleWidth * .5, factBox.Center.Y);
						}
					}
					if (!testCenter.IsEmpty)
					{
						// Compare the slope to a single role box height/width to see
						// if we should connect to the edge or the top/bottom
						double run = objCenter.X - testCenter.X;
						if (!VGConstants.FuzzZero(run, VGConstants.FuzzDistance))
						{
							double slope = (objCenter.Y - testCenter.Y) / run;
							if (Math.Abs(slope) < (factBox.Height / roleWidth))
							{
								finalY = testCenter.Y;
								// The line coming in is flatter than the line
								// across opposite corners of the role box,
								// connect to the left/right edge
								if (factRoleCount == 1)
								{
									finalX = (objCenter.X < factBox.Left) ? factBox.Left : factBox.Right;
								}
								else if (roleIndex == 0)
								{
									finalX = factBox.Left;
								}
								else if (roleIndex == (factRoleCount - 1))
								{
									finalX = factBox.Right;
								}
							}
						}

					}
				}
				return new PointD(finalX, finalY);
			}
			return AbsoluteCenter;
		}
		PointD ICustomShapeFolding.CalculateConnectionPoint(NodeShape oppositeShape)
		{
			return CalculateConnectionPoint(oppositeShape);
		}
		#endregion // ICustomShapeFolding implementation
		#region IModelErrorActivation Implementation
		/// <summary>
		/// Implements IModelErrorActivation.ActivateModelError
		/// </summary>
		/// <param name="error">Activated model error</param>
		protected void ActivateModelError(ModelError error)
		{
			TooFewReadingRolesError tooFew;
			TooManyReadingRolesError tooMany;
			Reading reading = null;
			if (null != (tooFew = error as TooFewReadingRolesError))
			{
				reading = tooFew.Reading;
			}
			else if (null != (tooMany = error as TooManyReadingRolesError))
			{
				reading = tooMany.Reading;
			}
			if (reading != null)
			{
				//UNDONE: add code to have it select it in the tree and highlight the correct row and start the edit
				ORMReadingEditorToolWindow window = ORMDesignerPackage.ReadingEditorWindow;
				window.Show();

			}
		}
		void IModelErrorActivation.ActivateModelError(ModelError error)
		{
			ActivateModelError(error);
		}
		#endregion // IModelErrorActivation Implementation
		#region FactTypeShape specific
		/// <summary>
		/// Get the FactType associated with this shape
		/// </summary>
		public FactType AssociatedFactType
		{
			get
			{
				return ModelElement as FactType;
			}
		}
		/// <summary>
		/// Return true if the associated fact type is an objectified fact
		/// </summary>
		public bool IsObjectified
		{
			get
			{
				FactType factType = AssociatedFactType;
				return (factType == null) ? false : (factType.NestingType != null);
			}
		}

		/// <summary>
		/// Gets the bounds of the specified InternalUniquenessConstraint within the FactTypeShape.
		/// </summary>
		/// <param name="constraint">The InternalUniquenessConstraint to find the location of.</param>
		/// <returns></returns>
		public RectangleD GetAbsolutePositionOfConstraint(InternalUniquenessConstraint constraint)
		{
			RectangleD rect = RectangleD.Empty;
			WalkConstraintBoxes(
				this,
				(ConstraintDisplayPosition == ConstraintDisplayPosition.Top) ? myTopConstraintShapeField.GetBounds(this) : myBottomConstraintShapeField.GetBounds(this),
				delegate(ref ConstraintBox constraintBox)
				{
					if (constraintBox.FactConstraint.Constraint == constraint)
					{
						rect = constraintBox.Bounds;
						return false;
					}
					return true;
				});
			rect.Offset(Bounds.Location);
			return rect;
		}
		/// <summary>
		/// Static property set when an external constraint is being created. The active
		/// connection is used to track which roles are highlighted.
		/// </summary>
		public static ExternalConstraintConnectAction ActiveExternalConstraintConnectAction
		{
			get
			{
				return myActiveExternalConstraintConnectAction;
			}
			set
			{
				myActiveExternalConstraintConnectAction = value;
			}
		}
		/// <summary>
		/// Static property set when an internal constraint is being created. The active
		/// connection is used to track which roles are highlighted.
		/// </summary>
		public static InternalUniquenessConstraintConnectAction ActiveInternalUniquenessConstraintConnectAction
		{
			get
			{
				return myActiveInternalUniquenessConstraintConnectAction;
			}
			set
			{
				myActiveInternalUniquenessConstraintConnectAction = value;
			}
		}
		/// <summary>
		/// The core shape model only draws preferred constraints
		/// for the conceptual preferred identifier concept. This does
		/// not include concepts such as the relational multi-column primary
		/// key, so (for example), there is no way to make a spanning constraint
		/// primary in the core model. Override this function in a derived model
		/// to represented a primary identifier as a preferred constraint.
		/// </summary>
		/// <param name="constraint">Any constraint. In the core model, only uniqueness
		/// constraints will be preferred</param>
		/// <returns>true if the PreferredIdentifierFor property on the role is not null.</returns>
		protected virtual bool ShouldDrawConstraintPreferred(IConstraint constraint)
		{
			ConstraintRoleSequence sequence = constraint as ConstraintRoleSequence;
			return (sequence != null) ? (sequence.PreferredIdentifierFor != null) : false;
		}
		#endregion // FactTypeShape specific
		#region Shape display update rules
		[RuleOn(typeof(NestingEntityTypeHasFactType), FireTime = TimeToFire.TopLevelCommit, Priority = DiagramFixupConstants.AddShapeRulePriority)]
		private class SwitchToNestedFact : AddRule
		{
			public override void ElementAdded(ElementAddedEventArgs e)
			{
				NestingEntityTypeHasFactType link = e.ModelElement as NestingEntityTypeHasFactType;
				FactType nestedFactType = link.NestedFactType;
				ObjectType nestingType = link.NestingType;

				// Part1: Make sure the fact shape is visible on any diagram where the
				// corresponding nestingType is displayed
				foreach (object obj in nestingType.AssociatedPresentationElements)
				{
					ObjectTypeShape objectShape = obj as ObjectTypeShape;
					if (objectShape != null)
					{
						ORMDiagram currentDiagram = objectShape.Diagram as ORMDiagram;
						NodeShape factShape = currentDiagram.FindShapeForElement(nestingType) as NodeShape;
						if (factShape == null)
						{
							Diagram.FixUpDiagram(currentDiagram.ModelElement, nestedFactType);
							factShape = currentDiagram.FindShapeForElement(nestingType) as NodeShape;
						}
						if (factShape != null)
						{
							factShape.Location = objectShape.Location;
						}
					}
				}

				// Part2: Move any links from the object type to the fact type
				foreach (ObjectTypePlaysRole modelLink in nestingType.GetElementLinks(ObjectTypePlaysRole.RolePlayerMetaRoleGuid))
				{
					foreach (object obj in modelLink.PresentationRolePlayers)
					{
						RolePlayerLink rolePlayer = obj as RolePlayerLink;
						if (rolePlayer != null)
						{
							ORMDiagram currentDiagram = rolePlayer.Diagram as ORMDiagram;
							NodeShape factShape = currentDiagram.FindShapeForElement(nestedFactType) as NodeShape;
							if (factShape != null)
							{
								rolePlayer.ToShape = factShape;
							}
							else
							{
								// Backup. Should only happen if the FixupDiagram call in part 1
								// did not add the fact type.
								rolePlayer.Remove();
							}
						}
					}
				}

				// Part3: Remove object type shapes from the diagram. Do this before
				// adding the labels to the objectified fact types so clearing the role
				// players doesn't blow the labels away. Also, FixUpDiagram will attempt
				// to fix up the existing shapes instead of creating new ones if the existing
				// ones are not cleared away.
				nestingType.PresentationRolePlayers.Clear();

				// Part4: Resize the fact type wherever it is displayed and add the
				// labels for the fact type display.
				foreach (object obj in nestedFactType.AssociatedPresentationElements)
				{
					FactTypeShape shape = obj as FactTypeShape;
					if (shape != null)
					{
						shape.AutoResize();
						Diagram.FixUpDiagram(nestedFactType, nestingType);
					}
				}
			}
		}
		[RuleOn(typeof(NestingEntityTypeHasFactType), FireTime = TimeToFire.TopLevelCommit, Priority = DiagramFixupConstants.AddShapeRulePriority)]
		private class SwitchFromNestedFact : RemoveRule
		{
			public override void ElementRemoved(ElementRemovedEventArgs e)
			{
				NestingEntityTypeHasFactType link = e.ModelElement as NestingEntityTypeHasFactType;
				FactType nestedFactType = link.NestedFactType;
				ObjectType nestingType = link.NestingType;

				// Part1: Remove any existing presentation elements for the object type.
				// This removes all of the ObjectifiedTypeNameShape objects
				nestingType.PresentationRolePlayers.Clear();

				// Part2: Resize the fact type wherever it is displayed, and make sure
				// the object type is made visible in the same location.
				foreach (object obj in nestedFactType.AssociatedPresentationElements)
				{
					FactTypeShape factShape = obj as FactTypeShape;
					if (factShape != null)
					{
						factShape.AutoResize();
						ORMDiagram currentDiagram = factShape.Diagram as ORMDiagram;
						NodeShape objectShape = currentDiagram.FindShapeForElement(nestingType) as NodeShape;
						if (objectShape == null)
						{
							Diagram.FixUpDiagram(nestingType.Model, nestingType);
							objectShape = currentDiagram.FindShapeForElement(nestingType) as NodeShape;
						}
						if (objectShape != null)
						{
							PointD location = factShape.Location;
							location.Offset(0.0, 2 * factShape.Size.Height);
							objectShape.Location = location;
						}
					}
				}

				// Part3: Move any links from the fact type to the object type
				foreach (ObjectTypePlaysRole modelLink in nestingType.GetElementLinks(ObjectTypePlaysRole.RolePlayerMetaRoleGuid))
				{
					foreach (RolePlayerLink rolePlayer in modelLink.PresentationRolePlayers)
					{
						NodeShape objShape = (rolePlayer.Diagram as ORMDiagram).FindShapeForElement(nestingType) as NodeShape;
						if (objShape != null)
						{
							rolePlayer.ToShape = objShape;
						}
						else
						{
							rolePlayer.Remove();
						}
					}
				}
			}
		}
		#region ConstraintDisplayPositionChangeRule class
		[RuleOn(typeof(FactTypeShape))]
		private class ConstraintDisplayPositionChangeRule : ChangeRule
		{
			public override void ElementAttributeChanged(ElementAttributeChangedEventArgs e)
			{
				Guid attributeId = e.MetaAttribute.Id;
				if (attributeId == ConstraintDisplayPositionMetaAttributeGuid) // InternalUniquenessConstraint.IsPreferredMetaAttributeGuid)
				{
					FactTypeShape factTypeShape = e.ModelElement as FactTypeShape; //InternalUniquenessConstraint;
					if (!factTypeShape.IsRemoved)
					{
						foreach (LinkConnectsToNode connection in factTypeShape.GetElementLinks(LinkConnectsToNode.NodesMetaRoleGuid))
						{
							BinaryLinkShape binaryLink = connection.Link as BinaryLinkShape;
							if (binaryLink != null)
							{
								binaryLink.RipUp();
							}
						}
						factTypeShape.AutoResize();
						factTypeShape.Invalidate(true);
					}
				}
			}
		}
		#endregion // ConstraintDisplayPositionChangeRule class
		#endregion // Shape display update rules
	}
	#endregion // FactTypeShape class
	#region ObjectifiedFactTypeNameShape class
	/// <summary>
	/// A specialized display of the nesting type as a relative
	/// child element of an objectified fact type
	/// </summary>
	public partial class ObjectifiedFactTypeNameShape
	{
		private static AutoSizeTextField myTextShapeField;
		/// <summary>
		/// Associate the text box with the object type name
		/// </summary>
		protected override Guid AssociatedShapeMetaAttributeGuid
		{
			get { return ObjectTypeNameMetaAttributeGuid; }
		}
		/// <summary>
		/// Store per-type value for the base class
		/// </summary>
		[CLSCompliant(false)]
		protected override AutoSizeTextField TextShapeField
		{
			get
			{
				return myTextShapeField;
			}
			set
			{
				Debug.Assert(myTextShapeField == null); // This should only be called once per type
				myTextShapeField = value;
			}
		}
		/// <summary>
		/// Get the ObjectType associated with this shape
		/// </summary>s
		public ObjectType AssociatedObjectType
		{
			get
			{
				return ModelElement as ObjectType;
			}
		}
		/// <summary>
		/// Move the name label above the parent fact type shape
		/// </summary>
		/// <param name="fixupState">BoundsFixupState</param>
		/// <param name="iteration">int</param>
		public override void OnBoundsFixup(BoundsFixupState fixupState, int iteration)
		{
			base.OnBoundsFixup(fixupState, iteration);
			if (fixupState != BoundsFixupState.Invalid)
			{
				SizeD size = Size;
				Location = new PointD(0, -1.5 * size.Height);
			}
		}
	}
	#endregion // ObjectifiedFactTypeNameShape class
}
