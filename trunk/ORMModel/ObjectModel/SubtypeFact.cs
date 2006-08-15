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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualStudio.Modeling;
using Neumont.Tools.Modeling;

namespace Neumont.Tools.ORM.ObjectModel
{
	public partial class SubtypeFact
	{
		#region Create functions
		/// <summary>
		/// Set the derived type as a subtype of the base type
		/// </summary>
		/// <param name="subtype">The object type to use as the subtype (base type)</param>
		/// <param name="supertype">The object type to use as the supertype (derived type)</param>
		/// <returns>Subtype object</returns>
		public static SubtypeFact Create(ObjectType subtype, ObjectType supertype)
		{
			Debug.Assert(subtype != null && supertype != null);
			SubtypeFact retVal = new SubtypeFact(subtype.Store);
			retVal.Model = subtype.Model;
			retVal.Subtype = subtype;
			retVal.Supertype = supertype;
			return retVal;
		}

		#endregion // Create functions
		#region Accessor functions
		/// <summary>
		/// Get the subtype for this relationship
		/// </summary>
		public ObjectType Subtype
		{
			get
			{
				Role role = SubtypeRole;
				return (role != null) ? role.RolePlayer : null;
			}
			set
			{
				SubtypeRole.RolePlayer = value;
			}
		}
		/// <summary>
		/// Get the super type for this relationship
		/// </summary>
		public ObjectType Supertype
		{
			get
			{
				Role role = SupertypeRole;
				return (role != null) ? role.RolePlayer : null;
			}
			set
			{
				SupertypeRole.RolePlayer = value;
			}
		}
		/// <summary>
		/// Get the Role attached to the subtype object
		/// </summary>
		public SubtypeMetaRole SubtypeRole
		{
			get
			{
				LinkedElementCollection<RoleBase> roles = RoleCollection;
				SubtypeMetaRole retVal = null;
				if (roles.Count == 2)
				{
					retVal = roles[0] as SubtypeMetaRole;
					if (retVal == null)
					{
						retVal = roles[1] as SubtypeMetaRole;
						Debug.Assert(retVal != null); // One of them better be a subtype
					}
				}
				return retVal;
			}
		}
		/// <summary>
		/// Get the Role attached to the supertype object
		/// </summary>
		public SupertypeMetaRole SupertypeRole
		{
			get
			{
				LinkedElementCollection<RoleBase> roles = RoleCollection;
				// Start with checking role 1, not 0. This corresponds
				// to the indices we set in the InitializeSubtypeAddRule.
				// This is not guaranteed (the user can switch them in the xml),
				// but will be the most common case, so we check it first.
				SupertypeMetaRole retVal = null;
				if (roles.Count == 2)
				{
					retVal = roles[1] as SupertypeMetaRole;
					if (retVal == null)
					{
						retVal = roles[0] as SupertypeMetaRole;
						Debug.Assert(retVal != null); // One of them better be a supertype
					}
				}
				return retVal;
			}
		}
		#endregion // Accessor functions
		#region Initialize pattern rules
		/// <summary>
		/// A rule to create a subtype-style FactType with all
		/// of the required roles and constraints.
		/// </summary>
		[RuleOn(typeof(SubtypeFact))] // AddRule
		private sealed partial class InitializeSubtypeAddRule : AddRule
		{
			/// <summary>
			/// Make sure a Subtype is a 1-1 fact with a mandatory role
			/// on the base type (role 0)
			/// </summary>
			public sealed override void ElementAdded(ElementAddedEventArgs e)
			{
				FactType fact = e.ModelElement as FactType;
				Store store = fact.Store;
			
				// Establish role collecton
				LinkedElementCollection<RoleBase> roles = fact.RoleCollection;
				SubtypeMetaRole subTypeMetaRole = new SubtypeMetaRole(store);
				SupertypeMetaRole superTypeMetaRole = new SupertypeMetaRole(store);
				roles.Add(subTypeMetaRole);
				roles.Add(superTypeMetaRole);
			
				// Add injection constraints
				superTypeMetaRole.Multiplicity = RoleMultiplicity.ExactlyOne;
				subTypeMetaRole.Multiplicity = RoleMultiplicity.ZeroToOne;

				// Add forward reading
				LinkedElementCollection<ReadingOrder> readingOrders = fact.ReadingOrderCollection;
				ReadingOrder order = new ReadingOrder(store);
				readingOrders.Add(order);
				roles = order.RoleCollection;
				roles.Add(subTypeMetaRole);
				roles.Add(superTypeMetaRole);
				Reading reading = new Reading(store);
				order.ReadingCollection.Add(reading);
				reading.Text = ResourceStrings.SubtypeFactPredicateReading;
				
				// Add inverse reading
				order = new ReadingOrder(store);
				readingOrders.Add(order);
				roles = order.RoleCollection;
				roles.Add(superTypeMetaRole);
				roles.Add(subTypeMetaRole);
				reading = new Reading(store);
				order.ReadingCollection.Add(reading);
				reading.Text = ResourceStrings.SubtypeFactPredicateInverseReading;
			}
		}
		#endregion Initialize pattern rules
		#region Role and constraint pattern locking rules
		private static void ThrowPatternModifiedException()
		{
			throw new InvalidOperationException(ResourceStrings.ModelExceptionSubtypeConstraintAndRolePatternFixed);
		}
		/// <summary>
		/// Block internal constraints from being added to a subtype
		/// after it is included in a model.
		/// </summary>
		[RuleOn(typeof(FactSetConstraint))] // AddRule
		private sealed partial class LimitSubtypeConstraintsAddRule : AddRule
		{
			/// <summary>
			/// Block internal constraint modification on subtypes
			/// </summary>
			public sealed override void ElementAdded(ElementAddedEventArgs e)
			{
				FactSetConstraint link = e.ModelElement as FactSetConstraint;
				if (link.SetConstraint.Constraint.ConstraintIsInternal)
				{
					SubtypeFact subtypeFact = link.FactType as SubtypeFact;
					if (subtypeFact != null)
					{
						if (subtypeFact.Model != null)
						{
							// Allow before adding to model, not afterwards
							ThrowPatternModifiedException();
						}
					}
				}
			}
		}
		/// <summary>
		/// Block internal constraints from being removed from a subtype
		/// after it is included in a model.
		/// </summary>
		[RuleOn(typeof(FactSetConstraint), FireTime = TimeToFire.LocalCommit)] // DeleteRule
		private sealed partial class LimitSubtypeConstraintsDeleteRule : DeleteRule
		{
			/// <summary>
			/// Block internal constraint modification on subtypes
			/// </summary>
			public sealed override void ElementDeleted(ElementDeletedEventArgs e)
			{
				FactSetConstraint link = e.ModelElement as FactSetConstraint;
				if (link.SetConstraint.Constraint.ConstraintIsInternal)
				{
					SubtypeFact subtypeFact = link.FactType as SubtypeFact;
					if (subtypeFact != null && !subtypeFact.IsDeleted)
					{
						if (subtypeFact.Model != null)
						{
							// Allow before adding to model, not afterwards
							ThrowPatternModifiedException();
						}
					}
				}
			}
		}
		/// <summary>
		/// Block roles from being added to a subtype
		/// after it is included in a model.
		/// </summary>
		[RuleOn(typeof(FactTypeHasRole))] // AddRule
		private sealed partial class LimitSubtypeRolesAddRule : AddRule
		{
			/// <summary>
			/// Block internal constraint modification on subtypes
			/// </summary>
			public sealed override void ElementAdded(ElementAddedEventArgs e)
			{
				FactTypeHasRole link = e.ModelElement as FactTypeHasRole;
				SubtypeFact subtypeFact = link.FactType as SubtypeFact;
				if (subtypeFact != null)
				{
					if (subtypeFact.Model != null)
					{
						// Allow before adding to model, not afterwards
						ThrowPatternModifiedException();
					}
				}
				else
				{
					RoleBase role = link.Role;
					if (role is SubtypeMetaRole || role is SupertypeMetaRole)
					{
						throw new InvalidOperationException(ResourceStrings.ModelExceptionSubtypeFactMustBeParentOfMetaRole);
					}
				}
			}
		}
		/// <summary>
		/// Block roles from being removed from a subtype
		/// after it is included in a model.
		/// </summary>
		[RuleOn(typeof(FactTypeHasRole), FireTime = TimeToFire.LocalCommit)] // DeleteRule
		private sealed partial class LimitSubtypeRolesDeleteRule : DeleteRule
		{
			/// <summary>
			/// Block internal role modification on subtypes
			/// </summary>
			public sealed override void ElementDeleted(ElementDeletedEventArgs e)
			{
				FactTypeHasRole link = e.ModelElement as FactTypeHasRole;
				SubtypeFact subtypeFact = link.FactType as SubtypeFact;
				if (subtypeFact != null && !subtypeFact.IsDeleted)
				{
					if (subtypeFact.Model != null)
					{
						// Allow before adding to model, not afterwards
						ThrowPatternModifiedException();
					}
				}
			}
		}
		/// <summary>
		/// Block internal constraints from being modified on a subtype.
		/// </summary>
		[RuleOn(typeof(ConstraintRoleSequenceHasRole))] // AddRule
		private sealed partial class LimitSubtypeConstraintRolesAddRule : AddRule
		{
			/// <summary>
			/// Block internal constraint modification on subtypes
			/// </summary>
			public sealed override void ElementAdded(ElementAddedEventArgs e)
			{
				ConstraintRoleSequenceHasRole link = e.ModelElement as ConstraintRoleSequenceHasRole;
				SetConstraint ic = link.ConstraintRoleSequence as SetConstraint;
				LinkedElementCollection<FactType> facts;
				if (ic != null &&
					ic.Constraint.ConstraintIsInternal &&
					1 == (facts = ic.FactTypeCollection).Count)
				{
					SubtypeFact subtypeFact = facts[0] as SubtypeFact;
					if (subtypeFact != null)
					{
						if (subtypeFact.Model != null)
						{
							// Allow before adding to model, not afterwards
							ThrowPatternModifiedException();
						}
					}
				}
			}
		}
		/// <summary>
		/// Block roles from being removed from subtype constraints
		/// after it is included in a model.
		/// </summary>
		[RuleOn(typeof(ConstraintRoleSequenceHasRole), FireTime = TimeToFire.LocalCommit)] // DeleteRule
		private sealed partial class LimitSubtypeConstraintRolesDeleteRule : DeleteRule
		{
			/// <summary>
			/// Block internal role modification on subtypes
			/// </summary>
			public sealed override void ElementDeleted(ElementDeletedEventArgs e)
			{
				ConstraintRoleSequenceHasRole link = e.ModelElement as ConstraintRoleSequenceHasRole;
				SetConstraint ic = link.ConstraintRoleSequence as SetConstraint;
				LinkedElementCollection<FactType> facts;
				if (ic != null &&
					!ic.IsDeleted &&
					ic.Constraint.ConstraintIsInternal &&
					1 == (facts = ic.FactTypeCollection).Count)
				{
					SubtypeFact subtypeFact = facts[0] as SubtypeFact;
					if (subtypeFact != null && !subtypeFact.IsDeleted)
					{
						if (subtypeFact.Model != null)
						{
							// Allow before adding to model, not afterwards
							ThrowPatternModifiedException();
						}
					}
				}
			}
		}
		/// <summary>
		/// Block the modality from being changed on internal constraints
		/// on subtype facts
		/// </summary>
		[RuleOn(typeof(SetConstraint))] // ChangeRule
		private sealed partial class LimitSubtypeConstraintChangeRule : ChangeRule
		{
			/// <summary>
			/// Block internal property modification on implicit subtype constraints
			/// </summary>
			public sealed override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
			{
				Guid attributeId = e.DomainProperty.Id;
				SetConstraint constraint = e.ModelElement as SetConstraint;
				if (!constraint.IsDeleted)
				{
					LinkedElementCollection<FactType> testFacts = null;
					if (attributeId == UniquenessConstraint.IsInternalDomainPropertyId ||
						attributeId == MandatoryConstraint.IsSimpleDomainPropertyId)
					{
						testFacts = constraint.FactTypeCollection;
					}
					else if (attributeId == SetConstraint.ModalityDomainPropertyId)
					{
						if (constraint.Constraint.ConstraintIsInternal)
						{
							testFacts = constraint.FactTypeCollection;
						}
					}
					if (testFacts != null)
					{
						int testFactsCount = testFacts.Count;
						for (int i = 0; i < testFactsCount; ++i)
						{
							if (testFacts[i] is SubtypeFact)
							{
								// We never do this internally, so block any modification,
								// not just those after the subtype fact is added to the model
								ThrowPatternModifiedException();
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// Ensure that a role player deletion on a subtype results in a deletion
		/// of the subtype itself.
		/// </summary>
		[RuleOn(typeof(ObjectTypePlaysRole), FireTime = TimeToFire.LocalCommit)] // DeleteRule
		private sealed partial class DeleteSubtypeWhenRolePlayerDeleted : DeleteRule
		{
			/// <summary>
			/// Remove the full SubtypeFact when a role player is removed
			/// </summary>
			public sealed override void ElementDeleted(ElementDeletedEventArgs e)
			{
				ObjectTypePlaysRole link = e.ModelElement as ObjectTypePlaysRole;
				Role role = link.PlayedRole;
				if (role != null && !role.IsDeleted)
				{
					SubtypeFact subtypeFact = role.FactType as SubtypeFact;
					if (subtypeFact != null && !subtypeFact.IsDeleted)
					{
						subtypeFact.Delete();
					}
				}
			}
		}
		#endregion // Role and constraint pattern locking rules
		#region Mixed role player types rules
		private static void ThrowMixedRolePlayerTypesException()
		{
			throw new InvalidOperationException(ResourceStrings.ModelExceptionSubtypeRolePlayerTypesCannotBeMixed);
		}
		/// <summary>
		/// Ensure consistent types (EntityType or ValueType) for role
		/// players in a subtyping relationship
		/// </summary>
		[RuleOn(typeof(ObjectTypePlaysRole), FireTime = TimeToFire.LocalCommit)] // AddRule
		private sealed partial class EnsureConsistentRolePlayerTypesAddRule : AddRule
		{
			public sealed override void ElementAdded(ElementAddedEventArgs e)
			{
				ObjectTypePlaysRole link = e.ModelElement as ObjectTypePlaysRole;
				SubtypeMetaRole subtypeRole;
				SubtypeFact subtypeFact;
				if (null != (subtypeRole = link.PlayedRole as SubtypeMetaRole) &&
					null != (subtypeFact = subtypeRole.FactType as SubtypeFact))
				{
					ObjectType superType = subtypeFact.Supertype;
					if (null == superType ||
						((superType.DataType == null) != (link.RolePlayer.DataType == null)))
					{
						ThrowMixedRolePlayerTypesException();
					}
				}
			}
		}
		/// <summary>
		/// Stop the ValueTypeHasDataType relationship from being
		/// added if an ObjectType participates in a subtyping relationship
		/// </summary>
		[RuleOn(typeof(ValueTypeHasDataType))] // AddRule
		private sealed partial class EnsureConsistentDataTypesAddRule : AddRule
		{
			public sealed override void ElementAdded(ElementAddedEventArgs e)
			{
				ValueTypeHasDataType link = e.ModelElement as ValueTypeHasDataType;
				ObjectType objectType = link.ValueType;
				LinkedElementCollection<Role> playedRoles = objectType.PlayedRoleCollection;
				int playedRoleCount = playedRoles.Count;
				for (int i = 0; i < playedRoleCount; ++i)
				{
					Role testRole = playedRoles[i];
					if (testRole is SubtypeMetaRole ||
						testRole is SupertypeMetaRole)
					{
						if (null != testRole.FactType)
						{
							ThrowMixedRolePlayerTypesException();
						}
					}
				}
			}
		}
		/// <summary>
		/// Stop the ValueTypeHasDataType relationship from being
		/// removed if an ObjectType participates in a subtyping relationship
		/// </summary>
		[RuleOn(typeof(ValueTypeHasDataType))] // DeleteRule
		private sealed partial class EnsureConsistentDataTypesDeleteRule : DeleteRule
		{
			public sealed override void ElementDeleted(ElementDeletedEventArgs e)
			{
				ValueTypeHasDataType link = e.ModelElement as ValueTypeHasDataType;
				ObjectType objectType = link.ValueType;
				if (!objectType.IsDeleted)
				{
					LinkedElementCollection<Role> playedRoles = objectType.PlayedRoleCollection;
					int playedRoleCount = playedRoles.Count;
					for (int i = 0; i < playedRoleCount; ++i)
					{
						Role testRole = playedRoles[i];
						if (testRole is SubtypeMetaRole ||
							testRole is SupertypeMetaRole)
						{
							if (null != testRole.FactType)
							{
								ThrowMixedRolePlayerTypesException();
							}
						}
					}
				}
			}
		}
		#endregion // Mixed role player types rules
		#region Deserialization Fixup
		/// <summary>
		/// Return a deserialization fixup listener. The listener
		/// validates all model errors and adds errors to the task provider.
		/// </summary>
		public static IDeserializationFixupListener FixupListener
		{
			get
			{
				return new SubtypeFactFixupListener();
			}
		}
		/// <summary>
		/// A listener class to enforce valid subtype facts on load.
		/// Invalid subtype patterns will either be fixed up or completely
		/// removed.
		/// </summary>
		private sealed class SubtypeFactFixupListener : DeserializationFixupListener<SubtypeFact>
		{
			/// <summary>
			/// Create a new SubtypeFactFixupListener
			/// </summary>
			public SubtypeFactFixupListener()
				: base((int)ORMDeserializationFixupPhase.ValidateImplicitStoredElements)
			{
			}
			/// <summary>
			/// Make sure the subtype fact constraint pattern
			/// and object types are appropriate.
			/// </summary>
			/// <param name="element">An SubtypeFact instance</param>
			/// <param name="store">The context store</param>
			/// <param name="notifyAdded">The listener to notify if elements are added during fixup</param>
			protected sealed override void ProcessElement(SubtypeFact element, Store store, INotifyElementAdded notifyAdded)
			{
				// Note that the arity and types of the subtype/supertype roles are
				// enforced by the schema.
				Role superTypeMetaRole;
				Role subTypeMetaRole;
				ObjectType superType;
				ObjectType subType;
				if (null == (superTypeMetaRole = element.SupertypeRole) ||
					null == (subTypeMetaRole = element.SubtypeRole) ||
					null == (superType = superTypeMetaRole.RolePlayer) ||
					null == (subType = subTypeMetaRole.RolePlayer) ||
					// They must both be value types or object types, but can't switch
					((superType.DataType == null) != (subType.DataType == null)))
				{
					RemoveFact(element);
				}
				else
				{
					// Note that rules aren't on, so we can read the Multiplicity properties,
					// but we can't set them. All changes must be made explicitly.
					if (superTypeMetaRole.Multiplicity != RoleMultiplicity.ExactlyOne)
					{
						EnsureSingleColumnUniqueAndMandatory(store, element.Model, subTypeMetaRole, true, notifyAdded);
					}
					if (subTypeMetaRole.Multiplicity != RoleMultiplicity.ZeroToOne)
					{
						EnsureSingleColumnUniqueAndMandatory(store, element.Model, superTypeMetaRole, false, notifyAdded);
					}
				}
			}
			/// <summary>
			/// Internal constraints are not fully connected at this point (FactSetConstraint instances
			/// are not implicitly constructed until a later phase), so we need to work a little harder
			/// to remove them.
			/// </summary>
			/// <param name="fact">The fact to clear of external constraints</param>
			private static void RemoveFact(FactType fact)
			{
				LinkedElementCollection<RoleBase> factRoles = fact.RoleCollection;
				int roleCount = factRoles.Count;
				for (int i = 0; i < roleCount; ++i)
				{
					Role role = factRoles[i].Role;
					LinkedElementCollection<ConstraintRoleSequence> sequences = role.ConstraintRoleSequenceCollection;
					int sequenceCount = sequences.Count;
					for (int j = sequenceCount - 1; j >= 0; --j)
					{
						SetConstraint ic = sequences[j] as SetConstraint;
						if (ic != null && ic.Constraint.ConstraintIsInternal)
						{
							ic.Delete();
						}
					}
				}
				fact.Delete();
			}
			private static void EnsureSingleColumnUniqueAndMandatory(Store store, ORMModel model, Role role, bool requireMandatory, INotifyElementAdded notifyAdded)
			{
				LinkedElementCollection<ConstraintRoleSequence> sequences = role.ConstraintRoleSequenceCollection;
				int sequenceCount = sequences.Count;
				bool haveUniqueness = false;
				bool haveMandatory = !requireMandatory;
				SetConstraint ic;
				for (int i = sequenceCount - 1; i >= 0; --i)
				{
					ic = sequences[i] as SetConstraint;
					if (ic != null && ic.Constraint.ConstraintIsInternal)
					{
						if (ic.RoleCollection.Count == 1 && ic.Modality == ConstraintModality.Alethic)
						{
							switch (ic.Constraint.ConstraintType)
							{
								case ConstraintType.InternalUniqueness:
									if (haveUniqueness)
									{
										ic.Delete();
									}
									else
									{
										haveUniqueness = true;
									}
									break;
								case ConstraintType.SimpleMandatory:
									if (haveMandatory)
									{
										ic.Delete();
									}
									else
									{
										haveMandatory = true;
									}
									break;
							}
						}
						else
						{
							ic.Delete();
						}
					}
				}
				if (!haveUniqueness)
				{
					ic = UniquenessConstraint.CreateInternalUniquenessConstraint(store);
					ic.RoleCollection.Add(role);
					ic.Model = model;
					notifyAdded.ElementAdded(ic, true);
				}
				if (!haveMandatory)
				{
					ic = MandatoryConstraint.CreateSimpleMandatoryConstraint(store);
					ic.RoleCollection.Add(role);
					ic.Model = model;
					notifyAdded.ElementAdded(ic, true);
				}
			}
		}
		#endregion Deserialization Fixup
	}
}
