using System;

namespace elp87.Finance.Helpers
{
    public static class Convert
    {
        #region Double
        public static string ToStringBase(this double value)
        {
            if (Compare.IsInteger(value))
            {
                return value.ToStringInt();
            }
            else
            {
                return value.ToStringFloat();
            }
        }

        public static string ToStringInt(this double value)
        {
            return value.ToString("0,0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public static string ToStringFloat(this double value)
        {
            return value.ToString("0,0.0#", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public static string ToStringShortFloat(this double value)
        {
            return value.ToString("0.0#", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
        }
        #endregion

        #region DateTime
        public static string ToStandardString(this DateTime value)
        {
            return value.ToString("dd.MM.yyyy HH:mm:ss");
        }
        #endregion
    }
}
