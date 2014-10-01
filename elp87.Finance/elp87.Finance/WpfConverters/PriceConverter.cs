using System;
using System.Windows.Data;
using elp87.Finance;

namespace elp87.Finance.WpfConverters
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //double price = (double)value;
            Money money = (Money)value;
            double price = (double)money.Value;
            if (price % 1 == 0) return price.ToString("0,0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            else return price.ToString("0,0.0#", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
