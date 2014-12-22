using System;
using System.Collections.Generic;
using System.Linq;

namespace elp87.Finance.Helpers
{
    public static class LINQExts
    {
        public static double PercentageCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int total = source.Count();
            int count = source.Count(predicate);

            return ((double)(count * 100)) / total;
        }

        public static int MaxCountInRow<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int maxRow = 0;
            int curRow = 0;
            foreach (T item in source)
            {
                if (predicate(item)) { curRow++; }
                else { curRow = 0; }

                if (curRow > maxRow) { maxRow = curRow; }
            }

            return maxRow;
        }
    }

    public static class LinqITrade
    {
        #region MaxDrawDown & MaxDrawDownPC
        // For Money
        private static Money MaxDrawDown(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate, Func<ITrade, Money> selector)
        {
            Money maxDD = 0;
            Money curDD = 0;
            Money maxProfit = 0;
            Money cumProfit = 0;

            foreach (ITrade trade in source.Where(predicate))
            {
                cumProfit += selector(trade);
                if (cumProfit > maxProfit) { maxProfit = cumProfit; }
                curDD = maxProfit - cumProfit;
                if (curDD > maxDD)
                {
                    maxDD = curDD;
                }
            }
            maxDD = -maxDD;
            return maxDD;
        }

        // For Percentage
        private static double MaxDrawDown(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate, Func<ITrade, double> selector)
        {
            double maxDD = 0;
            double curDD = 0;
            double maxProfit = 0;
            double cumProfit = 0;

            foreach (ITrade trade in source.Where(predicate))
            {
                cumProfit += selector(trade);
                if (cumProfit > maxProfit) { maxProfit = cumProfit; }
                curDD = maxProfit - cumProfit;
                if (curDD > maxDD)
                {
                    maxDD = curDD;
                }
            }
            maxDD = -maxDD;
            return maxDD;
        }


        public static Money MaxDrawDown(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate)
        {
            return source.MaxDrawDown(predicate, (trade => trade.Profit));
        }

        public static Money MaxDrawDown(this IEnumerable<ITrade> source)
        {
            return source.MaxDrawDown(trade => true);
        }

        public static double MaxDrawDownPC(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate)
        {
            return source.MaxDrawDown(predicate, (trade => trade.ProfitPC));
        }

        public static double MaxDrawDownPC(this IEnumerable<ITrade> source)
        {
            return source.MaxDrawDownPC(trade => true);
        }
        #endregion

        #region MaxDrawDownDate & MaxDrawDownPCDate
        private static DateTime MaxDrawDownDate(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate, Func<ITrade, Money> selector)
        {
            Money maxDD = 0;
            Money curDD = 0;
            Money maxProfit = 0;
            Money cumProfit = 0;
            DateTime date = new DateTime();
            foreach (ITrade trade in source.Where(predicate))
            {
                cumProfit += selector(trade);
                if (cumProfit > maxProfit) { maxProfit = cumProfit; }
                curDD = maxProfit - cumProfit;
                if (curDD > maxDD)
                {
                    maxDD = curDD;
                    date = trade.ExitDateTime;
                }
            }
            return date;
        }
        public static DateTime MaxDrawDownDate(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate)
        {
            return source.MaxDrawDownDate(predicate, (trade => trade.Profit));
        }

        public static DateTime MaxDrawDownDate(this IEnumerable<ITrade> source)
        {
            return source.MaxDrawDownDate(trade => true);
        }

        public static DateTime MaxDrawDownPCDate(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate)
        {
            return source.MaxDrawDownDate(predicate, (trade => trade.ProfitPC));
        }

        public static DateTime MaxDrawDownPCDate(this IEnumerable<ITrade> source)
        {
            return source.MaxDrawDownPCDate(trade => true);
        }
        #endregion

        #region Profit-factor
        public static double ProfitFactor(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate)
        {
            Money sumWinProfit = source.Where(trade => (trade.Profit > 0) && predicate(trade)).Sum(trade => trade.Profit.Value);
            Money sumLoseProfit = source.Where(trade => (trade.Profit <= 0) && predicate(trade)).Sum(trade => trade.Profit.Value);
            if (sumLoseProfit == 0)
            {
                if (sumWinProfit > 0) return Double.PositiveInfinity;
                else if (sumWinProfit < 0) return Double.NegativeInfinity;
                else if (sumWinProfit == 0) return Double.NaN;
            }
            return -(sumWinProfit / sumLoseProfit);
        }

        public static double ProfitFactor(this IEnumerable<ITrade> source)
        {
            return source.ProfitFactor(trade => true);
        }
        #endregion

        #region Recovery factor
        public static double RecoveryFactor(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate)
        {
            Money profit = source.Where(predicate).Sum(trade => trade.Profit.Value);
            Money maxDD = source.MaxDrawDown(predicate);
            if (maxDD == 0)
            {
                if (profit > 0) return Double.PositiveInfinity;
                else if (profit < 0) return Double.NegativeInfinity;
                else if (profit == 0) return Double.NaN;
            }
            return -(profit / maxDD);
        }

        public static double RecoveryFactor(this IEnumerable<ITrade> source)
        {
            return source.RecoveryFactor(trade => true);
        }
        #endregion

        #region Payoff ratio
        public static double PayoffRatio(this IEnumerable<ITrade> source, Func<ITrade, bool> predicate)
        {
            Money avgLoseProfit, avgWinProfit;

            var winTrades = source.Where(trade => (trade.Profit > 0) && predicate(trade));
            if (winTrades.Count() == 0) avgWinProfit = 0;
            else avgWinProfit = winTrades.Average(trade => trade.Profit.Value);

            var loseTrades = source.Where(trade => (trade.Profit <= 0) && predicate(trade));
            if (loseTrades.Count() == 0) avgLoseProfit = 0;
            else avgLoseProfit = loseTrades.Average(trade => trade.Profit.Value);

            if (avgLoseProfit == 0)
            {
                if (avgWinProfit > 0) return Double.PositiveInfinity;
                else if (avgWinProfit < 0) return Double.NegativeInfinity;
                else if (avgWinProfit == 0) return Double.NaN;
            }
            return -(avgWinProfit / avgLoseProfit);
        }

        public static double PayoffRatio(this IEnumerable<ITrade> source)
        {
            return source.PayoffRatio(trade => true);
        }
        #endregion
    }
}
