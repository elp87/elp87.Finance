using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public class TradeSystemTimeDistributionDiagram : TimeDistributionDiagram
    {
        public TradeSystemTimeDistributionDiagram(Grid grid, TradeSystem system, CalcTypes calcType)
            : base(grid)
        {
            this._categories = new List<DiagramCategoryData>();
            List<ISysTrade> trades = system.TradeList;

            for (int hour = 0; hour < HoursInDay; hour++)
            {
                IEnumerable<ISysTrade> hourTrades;
                if (calcType == CalcTypes.EntryDate)
                {
                    hourTrades = trades.Where(trade => trade.EntryDateTime.Hour == hour);
                }
                else
                {
                    hourTrades = trades.Where(trade => trade.ExitDateTime.Hour == hour);
                }

                if (this._categories.Count != 0 || hourTrades.Count() != 0)
                {
                    string title = hour.ToString();
                    Money value = hourTrades.Sum(trade => trade.Profit.Value);

                    Money winProfit = hourTrades.Where(trade => trade.Profit > 0).Sum(trade => trade.Profit.Value);
                    int winCount = hourTrades.Where(trade => trade.Profit > 0).Count();
                    Money loseProfit = hourTrades.Where(trade => trade.Profit <= 0).Sum(trade => trade.Profit.Value);
                    int loseCount = hourTrades.Where(trade => trade.Profit <= 0).Count();
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

                    string tradesCountNotation = "Кол-во сделок - " + hourTrades.Count().ToString();
                    string winNotation = "Прибыльных - " + winCount.ToString() + "(" + winProfit.ToString() + ")";
                    string loseNotation = "Убыточных - " + loseCount.ToString() + "(" + loseProfit.ToString() + ")";
                    string profitFactorNotation = "Профит-фактор - " + profitFactor.ToString();
                    this._categories.Add(new DiagramCategoryData()
                    {
                        Title = title,
                        Value = Convert.ToDouble(value.Value),
                        AttachedData = new object[] { tradesCountNotation, winNotation, loseNotation, profitFactorNotation }
                    });


                }
            }
        }
    }
}
