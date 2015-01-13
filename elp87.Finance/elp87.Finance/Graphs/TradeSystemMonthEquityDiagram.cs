using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public class TradeSystemMonthEquityDiagram : MonthEquityDiagram
    {
        public TradeSystemMonthEquityDiagram(Grid grid, TradeSystem system)
            : base(grid)
        {
            if (system.TradeList.Count != 0)
            {
                this._categories = new List<DiagramCategoryData>();
                List<ISysTrade> trades = system.TradeList;
                DateTime minDate = trades.Min(trade => trade.ExitDateTime);
                DateTime minMonth = new DateTime(minDate.Year, minDate.Month, 1);
                DateTime maxDate = trades.Max(trade => trade.ExitDateTime);
                DateTime maxMonth = new DateTime(maxDate.Year, maxDate.Month, 1);

                DateTime month = minMonth;
                while (month <= maxMonth)
                {
                    double monthProfit = trades.Where(trade => trade.ExitDateTime.Month == month.Month && trade.ExitDateTime.Year == month.Year).Sum(trade => trade.ProfitPC);
                    this._categories.Add(new DiagramCategoryData() { Title = month, Value = monthProfit });
                    month = month.AddMonths(1);
                }
            }
        }
    }
}
