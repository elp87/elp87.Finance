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
        #endregion
        
    }
}
