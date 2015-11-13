using System;

namespace elp87.Finance
{
    public class TsLabTrade : SysTrade
    {
        public string TsLabDealType
        {
            set { IsLong = (value == "Длинная") ? true : false; }
        }

        public string TsLabEntryDateTime
        {
            set { EntryDateTime = DateTime.Parse(value); }
        }

        public string TsLabExitDateTime
        {
            set { ExitDateTime = DateTime.Parse(value); }
        }

        public string TsLabEntryPrice
        {
            set { EntryPrice = Convert.ToDecimal(value); }
        }

        public string TsLabExitPrice
        {
            set { ExitPrice = Convert.ToDecimal(value); }
        }
    }
}
