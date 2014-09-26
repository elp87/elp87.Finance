using System;

namespace elp87.Finance
{
    public class Trade : ITrade
    {
        #region Fields
        protected DateTime _entryDateTime;
        protected DateTime _exitDateTime;
        protected Money _entryPrice;
        protected Money _exitPrice;
        protected int _count;
        protected string _instrumentName;
        protected bool _isLong;
        #endregion

        #region Properties
        public DateTime EntryDateTime
        {
            get { return _entryDateTime; }
            set { _entryDateTime = value; }
        }

        public DateTime ExitDateTime
        {
            get { return _exitDateTime; }
            set { _exitDateTime = value; }
        }

        public Money EntryPrice
        {
            get { return _entryPrice; }
            set { _entryPrice = value; }
        }

        public Money ExitPrice
        {
            get { return _exitPrice; }
            set { _exitPrice = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public Money EntryVolume
        {
            get { return (_entryPrice * _count); }
        }

        public Money ExitVolume
        {
            get { return (_exitPrice * _count); }
        }

        public Money Profit
        {
            get { return (_isLong) ? (ExitVolume - EntryVolume) : (EntryVolume - ExitVolume); }
        }

        public double ProfitPC
        {
            get { return (Profit / ExitVolume) * 100; }
        }

        public string InstrumentName
        {
            get { return _instrumentName; }
            set { _instrumentName = value; }
        }

        public bool IsLong
        {
            get { return _isLong; }
            set { _isLong = value; }
        }
        #endregion

        #region Methods
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

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
    }
}
