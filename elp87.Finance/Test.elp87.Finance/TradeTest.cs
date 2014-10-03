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

        [TestMethod]
        public void TestClone()
        {
            object[] cloneTrades = new object[_trades.Length];

            for (int i = 0; i < cloneTrades.Length; i++)
            {
                cloneTrades[i] = _trades[i].Clone();
            }

            for (int i = 0; i < cloneTrades.Length; i++)
            {
                Assert.AreEqual(_trades[i], (Trade)cloneTrades[i]);
            }
        }

        [TestMethod]
        public void TestDeepCloneForEntryPrice()
        {
            Trade trade = new Trade()
            {
                EntryDateTime = new DateTime(2014, 10, 3, 10, 15, 37),
                EntryPrice = 127356m,
                ExitDateTime = new DateTime(2014, 10, 3, 11, 13, 16),
                ExitPrice = 128567m,
                Count = 1,
                IsLong = true
            };

            Trade cloneTrade = trade.Clone() as Trade;
            trade.EntryPrice = 127000m;
            
            Assert.AreNotEqual(trade, cloneTrade);
        }

        [TestMethod]
        public void TestDeepCloneForEntryDateTime()
        {
            Trade trade = new Trade()
            {
                EntryDateTime = new DateTime(2014, 10, 3, 10, 15, 37),
                EntryPrice = 127356m,
                ExitDateTime = new DateTime(2014, 10, 3, 11, 13, 16),
                ExitPrice = 128567m,
                Count = 1,
                IsLong = true
            };

            Trade cloneTrade = trade.Clone() as Trade;
            trade.EntryDateTime = new DateTime(2014, 10, 3, 0, 0, 0);

            Assert.AreNotEqual(trade, cloneTrade);
        }
    }
}
