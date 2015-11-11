using System;

namespace elp87.Finance
{
    public class TradeDay        
    {
        #region Поля
        private DateTime _date;
        private Money _capital, _dayProfit, _cumProfit, _introduce, _drawDown;
        private double _cumProfitPc;
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
            _date = date;
            _capital = capital;
            _introduce = introduce;

            if (previousDay != null)
            {
                _dayProfit = (_capital - previousDay._capital) - _introduce;
                _cumProfit = previousDay._cumProfit + _dayProfit;

                Money dd = previousDay._drawDown - _dayProfit;
                _drawDown = (dd < 0) ? 0 : dd;

                _cumProfitPc = previousDay._cumProfitPc + DayProfitPC;
            }
            else
            {
                _dayProfit = 0;
                _cumProfit = _dayProfit;
                _drawDown = (_dayProfit > 0) ? 0 : _dayProfit;
                _cumProfitPc = DayProfitPC;
            }
        }
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
                return _cumProfitPc;
            }
            set
            {
                _cumProfitPc = value;
            }
        }

        public double DayProfitPC
        {
            get
            {
                if (_capital.Value == 0m) return 0; // В случае полного вывода возвращается 0
                return (_dayProfit / _capital) * 100;
            }
        }
        #endregion
    }
}
