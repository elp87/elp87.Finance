using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public class TradesProfitDistributionDiagram : ProfitDistributionDiagram
    {
        public TradesProfitDistributionDiagram(Grid grid, TradeSystem system)
            : base(grid)
        {
            if (system.TradeList.Count != 0)
            {
                this._categories = new List<DiagramCategoryData>();
                List<ISysTrade> trades = system.TradeList;
                Money minValue = Math.Round(trades.Min(trade => trade.ProfitPC), 1);
                Money maxValue = Math.Round(trades.Max(trade => trade.ProfitPC), 1);
                for (Money value = minValue; value <= maxValue; value += 0.1)
                {
                    int count = trades.Count(trade => Math.Round(trade.ProfitPC, 1) == value);
                    this._categories.Add(new DiagramCategoryData() { Title = value, Value = count });
                }
            }
        }
    }
}
