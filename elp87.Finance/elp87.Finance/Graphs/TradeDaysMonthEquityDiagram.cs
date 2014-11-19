using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public class TradeDaysMonthEquityDiagram : MonthEquityDiagram
    {
        public TradeDaysMonthEquityDiagram(Grid grid, List<TradeDay> tradeDays)
            : base(grid)
        {
            this._categories = new List<DiagramCategoryData>();
            DateTime minDate = tradeDays.Min(day => day.Date);
            DateTime minMonth = new DateTime(minDate.Year, minDate.Month, 1);
            DateTime maxDate = tradeDays.Max(day => day.Date);
            DateTime maxMonth = new DateTime(maxDate.Year, maxDate.Month, 1);

            DateTime month = minMonth;
            while (month <= maxMonth)
            {
                double monthProfit = tradeDays.Where(day => day.Date.Month == month.Month && day.Date.Year == month.Year).Sum(day => day.DayProfitPC);
                this._categories.Add(new DiagramCategoryData() { Title = month.ToShortDateString(), Value = monthProfit });
                month = month.AddMonths(1);
            }
        }
    }
}
