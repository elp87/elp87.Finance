using System;

namespace elp87.Finance
{
    public class Trade : ITrade, ICloneable
    {
        #region Fields

        #endregion

        #region Properties
        public DateTime EntryDateTime { get; set; }

        public DateTime ExitDateTime { get; set; }

        public Money EntryPrice { get; set; }

        public Money ExitPrice { get; set; }

        public int Count { get; set; }

        public Money EntryVolume
        {
            get { return (EntryPrice * Count); }
        }

        public Money ExitVolume
        {
            get { return (ExitPrice * Count); }
        }

        public Money Profit
        {
            get { return (IsLong) ? (ExitVolume - EntryVolume) : (EntryVolume - ExitVolume); }
        }

        public double ProfitPC
        {
            get { return (Profit / ExitVolume) * 100; }
        }

        public string InstrumentName { get; set; }

        public bool IsLong { get; set; }

        public string DealType
        {
            get
            {
                if (IsLong) return "Long";
                else return "Short";
            }
        }
        #endregion

        #region Methods
        #region Override
        public override bool Equals(Object obj)
        {
            Trade eqTrade = obj as Trade;
            if (eqTrade == null) return false;


            if ((this.EntryDateTime == eqTrade.EntryDateTime) &&
                (this.EntryPrice == eqTrade.EntryPrice) &&
                (this.ExitDateTime == eqTrade.ExitDateTime) &&
                (this.ExitPrice == eqTrade.ExitPrice) &&
                (this.Count == eqTrade.Count) &&
                (this.IsLong == eqTrade.IsLong))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        } 
        #endregion

        #region Interfaces
        public object Clone()
        {
            Trade cloneTrade = this.MemberwiseClone() as Trade;
            return cloneTrade;
        }
        #endregion
        #endregion

        
    }
}
