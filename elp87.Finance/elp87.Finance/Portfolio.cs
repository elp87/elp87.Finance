using System.Collections.Generic;

namespace elp87.Finance
{
    public class Portfolio
    {
        public Portfolio()
        {
            TradeSystems = new List<TradeSystem>();
            Days = new List<TradeDay>();
        }
        public List<TradeSystem> TradeSystems { get; set; }
        public List<TradeDay> Days { get; set; }
    }
}
