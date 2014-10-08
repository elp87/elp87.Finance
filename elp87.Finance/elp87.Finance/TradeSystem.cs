using System;
using System.Collections.Generic;
using System.Linq;

namespace elp87.Finance
{
    public partial class TradeSystem
    {
        private List<ISysTrade> _tradeList;
        #region Constructors
        public TradeSystem()
        {
            this._tradeList = new List<ISysTrade>();
            this.Properties = new TradeSystemProperties(this);
        }

        public TradeSystem(List<ISysTrade> trades)
        {
            this._tradeList = trades;
            this.Properties = new TradeSystemProperties(this);
            this.CalcTradeProperties();
        }
        #endregion

        #region Properties
        public ISysTrade[] Trades
        {
            get
            {
                return this._tradeList.ToArray();
            }
        }

        public string Name { get; set; }

        public string Ticker { get; set; }

        public string FullName
        {
            get { return (this.Ticker + " - " + this.Name); }
        }

        public TradeSystemProperties Properties { get; set; }
        #endregion

        #region Methods
        #region Public
        public void AddTrade(ISysTrade trade)
        {
            this._tradeList.Add(trade);
            // CalcTradeProperties() очень тяжелый. Пихать сюда его не нужно. Запускать только после полного сбора списка сделок, а не после добавления каждого трейда
        }

        public void Sort()
        {
            this._tradeList.Sort();
        }
        #endregion

        #region Protected
        public void CalcTradeProperties()
        {
            if (this._tradeList.Count > 0)
            {
                Money maxProfit = 0;
                double maxProfitPC = 0;

                ISysTrade firstTrade = this.Trades.First();
                firstTrade.CumProfit = firstTrade.Profit;
                firstTrade.CumProfitPC = firstTrade.ProfitPC;
                firstTrade.ContractProfit = firstTrade.Profit;
                firstTrade.ContractProfitPC = firstTrade.ProfitPC;

                if (firstTrade.Profit.Value > 0)
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

                for (int tradeNum = 1; tradeNum < this._tradeList.Count; tradeNum++)
                {
                    ISysTrade curTrade = this.Trades[tradeNum];
                    ISysTrade prevTrade = this.Trades[tradeNum - 1];

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
        #endregion
    }
}
