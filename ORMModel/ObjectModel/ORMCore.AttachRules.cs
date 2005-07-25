﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50215.44
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Northface.Tools.ORM.ObjectModel
{
    using System;
    using System.Reflection;
    
    /// <summary>
    ///</summary>
    public partial class ORMMetaModel
    {
        /// <summary>
        ///</summary>
        protected override Type[] AllMetaModelTypes()
        {
            return new Type[] {
                    typeof(ConstraintUtility).GetNestedType("ConstraintRoleSequenceHasRoleRemoved", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(CustomReferenceMode).GetNestedType("CustomReferenceModeChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(DataTypeNotSpecifiedError).GetNestedType("UnspecifiedTypeAddedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(EntityTypeHasPreferredIdentifier).GetNestedType("PreferredIdentifierAddedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(EntityTypeHasPreferredIdentifier).GetNestedType("TestRemovePreferredIdentifierRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(EqualityConstraint).GetNestedType("ConstraintRoleSequenceHasRoleAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(EqualityConstraint).GetNestedType("ConstraintRoleSequenceHasRoleRemoved", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ExternalUniquenessConstraint).GetNestedType("ExternalUniquenessConstraintChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("FactTypeChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("FactTypeHasReadingOrderAddRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("FactTypeHasReadingOrderRemovedRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("ModelHasFactTypeAddRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("ModelHasInternalConstraintAddRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("ModelHasInternalConstraintRemoveRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("ReadingOrderHasReadingAddRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(FactType).GetNestedType("ReadingOrderHasReadingRemoveRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalConstraint).GetNestedType("ConstraintRoleSequenceHasRoleAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalConstraint).GetNestedType("FactTypeHasInternalConstraintAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalUniquenessConstraint).GetNestedType("InternalUniquenessConstraintChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalUniquenessConstraint).GetNestedType("NMinusOneAddRuleModelConstraintAddValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalUniquenessConstraint).GetNestedType("NMinusOneAddRuleModelFactAddValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalUniquenessConstraint).GetNestedType("NMinusOneAddRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalUniquenessConstraint).GetNestedType("NMinusOneRemoveRuleModelConstraintRemoveValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalUniquenessConstraint).GetNestedType("NMinusOneRemoveRuleModelFactRemoveValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(InternalUniquenessConstraint).GetNestedType("NMinusOneRemoveRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ModelError).GetNestedType("SynchronizeErrorForOwnerRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ModelError).GetNestedType("SynchronizeErrorTextForModelRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("ConstraintHasRoleSequenceAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("ConstraintRoleSequenceHasRoleAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("EnforceRoleSequenceCardinalityForAdd", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("EnforceRoleSequenceCardinalityForConstraintAdd", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("EnforceRoleSequenceCardinalityForRemove", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("EnforceRoleSequenceValidityForAdd", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("EnforceRoleSequenceValidityForFactTypeAdd", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("EnforceRoleSequenceValidityForFactTypeRemove", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("EnforceRoleSequenceValidityForRemove", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(MultiColumnExternalConstraint).GetNestedType("ExternalRoleConstraintRemoved", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(NamedElementDictionary).GetNestedType("ElementLinkAddedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(NamedElementDictionary).GetNestedType("ElementLinkRemovedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(NamedElementDictionary).GetNestedType("NamedElementChangedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("CheckForIncompatibleRelationshipRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("ModelHasObjectTypeAddRuleModelValidation", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("ObjectTypeChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("ObjectTypeRemoveRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("VerifyNestingEntityTypeHasFactTypeAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("VerifyNestingEntityTypeHasFactTypeRemoveRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("VerifyReferenceSchemeAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("VerifyReferenceSchemeRemoveRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("VerifyValueTypeHasDataTypeAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ObjectType).GetNestedType("VerifyValueTypeHasDataTypeRemoveRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ORMModel).GetNestedType("RemoveDuplicateConstraintNameErrorRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ORMModel).GetNestedType("RemoveDuplicateFactTypeNameErrorRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ORMModel).GetNestedType("RemoveDuplicateObjectTypeNameErrorRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(Reading).GetNestedType("ReadingOrderHasRoleRemoved", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(Reading).GetNestedType("ReadingPropertiesChanged", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReadingOrder).GetNestedType("FactTypeHasRoleAddedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReadingOrder).GetNestedType("ReadingOrderHasReadingAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReadingOrder).GetNestedType("ReadingOrderHasReadingRemoved", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReadingOrder).GetNestedType("ReadingOrderHasRoleRemoving", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReferenceMode).GetNestedType("ReferenceModeAddedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReferenceMode).GetNestedType("ReferenceModeChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReferenceModeHasReferenceModeKind).GetNestedType("ReferenceModeHasReferenceModeKindChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReferenceModeHasReferenceModeKind).GetNestedType("ReferenceModeHasReferenceModeKindRemovingRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ReferenceModeKind).GetNestedType("ReferenceModeKindChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(Role).GetNestedType("RoleChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(Role).GetNestedType("RolePlayerRequiredAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(Role).GetNestedType("RolePlayerRequiredForNewRoleAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(Role).GetNestedType("RolePlayerRequiredRemovedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(Role).GetNestedType("UpdatedRolePlayerRequiredErrorsRemovedRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(RoleValueRangeDefinition).GetNestedType("RoleValueRangeDefinitionChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SingleColumnExternalConstraint).GetNestedType("ConstraintHasRoleSequenceAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SingleColumnExternalConstraint).GetNestedType("ConstraintRoleSequenceHasRoleAdded", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SingleColumnExternalConstraint).GetNestedType("EnforceRoleSequenceValidityForFactTypeAdd", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SingleColumnExternalConstraint).GetNestedType("EnforceRoleSequenceValidityForFactTypeRemove", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("BlockCircularSubtypesAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("InitializeSubtypeAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("LimitSubtypeConstraintRolesAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("LimitSubtypeConstraintRolesRemoveRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("LimitSubtypeConstraintsAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("LimitSubtypeConstraintsRemoveRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("LimitSubtypeRolesAddRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("LimitSubtypeRolesRemoveRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(SubtypeFact).GetNestedType("RemoveSubtypeWhenRolePlayerRemoved", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ValueRange).GetNestedType("ValueRangeChangeRule", (BindingFlags.Public | BindingFlags.NonPublic)),
                    typeof(ValueTypeValueRangeDefinition).GetNestedType("ValueTypeValueRangeDefinitionChangeRule", (BindingFlags.Public | BindingFlags.NonPublic))};
        }
    }
}
