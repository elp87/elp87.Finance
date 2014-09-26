using System;

namespace elp87.Finance
{
    public class TsLabTrade : SysTrade
    {
        public string TsLabDealType
        {
            set { _isLong = (value == "Длинная") ? true : false; }
        }

        public string TsLabEntryDateTime
        {
            set { _entryDateTime = DateTime.Parse(value); }
        }

        public string TsLabExitDateTime
        {
            set { _exitDateTime = DateTime.Parse(value); }
        }

        public string TsLabEntryPrice
        {
            set { _entryPrice = Convert.ToDecimal(value); }
        }

        public string TsLabExitPrice
        {
            set { _exitPrice = Convert.ToDecimal(value); }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
