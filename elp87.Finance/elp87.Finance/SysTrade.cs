using System;

namespace elp87.Finance
{
    public class SysTrade : Trade, ISysTrade
    {
        public Money CumProfit { get; set; }

        public double CumProfitPC { get; set; }

        public Money ContractProfit { get; set; }

        public double ContractProfitPC { get; set; }

        public Money DrawDown { get; set; }

        public double DrawDownPC { get; set; }

        public bool NewContract { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(ISysTrade other)
        {
            if (other != null)
            {
                return this.ExitDateTime.CompareTo(other.ExitDateTime);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
