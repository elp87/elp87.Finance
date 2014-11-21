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
                Money value;
                if (calcType == CalcTypes.EntryDate)
                {
                    value = trades.Where(trade => (int)trade.EntryDateTime.DayOfWeek == i).Sum(trade => trade.Profit.Value);
                }
                else
                {
                    value = trades.Where(trade => (int)trade.ExitDateTime.DayOfWeek == i).Sum(trade => trade.Profit.Value);
                }
                this._categories.Add(new DiagramCategoryData() { Title = title, Value = Convert.ToDouble(value.Value), AttachedData = new object[] { "" } });
            }
        }
    }
}
