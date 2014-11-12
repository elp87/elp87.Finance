using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Finance;
using elp87.Finance.Helpers;

namespace Test.elp87.Finance
{
    [TestClass]
    public class TradeSystemPropertiesTest
    {
        private TradeSystem sys1;
        private TradeSystem oneTradeSystem;
        private TradeSystemProperties sys1Prop;
        private TradeSystemProperties otsProp;

        public TradeSystemPropertiesTest()
        {
            sys1 = new TradeSystem();

            SysTrade trade0 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 18, 11, 0, 0),
                IsLong = false,
                Count = 1,
                EntryPrice = 129980,
                ExitDateTime = new DateTime(2013, 04, 18, 11, 30, 0),
                ExitPrice = 130470
            };
            sys1.AddTrade(trade0);

            SysTrade trade1 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 18, 11, 0, 0),
                IsLong = true,
                Count = 1,
                EntryPrice = 130000,
                ExitDateTime = new DateTime(2013, 04, 18, 15, 30, 0),
                ExitPrice = 130560
            };
            sys1.AddTrade(trade1);

            SysTrade trade2 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 18, 20, 30, 0),
                IsLong = true,
                Count = 1,
                EntryPrice = 129690,
                ExitDateTime = new DateTime(2013, 04, 18, 22, 0, 0),
                ExitPrice = 128990
            };
            sys1.AddTrade(trade2);

            SysTrade trade3 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 19, 14, 0, 0),
                IsLong = true,
                Count = 1,
                EntryPrice = 129790,
                ExitDateTime = new DateTime(2013, 04, 19, 20, 30, 0),
                ExitPrice = 129910
            };
            sys1.AddTrade(trade3);

            SysTrade trade4 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 22, 13, 0, 0),
                IsLong = false,
                Count = 1,
                EntryPrice = 130880,
                ExitDateTime = new DateTime(2013, 04, 22, 20, 0, 0),
                ExitPrice = 128910
            };
            sys1.AddTrade(trade4);

            SysTrade trade5 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 22, 20, 0, 0),
                IsLong = true,
                Count = 1,
                EntryPrice = 128910,
                ExitDateTime = new DateTime(2013, 04, 23, 10, 0, 0),
                ExitPrice = 128400
            };
            sys1.AddTrade(trade5);

            SysTrade trade6 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 23, 10, 0, 0),
                IsLong = false,
                Count = 1,
                EntryPrice = 128240,
                ExitDateTime = new DateTime(2013, 04, 23, 11, 0, 0),
                ExitPrice = 128540
            };
            sys1.AddTrade(trade6);

            SysTrade trade7 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 23, 13, 30, 0),
                IsLong = true,
                Count = 1,
                EntryPrice = 129090,
                ExitDateTime = new DateTime(2013, 04, 23, 16, 30, 0),
                ExitPrice = 129230
            };
            sys1.AddTrade(trade7);

            SysTrade trade8 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 23, 15, 0, 0),
                IsLong = false,
                Count = 1,
                EntryPrice = 129630,
                ExitDateTime = new DateTime(2013, 04, 23, 17, 30, 0),
                ExitPrice = 129420
            };
            sys1.AddTrade(trade8);

            SysTrade trade9 = new SysTrade()
            {
                EntryDateTime = new DateTime(2013, 04, 24, 12, 0, 0),
                IsLong = true,
                Count = 1,
                EntryPrice = 131400,
                ExitDateTime = new DateTime(2013, 04, 25, 11, 30, 0),
                ExitPrice = 134850
            };
            sys1.AddTrade(trade9);
            sys1Prop = new TradeSystemProperties(sys1);

            oneTradeSystem = new TradeSystem();
            oneTradeSystem.AddTrade(trade0);
            otsProp = new TradeSystemProperties(oneTradeSystem);
        }

        [TestMethod]
        public void TestCumProfit()
        {
            string expCumProfitAll = ((double)4450).ToStringInt();
            string expCumProfitLong = ((double)3060).ToStringInt();
            string expCumProfitShort = ((double)1390).ToStringInt();

            Assert.AreEqual(expCumProfitAll, sys1Prop.CumProfitAll);
            Assert.AreEqual(expCumProfitLong, sys1Prop.CumProfitLong);
            Assert.AreEqual(expCumProfitShort, sys1Prop.CumProfitShort);
        }

        [TestMethod]
        public void TestCumProfitPC()
        {
            string expAll = ((double)3.33).ToStringShortFloat() + "%";
            string expLong = ((double)2.25).ToStringShortFloat() + "%";
            string expShort = ((double)1.08).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.CumProfitPCAll);
            Assert.AreEqual(expLong, sys1Prop.CumProfitPCLong);
            Assert.AreEqual(expShort, sys1Prop.CumProfitPCShort);
        }

        [TestMethod]
        public void TestYearProfit()
        {
            string expAll = ((double)173.1).ToStringShortFloat() + "%";
            string expLong = ((double)116.88).ToStringShortFloat() + "%";
            string expShort = ((double)56.23).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.YearProfitAll);
            Assert.AreEqual(expLong, sys1Prop.YearProfitLong);
            Assert.AreEqual(expShort, sys1Prop.YearProfitShort);
        }

        [TestMethod]
        public void TestMonthProfit()
        {
            string expAll = ((double)14.23).ToStringShortFloat() + "%";
            string expLong = ((double)9.61).ToStringShortFloat() + "%";
            string expShort = ((double)4.62).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.MonthProfitAll);
            Assert.AreEqual(expLong, sys1Prop.MonthProfitLong);
            Assert.AreEqual(expShort, sys1Prop.MonthProfitShort);
        }

        [TestMethod]
        public void TestTradeCount()
        {
            string expAll = "10";
            string expLong = "6";
            string expShort = "4";

            Assert.AreEqual(expAll, sys1Prop.TradeCountAll);
            Assert.AreEqual(expLong, sys1Prop.TradeCountLong);
            Assert.AreEqual(expShort, sys1Prop.TradeCountShort);
        }

        [TestMethod]
        public void TestAverageTradeProfit()
        {
            string expAll = ((double)445).ToStringFloat();
            string expLong = ((double)510).ToStringFloat();
            string expShort = ((double)347.5).ToStringFloat();

            Assert.AreEqual(expAll, sys1Prop.TradeProfitAverageAll);
            Assert.AreEqual(expLong, sys1Prop.TradeProfitAverageLong);
            Assert.AreEqual(expShort, sys1Prop.TradeProfitAverageShort);
        }

        [TestMethod]
        public void TestAverageTradeProfitPC()
        {
            string expAll = ((double)0.33).ToStringShortFloat() + "%";
            string expLong = ((double)0.37).ToStringShortFloat() + "%";
            string expShort = ((double)0.27).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.TradeProfitPCAverageAll);
            Assert.AreEqual(expLong, sys1Prop.TradeProfitPCAverageLong);
            Assert.AreEqual(expShort, sys1Prop.TradeProfitPCAverageShort);
        }

        [TestMethod]
        public void TestWinTradeCount()
        {
            string expAll = (6).ToString();
            string expLong = (4).ToString();
            string expShort = (2).ToString();

            Assert.AreEqual(expAll, sys1Prop.WinTradeCountAll);
            Assert.AreEqual(expLong, sys1Prop.WinTradeCountLong);
            Assert.AreEqual(expShort, sys1Prop.WinTradeCountShort);
        }

        [TestMethod]
        public void TestWinTradeCountPC()
        {
            string expAll = ((double)60).ToStringShortFloat() + "%";
            string expLong = ((double)66.67).ToStringShortFloat() + "%";
            string expShort = ((double)50).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.WinTradeCountPCAll);
            Assert.AreEqual(expLong, sys1Prop.WinTradeCountPCLong);
            Assert.AreEqual(expShort, sys1Prop.WinTradeCountPCShort);
        }

        [TestMethod]
        public void TestWinProfitSum()
        {
            string expAll = ((double)6450).ToStringBase();
            string expLong = ((double)4270).ToStringBase();
            string expShort = ((double)2180).ToStringBase();

            Assert.AreEqual(expAll, sys1Prop.WinProfitSumAll);
            Assert.AreEqual(expLong, sys1Prop.WinProfitSumLong);
            Assert.AreEqual(expShort, sys1Prop.WinProfitSumShort);
        }

        [TestMethod]
        public void TestWinProfitAverage()
        {
            string expAll = ((double)1075).ToStringBase();
            string expLong = ((double)1067.5).ToStringBase();
            string expShort = ((double)1090).ToStringBase();

            Assert.AreEqual(expAll, sys1Prop.WinProfitAverageAll);
            Assert.AreEqual(expLong, sys1Prop.WinProfitAverageLong);
            Assert.AreEqual(expShort, sys1Prop.WinProfitAverageShort);
        }

        [TestMethod]
        public void TestWinProfitPCAverage()
        {
            string expAll = ((double)0.81).ToStringShortFloat() + "%";
            string expLong = ((double)0.8).ToStringShortFloat() + "%";
            string expShort = ((double)0.85).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.WinProfitPCAverageAll);
            Assert.AreEqual(expLong, sys1Prop.WinProfitPCAverageLong);
            Assert.AreEqual(expShort, sys1Prop.WinProfitPCAverageShort);
        }

        [TestMethod]
        public void TestMaxWinRow()
        {
            string expAll = (3).ToString();
            string expLong = (2).ToString();
            string expShort = (1).ToString();

            Assert.AreEqual(expAll, sys1Prop.MaxWinRowAll);
            Assert.AreEqual(expLong, sys1Prop.MaxWinRowLong);
            Assert.AreEqual(expShort, sys1Prop.MaxWinRowShort);
        }

        [TestMethod]
        public void TestLoseTradeCount()
        {
            string expAll = (4).ToString();
            string expLong = (2).ToString();
            string expShort = (2).ToString();

            Assert.AreEqual(expAll, sys1Prop.LoseTradeCountAll);
            Assert.AreEqual(expLong, sys1Prop.LoseTradeCountLong);
            Assert.AreEqual(expShort, sys1Prop.LoseTradeCountShort);
        }

        [TestMethod]
        public void TestLoseTradeCountPC()
        {
            string expAll = ((double)40).ToStringShortFloat() + "%";
            string expLong = ((double)33.33).ToStringShortFloat() + "%";
            string expShort = ((double)50).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.LoseTradeCountPCAll);
            Assert.AreEqual(expLong, sys1Prop.LoseTradeCountPCLong);
            Assert.AreEqual(expShort, sys1Prop.LoseTradeCountPCShort);
        }

        [TestMethod]
        public void TestLoseProfitSum()
        {
            string expAll = ((double)-2000).ToStringBase();
            string expLong = ((double)-1210).ToStringBase();
            string expShort = ((double)-790).ToStringBase();

            Assert.AreEqual(expAll, sys1Prop.LoseProfitSumAll);
            Assert.AreEqual(expLong, sys1Prop.LoseProfitSumLong);
            Assert.AreEqual(expShort, sys1Prop.LoseProfitSumShort);
        }

        [TestMethod]
        public void TestLoseProfitAverage()
        {
            string expAll = ((double)-500).ToStringBase();
            string expLong = ((double)-605).ToStringBase();
            string expShort = ((double)-395).ToStringBase();

            Assert.AreEqual(expAll, sys1Prop.LoseProfitAverageAll);
            Assert.AreEqual(expLong, sys1Prop.LoseProfitAverageLong);
            Assert.AreEqual(expShort, sys1Prop.LoseProfitAverageShort);
        }

        [TestMethod]
        public void TestLoseProfitPCAverage()
        {
            string expAll = ((double)-0.39).ToStringShortFloat() + "%";
            string expLong = ((double)-0.47).ToStringShortFloat() + "%";
            string expShort = ((double)-0.3).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.LoseProfitPCAverageAll);
            Assert.AreEqual(expLong, sys1Prop.LoseProfitPCAverageLong);
            Assert.AreEqual(expShort, sys1Prop.LoseProfitPCAverageShort);
        }

        [TestMethod]
        public void TestMaxLoseRow()
        {
            string expAll = (2).ToString();
            string expLong = (1).ToString();
            string expShort = (1).ToString();

            Assert.AreEqual(expAll, sys1Prop.MaxLoseRowAll);
            Assert.AreEqual(expLong, sys1Prop.MaxLoseRowLong);
            Assert.AreEqual(expShort, sys1Prop.MaxLoseRowShort);
        }

        [TestMethod]
        public void TestMaxDrawDown()
        {
            string expAll = ((double)-810).ToStringBase();
            string expLong = ((double)-1090).ToStringBase();
            string expShort = ((double)-490).ToStringBase();

            Assert.AreEqual(expAll, sys1Prop.MaxDrawDownAll);
            Assert.AreEqual(expLong, sys1Prop.MaxDrawDownLong);
            Assert.AreEqual(expShort, sys1Prop.MaxDrawDownShort);
        }

        [TestMethod]
        public void TestMaxDrawDownDate()
        {
            string expAll = new DateTime(2013, 4, 23, 11, 0, 0).ToStandardString();
            string expLong = new DateTime(2013, 4, 23, 10, 0, 0).ToStandardString();
            string expShort = new DateTime(2013, 4, 18, 11, 30, 0).ToStandardString();

            Assert.AreEqual(expAll, sys1Prop.MaxDrawDownDateAll);
            Assert.AreEqual(expLong, sys1Prop.MaxDrawDownDateLong);
            Assert.AreEqual(expShort, sys1Prop.MaxDrawDownDateShort);
        }

        [TestMethod]
        public void TestMaxDrawDownPC()
        {
            string expAll = ((double)-0.63).ToStringShortFloat() + "%";
            string expLong = ((double)-0.85).ToStringShortFloat() + "%";
            string expShort = ((double)-0.38).ToStringShortFloat() + "%";

            Assert.AreEqual(expAll, sys1Prop.MaxDrawDownPCAll);
            Assert.AreEqual(expLong, sys1Prop.MaxDrawDownPCLong);
            Assert.AreEqual(expShort, sys1Prop.MaxDrawDownPCShort);
        }

        [TestMethod]
        public void TestMaxDrawDownPCDate()
        {
            string expAll = new DateTime(2013, 4, 23, 11, 0, 0).ToStandardString();
            string expLong = new DateTime(2013, 4, 23, 10, 0, 0).ToStandardString();
            string expShort = new DateTime(2013, 4, 18, 11, 30, 0).ToStandardString();

            Assert.AreEqual(expAll, sys1Prop.MaxDrawDownPCDateAll);
            Assert.AreEqual(expLong, sys1Prop.MaxDrawDownPCDateLong);
            Assert.AreEqual(expShort, sys1Prop.MaxDrawDownPCDateShort);
        }

        [TestMethod]
        public void TestProfitFactor()
        {
            string expAll = ((double)3.23).ToStringShortFloat();
            string expLong = ((double)3.53).ToStringShortFloat();
            string expShort = ((double)2.76).ToStringShortFloat();

            Assert.AreEqual(expAll, sys1Prop.ProfitFactorAll);
            Assert.AreEqual(expLong, sys1Prop.ProfitFactorLong);
            Assert.AreEqual(expShort, sys1Prop.ProfitFactorShort);
        }

        [TestMethod]
        public void TestRecoveryFactor()
        {
            string expAll = ((double)5.49).ToStringShortFloat();
            string expLong = ((double)2.81).ToStringShortFloat();
            string expShort = ((double)2.84).ToStringShortFloat();

            Assert.AreEqual(expAll, sys1Prop.RecoveryFactorAll);
            Assert.AreEqual(expLong, sys1Prop.RecoveryFactorLong);
            Assert.AreEqual(expShort, sys1Prop.RecoveryFactorShort);
        }

        [TestMethod]
        public void TestPayoffRatio()
        {
            string expAll = ((double)2.15).ToStringShortFloat();
            string expLong = ((double)1.76).ToStringShortFloat();
            string expShort = ((double)2.76).ToStringShortFloat();

            Assert.AreEqual(expAll, sys1Prop.PayoffRatioAll);
            Assert.AreEqual(expLong, sys1Prop.PayoffRatioLong);
            Assert.AreEqual(expShort, sys1Prop.PayoffRatioShort);
        }        
    }
}
