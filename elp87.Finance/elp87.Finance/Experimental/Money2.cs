using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace elp87.Finance.Experimental
{
    public class Money2 : IComparable
    {
        #region Fields

        private readonly BigInteger _value;
        private static readonly BigInteger Bi10000 = new BigInteger(10000);

        #endregion

        #region Constructors

        public Money2(double value)
        {
            _value = new BigInteger(value*10000);
        }

        public Money2(int value)
        {
            _value = new BigInteger(value*10000);
        }

        private Money2(BigInteger value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        public double Value
        {
            get { return (double) BigInteger.Divide(_value, Bi10000); }
        }

        #endregion

        #region Methods

        #region Override

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Money2 other = (Money2) obj;
            return _value.Equals(other._value);
        }

        protected bool Equals(Money2 other)
        {
            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region Interfaces

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Money2 other = obj as Money2;
            if (other != null)
            {
                return _value.CompareTo(other._value);
            }
            else
            {
                throw new ArgumentException("Object is not Money2");
            }
        }

        #endregion
        #endregion

        #region Operators

        public static Money2 operator +(Money2 a, Money2 b)
        {
            return new Money2(BigInteger.Add(a._value, b._value));
        }

        public static Money2 operator -(Money2 a, Money2 b)
        {
            return new Money2(BigInteger.Subtract(a._value, b._value));
        }

        public static Money2 operator -(Money2 a)
        {
            return new Money2(BigInteger.Negate(a._value));
        }

        public static Money2 operator *(Money2 a, int mult)
        {
            return new Money2(BigInteger.Multiply(a._value, new BigInteger(mult)));
        }

        public static Money2 operator *(Money2 a, double mult)
        {
            return new Money2(BigInteger.Multiply(a._value, new BigInteger(mult)));
        }

        public static Money2 operator /(Money2 a, int divisor)
        {
            return new Money2(BigInteger.Divide(a._value, new BigInteger(divisor)));
        }

        public static Money2 operator /(Money2 a, double divisor)
        {
            return new Money2(BigInteger.Divide(a._value, new BigInteger(divisor)));
        }

        public static bool operator >(Money2 a, Money2 b)
        {
            return (a._value > b._value);
        }

        public static bool operator <(Money2 a, Money2 b)
        {
            return (a._value < b._value);
        }

        public static bool operator >=(Money2 a, Money2 b)
        {
            return (a._value >= b._value);
        }

        public static bool operator <=(Money2 a, Money2 b)
        {
            return (a._value <= b._value);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static bool operator ==(Money2 a, Money2 b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a._value.Equals(b._value);
        }

        public static bool operator !=(Money2 a, Money2 b)
        {
            return !(a == b);
        }

        #endregion

        
    }
}
