﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50215.44
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Neumont.Tools.ORM.ObjectModel
{
    using System;
    
    /// <summary>
    ///</summary>
    [System.CLSCompliant(true)]
    public enum PortableDataType
    {
        /// <summary>
        ///</summary>
        Unspecified,
        /// <summary>
        ///</summary>
        TextFixedLength,
        /// <summary>
        ///</summary>
        TextVariableLength,
        /// <summary>
        ///</summary>
        TextLargeLength,
        /// <summary>
        ///</summary>
        NumericSignedInteger,
        /// <summary>
        ///</summary>
        NumericUnsignedInteger,
        /// <summary>
        ///</summary>
        NumericAutoCounter,
        /// <summary>
        ///</summary>
        NumericFloatingPoint,
        /// <summary>
        ///</summary>
        NumericDecimal,
        /// <summary>
        ///</summary>
        NumericMoney,
        /// <summary>
        ///</summary>
        RawDataFixedLength,
        /// <summary>
        ///</summary>
        RawDataVariableLength,
        /// <summary>
        ///</summary>
        RawDataLargeLength,
        /// <summary>
        ///</summary>
        RawDataPicture,
        /// <summary>
        ///</summary>
        RawDataOleObject,
        /// <summary>
        ///</summary>
        TemporalAutoTimestamp,
        /// <summary>
        ///</summary>
        TemporalTime,
        /// <summary>
        ///</summary>
        TemporalDate,
        /// <summary>
        ///</summary>
        TemporalDateAndTime,
        /// <summary>
        ///</summary>
        LogicalTrueOrFalse,
        /// <summary>
        ///</summary>
        LogicalYesOrNo,
        /// <summary>
        ///</summary>
        OtherRowId,
        /// <summary>
        ///</summary>
        OtherObjectId,
        /// <summary>
        ///</summary>
        UserDefined,
    }
    /// <summary>
    ///</summary>
    public partial class ORMModel
    {
        private sealed partial class AddIntrinsicDataTypesFixupListener
        {
            private static System.Type[] typeArray = new System.Type[] {
                    typeof(UnspecifiedDataType),
                    typeof(FixedLengthTextDataType),
                    typeof(VariableLengthTextDataType),
                    typeof(LargeLengthTextDataType),
                    typeof(SignedIntegerNumericDataType),
                    typeof(UnsignedIntegerNumericDataType),
                    typeof(AutoCounterNumericDataType),
                    typeof(FloatingPointNumericDataType),
                    typeof(DecimalNumericDataType),
                    typeof(MoneyNumericDataType),
                    typeof(FixedLengthRawDataDataType),
                    typeof(VariableLengthRawDataDataType),
                    typeof(LargeLengthRawDataDataType),
                    typeof(PictureRawDataDataType),
                    typeof(OleObjectRawDataDataType),
                    typeof(AutoTimestampTemporalDataType),
                    typeof(TimeTemporalDataType),
                    typeof(DateTemporalDataType),
                    typeof(DateAndTimeTemporalDataType),
                    typeof(TrueOrFalseLogicalDataType),
                    typeof(YesOrNoLogicalDataType),
                    typeof(RowIdOtherDataType),
                    typeof(ObjectIdOtherDataType)};
        }
    }
    /// <summary>
    ///</summary>
    public partial class UnspecifiedDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.Unspecified;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeUnspecified;
        }
    }
    /// <summary>
    ///</summary>
    public partial class FixedLengthTextDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.TextFixedLength;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeTextFixedLength;
        }
    }
    /// <summary>
    ///</summary>
    public partial class VariableLengthTextDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.TextVariableLength;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeTextVariableLength;
        }
    }
    /// <summary>
    ///</summary>
    public partial class LargeLengthTextDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.TextLargeLength;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeTextLargeLength;
        }
    }
    /// <summary>
    ///</summary>
    public partial class SignedIntegerNumericDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.NumericSignedInteger;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeNumericSignedInteger;
        }
    }
    /// <summary>
    ///</summary>
    public partial class UnsignedIntegerNumericDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.NumericUnsignedInteger;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeNumericUnsignedInteger;
        }
    }
    /// <summary>
    ///</summary>
    public partial class AutoCounterNumericDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.NumericAutoCounter;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeNumericAutoCounter;
        }
    }
    /// <summary>
    ///</summary>
    public partial class FloatingPointNumericDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.NumericFloatingPoint;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeNumericFloatingPoint;
        }
    }
    /// <summary>
    ///</summary>
    public partial class DecimalNumericDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.NumericDecimal;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeNumericDecimal;
        }
    }
    /// <summary>
    ///</summary>
    public partial class MoneyNumericDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.NumericMoney;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeNumericMoney;
        }
    }
    /// <summary>
    ///</summary>
    public partial class FixedLengthRawDataDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.RawDataFixedLength;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeRawDataFixedLength;
        }
    }
    /// <summary>
    ///</summary>
    public partial class VariableLengthRawDataDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.RawDataVariableLength;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeRawDataVariableLength;
        }
    }
    /// <summary>
    ///</summary>
    public partial class LargeLengthRawDataDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.RawDataLargeLength;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeRawDataLargeLength;
        }
    }
    /// <summary>
    ///</summary>
    public partial class PictureRawDataDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.RawDataPicture;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeRawDataPicture;
        }
    }
    /// <summary>
    ///</summary>
    public partial class OleObjectRawDataDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.RawDataOleObject;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeRawDataOleObject;
        }
    }
    /// <summary>
    ///</summary>
    public partial class AutoTimestampTemporalDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.TemporalAutoTimestamp;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeTemporalAutoTimestamp;
        }
    }
    /// <summary>
    ///</summary>
    public partial class TimeTemporalDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.TemporalTime;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeTemporalTime;
        }
    }
    /// <summary>
    ///</summary>
    public partial class DateTemporalDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.TemporalDate;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeTemporalDate;
        }
    }
    /// <summary>
    ///</summary>
    public partial class DateAndTimeTemporalDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.TemporalDateAndTime;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeTemporalDateAndTime;
        }
    }
    /// <summary>
    ///</summary>
    public partial class TrueOrFalseLogicalDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.LogicalTrueOrFalse;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeLogicalTrueOrFalse;
        }
    }
    /// <summary>
    ///</summary>
    public partial class YesOrNoLogicalDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.LogicalYesOrNo;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeLogicalYesOrNo;
        }
    }
    /// <summary>
    ///</summary>
    public partial class RowIdOtherDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.OtherRowId;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeOtherRowId;
        }
    }
    /// <summary>
    ///</summary>
    public partial class ObjectIdOtherDataType
    {
        /// <summary>
        ///</summary>
        public override PortableDataType PortableDataType
        {
            get
            {
                return PortableDataType.OtherObjectId;
            }
        }
        /// <summary>
        ///</summary>
        public override string ToString()
        {
            return ResourceStrings.PortableDataTypeOtherObjectId;
        }
    }
}
