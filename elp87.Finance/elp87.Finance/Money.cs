namespace elp87.Finance
{
    public class Money
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
        public override string ToString()
        {
            return this._value.ToString();
        }

        public override bool Equals(object obj)
        {
            Money value = ((Money)obj).Value;

            return (this.Value == value.Value);
        }

        public override int GetHashCode()
        {
            return (int)this.Value;
        }
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
            return a.Equals(b);
        }

        public static bool operator !=(Money a, Money b)
        {
            return !a.Equals(b);
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
