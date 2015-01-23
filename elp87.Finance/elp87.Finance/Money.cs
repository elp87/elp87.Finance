using System;

namespace elp87.Finance
{
    public class Money : IComparable, ICloneable
    {
        #region Fields
        private decimal _value;
        #endregion

        #region Constructors
        public Money(double value)
        {
            this._value = (decimal)value;
        }

        public Money(decimal value)
        {
            this._value = value;
        }

        public Money(int value)
        {
            this._value = value;
        }
        #endregion

        #region Properties
        public decimal Value { get { return _value; } set { _value = value; } }
        #endregion

        #region Methods
        #region Override
        public override string ToString()
        {
            return this._value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Money value = ((Money)obj).Value;

            return (this.Value == value.Value);
        }

        public override int GetHashCode()
        {
            return (int)this.Value;
        } 
        #endregion

        #region Interfaces
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Money otherMoney = obj as Money;
            if (otherMoney != null)
            {
                return this.Value.CompareTo(otherMoney.Value);
            }
            else
            {
                throw new ArgumentException("Object is not Money");
            }
        }

        public object Clone()
        {
            return new Money(this._value);
        }

        #region IConvertible
        /*public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            if (this.Value >= 0) return true;
            else return false;
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return this.Value;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(this.Value);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this.Value);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(this.Value);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(this.Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(this.Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.Value.ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this.Value, conversionType);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.Value);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.Value);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.Value);
        }*/
        #endregion
        #endregion
        #endregion

        #region Operators
        public static Money operator +(Money a, Money b)
        {
            return new Money(a.Value + b.Value);
        }

        public static Money operator -(Money a, Money b)
        {
            return new Money(a.Value - b.Value);
        }

        public static Money operator -(Money a)
        {
            return new Money(-a.Value);
        }

        public static Money operator *(Money money, int mult)
        {
            return new Money(money.Value * mult);
        }

        public static Money operator *(Money money, double mult)
        {
            return new Money(money.Value * (decimal)mult);
        }

        public static Money operator /(Money money, int mult)
        {
            return new Money(money.Value / (decimal)mult);
        }

        public static Money operator /(Money money, double mult)
        {
            return new Money(money.Value / (decimal)mult);
        }

        public static double operator /(Money a, Money b)
        {
            return (double)(a.Value / b.Value);
        }

        public static bool operator >(Money a, Money b)
        {
            return (a.Value > b.Value);
        }

        public static bool operator <(Money a, Money b)
        {
            return (a.Value < b.Value);
        }

        public static bool operator >=(Money a, Money b)
        {
            return (a.Value >= b.Value);
        }

        public static bool operator <=(Money a, Money b)
        {
            return (a.Value <= b.Value);
        }

        public static bool operator ==(Money a, Money b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            else return a.Equals(b);
        }

        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }
        
        #region Implicit
        public static implicit operator Money(int value)
        {
            return new Money(value);
        }

        public static implicit operator Money(decimal value)
        {
            return new Money(value);
        }

        public static implicit operator Money(double value)
        {
            return new Money(value);
        }
        #endregion
        #endregion
    }
}
