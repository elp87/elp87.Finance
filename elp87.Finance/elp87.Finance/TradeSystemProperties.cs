using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using elp87.Finance.Helpers;
using elp87.Finance.Properties;

namespace elp87.Finance
{
    public partial class TradeSystem
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
            private double _sumProfit
            {
                get { return _parent.TradeList.Sum(trade => trade.Profit); }
            }

            private double _sumProfitLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).Sum(trade => trade.Profit); }
            }

            private double _sumProfitShort
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).Sum(trade => trade.Profit); }
            }

            private double _sumProfitPC
            {
                get { return _parent.TradeList.Sum(trade => trade.ProfitPC); }
            }

            private double _sumProfitPCLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).Sum(trade => trade.ProfitPC); }
            }

            private double _sumProfitPCShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong).Sum(trade => trade.ProfitPC); }
            }

            private DateTime _beginDate
            {
                get { return _parent.TradeList.Min(trade => trade.EntryDateTime); }
            }

            private DateTime _endDate
            {
                get { return _parent.TradeList.Max(trade => trade.ExitDateTime); }
            }

            private double _tradePeriod
            {
                get { return (this._endDate - this._beginDate).TotalDays; }
            }
            #endregion

            #region CumProfit
            public string CumProfitAll
            {
                get { return _parent.TradeList.Sum(trade => trade.Profit).ToStringBase(); }
            }

            public string CumProfitLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).Sum(trade => trade.Profit).ToStringBase(); }
            }

            public string CumProfitShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong).Sum(trade => trade.Profit).ToStringBase(); }
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
                get { return _parent.TradeList.Count(trade => trade.IsLong).ToString(); }
            }

            public string TradeCountShort
            {
                get { return _parent.TradeList.Count(trade => !trade.IsLong).ToString(); }
            }
            #endregion

            #region TradeProfitAverage
            public string TradeProfitAverageAll
            {
                get { return _parent.TradeList.Average(trade => trade.Profit).ToStringFloat(); }
            }

            public string TradeProfitAverageLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).Average(trade => trade.Profit).ToStringFloat(); }
            }

            public string TradeProfitAverageShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong).Average(trade => trade.Profit).ToStringFloat(); }
            }
            #endregion

            #region AverageTradeProfitPC
            public string TradeProfitPCAverageAll
            {
                get { return _parent.TradeList.Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }

            public string TradeProfitPCAverageLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }

            public string TradeProfitPCAverageShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }
            #endregion

            #region WinTradeCount
            public string WinTradeCountAll
            {
                get { return _parent.TradeList.Count(trade => trade.Profit > 0).ToString(); }
            }

            public string WinTradeCountLong
            {
                get { return _parent.TradeList.Count(trade => trade.IsLong && (trade.Profit > 0)).ToString(); }
            }

            public string WinTradeCountShort
            {
                get { return _parent.TradeList.Count(trade => !trade.IsLong && (trade.Profit > 0)).ToString(); }
            }
            #endregion

            #region WinTradeCountPC
            public string WinTradeCountPCAll
            {
                get { return (_parent.TradeList.PercentageCount(trade => trade.Profit > 0).ToStringShortFloat() + "%"); }
            }

            public string WinTradeCountPCLong
            {
                get { return (_parent.TradeList.Where(trade => trade.IsLong).PercentageCount(trade => trade.Profit > 0).ToStringShortFloat() + "%"); }
            }

            public string WinTradeCountPCShort
            {
                get { return (_parent.TradeList.Where(trade => !trade.IsLong).PercentageCount(trade => trade.Profit > 0).ToStringShortFloat() + "%"); }
            }
            #endregion

            #region WinProfitSum
            public string WinProfitSumAll
            {
                get { return _parent.TradeList.Where(trade => trade.Profit > 0).Sum(trade => trade.Profit).ToStringBase(); }
            }

            public string WinProfitSumLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong && trade.Profit > 0).Sum(trade => trade.Profit).ToStringBase(); }
            }

            public string WinProfitSumShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong && trade.Profit > 0).Sum(trade => trade.Profit).ToStringBase(); }
            }
            #endregion

            #region WinProfitAverage
            public string WinProfitAverageAll
            {
                get { return _parent.TradeList.Where(trade => trade.Profit > 0).Average(trade => trade.Profit).ToStringBase(); }
            }

            public string WinProfitAverageLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong && trade.Profit > 0).Average(trade => trade.Profit).ToStringBase(); }
            }

            public string WinProfitAverageShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong && trade.Profit > 0).Average(trade => trade.Profit).ToStringBase(); }
            }
            #endregion

            #region WinProfitPCAverage
            public string WinProfitPCAverageAll
            {
                get { return _parent.TradeList.Where(trade => trade.ProfitPC > 0).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }

            public string WinProfitPCAverageLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong && trade.ProfitPC > 0).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }

            public string WinProfitPCAverageShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong && trade.ProfitPC > 0).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }
            #endregion

            #region MaxWinRow
            public string MaxWinRowAll
            {
                get { return _parent.TradeList.MaxCountInRow(trade => trade.Profit > 0).ToString(); }
            }

            public string MaxWinRowLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).MaxCountInRow(trade => trade.Profit > 0).ToString(); }
            }

            public string MaxWinRowShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong).MaxCountInRow(trade => trade.Profit > 0).ToString(); }
            }
            #endregion

            #region LoseTradeCount
            public string LoseTradeCountAll
            {
                get { return _parent.TradeList.Count(trade => trade.Profit <= 0).ToString(); }
            }

            public string LoseTradeCountLong
            {
                get { return _parent.TradeList.Count(trade => trade.IsLong && (trade.Profit <= 0)).ToString(); }
            }

            public string LoseTradeCountShort
            {
                get { return _parent.TradeList.Count(trade => !trade.IsLong && (trade.Profit <= 0)).ToString(); }
            }
            #endregion

            #region LoseTradeCountPC
            public string LoseTradeCountPCAll
            {
                get { return (_parent.TradeList.PercentageCount(trade => trade.Profit <= 0).ToStringShortFloat() + "%"); }
            }

            public string LoseTradeCountPCLong
            {
                get { return (_parent.TradeList.Where(trade => trade.IsLong).PercentageCount(trade => trade.Profit <= 0).ToStringShortFloat() + "%"); }
            }

            public string LoseTradeCountPCShort
            {
                get { return (_parent.TradeList.Where(trade => !trade.IsLong).PercentageCount(trade => trade.Profit <= 0).ToStringShortFloat() + "%"); }
            }
            #endregion

            #region LoseProfitSum
            public string LoseProfitSumAll
            {
                get { return _parent.TradeList.Where(trade => trade.Profit <= 0).Sum(trade => trade.Profit).ToStringBase(); }
            }

            public string LoseProfitSumLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong && trade.Profit <= 0).Sum(trade => trade.Profit).ToStringBase(); }
            }

            public string LoseProfitSumShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong && trade.Profit <= 0).Sum(trade => trade.Profit).ToStringBase(); }
            }
            #endregion

            #region LoseProfitAverage
            public string LoseProfitAverageAll
            {
                get { return _parent.TradeList.Where(trade => trade.Profit <= 0).Average(trade => trade.Profit).ToStringBase(); }
            }

            public string LoseProfitAverageLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong && trade.Profit <= 0).Average(trade => trade.Profit).ToStringBase(); }
            }

            public string LoseProfitAverageShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong && trade.Profit <= 0).Average(trade => trade.Profit).ToStringBase(); }
            }
            #endregion

            #region LoseProfitPCAverage
            public string LoseProfitPCAverageAll
            {
                get { return _parent.TradeList.Where(trade => trade.ProfitPC <= 0).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }

            public string LoseProfitPCAverageLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong && trade.ProfitPC <= 0).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }

            public string LoseProfitPCAverageShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong && trade.ProfitPC <= 0).Average(trade => trade.ProfitPC).ToStringShortFloat() + "%"; }
            }
            #endregion

            #region MaxLoseRow
            public string MaxLoseRowAll
            {
                get { return _parent.TradeList.MaxCountInRow(trade => trade.Profit <= 0).ToString(); }
            }

            public string MaxLoseRowLong
            {
                get { return _parent.TradeList.Where(trade => trade.IsLong).MaxCountInRow(trade => trade.Profit <= 0).ToString(); }
            }

            public string MaxLoseRowShort
            {
                get { return _parent.TradeList.Where(trade => !trade.IsLong).MaxCountInRow(trade => trade.Profit <= 0).ToString(); }
            }
            #endregion

            #region MaxDrawDown
            public string MaxDrawDownAll
            {
                get { return _parent.TradeList.MaxDrawDown().ToStringBase(); }
            }

            public string MaxDrawDownLong
            {
                get { return _parent.TradeList.MaxDrawDown(trade => trade.IsLong).ToStringBase(); }
            }

            public string MaxDrawDownShort
            {
                get { return _parent.TradeList.MaxDrawDown(trade => !trade.IsLong).ToStringBase(); }
            }
            #endregion

            #region MaxDrawDownDate
            public string MaxDrawDownDateAll
            {
                get { return _parent.TradeList.MaxDrawDownDate().ToStandardString(); }
            }

            public string MaxDrawDownDateLong
            {
                get { return _parent.TradeList.MaxDrawDownDate(trade => trade.IsLong).ToStandardString(); }
            }

            public string MaxDrawDownDateShort
            {
                get { return _parent.TradeList.MaxDrawDownDate(trade => !trade.IsLong).ToStandardString(); }
            }
            #endregion

            #region MaxDrawDownPC
            public string MaxDrawDownPCAll
            {
                get { return _parent.TradeList.MaxDrawDownPC().ToStringShortFloat() + "%"; }
            }

            public string MaxDrawDownPCLong
            {
                get { return _parent.TradeList.MaxDrawDownPC(trade => trade.IsLong).ToStringShortFloat() + "%"; }
            }

            public string MaxDrawDownPCShort
            {
                get { return _parent.TradeList.MaxDrawDownPC(trade => !trade.IsLong).ToStringShortFloat() + "%"; }
            }
            #endregion

            #region MaxDrawDownPCDate
            public string MaxDrawDownPCDateAll
            {
                get { return _parent.TradeList.MaxDrawDownPCDate().ToStandardString(); }
            }

            public string MaxDrawDownPCDateLong
            {
                get { return _parent.TradeList.MaxDrawDownPCDate(trade => trade.IsLong).ToStandardString(); }
            }

            public string MaxDrawDownPCDateShort
            {
                get { return _parent.TradeList.MaxDrawDownPCDate(trade => !trade.IsLong).ToStandardString(); }
            }
            #endregion

            #region ProfitFactor
            public string ProfitFactorAll
            {
                get { return _parent.TradeList.ProfitFactor().ToStringShortFloat(); }
            }

            public string ProfitFactorLong
            {
                get { return _parent.TradeList.ProfitFactor(trade => trade.IsLong).ToStringShortFloat(); }
            }

            public string ProfitFactorShort
            {
                get { return _parent.TradeList.ProfitFactor(trade => !trade.IsLong).ToStringShortFloat(); }
            }
            #endregion

            #region RecoveryFactor
            public string RecoveryFactorAll
            {
                get { return _parent.TradeList.RecoveryFactor().ToStringShortFloat(); }
            }

            public string RecoveryFactorLong
            {
                get { return _parent.TradeList.RecoveryFactor(trade => trade.IsLong).ToStringShortFloat(); }
            }

            public string RecoveryFactorShort
            {
                get { return _parent.TradeList.RecoveryFactor(trade => !trade.IsLong).ToStringShortFloat(); }
            }
            #endregion

            #region PayoffRatio
            public string PayoffRatioAll
            {
                get { return _parent.TradeList.PayoffRatio().ToStringShortFloat(); }
            }

            public string PayoffRatioLong
            {
                get { return _parent.TradeList.PayoffRatio(trade => trade.IsLong).ToStringShortFloat(); }
            }

            public string PayoffRatioShort
            {
                get { return _parent.TradeList.PayoffRatio(trade => !trade.IsLong).ToStringShortFloat(); }
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
}
