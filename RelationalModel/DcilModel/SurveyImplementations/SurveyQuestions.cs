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
using System.Collections.Generic;
using System.Diagnostics;
using Neumont.Tools.Modeling.Design;
using System.ComponentModel;
using Neumont.Tools.Modeling.Shell.DynamicSurveyTreeGrid;
using System.Windows.Forms;
using Microsoft.VisualStudio.Modeling;
using System.Globalization;
using Neumont.Tools.Modeling;
using Neumont.Tools.ORM.ObjectModel;

namespace Neumont.Tools.RelationalModels.ConceptualDatabase
{
	#region Survey Question Type Enums
	/// <summary>
	/// The list of possible answers for the Schema grouping in the model browser
	/// </summary>
	[TypeConverter(typeof(EnumConverter<SurveySchemaType, Catalog>))]
	public enum SurveySchemaType
	{
		/// <summary>
		/// This element is a schema
		/// </summary>
		Schema,
		/// <summary>
		/// The current highest-valued value in the enumeration
		/// </summary>
		Last = Schema,
	}
	/// <summary>
	/// The list of possible answers for the SchemaChildType grouping in the model browser
	/// </summary>
	[TypeConverter(typeof(EnumConverter<SurveySchemaChildType, Catalog>))]
	public enum SurveySchemaChildType
	{
		/// <summary>
		/// This element is a Table
		/// </summary>
		Table,
		/// <summary>
		/// The element is a Domain
		/// </summary>
		Domain,
		/// <summary>
		/// The current highest-valued value in the enumeration
		/// </summary>
		Last = Domain,
	}
	/// <summary>
	/// The list of possible answers for the TableChildType grouping in the model browser
	/// </summary>
	[TypeConverter(typeof(EnumConverter<SurveyTableChildType, Catalog>))]
	public enum SurveyTableChildType
	{
		/// <summary>
		/// This element is a Column
		/// </summary>
		Column,
		/// <summary>
		/// The element is a Constraint
		/// </summary>
		Constraint,
		/// <summary>
		/// The element is a ReferenceConstraint
		/// </summary>
		ReferenceConstraint,
	}
	/// <summary>
	/// The list of possible answers for the TableChildGlyphType category
	/// </summary>
	public enum SurveyTableChildGlyphType
	{
		/// <summary>
		/// The element is a column
		/// </summary>
		Column,
		/// <summary>
		/// The element is a foreign key
		/// </summary>
		ReferenceConstraint,
		/// <summary>
		/// The element is a uniqueness constraint
		/// </summary>
		UniquenessConstraint,
		/// <summary>
		/// The element is a primary uniqueness constraint
		/// </summary>
		PrimaryUniquenessConstraint,
		/// <summary>
		/// The current highest-valued value in the enumeration
		/// </summary>
		Last = PrimaryUniquenessConstraint,
	}
	/// <summary>
	/// The list of possible answers to the ColumnNullableType category in the model browser
	/// </summary>
	public enum SurveyColumnClassificationType
	{
		/// <summary>
		/// The column is required
		/// </summary>
		Required,
		/// <summary>
		/// The column is primary and required
		/// </summary>
		PrimaryRequired,
		/// <summary>
		/// The column is nullable
		/// </summary>
		Nullable,
		/// <summary>
		/// The column is primary and nullable
		/// </summary>
		PrimaryNullable,
		/// <summary>
		/// The current highest-valued value in the enumeration
		/// </summary>
		Last = PrimaryNullable,
	}
	/// <summary>
	/// The list of possible answers for the ReferenceConstraintChildType grouping in the model browser
	/// </summary>
	[TypeConverter(typeof(EnumConverter<SurveyReferenceConstraintChildType, Catalog>))]
	public enum SurveyReferenceConstraintChildType
	{
		/// <summary>
		/// The table being referenced
		/// </summary>
		TableReference,
		/// <summary>
		/// The column being referenced
		/// </summary>
		ColumnReference,
		/// <summary>
		/// The current highest-valued value in the enumeration
		/// </summary>
		Last = ColumnReference,
	}
	/// <summary>
	/// The list of possible answers for the ForeignKeyChildType grouping in the model browser
	/// </summary>
	[TypeConverter(typeof(EnumConverter<SurveyUniquenessConstraintChildType, Catalog>))]
	public enum SurveyUniquenessConstraintChildType
	{
		/// <summary>
		/// The column being referenced
		/// </summary>
		ColumnReference,
		/// <summary>
		/// The current highest-valued value in the enumeration
		/// </summary>
		Last = ColumnReference,
	}
	#endregion // Survey Question Type Enums
	#region Schema answers
	partial class Schema : IAnswerSurveyQuestion<SurveySchemaType>, ISurveyNode
	{
		#region IAnswerSurveyQuestion<SurveySchemaType> Implementation
		int IAnswerSurveyQuestion<SurveySchemaType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestion()
		{
			return (int)SurveySchemaType.Schema;
		}
		#endregion // IAnswerSurveyQuestion<SurveySchemaType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(this, Schema.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return this.Name;
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return this.Name;
			}
			set
			{
				DomainTypeDescriptor.CreatePropertyDescriptor(this, Schema.NameDomainPropertyId).SetValue(this, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(Schema), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// The key used to retrieve expansion details for the model browser
		/// </summary>
		public static readonly object SurveyExpansionKey = new object();
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>		
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return SurveyExpansionKey;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
	}
	#endregion // Schema answers
	#region Table answers
	partial class Table : IAnswerSurveyQuestion<SurveySchemaChildType>, ISurveyNode, ISurveyNodeContext
	{
		#region IAnswerSurveyQuestion<SurveySchemaChildType> Implementation
		int IAnswerSurveyQuestion<SurveySchemaChildType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestion()
		{
			return (int)SurveySchemaChildType.Table;
		}
		#endregion // IAnswerSurveyQuestion<SurveySchemaChildType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(this, Table.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return this.Name;
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return this.Name;
			}
			set
			{
				DomainTypeDescriptor.CreatePropertyDescriptor(this, Table.NameDomainPropertyId).SetValue(this, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(Table), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// The key used to retrieve expansion details for the model browser
		/// </summary>
		public static readonly object SurveyExpansionKey = new object();
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>		
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return SurveyExpansionKey;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
		#region ISurveyNodeContext Implementation
		/// <summary>
		/// The survey node context for a <see cref="Table"/> is
		/// the <see cref="Schema"/>
		/// </summary>
		protected object SurveyNodeContext
		{
			get
			{
				return Schema;
			}
		}
		object ISurveyNodeContext.SurveyNodeContext
		{
			get
			{
				return SurveyNodeContext;
			}
		}
		#endregion // ISurveyNodeContext Implementation
	}
	#endregion // Table answers
	#region Column answers
	partial class Column : IAnswerSurveyQuestion<SurveyTableChildType>, IAnswerSurveyQuestion<SurveyTableChildGlyphType>, IAnswerSurveyQuestion<SurveyColumnClassificationType>, ISurveyNode, ISurveyNodeContext, ICustomComparableSurveyNode
	{
		#region IAnswerSurveyQuestion<SurveyTableChildType> Implementation
		int IAnswerSurveyQuestion<SurveyTableChildType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestion()
		{
			return (int)SurveyTableChildType.Column;
		}
		#endregion // IAnswerSurveyQuestion<SurveyTableChildType> Implementation
		#region IAnswerSurveyQuestion<SurveyTableChildGlyphType> Implementation
		int IAnswerSurveyQuestion<SurveyTableChildGlyphType>.AskQuestion()
		{
			return AskElementQuestionForGlyph();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestionForGlyph()
		{
			return (int)SurveyTableChildGlyphType.Column;
		}
		#endregion // IAnswerSurveyQuestion<SurveyTableChildGlyphType> Implementation
		#region IAnswerSurveyQuestion<SurveyColumnClassificationType> Implementation
		int IAnswerSurveyQuestion<SurveyColumnClassificationType>.AskQuestion()
		{
			return AskElementQuestionForClassification();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestionForClassification()
		{
			return (int)(IsPartOfPrimaryIdentifier ?
				IsNullable ? SurveyColumnClassificationType.PrimaryNullable : SurveyColumnClassificationType.PrimaryRequired :
				IsNullable ? SurveyColumnClassificationType.Nullable : SurveyColumnClassificationType.Required);
		}
		#endregion // IAnswerSurveyQuestion<SurveyTableChildGlyphType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(this, Column.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return this.Name;
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return this.Name;
			}
			set
			{
				DomainTypeDescriptor.CreatePropertyDescriptor(this, Column.NameDomainPropertyId).SetValue(this, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(Column), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>		
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return null;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
		#region ISurveyNodeContext Implementation
		/// <summary>
		/// The survey node context for a <see cref="Column"/> is
		/// the <see cref="Table"/>
		/// </summary>
		protected object SurveyNodeContext
		{
			get
			{
				return Table;
			}
		}
		object ISurveyNodeContext.SurveyNodeContext
		{
			get
			{
				return SurveyNodeContext;
			}
		}
		#endregion // ISurveyNodeContext Implementation
		#region ICustomComparableSurveyNode Implementation
		int ICustomComparableSurveyNode.CompareToSurveyNode(object other, object customSortData, object otherCustomSortData)
		{
			return CompareToSurveyNode(other, customSortData, otherCustomSortData);
		}
		/// <summary>
		/// Implements <see cref="ICustomComparableSurveyNode.CompareToSurveyNode"/>. Columns
		/// sort with columns in the preferred identifier first.
		/// </summary>
		protected int CompareToSurveyNode(object other, object customSortData, object otherCustomSortData)
		{
			if (other is Column)
			{
				bool leftIsPrimary = (bool)customSortData;
				bool rightIsPrimary = (bool)otherCustomSortData;
				if (leftIsPrimary ^ rightIsPrimary)
				{
					return leftIsPrimary ? -1 : 1;
				}
			}
			// For this comparison, 0 implies no information is available
			return 0;
		}
		bool ICustomComparableSurveyNode.ResetCustomSortData(ref object customSortData)
		{
			return ResetCustomSortData(ref customSortData);
		}
		/// <summary>
		/// Implements <see cref="ICustomComparableSurveyNode.ResetCustomSortData"/>. Returns
		/// a boolean corresponding to the <see cref="IsPartOfPrimaryIdentifier"/> property.
		/// </summary>
		protected bool ResetCustomSortData(ref object customSortData)
		{
			bool retVal = IsPartOfPrimaryIdentifier;
			if (customSortData == null || (bool)customSortData != retVal)
			{
				customSortData = retVal;
				return true;
			}
			return false;

		}
		/// <summary>
		/// Is this column part of the primary identification scheme?
		/// </summary>
		public bool IsPartOfPrimaryIdentifier
		{
			get
			{
				foreach (UniquenessConstraint constraint in UniquenessConstraintIncludesColumn.GetUniquenessConstraints(this))
				{
					if (constraint.IsPrimary)
					{
						return true;
					}
				}
				return false;
			}
		}
		#endregion // ICustomComparableSurveyNode Implementation
	}
	#endregion // Column answers
	#region ReferenceConstraint answers
	partial class ReferenceConstraint : IAnswerSurveyQuestion<SurveyTableChildType>, IAnswerSurveyQuestion<SurveyTableChildGlyphType>, ISurveyNode, ISurveyNodeContext
	{
		#region IAnswerSurveyQuestion<SurveyTableChildType> Implementation
		int IAnswerSurveyQuestion<SurveyTableChildType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestion()
		{
			return (int)SurveyTableChildType.ReferenceConstraint;
		}
		#endregion // IAnswerSurveyQuestion<SurveyTableChildType> Implementation
		#region IAnswerSurveyQuestion<SurveyTableChildGlyphType> Implementation
		int IAnswerSurveyQuestion<SurveyTableChildGlyphType>.AskQuestion()
		{
			return AskElementQuestionForGlyph();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestionForGlyph()
		{
			return (int)SurveyTableChildGlyphType.ReferenceConstraint;
		}
		#endregion // IAnswerSurveyQuestion<SurveyTableChildGlyphType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(this, ReferenceConstraint.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return this.Name;
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return this.Name;
			}
			set
			{
				DomainTypeDescriptor.CreatePropertyDescriptor(this, ReferenceConstraint.NameDomainPropertyId).SetValue(this, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(ReferenceConstraint), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// The key used to retrieve expansion details for the model browser
		/// </summary>
		public static readonly object SurveyExpansionKey = new object();
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>		
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return SurveyExpansionKey;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
		#region ISurveyNodeContext Implementation
		/// <summary>
		/// The survey node context for a <see cref="ReferenceConstraint"/> is
		/// the <see cref="SourceTable"/>
		/// </summary>
		protected object SurveyNodeContext
		{
			get
			{
				return SourceTable;
			}
		}
		object ISurveyNodeContext.SurveyNodeContext
		{
			get
			{
				return SurveyNodeContext;
			}
		}
		#endregion // ISurveyNodeContext Implementation
	}
	#endregion // ReferenceConstraint answers
	#region UniquenessConstraint answers
	partial class UniquenessConstraint : IAnswerSurveyQuestion<SurveyTableChildType>, IAnswerSurveyQuestion<SurveyTableChildGlyphType>, ISurveyNode, ISurveyNodeContext
	{
		#region IAnswerSurveyQuestion<SurveyTableChildType> Implementation
		int IAnswerSurveyQuestion<SurveyTableChildType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestion()
		{
			return (int)SurveyTableChildType.Constraint;
		}
		#endregion // IAnswerSurveyQuestion<SurveyTableChildType> Implementation
		#region IAnswerSurveyQuestion<SurveyTableChildGlyphType> Implementation
		int IAnswerSurveyQuestion<SurveyTableChildGlyphType>.AskQuestion()
		{
			return AskElementQuestionForGlyph();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestionForGlyph()
		{
			return IsPrimary ? (int)SurveyTableChildGlyphType.PrimaryUniquenessConstraint : (int)SurveyTableChildGlyphType.UniquenessConstraint;
		}
		#endregion // IAnswerSurveyQuestion<SurveyTableChildGlyphType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(this, UniquenessConstraint.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return this.Name;
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return this.Name;
			}
			set
			{
				DomainTypeDescriptor.CreatePropertyDescriptor(this, UniquenessConstraint.NameDomainPropertyId).SetValue(this, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(UniquenessConstraint), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// The key used to retrieve expansion details for the model browser
		/// </summary>
		public static readonly object SurveyExpansionKey = new object();
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>		
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return SurveyExpansionKey;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
		#region ISurveyNodeContext Implementation
		/// <summary>
		/// The survey node context for a <see cref="UniquenessConstraint"/> is
		/// the <see cref="Table"/>
		/// </summary>
		protected object SurveyNodeContext
		{
			get
			{
				return Table;
			}
		}
		object ISurveyNodeContext.SurveyNodeContext
		{
			get
			{
				return SurveyNodeContext;
			}
		}
		#endregion // ISurveyNodeContext Implementation
	}
	#endregion // UniquenessConstraint answers
	#region ReferenceConstraintTargetsTable answers
	partial class ReferenceConstraintTargetsTable : IAnswerSurveyQuestion<SurveyReferenceConstraintChildType>, ISurveyNode, ISurveyNodeContext
	{
		#region IAnswerSurveyQuestion<SurveyReferenceConstraintChildType> Implementation
		int IAnswerSurveyQuestion<SurveyReferenceConstraintChildType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestion()
		{
			return (int)SurveyReferenceConstraintChildType.TableReference;
		}
		#endregion // IAnswerSurveyQuestion<SurveyReferenceConstraintChildType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(TargetTable, Table.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return TargetTable.Name;
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return TargetTable.Name;
			}
			set
			{
				Table targetTable = TargetTable;
				DomainTypeDescriptor.CreatePropertyDescriptor(targetTable, Table.NameDomainPropertyId).SetValue(targetTable, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(ReferenceConstraintTargetsTable), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>		
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return null;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
		#region ISurveyNodeContext Implementation
		/// <summary>
		/// The survey node context for a <see cref="ReferenceConstraintTargetsTable"/> is
		/// the <see cref="ReferenceConstraint"/>
		/// </summary>
		protected object SurveyNodeContext
		{
			get
			{
				return ReferenceConstraint;
			}
		}
		object ISurveyNodeContext.SurveyNodeContext
		{
			get
			{
				return SurveyNodeContext;
			}
		}
		#endregion // ISurveyNodeContext Implementation
	}
	#endregion // ReferenceConstraintTargetsTable answers
	#region ColumnReference answers
	partial class ColumnReference : IAnswerSurveyQuestion<SurveyReferenceConstraintChildType>, ISurveyNode, ISurveyNodeContext, ICustomComparableSurveyNode
	{
		#region IAnswerSurveyQuestion<SurveyReferenceConstraintChildType> Implementation
		int IAnswerSurveyQuestion<SurveyReferenceConstraintChildType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected static int AskElementQuestion()
		{
			return (int)SurveyReferenceConstraintChildType.ColumnReference;
		}
		#endregion // IAnswerSurveyQuestion<SurveyReferenceConstraintChildType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(SourceColumn, Column.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, ResourceStrings.ColumnReferenceDisplayFormatString, SourceColumn.Name, TargetColumn.Name);
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return SourceColumn.Name;
			}
			set
			{
				Column sourceColumn = SourceColumn;
				DomainTypeDescriptor.CreatePropertyDescriptor(sourceColumn, Column.NameDomainPropertyId).SetValue(sourceColumn, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(ColumnReference), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return null;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
		#region ISurveyNodeContext Implementation
		/// <summary>
		/// The survey node context for a <see cref="ColumnReference"/> is
		/// the <see cref="ReferenceConstraint"/>
		/// </summary>
		protected object SurveyNodeContext
		{
			get
			{
				return ReferenceConstraint;
			}
		}
		object ISurveyNodeContext.SurveyNodeContext
		{
			get
			{
				return SurveyNodeContext;
			}
		}
		#endregion // ISurveyNodeContext Implementation
		#region ICustomComparableSurveyNode Implementation
		int ICustomComparableSurveyNode.CompareToSurveyNode(object other, object customSortData, object otherCustomSortData)
		{
			return CompareToSurveyNode(other, customSortData, otherCustomSortData);
		}
		/// <summary>
		/// Implements <see cref="ICustomComparableSurveyNode.CompareToSurveyNode"/>. Columns
		/// sort with columns in the preferred identifier first.
		/// </summary>
		protected int CompareToSurveyNode(object other, object customSortData, object otherCustomSortData)
		{
			if (other is ColumnReference)
			{
				int thisIndex = (int)customSortData;
				int otherIndex = (int)otherCustomSortData;
				if (thisIndex < otherIndex)
				{
					return -1;
				}
				else if (thisIndex != otherIndex)
				{
					return 1;
				}
			}
			// For this comparison, 0 implies no information is available
			return 0;
		}
		bool ICustomComparableSurveyNode.ResetCustomSortData(ref object customSortData)
		{
			return ResetCustomSortData(ref customSortData);
		}
		/// <summary>
		/// Implements <see cref="ICustomComparableSurveyNode.ResetCustomSortData"/>. Returns
		/// the current position in the ColumnReferenceCollection of the parent <see cref="ReferenceConstraint"/>
		/// </summary>
		protected bool ResetCustomSortData(ref object customSortData)
		{
			int retVal = -1;
			ReferenceConstraint parentConstraint;
			if (null != (parentConstraint = ReferenceConstraint))
			{
				retVal = parentConstraint.ColumnReferenceCollection.IndexOf(this);
			}
			if (customSortData == null || (int)customSortData != retVal)
			{
				customSortData = retVal;
				return true;
			}
			return false;
		}
		#endregion // ICustomComparableSurveyNode Implementation
	}
	#endregion // ColumnReference answers
	#region UniquenessConstraintIncludesColumn answers
	partial class UniquenessConstraintIncludesColumn : IAnswerSurveyQuestion<SurveyUniquenessConstraintChildType>, ISurveyNode, ISurveyNodeContext, ICustomComparableSurveyNode
	{
		#region IAnswerSurveyQuestion<SurveyUniquenessConstraintChildType> Implementation
		int IAnswerSurveyQuestion<SurveyUniquenessConstraintChildType>.AskQuestion()
		{
			return AskElementQuestion();
		}
		/// <summary>
		/// implementation of AskQuestion method from IAnswerSurveyQuestion
		/// </summary>
		protected int AskElementQuestion()
		{
			return (int)SurveyUniquenessConstraintChildType.ColumnReference;
		}
		#endregion // IAnswerSurveyQuestion<SurveyUniquenessConstraintChildType> Implementation
		#region ISurveyNode Implementation
		bool ISurveyNode.IsSurveyNameEditable
		{
			get
			{
				return IsSurveyNameEditable;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.IsSurveyNameEditable"/>
		/// </summary>
		protected bool IsSurveyNameEditable
		{
			get
			{
				return DomainTypeDescriptor.CreatePropertyDescriptor(Column, Column.NameDomainPropertyId).IsReadOnly;
			}
		}
		string ISurveyNode.SurveyName
		{
			get
			{
				return SurveyName;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyName"/>
		/// </summary>
		protected string SurveyName
		{
			get
			{
				return Column.Name;
			}
		}
		string ISurveyNode.EditableSurveyName
		{
			get
			{
				return EditableSurveyName;
			}
			set
			{
				EditableSurveyName = value;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.EditableSurveyName"/>
		/// </summary>
		protected string EditableSurveyName
		{
			get
			{
				return Column.Name;
			}
			set
			{
				Column targetColumn = Column;
				DomainTypeDescriptor.CreatePropertyDescriptor(targetColumn, Column.NameDomainPropertyId).SetValue(targetColumn, value);
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeDataObject"/>
		/// </summary>
		protected object SurveyNodeDataObject
		{
			get
			{
				DataObject retVal = new DataObject();
				retVal.SetData(typeof(UniquenessConstraintIncludesColumn), this);
				return retVal;
			}
		}
		object ISurveyNode.SurveyNodeDataObject
		{
			get
			{
				return SurveyNodeDataObject;
			}
		}
		/// <summary>
		/// Implements <see cref="ISurveyNode.SurveyNodeExpansionKey"/>
		/// </summary>		
		protected static object SurveyNodeExpansionKey
		{
			get
			{
				return null;
			}
		}
		object ISurveyNode.SurveyNodeExpansionKey
		{
			get
			{
				return SurveyNodeExpansionKey;
			}
		}
		#endregion // ISurveyNode Implementation
		#region ISurveyNodeContext Implementation
		/// <summary>
		/// The survey node context for a <see cref="UniquenessConstraintIncludesColumn"/> is
		/// the <see cref="UniquenessConstraint"/>
		/// </summary>
		protected object SurveyNodeContext
		{
			get
			{
				return UniquenessConstraint;
			}
		}
		object ISurveyNodeContext.SurveyNodeContext
		{
			get
			{
				return SurveyNodeContext;
			}
		}
		#endregion // ISurveyNodeContext Implementation
		#region ICustomComparableSurveyNode Implementation
		int ICustomComparableSurveyNode.CompareToSurveyNode(object other, object customSortData, object otherCustomSortData)
		{
			return CompareToSurveyNode(other, customSortData, otherCustomSortData);
		}
		/// <summary>
		/// Implements <see cref="ICustomComparableSurveyNode.CompareToSurveyNode"/>. Columns
		/// sort with columns in the preferred identifier first.
		/// </summary>
		protected int CompareToSurveyNode(object other, object customSortData, object otherCustomSortData)
		{
			if (other is UniquenessConstraintIncludesColumn)
			{
				int thisIndex = (int)customSortData;
				int otherIndex = (int)otherCustomSortData;
				if (thisIndex < otherIndex)
				{
					return -1;
				}
				else if (thisIndex != otherIndex)
				{
					return 1;
				}
			}
			// For this comparison, 0 implies no information is available
			return 0;
		}
		bool ICustomComparableSurveyNode.ResetCustomSortData(ref object customSortData)
		{
			return ResetCustomSortData(ref customSortData);
		}
		/// <summary>
		/// Implements <see cref="ICustomComparableSurveyNode.ResetCustomSortData"/>. Returns
		/// the current position in the ColumnReferenceCollection of the parent <see cref="UniquenessConstraint"/>
		/// </summary>
		protected bool ResetCustomSortData(ref object customSortData)
		{
			int retVal = -1;
			UniquenessConstraint parentConstraint;
			if (null != (parentConstraint = UniquenessConstraint))
			{
				retVal = UniquenessConstraintIncludesColumn.GetLinksToColumnCollection(parentConstraint).IndexOf(this);
			}
			if (customSortData == null || (int)customSortData != retVal)
			{
				customSortData = retVal;
				return true;
			}
			return false;
		}
		#endregion // ICustomComparableSurveyNode Implementation
	}
	#endregion // ReferenceConstraintTargetsTable answers
	#region ISurveyNodeProvider Implementation
	partial class ConceptualDatabaseDomainModel : ISurveyNodeProvider, IModelingEventSubscriber
	{
		#region ISurveyNodeProvider Implementation
		IEnumerable<object> ISurveyNodeProvider.GetSurveyNodes(object context, object expansionKey)
		{
			return this.GetSurveyNodes(context, expansionKey);
		}
		/// <summary>
		/// Implements <see cref="ISurveyNodeProvider.GetSurveyNodes"/>
		/// </summary>
		protected IEnumerable<object> GetSurveyNodes(object context, object expansionKey)
		{
			if (expansionKey == null)
			{
				IElementDirectory elementDirectory = Store.ElementDirectory;
				foreach (Schema element in elementDirectory.FindElements<Schema>(true))
				{
					yield return element;
				}
			}
			else if (expansionKey == Schema.SurveyExpansionKey)
			{
				Schema schema = context as Schema;
				if (schema != null)
				{
					foreach (Table table in schema.TableCollection)
					{
						yield return table;
					}
				}
			}
			else if (expansionKey == Table.SurveyExpansionKey)
			{
				Table table = context as Table;
				if (table != null)
				{
					foreach (Column column in table.ColumnCollection)
					{
						yield return column;
					}
					foreach (ReferenceConstraint referenceConstraint in table.ReferenceConstraintCollection)
					{
						yield return referenceConstraint;
					}
					foreach (UniquenessConstraint uniquenessConstraint in table.UniquenessConstraintCollection)
					{
						yield return uniquenessConstraint;
					}
				}
			}
			else if (expansionKey == ReferenceConstraint.SurveyExpansionKey)
			{
				ReferenceConstraint referenceConstraint = context as ReferenceConstraint;
				if (referenceConstraint != null)
				{
					// UNDONE: Do the table reference directly as a link in the model browser when the construct is available
					ReferenceConstraintTargetsTable tableLink = ReferenceConstraintTargetsTable.GetLinkToTargetTable(referenceConstraint);
					if (tableLink != null)
					{
						yield return tableLink;
					}
					foreach (ColumnReference columnRef in referenceConstraint.ColumnReferenceCollection)
					{
						yield return columnRef;
					}
				}
			}
			else if (expansionKey == UniquenessConstraint.SurveyExpansionKey)
			{
				UniquenessConstraint uniquenessConstraint = context as UniquenessConstraint;
				if (uniquenessConstraint != null)
				{
					// UNDONE: Do this directly as a link in the model browser when the construct is available
					foreach (UniquenessConstraintIncludesColumn columnRef in UniquenessConstraintIncludesColumn.GetLinksToColumnCollection(uniquenessConstraint))
					{
						yield return columnRef;
					}
				}
			}
		}
		#endregion // ISurveyNodeProvider Implementation
		#region IModelingEventSubscriber Implementation
		/// <summary>
		/// Implements <see cref="IModelingEventSubscriber.ManagePostLoadModelingEventHandlers"/>
		/// </summary>
		protected static void ManagePostLoadModelingEventHandlers(ModelingEventManager eventManager, bool isReload, EventHandlerAction action)
		{
			// Nothing to do
		}
		void IModelingEventSubscriber.ManagePostLoadModelingEventHandlers(ModelingEventManager eventManager, bool isReload, EventHandlerAction action)
		{
			ManagePostLoadModelingEventHandlers(eventManager, isReload, action);
		}
		/// <summary>
		/// Implements <see cref="IModelingEventSubscriber.ManagePreLoadModelingEventHandlers"/>
		/// </summary>
		protected static void ManagePreLoadModelingEventHandlers(ModelingEventManager eventManager, bool isReload, EventHandlerAction action)
		{
			// Nothing to do
		}
		void IModelingEventSubscriber.ManagePreLoadModelingEventHandlers(ModelingEventManager eventManager, bool isReload, EventHandlerAction action)
		{
			ManagePreLoadModelingEventHandlers(eventManager, isReload, action);
		}
		/// <summary>
		/// Implements <see cref="IModelingEventSubscriber.ManageSurveyQuestionModelingEventHandlers"/>
		/// </summary>
		protected void ManageSurveyQuestionModelingEventHandlers(ModelingEventManager eventManager, bool isReload, EventHandlerAction action)
		{
			Store store = this.Store;
			DomainDataDirectory dataDir = store.DomainDataDirectory;
			DomainClassInfo classInfo;
			DomainRoleInfo roleInfo;

			// Schema elements (top level)
			classInfo = dataDir.FindDomainClass(Schema.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementAddedEventArgs>(SchemaAdded), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(ElementRemoved), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementPropertyChangedEventArgs>(SchemaChanged), action);
				
			// Table elements (schema expansion)
			eventManager.AddOrRemoveHandler(dataDir.FindDomainRelationship(SchemaContainsTable.DomainClassId), new EventHandler<ElementAddedEventArgs>(TableAdded), action);
			classInfo = dataDir.FindDomainClass(Table.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(ElementRemoved), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementPropertyChangedEventArgs>(TableChanged), action);

			// Column, uniqueness, and foreign key elements (inside table)
			// UNDONE: This does not handle updates to the primary identifier keys.
			// There are currently no incremental updates involving primary keys.
			eventManager.AddOrRemoveHandler(dataDir.FindDomainRelationship(TableContainsColumn.DomainClassId), new EventHandler<ElementAddedEventArgs>(ColumnAdded), action);
			classInfo = dataDir.FindDomainClass(Column.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(ElementRemoved), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementPropertyChangedEventArgs>(ColumnChanged), action);
			eventManager.AddOrRemoveHandler(dataDir.FindDomainRelationship(TableContainsReferenceConstraint.DomainClassId), new EventHandler<ElementAddedEventArgs>(ReferenceConstraintAdded), action);
			classInfo = dataDir.FindDomainClass(ReferenceConstraint.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(ElementRemoved), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementPropertyChangedEventArgs>(ReferenceConstraintChanged), action);
			eventManager.AddOrRemoveHandler(dataDir.FindDomainRelationship(TableContainsUniquenessConstraint.DomainClassId), new EventHandler<ElementAddedEventArgs>(UniquenessConstraintAdded), action);
			classInfo = dataDir.FindDomainClass(UniquenessConstraint.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(ElementRemoved), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementPropertyChangedEventArgs>(UniquenessConstraintChanged), action);
			roleInfo = dataDir.FindDomainRole(UniquenessConstraintIncludesColumn.ColumnDomainRoleId);
			eventManager.AddOrRemoveHandler(roleInfo, new EventHandler<RolePlayerOrderChangedEventArgs>(UniquenessConstraintOrderChanged), action);

			// Reference constraint expansion elements
			classInfo = dataDir.FindDomainRelationship(ReferenceConstraintTargetsTable.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementAddedEventArgs>(TargetedTableAdded), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(ElementRemoved), action);
			classInfo = dataDir.FindDomainRelationship(ReferenceConstraintContainsColumnReference.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementAddedEventArgs>(ReferenceConstraintColumnReferenceAdded), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(ReferenceConstraintColumnReferenceDeleted), action);
			roleInfo = dataDir.FindDomainRole(ReferenceConstraintContainsColumnReference.ColumnReferenceDomainRoleId);
			eventManager.AddOrRemoveHandler(roleInfo, new EventHandler<RolePlayerOrderChangedEventArgs>(ReferenceConstraintColumnOrderChanged), action);

			// Uniqueness constraint expansion elements
			classInfo = dataDir.FindDomainRelationship(UniquenessConstraintIncludesColumn.DomainClassId);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementAddedEventArgs>(UniquenessConstraintColumnAdded), action);
			eventManager.AddOrRemoveHandler(classInfo, new EventHandler<ElementDeletedEventArgs>(UniquenessConstraintColumnDeleted), action);
		}
		void IModelingEventSubscriber.ManageSurveyQuestionModelingEventHandlers(ModelingEventManager eventManager, bool isReload, EventHandlerAction action)
		{
			ManageSurveyQuestionModelingEventHandlers(eventManager, isReload, action);
		}
		#endregion // IModelingEventSubscriber Implementation
		#region SurveyQuestion event handlers
		private static readonly Type[] SurveyTableChildGlyphTypeQuestionTypes = new Type[] { typeof(SurveyTableChildGlyphType) };
		private static readonly Type[] SurveyColumnClassificationTypeQuestionTypes = new Type[] { typeof(SurveyColumnClassificationType) };
		/// <summary>
		/// This will work for almost all delete scenarios
		/// </summary>
		private static void ElementRemoved(object sender, ElementDeletedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				eventNotify.ElementDeleted(element);
			}
		}
		private static void SchemaAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				eventNotify.ElementAdded(element, null);
			}
		}
		private static void SchemaChanged(object sender, ElementPropertyChangedEventArgs e)
		{
			if (e.DomainProperty.Id == Schema.NameDomainPropertyId)
			{
				INotifySurveyElementChanged eventNotify;
				ModelElement element = e.ModelElement;
				if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
				{
					eventNotify.ElementRenamed(element);
				}
			}
		}
		private static void TableAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				SchemaContainsTable link = element as SchemaContainsTable;
				eventNotify.ElementAdded(link.Table, link.Schema);
			}
		}
		private static void TableChanged(object sender, ElementPropertyChangedEventArgs e)
		{
			if (e.DomainProperty.Id == Table.NameDomainPropertyId)
			{
				INotifySurveyElementChanged eventNotify;
				ModelElement element = e.ModelElement;
				if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
				{
					eventNotify.ElementRenamed(element);
					foreach (ReferenceConstraintTargetsTable targetTableLink in ReferenceConstraintTargetsTable.GetLinksToReferenceConstraints((Table)element))
					{
						eventNotify.ElementRenamed(targetTableLink);
					}
				}
			}
		}
		private static void ColumnAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				TableContainsColumn link = element as TableContainsColumn;
				eventNotify.ElementAdded(link.Column, link.Table);
			}
		}
		private static void ColumnChanged(object sender, ElementPropertyChangedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				Guid attributeId = e.DomainProperty.Id;
				if (attributeId == Column.NameDomainPropertyId)
				{
					eventNotify.ElementRenamed(element);
					Column column = (Column)element;
					foreach (ColumnReference columnRef in ColumnReference.GetLinksToSourceColumnCollection(column))
					{
						eventNotify.ElementRenamed(columnRef);
					}
					foreach (ColumnReference columnRef in ColumnReference.GetLinksToTargetColumnCollection(column))
					{
						eventNotify.ElementRenamed(columnRef);
					}
					foreach (UniquenessConstraintIncludesColumn columnRef in UniquenessConstraintIncludesColumn.GetLinksToUniquenessConstraints(column))
					{
						eventNotify.ElementRenamed(columnRef);
					}
				}
				else if (attributeId == Column.IsNullableDomainPropertyId)
				{
					eventNotify.ElementChanged(e.ModelElement, SurveyColumnClassificationTypeQuestionTypes);
				}
			}
		}
		private static void ReferenceConstraintAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				TableContainsReferenceConstraint link = element as TableContainsReferenceConstraint;
				eventNotify.ElementAdded(link.ReferenceConstraint, link.Table);
			}
		}
		private static void ReferenceConstraintChanged(object sender, ElementPropertyChangedEventArgs e)
		{
			if (e.DomainProperty.Id == ReferenceConstraint.NameDomainPropertyId)
			{
				INotifySurveyElementChanged eventNotify;
				ModelElement element = e.ModelElement;
				if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
				{
					eventNotify.ElementRenamed(element);
				}
			}
		}
		private static void TargetedTableAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				ReferenceConstraintTargetsTable link = element as ReferenceConstraintTargetsTable;
				eventNotify.ElementAdded(link.TargetTable, link.ReferenceConstraint);
			}
		}
		private static void ReferenceConstraintColumnReferenceAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				ReferenceConstraintContainsColumnReference link = (ReferenceConstraintContainsColumnReference)element;
				if (!link.IsDeleted)
				{
					ReferenceConstraint constraint = link.ReferenceConstraint;
					ColumnReference newColumnReference = link.ColumnReference;
					foreach (ColumnReference otherColumnReference in constraint.ColumnReferenceCollection)
					{
						if (otherColumnReference != newColumnReference)
						{
							eventNotify.ElementCustomSortChanged(otherColumnReference);
						}
					}
					eventNotify.ElementAdded(newColumnReference, constraint);
				}
			}
		}
		private static void ReferenceConstraintColumnReferenceDeleted(object sender, ElementDeletedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				ReferenceConstraintContainsColumnReference link = (ReferenceConstraintContainsColumnReference)element;
				ReferenceConstraint constraint = link.ReferenceConstraint;
				eventNotify.ElementDeleted(link.ColumnReference);
				if (!constraint.IsDeleted)
				{
					foreach (ColumnReference columnReference in constraint.ColumnReferenceCollection)
					{
						eventNotify.ElementCustomSortChanged(columnReference);
					}
				}
			}
		}
		private static void ReferenceConstraintColumnOrderChanged(object sender, RolePlayerOrderChangedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.SourceElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				ReferenceConstraint constraint = (ReferenceConstraint)element;
				if (!constraint.IsDeleted)
				{
					foreach (ColumnReference columnRef in constraint.ColumnReferenceCollection)
					{
						eventNotify.ElementCustomSortChanged(columnRef);
					}
				}
			}
		}
		private static void UniquenessConstraintAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				TableContainsUniquenessConstraint link = element as TableContainsUniquenessConstraint;
				eventNotify.ElementAdded(link.UniquenessConstraint, link.Table);
			}
		}
		private static void UniquenessConstraintChanged(object sender, ElementPropertyChangedEventArgs e)
		{

			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				Guid attributeId = e.DomainProperty.Id;
				if (attributeId == UniquenessConstraint.NameDomainPropertyId)
				{
					eventNotify.ElementRenamed(element);
				}
				else if (attributeId == UniquenessConstraint.IsPrimaryDomainPropertyId)
				{
					eventNotify.ElementChanged(element, SurveyTableChildGlyphTypeQuestionTypes);
				}
			}
		}
		private static void UniquenessConstraintOrderChanged(object sender, RolePlayerOrderChangedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.SourceElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				UniquenessConstraint constraint = (UniquenessConstraint)element;
				if (!constraint.IsDeleted)
				{
					foreach (UniquenessConstraintIncludesColumn link in UniquenessConstraintIncludesColumn.GetLinksToColumnCollection(constraint))
					{
						eventNotify.ElementCustomSortChanged(link);
					}
				}
			}
		}
		private static void UniquenessConstraintColumnAdded(object sender, ElementAddedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				UniquenessConstraintIncludesColumn link = (UniquenessConstraintIncludesColumn)element;
				if (!link.IsDeleted)
				{
					UniquenessConstraint constraint = link.UniquenessConstraint;
					foreach (UniquenessConstraintIncludesColumn otherLink in UniquenessConstraintIncludesColumn.GetLinksToColumnCollection(constraint))
					{
						if (otherLink != link)
						{
							eventNotify.ElementCustomSortChanged(otherLink);
						}
					}
					eventNotify.ElementAdded(link, constraint);
				}
			}
		}
		private static void UniquenessConstraintColumnDeleted(object sender, ElementDeletedEventArgs e)
		{
			INotifySurveyElementChanged eventNotify;
			ModelElement element = e.ModelElement;
			if (null != (eventNotify = (element.Store as IORMToolServices).NotifySurveyElementChanged))
			{
				UniquenessConstraintIncludesColumn link = (UniquenessConstraintIncludesColumn)element;
				UniquenessConstraint constraint = link.UniquenessConstraint;
				eventNotify.ElementDeleted(link);
				if (!constraint.IsDeleted)
				{
					foreach (UniquenessConstraintIncludesColumn otherLink in UniquenessConstraintIncludesColumn.GetLinksToColumnCollection(constraint))
					{
						eventNotify.ElementCustomSortChanged(otherLink);
					}
				}
			}
		}
		#endregion // SurveyQuestion event handlers
	}
	#endregion // ISurveyNodeProvider Implementation
}