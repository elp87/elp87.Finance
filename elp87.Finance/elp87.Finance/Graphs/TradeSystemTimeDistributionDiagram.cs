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

                    this._categories.Add(GetCategoryForTradeSystem(hourTrades, title));
                }
            }
        }
    }
}
