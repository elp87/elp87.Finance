﻿namespace elp87.Finance.Helpers
{
    public static class Compare
    {
        public static bool IsInteger(double value)
        {
            return (value % 1 == 0) ? true : false;
        }

        public static bool IsInteger(decimal value)
        {
            return (value % 1 == 0) ? true : false;
        }
    }
}
