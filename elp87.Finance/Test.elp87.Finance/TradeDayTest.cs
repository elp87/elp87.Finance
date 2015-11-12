using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using elp87.Finance;

namespace Test.elp87.Finance
{
    [TestClass]
    public class TradeDayTest
    {
        [TestMethod]
        public void TestCtor()
        {
            List<TradeDay> tradeDays = new List<TradeDay>();
            TradeDay day0 = new TradeDay(new DateTime(2014, 10, 20), 100000, 100000, null);
            TradeDay day1 = new TradeDay(new DateTime(2014, 10, 21), 120000, 0, day0);
            TradeDay day2 = new TradeDay(new DateTime(2014, 10, 22), 110000, 0, day1);
            TradeDay day3 = new TradeDay(new DateTime(2014, 10, 23), 130000, 0, day2);
            TradeDay day4 = new TradeDay(new DateTime(2014, 10, 24), 225000, 100000, day3);
            tradeDays.Add(day0);
            tradeDays.Add(day1);
            tradeDays.Add(day2);
            tradeDays.Add(day3);
            tradeDays.Add(day4);

            Money[] expCumProfit = new Money[] { 0, 20000, 10000, 30000, 25000 };
            Money[] expDrawDown = new Money[] { 0, 0, 10000, 0, 5000 };

            for (int i = 0; i < tradeDays.Count; i++)
            {
                Assert.AreEqual(expCumProfit[i], tradeDays[i].CumProfit);
                Assert.AreEqual(expDrawDown[i], tradeDays[i].DrawDown);
            }
        }

        [TestMethod]
        public void TestCtorFullWithdrawal()
        {
            TradeDay firstDay = new TradeDay(new DateTime(2015, 1, 1), new Money(100000), new Money(0), null);
            TradeDay seconDay = new TradeDay(new DateTime(2015, 1, 2), new Money(0), new Money(-100000), firstDay);
            Assert.AreEqual(seconDay.DayProfitPC, 0, 0.001);
        }
    }
}
