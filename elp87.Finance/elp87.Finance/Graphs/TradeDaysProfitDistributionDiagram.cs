using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public class TradeDaysProfitDistributionDiagram : ProfitDistributionDiagram
    {
        public TradeDaysProfitDistributionDiagram(Grid grid, List<TradeDay> tradeDays)
            : base(grid)
        {
            this._categories = new List<DiagramCategoryData>();
            Money minValue = Math.Round(tradeDays.Min(day => day.DayProfitPC), 1);
            Money maxValue = Math.Round(tradeDays.Max(day => day.DayProfitPC), 1);
            for (Money value = minValue; value <= maxValue; value += 0.1)
            {
                int count = tradeDays.Count(trade => Math.Round(trade.DayProfitPC, 1) == value);
                this._categories.Add(new DiagramCategoryData() { Title = value, Value = count });
            }
        }
    }
}
