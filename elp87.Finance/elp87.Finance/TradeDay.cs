using System;

namespace elp87.Finance
{
    public class TradeDay        
    {
        #region Поля

        #endregion

        #region Constructors
        public TradeDay()
        {
        }

        public TradeDay(DateTime date,
            Money capital,
            Money introduce,
            TradeDay previousDay)
        {
            Date = date;
            Capital = capital;
            Introduce = introduce;

            if (previousDay != null)
            {
                DayProfit = (Capital - previousDay.Capital) - Introduce;
                CumProfit = previousDay.CumProfit + DayProfit;

                Money dd = previousDay.DrawDown - DayProfit;
                DrawDown = (dd < 0) ? 0 : dd;

                CumProfitPC = previousDay.CumProfitPC + DayProfitPC;
            }
            else
            {
                DayProfit = 0;
                CumProfit = DayProfit;
                DrawDown = (DayProfit > 0) ? 0 : DayProfit;
                CumProfitPC = DayProfitPC;
            }
        }
        #endregion

        #region Properties
        public DateTime Date { get; set; }

        public Money Capital { get; set; }

        public Money DayProfit { get; set; }

        public Money CumProfit { get; set; }

        public Money Introduce { get; set; }

        public Money DrawDown { get; set; }

        public double CumProfitPC { get; set; }

        public double DayProfitPC
        {
            get
            {
                if (Capital.Value == 0m) return 0; // В случае полного вывода возвращается 0
                return (DayProfit / Capital) * 100;
            }
        }
        #endregion
    }
}
