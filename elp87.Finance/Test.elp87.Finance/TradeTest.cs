using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Finance;

namespace Test.elp87.Finance
{
    [TestClass]
    public class TradeTest
    {
        private Trade[] _trades = new Trade[]
            {
                new Trade() 
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 1,
                    IsLong = true
                },
                new Trade() 
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 2,
                    IsLong = true
                },
                new Trade() 
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 2,
                    IsLong = false
                }, 
                new Trade() 
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 387,
                    ExitPrice = 215,
                    Count = 2,
                    IsLong = false
                }, 
                new Trade() 
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 387.75,
                    ExitPrice = 215.16,
                    Count = 2,
                    IsLong = false
                }
            };

        [TestMethod]
        public void TestEntryVolume()
        {
            Money[] expEntryVolumes = new Money[] { 100, 200, 200, 774 };

            for (int i = 0; i < expEntryVolumes.Length; i++)
            {
                Assert.AreEqual(expEntryVolumes[i], _trades[i].EntryVolume);
            }
        }

        [TestMethod]
        public void TestExitVolume()
        {
            Money[] expExitVolumes = new Money[] { 101, 202, 202, 430 };

            for (int i = 0; i < expExitVolumes.Length; i++)
            {
                Assert.AreEqual(expExitVolumes[i], _trades[i].ExitVolume);
            }
        }

        [TestMethod]
        public void TestProfit()
        {
            Money[] expProfits = new Money[] { 1, 2, -2, 344 };

            for (int i = 0; i < expProfits.Length; i++)
            {
                Assert.AreEqual(expProfits[i], _trades[i].Profit);
            }
        }

        [TestMethod]
        public void TestProfitPC()
        {
            double[] expProfits = new double[] { 1, 1, -1, 80, 80.21 };

            for (int i = 0; i < expProfits.Length; i++)
            {
                Assert.AreEqual(expProfits[i], _trades[i].ProfitPC, 0.01);
            }
        }
    }
}
