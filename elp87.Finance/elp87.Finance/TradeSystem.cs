using System;
using System.Collections.Generic;
using System.Linq;

namespace elp87.Finance
{
    public partial class TradeSystem
    {
        #region Constructors
        public TradeSystem()
        {
            this.TradeList = new List<ISysTrade>();
            this.Properties = new TradeSystemProperties(this);
        }
        #endregion

        #region Properties
        public List<ISysTrade> TradeList { get; set; }

        public string Name { get; set; }

        public string Ticker { get; set; }

        public string FullName
        {
            get { return (this.Ticker + " - " + this.Name); }
        }

        public TradeSystemProperties Properties { get; set; }
        #endregion

        #region Methods
        public void CalcTradeProperties()
        {
            if (this.TradeList.Count > 0)
            {
                double maxProfit = 0;
                double maxProfitPC = 0;

                ISysTrade firstTrade = this.TradeList.First();
                firstTrade.CumProfit = firstTrade.Profit;
                firstTrade.CumProfitPC = firstTrade.ProfitPC;
                firstTrade.ContractProfit = firstTrade.Profit;
                firstTrade.ContractProfitPC = firstTrade.ProfitPC;

                if (firstTrade.Profit > 0)
                {
                    maxProfit = firstTrade.Profit;
                    maxProfitPC = firstTrade.ProfitPC;
                    firstTrade.DrawDown = 0;
                    firstTrade.DrawDownPC = 0;
                }
                else
                {
                    firstTrade.DrawDown = -firstTrade.Profit;
                    firstTrade.DrawDownPC = -firstTrade.ProfitPC;
                }

                for (int tradeNum = 1; tradeNum < this.TradeList.Count; tradeNum++)
                {
                    ISysTrade curTrade = this.TradeList[tradeNum];
                    ISysTrade prevTrade = this.TradeList[tradeNum - 1];

                    curTrade.CumProfit = prevTrade.CumProfit + curTrade.Profit;
                    curTrade.CumProfitPC = Math.Round(prevTrade.CumProfitPC + curTrade.ProfitPC, 2);

                    if (!curTrade.NewContract)
                    {
                        curTrade.ContractProfit = prevTrade.ContractProfit + curTrade.Profit;
                        curTrade.ContractProfitPC = Math.Round(prevTrade.ContractProfitPC + curTrade.ProfitPC, 2);
                    }
                    else
                    {
                        curTrade.ContractProfit = curTrade.Profit;
                        curTrade.ContractProfitPC = curTrade.ProfitPC;
                    }

                    if (curTrade.CumProfit > maxProfit) maxProfit = curTrade.CumProfit;
                    if (curTrade.CumProfitPC > maxProfitPC) maxProfitPC = curTrade.CumProfitPC;
                    curTrade.DrawDown = maxProfit - curTrade.CumProfit;
                    curTrade.DrawDownPC = maxProfitPC - curTrade.CumProfitPC;
                }
            }
        }
        #endregion
    }
}
