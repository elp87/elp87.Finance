using System;

namespace elp87.Finance
{
    public class Bar
    {
        public Bar(DateTime date, Money open, Money high, Money low, Money close)
        {
            this.Date = date;
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
        }

        public DateTime Date { get; set; }

        public Money Open { get; set; }

        public Money High { get; set; }

        public Money Low { get; set; }

        public Money Close { get; set; }        
    }
}
