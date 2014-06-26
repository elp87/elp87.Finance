﻿using System;

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
            set { _entryPrice = Convert.ToDouble(value); }
        }

        public string TsLabExitPrice
        {
            set { _exitPrice = Convert.ToDouble(value); }
        }
    }
}
