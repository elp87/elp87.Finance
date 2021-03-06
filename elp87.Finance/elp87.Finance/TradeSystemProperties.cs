﻿using elp87.Finance.Helpers;
using elp87.Finance.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace elp87.Finance
{
    public class TradeProperty
    {
        public string PropName { get; set; }
        public string PropValueAll { get; set; }
        public string PropValueLong { get; set; }
        public string PropValueShort { get; set; }
    }

    public class TradeSystemProperties
    {
        #region Constants
        private const int _daysInYear = 365;
        private const int _daysInMonth = 30;
        #endregion

        #region Fields
        private TradeSystem _parent;
        #endregion

        #region Constructors
        public TradeSystemProperties(TradeSystem parent)
        {
            this._parent = parent;
        }
        #endregion

        #region Properties
        #region Private
        private Money _sumProfit
        {
            get { return _parent.Trades.Sum(trade => trade.Profit.Value); }
        }

        private Money _sumProfitLong
        {
            get { return _parent.Trades.Where(trade => trade.IsLong).Sum(trade => trade.Profit.Value); }
        }

        private Money _sumProfitShort
        {
            get { return _parent.Trades.Where(trade => trade.IsLong).Sum(trade => trade.Profit.Value); }
        }

        private double _sumProfitPC
        {
            get { return _parent.Trades.Sum(trade => trade.ProfitPC); }
        }

        private double _sumProfitPCLong
        {
            get { return _parent.Trades.Where(trade => trade.IsLong).Sum(trade => trade.ProfitPC); }
        }

        private double _sumProfitPCShort
        {
            get { return _parent.Trades.Where(trade => !trade.IsLong).Sum(trade => trade.ProfitPC); }
        }

        private DateTime _beginDate
        {
            get { return _parent.Trades.Min(trade => trade.EntryDateTime); }
        }

        private DateTime _endDate
        {
            get { return _parent.Trades.Max(trade => trade.ExitDateTime); }
        }

        private double _tradePeriod
        {
            get { return (this._endDate - this._beginDate).TotalDays; }
        }
        #endregion

        #region CumProfit
        public string CumProfitAll
        {
            get { return _parent.Trades.Sum(trade => trade.Profit.Value).ToStringBase(); }
        }

        public string CumProfitLong
        {
            get 
            {                 
                var trades = _parent.Trades.Where(trade => trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase(); 
            }
        }

        public string CumProfitShort
        {
            get 
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase(); 
            }
        }
        #endregion

        #region CumProfitPC
        public string CumProfitPCAll
        {
            get { return (this._sumProfitPC.ToStringShortFloat() + "%"); }
        }

        public string CumProfitPCLong
        {
            get { return (this._sumProfitPCLong.ToStringShortFloat() + "%"); }
        }

        public string CumProfitPCShort
        {
            get { return (this._sumProfitPCShort.ToStringShortFloat() + "%"); }
        }
        #endregion

        #region YearProfit
        public string YearProfitAll
        {
            get { return ((this._sumProfitPC / this._tradePeriod * _daysInYear).ToStringShortFloat() + "%"); }
        }

        public string YearProfitLong
        {
            get { return ((this._sumProfitPCLong / this._tradePeriod * _daysInYear).ToStringShortFloat() + "%"); }
        }

        public string YearProfitShort
        {
            get { return ((this._sumProfitPCShort / this._tradePeriod * _daysInYear).ToStringShortFloat() + "%"); }
        }
        #endregion

        #region MonthProfit
        public string MonthProfitAll
        {
            get { return ((this._sumProfitPC / this._tradePeriod * _daysInMonth).ToStringShortFloat() + "%"); }
        }

        public string MonthProfitLong
        {
            get { return ((this._sumProfitPCLong / this._tradePeriod * _daysInMonth).ToStringShortFloat() + "%"); }
        }

        public string MonthProfitShort
        {
            get { return ((this._sumProfitPCShort / this._tradePeriod * _daysInMonth).ToStringShortFloat() + "%"); }
        }
        #endregion

        #region TradeCount
        public string TradeCountAll
        {
            get { return _parent.TradeList.Count.ToString(); }
        }

        public string TradeCountLong
        {
            get { return _parent.Trades.Count(trade => trade.IsLong).ToString(); }
        }

        public string TradeCountShort
        {
            get { return _parent.Trades.Count(trade => !trade.IsLong).ToString(); }
        }
        #endregion

        #region TradeProfitAverage
        public string TradeProfitAverageAll
        {
            get { return _parent.Trades.Average(trade => trade.Profit.Value).ToStringFloat(); }
        }

        public string TradeProfitAverageLong
        {
            get
            {
                var longTrades = _parent.Trades.Where(trade => trade.IsLong);
                if (longTrades.Count() == 0) return "";
                else return longTrades.Average(trade => trade.Profit.Value).ToStringFloat();
            }
        }

        public string TradeProfitAverageShort
        {
            get
            {
                var shortTrades = _parent.Trades.Where(trade => !trade.IsLong);
                if (shortTrades.Count() == 0) return "";
                else return shortTrades.Average(trade => trade.Profit.Value).ToStringFloat();
            }
        }
        #endregion

        #region AverageTradeProfitPC
        public string TradeProfitPCAverageAll
        {
            get { return _parent.Trades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
        }

        public string TradeProfitPCAverageLong
        {
            get
            {
                var longTrades = _parent.Trades.Where(trade => trade.IsLong);
                if (longTrades.Count() == 0) return "";
                return longTrades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }

        public string TradeProfitPCAverageShort
        {
            get
            {
                var shortTrades = _parent.Trades.Where(trade => !trade.IsLong);
                if (shortTrades.Count() == 0) return "";
                return shortTrades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }
        #endregion

        #region WinTradeCount
        public string WinTradeCountAll
        {
            get { return _parent.Trades.Count(trade => trade.Profit.Value > 0).ToString(); }
        }

        public string WinTradeCountLong
        {
            get { return _parent.Trades.Count(trade => trade.IsLong && (trade.Profit.Value > 0)).ToString(); }
        }

        public string WinTradeCountShort
        {
            get { return _parent.Trades.Count(trade => !trade.IsLong && (trade.Profit.Value > 0)).ToString(); }
        }
        #endregion

        #region WinTradeCountPC
        public string WinTradeCountPCAll
        {
            get { return (_parent.Trades.PercentageCount(trade => trade.Profit.Value > 0).ToStringShortFloat() + "%"); }
        }

        public string WinTradeCountPCLong
        {
            get 
            {                 
                var trades = _parent.Trades.Where(trade => trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.PercentageCount(trade => trade.Profit.Value > 0).ToStringShortFloat() + "%"; 
            }
        }

        public string WinTradeCountPCShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.PercentageCount(trade => trade.Profit.Value > 0).ToStringShortFloat() + "%";
            }
        }
        #endregion

        #region WinProfitSum
        public string WinProfitSumAll
        {
            get 
            { 
                var trades = _parent.Trades.Where(trade => trade.Profit.Value > 0);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase(); ; 
            }
        }

        public string WinProfitSumLong
        {
            get 
            {                 
                var trades = _parent.Trades.Where(trade => trade.IsLong && trade.Profit.Value > 0);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase();
            }
        }

        public string WinProfitSumShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong && trade.Profit.Value > 0);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase();
            }
        }
        #endregion

        #region WinProfitAverage
        public string WinProfitAverageAll
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.Profit.Value > 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.Profit.Value).ToStringBase();
            }
        }

        public string WinProfitAverageLong
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.IsLong && trade.Profit.Value > 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.Profit.Value).ToStringBase();
            }
        }

        public string WinProfitAverageShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong && trade.Profit.Value > 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.Profit.Value).ToStringBase();
            }
        }
        #endregion

        #region WinProfitPCAverage
        public string WinProfitPCAverageAll
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.ProfitPC > 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }

        public string WinProfitPCAverageLong
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.IsLong && trade.ProfitPC > 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }

        public string WinProfitPCAverageShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong && trade.ProfitPC > 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }
        #endregion

        #region MaxWinRow
        public string MaxWinRowAll
        {
            get { return _parent.Trades.MaxCountInRow(trade => trade.Profit.Value > 0).ToString(); }
        }

        public string MaxWinRowLong
        {
            get 
            { 
                var trades = _parent.Trades.Where(trade => trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.MaxCountInRow(trade => trade.Profit.Value > 0).ToString();
            }
        }

        public string MaxWinRowShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.MaxCountInRow(trade => trade.Profit.Value > 0).ToString();
            }
        }
        #endregion

        #region LoseTradeCount
        public string LoseTradeCountAll
        {
            get { return _parent.Trades.Count(trade => trade.Profit.Value <= 0).ToString(); }
        }

        public string LoseTradeCountLong
        {
            get { return _parent.Trades.Count(trade => trade.IsLong && (trade.Profit.Value <= 0)).ToString(); }
        }

        public string LoseTradeCountShort
        {
            get { return _parent.Trades.Count(trade => !trade.IsLong && (trade.Profit.Value <= 0)).ToString(); }
        }
        #endregion

        #region LoseTradeCountPC
        public string LoseTradeCountPCAll
        {
            get { return (_parent.Trades.PercentageCount(trade => trade.Profit.Value <= 0).ToStringShortFloat() + "%"); }
        }

        public string LoseTradeCountPCLong
        {
            get 
            { 
                var trades = _parent.Trades.Where(trade => trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.PercentageCount(trade => trade.Profit.Value <= 0).ToStringShortFloat() + "%";
            }
        }

        public string LoseTradeCountPCShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong);
                if (trades.Count() == 0) return "0";
                else return trades.PercentageCount(trade => trade.Profit.Value <= 0).ToStringShortFloat() + "%";
            }
        }
        #endregion

        #region LoseProfitSum
        public string LoseProfitSumAll
        {
            get 
            {                 
                var trades = _parent.Trades.Where(trade => trade.Profit.Value <= 0);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase();
            }
        }

        public string LoseProfitSumLong
        {
            get 
            { 
                var trades = _parent.Trades.Where(trade => trade.IsLong && trade.Profit.Value <= 0);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase();
            }
        }

        public string LoseProfitSumShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong && trade.Profit.Value <= 0);
                if (trades.Count() == 0) return "0";
                else return trades.Sum(trade => trade.Profit.Value).ToStringBase();
            }
        }
        #endregion

        #region LoseProfitAverage
        public string LoseProfitAverageAll
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.Profit.Value <= 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.Profit.Value).ToStringBase();
            }
        }

        public string LoseProfitAverageLong
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.IsLong && trade.Profit.Value <= 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.Profit.Value).ToStringBase();
            }
        }

        public string LoseProfitAverageShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong && trade.Profit.Value <= 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.Profit.Value).ToStringBase();
            }
        }
        #endregion

        #region LoseProfitPCAverage
        public string LoseProfitPCAverageAll
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.ProfitPC <= 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }

        public string LoseProfitPCAverageLong
        {
            get
            {
                var trades = _parent.Trades.Where(trade => trade.IsLong && trade.ProfitPC <= 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }

        public string LoseProfitPCAverageShort
        {
            get
            {
                var trades = _parent.Trades.Where(trade => !trade.IsLong && trade.ProfitPC <= 0);
                if (trades.Count() == 0) return "";
                else return trades.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%";
            }
        }
        #endregion

        #region MaxLoseRow
        public string MaxLoseRowAll
        {
            get 
            { 
                return _parent.Trades.MaxCountInRow(trade => trade.Profit.Value <= 0).ToString(); 
            }
        }

        public string MaxLoseRowLong
        {
            get 
            { 
                //return _parent.Trades.Where(trade => trade.IsLong).MaxCountInRow(trade => trade.Profit.Value <= 0).ToString(); 
                var trades = _parent.Trades.Where(trade => trade.IsLong);
                if (trades.Count() == 0) return "";
                else return trades.MaxCountInRow(trade => trade.Profit.Value <= 0).ToString();
            }
        }

        public string MaxLoseRowShort
        {
            get { return _parent.Trades.Where(trade => !trade.IsLong).MaxCountInRow(trade => trade.Profit.Value <= 0).ToString(); }
        }
        #endregion

        #region MaxDrawDown
        public string MaxDrawDownAll
        {
            get { return _parent.Trades.MaxDrawDown().Value.ToStringBase(); }
        }

        public string MaxDrawDownLong
        {
            get { return _parent.Trades.MaxDrawDown(trade => trade.IsLong).Value.ToStringBase(); }
        }

        public string MaxDrawDownShort
        {
            get { return _parent.Trades.MaxDrawDown(trade => !trade.IsLong).Value.ToStringBase(); }
        }
        #endregion

        #region MaxDrawDownDate
        public string MaxDrawDownDateAll
        {
            get { return _parent.Trades.MaxDrawDownDate().ToStandardString(); }
        }

        public string MaxDrawDownDateLong
        {
            get { return _parent.Trades.MaxDrawDownDate(trade => trade.IsLong).ToStandardString(); }
        }

        public string MaxDrawDownDateShort
        {
            get { return _parent.Trades.MaxDrawDownDate(trade => !trade.IsLong).ToStandardString(); }
        }
        #endregion

        #region MaxDrawDownPC
        public string MaxDrawDownPCAll
        {
            get { return _parent.Trades.MaxDrawDownPC().ToStringShortFloat() + "%"; }
        }

        public string MaxDrawDownPCLong
        {
            get { return _parent.Trades.MaxDrawDownPC(trade => trade.IsLong).ToStringShortFloat() + "%"; }
        }

        public string MaxDrawDownPCShort
        {
            get { return _parent.Trades.MaxDrawDownPC(trade => !trade.IsLong).ToStringShortFloat() + "%"; }
        }
        #endregion

        #region MaxDrawDownPCDate
        public string MaxDrawDownPCDateAll
        {
            get { return _parent.Trades.MaxDrawDownPCDate().ToStandardString(); }
        }

        public string MaxDrawDownPCDateLong
        {
            get { return _parent.Trades.MaxDrawDownPCDate(trade => trade.IsLong).ToStandardString(); }
        }

        public string MaxDrawDownPCDateShort
        {
            get { return _parent.Trades.MaxDrawDownPCDate(trade => !trade.IsLong).ToStandardString(); }
        }
        #endregion

        #region ProfitFactor
        public string ProfitFactorAll
        {
            get { return _parent.Trades.ProfitFactor().ToStringShortFloat(); }
        }

        public string ProfitFactorLong
        {
            get { return _parent.Trades.ProfitFactor(trade => trade.IsLong).ToStringShortFloat(); }
        }

        public string ProfitFactorShort
        {
            get { return _parent.Trades.ProfitFactor(trade => !trade.IsLong).ToStringShortFloat(); }
        }
        #endregion

        #region RecoveryFactor
        public string RecoveryFactorAll
        {
            get { return _parent.Trades.RecoveryFactor().ToStringShortFloat(); }
        }

        public string RecoveryFactorLong
        {
            get { return _parent.Trades.RecoveryFactor(trade => trade.IsLong).ToStringShortFloat(); }
        }

        public string RecoveryFactorShort
        {
            get { return _parent.Trades.RecoveryFactor(trade => !trade.IsLong).ToStringShortFloat(); }
        }
        #endregion

        #region PayoffRatio
        public string PayoffRatioAll
        {
            get { return _parent.Trades.PayoffRatio().ToStringShortFloat(); }
        }

        public string PayoffRatioLong
        {
            get { return _parent.Trades.PayoffRatio(trade => trade.IsLong).ToStringShortFloat(); }
        }

        public string PayoffRatioShort
        {
            get { return _parent.Trades.PayoffRatio(trade => !trade.IsLong).ToStringShortFloat(); }
        }
        #endregion
        #endregion

        #region Methods
        public List<TradeProperty> GetPropertyList()
        {
            if (_parent.TradeList.Count > 0)
            {
                #region GenPropertyList
                List<TradeProperty> propertyList = new List<TradeProperty>()
                    {
                        new TradeProperty() {
                            PropName = Resources.prop_ClearProfit,
                            PropValueAll = this.CumProfitAll,
                            PropValueLong = this.CumProfitLong,
                            PropValueShort = this.CumProfitShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_ClearProfitPC,
                            PropValueAll = this.CumProfitPCAll,
                            PropValueLong = this.CumProfitPCLong,
                            PropValueShort = this.CumProfitPCShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_YearProfit,
                            PropValueAll = this.YearProfitAll,
                            PropValueLong = this.YearProfitLong,
                            PropValueShort = this.YearProfitShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_MonthProfit,
                            PropValueAll = this.MonthProfitAll,
                            PropValueLong = this.MonthProfitLong,
                            PropValueShort = this.MonthProfitShort
                        },
                        new TradeProperty(),

                        new TradeProperty() {
                            PropName = Resources.prop_TradeCount,
                            PropValueAll = this.TradeCountAll,
                            PropValueLong = this.TradeCountLong,
                            PropValueShort = this.TradeCountShort
                        },
                        new TradeProperty() {
                            PropName= Resources.prop_AvgProfit,
                            PropValueAll = this.TradeProfitAverageAll,
                            PropValueLong = this.TradeProfitAverageLong,
                            PropValueShort = this.TradeProfitAverageShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_AvgProfitPC,
                            PropValueAll = this.TradeProfitPCAverageAll,
                            PropValueLong = this.TradeProfitPCAverageLong,
                            PropValueShort = this.TradeProfitPCAverageShort
                        },
                        new TradeProperty(),

                        new TradeProperty() {
                            PropName = Resources.prop_WinTradeCount,
                            PropValueAll = this.WinTradeCountAll,
                            PropValueLong = this.WinTradeCountLong,
                            PropValueShort = this.WinTradeCountShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_WinTradeCountPC,
                            PropValueAll = this.WinTradeCountPCAll,
                            PropValueLong = this.WinTradeCountPCLong,
                            PropValueShort = this.WinTradeCountPCShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_WinProfitSum,
                            PropValueAll = this.WinProfitSumAll,
                            PropValueLong = this.WinProfitSumLong,
                            PropValueShort = this.WinProfitSumShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_AvgWinProfit,
                            PropValueAll = this.WinProfitAverageAll,
                            PropValueLong = this.WinProfitAverageLong,
                            PropValueShort = this.WinProfitAverageShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_AvgWinProfitPC,
                            PropValueAll = this.WinProfitPCAverageAll,
                            PropValueLong = this.WinProfitPCAverageLong,
                            PropValueShort = this.WinProfitPCAverageShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_MaxWinRow,
                            PropValueAll = this.MaxWinRowAll,
                            PropValueLong = this.MaxWinRowLong,
                            PropValueShort = this.MaxWinRowShort
                        },
                        new TradeProperty(),

                        new TradeProperty() {
                            PropName = Resources.prop_LoseTradeCount,
                            PropValueAll = this.LoseTradeCountAll,
                            PropValueLong = this.LoseTradeCountLong,
                            PropValueShort = this.LoseTradeCountShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_LoseTradeCountPC,
                            PropValueAll = this.LoseTradeCountPCAll,
                            PropValueLong = this.LoseTradeCountPCLong,
                            PropValueShort = this.LoseTradeCountPCShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_LoseProfitSum,
                            PropValueAll = this.LoseProfitSumAll,
                            PropValueLong = this.LoseProfitSumLong,
                            PropValueShort = this.LoseProfitSumShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_AvgLoseProfit,
                            PropValueAll = this.LoseProfitAverageAll,
                            PropValueLong = this.LoseProfitAverageLong,
                            PropValueShort = this.LoseProfitAverageShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_AvgLoseProfitPC,
                            PropValueAll = this.LoseProfitPCAverageAll,
                            PropValueLong = this.LoseProfitPCAverageLong,
                            PropValueShort = this.LoseProfitPCAverageShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_MaxLoseRow,
                            PropValueAll = this.MaxLoseRowAll,
                            PropValueLong = this.MaxLoseRowLong,
                            PropValueShort = this.MaxLoseRowShort
                        },
                        new TradeProperty(),

                        new TradeProperty() {
                            PropName = Resources.prop_MaxDD,
                            PropValueAll = this.MaxDrawDownAll,
                            PropValueLong = this.MaxDrawDownLong,
                            PropValueShort = this.MaxDrawDownShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_MaxDDDate,
                            PropValueAll = this.MaxDrawDownDateAll,
                            PropValueLong = this.MaxDrawDownDateLong,
                            PropValueShort = this.MaxDrawDownDateShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_MaxDDPC,
                            PropValueAll = this.MaxDrawDownPCAll,
                            PropValueLong = this.MaxDrawDownPCLong,
                            PropValueShort = this.MaxDrawDownPCShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_MaxDDDate,
                            PropValueAll = this.MaxDrawDownPCDateAll,
                            PropValueLong = this.MaxDrawDownPCDateLong,
                            PropValueShort = this.MaxDrawDownPCDateShort
                        },
                        new TradeProperty(),

                        new TradeProperty() {
                            PropName = Resources.prop_ProfitFactor,
                            PropValueAll = this.ProfitFactorAll,
                            PropValueLong = this.ProfitFactorLong,
                            PropValueShort = this.ProfitFactorShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_RecoveryFactor,
                            PropValueAll = this.RecoveryFactorAll,
                            PropValueLong = this.RecoveryFactorLong,
                            PropValueShort = this.RecoveryFactorShort
                        },
                        new TradeProperty() {
                            PropName = Resources.prop_PayoffRatio,
                            PropValueAll = this.PayoffRatioAll,
                            PropValueLong = this.PayoffRatioLong,
                            PropValueShort = this.PayoffRatioShort
                        }
                    };
                #endregion
                return propertyList;
            }
            else { return null; }
        }
        #endregion
    }


}
