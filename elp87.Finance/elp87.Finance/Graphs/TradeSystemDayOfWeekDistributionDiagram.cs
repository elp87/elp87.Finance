using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public class TradeSystemDayOfWeekDistributionDiagram : DayOfWeekDistributionDiagram 
    {
        public TradeSystemDayOfWeekDistributionDiagram(Grid grid, TradeSystem system, CalcTypes calcType)
            : base(grid)
        {
            this._categories = new List<DiagramCategoryData>();
            List<ISysTrade> trades = system.TradeList;

            CultureInfo ci = CultureInfo.CurrentCulture;
            DateTimeFormatInfo dateFormat = ci.DateTimeFormat;

            for (int i = 0; i < DaysInWeekCount; i++)
            {
                string title = dateFormat.DayNames[i];
                IEnumerable<ISysTrade> dayTrades;
                if (calcType == CalcTypes.EntryDate)
                {
                    dayTrades = trades.Where(trade => (int)trade.EntryDateTime.DayOfWeek == i);
                }
                else
                {
                    dayTrades = trades.Where(trade => (int)trade.ExitDateTime.DayOfWeek == i);
                }
                Money value = dayTrades.Sum(trade => trade.Profit.Value);
                Money winProfit = dayTrades.Where(trade => trade.Profit > 0).Sum(trade => trade.Profit.Value);
                int winCount = dayTrades.Where(trade => trade.Profit > 0).Count();
                Money loseProfit = dayTrades.Where(trade => trade.Profit <= 0).Sum(trade => trade.Profit.Value);
                int loseCount = dayTrades.Where(trade => trade.Profit <= 0).Count();
                double profitFactor;
                try
                {
                    profitFactor = Math.Round(winProfit / loseProfit, 2);
                }
                catch (DivideByZeroException)
                {
                    if (winProfit > 0) profitFactor = Double.PositiveInfinity;
                    else if (winProfit == 0) profitFactor = Double.NaN;
                    else profitFactor = Double.NegativeInfinity;
                }

                string tradesCountNotation = "Кол-во сделок - " + dayTrades.Count().ToString();
                string winNotation = "Прибыльных - " + winCount.ToString() + "(" + winProfit.ToString() + ")";
                string loseNotation = "Убыточных - " + loseCount.ToString() + "(" + loseProfit.ToString() + ")";
                string profitFactorNotation = "Профит-фактор - " + profitFactor.ToString();
                this._categories.Add(new DiagramCategoryData() { 
                    Title = title, 
                    Value = Convert.ToDouble(value.Value), 
                    AttachedData = new object[] { tradesCountNotation, winNotation, loseNotation, profitFactorNotation } 
                });
            }
        }
    }
}
