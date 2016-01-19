using System.Globalization;
using System.Numerics;

namespace elp87.Finance
{
    public class Money2
    {
        #region Fields
        private readonly BigInteger _value;
        private static readonly BigInteger Bi10000 = new BigInteger(10000);
        #endregion

        #region Constructors
        public Money2(double value)
        {
            _value = new BigInteger(value * 10000);
        }

        public Money2(int value)
        {
            _value = new BigInteger(value * 10000);
        }

        private Money2(BigInteger value)
        {
            _value = value;
        }
	    #endregion

        #region Properties
        public double Value { get { return (double)BigInteger.Divide(_value, Bi10000); } }
        #endregion

        #region Methods
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

        public static bool operator ==(Money2 a, Money2 b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;// TODO: Тесты на null
            return (a != null && b != null &&  a._value.Equals(b._value));
        }

        public static bool operator !=(Money2 a, Money2 b)
        {
            return !(a == b);
        }
        #endregion
        
    }
}
