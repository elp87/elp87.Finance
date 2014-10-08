using System;

namespace elp87.Finance
{
    public class TradeDay        
    {
        #region Поля
        private DateTime _date;
        private Money _capital, _dayProfit, _cumProfit, _introduce, _drawDown;
        private double _cumProfitPC;
        #endregion

        #region Properties
        public DateTime Date
        {
            get 
            { 
                return _date; 
            }
            set
            {
                _date = value;
            }
        }

        public Money Capital
        {
            get
            {
                return _capital;
            }
            set
            {
                _capital = value;
            }
        }

        public Money DayProfit
        {
            get
            {
                return _dayProfit;
            }
            set
            {
                _dayProfit = value;
            }
        }

        public Money CumProfit
        {
            get
            {
                return _cumProfit;
            }
            set
            {
                _cumProfit = value;
            }
        }

        public Money Introduce
        {
            get
            {
                return _introduce;
            }
            set
            {
                _introduce = value;
            }
        }

        public Money DrawDown
        {
            get
            {
                return _drawDown;
            }
            set
            {
                _drawDown = value;
            }
        }

        public double CumProfitPC
        {
            get
            {
                return _cumProfitPC;
            }
            set
            {
                _cumProfitPC = value;
            }
        }

        public double DayProfitPC
        {
            get
            {
                return (_dayProfit / _capital) * 100;
            }
        }
        #endregion
    }
}
