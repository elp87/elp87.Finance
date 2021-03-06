﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Finance;

namespace Test.elp87.Finance
{
    [TestClass]
    public class SysTradeTest
    {
        TradeSystem system;

        public SysTradeTest()
        {
            system = new TradeSystem();
            system.AddTrade(
                new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 1,
                    IsLong = true
                }
                );
            system.AddTrade(
                new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 2,
                    IsLong = true
                }
                );
            system.AddTrade(
                new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 2,
                    IsLong = false
                }
                );
            system.AddTrade(
                new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 387,
                    ExitPrice = 215,
                    Count = 2,
                    IsLong = false,
                    NewContract = true
                }
                );
            system.AddTrade(
                new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 387.75,
                    ExitPrice = 215.16,
                    Count = 2,
                    IsLong = false
                }
            );
            system.CalcTradeProperties();

        }

        [TestMethod]
        public void TestCumProfit()
        {
            Money[] expCumProfit = new Money[] { 1, 3, 1, 345, 690.18 };

            for (int i = 0; i < expCumProfit.Length; i++)
            {
                Assert.AreEqual(expCumProfit[i], system.Trades[i].CumProfit);
            }
        }

        [TestMethod]
        public void TestCumProfitPC()
        {
            double[] expCumProfitPC = new double[] { 0.99, 1.98, 0.99, 80.99, 161.2 };
            double delta = 0.01;

            for (int i = 0; i < expCumProfitPC.Length; i++)
            {
                Assert.AreEqual(expCumProfitPC[i], system.Trades[i].CumProfitPC, delta);
            }
        }

        [TestMethod]
        public void TestContractProfit()
        {
            Money[] expProfit = new Money[] { 1, 3, 1, 344, 689.18 };

            for (int i = 0; i < expProfit.Length; i++)
            {
                Assert.AreEqual(expProfit[i], system.Trades[i].ContractProfit);
            }
        }

        [TestMethod]
        public void TestContractProfitPC()
        {
            double[] expProfitPC = new double[] { 0.99, 1.98, 0.99, 80, 160.21 };
            double delta = 0.01;

            for (int i = 0; i < expProfitPC.Length; i++)
            {
                Assert.AreEqual(expProfitPC[i], system.Trades[i].ContractProfitPC, delta);
            }
        }

        [TestMethod]
        public void TestDrawDown()
        {
            Money[] expDD = new Money[] { 0, 0, 2, 0, 0 };

            for (int i = 0; i < expDD.Length; i++)
            {
                Assert.AreEqual(expDD[i], system.Trades[i].DrawDown);
            }
        }

        [TestMethod]
        public void TestDrawDownPC()
        {
            double[] expDDPC = new double[] { 0, 0, 0.99, 0, 0 };
            double delta = 0.01;

            for (int i = 0; i < expDDPC.Length; i++)
            {
                Assert.AreEqual(expDDPC[i], system.Trades[i].DrawDownPC, delta);
            }
        }

        [TestMethod]
        public void TestNewContract()
        {
            bool[] expValue = new bool[] { false, false, false, true, false };

            for (int i = 0; i < expValue.Length; i++)
            {
                Assert.AreEqual(expValue[i], system.Trades[i].NewContract);
            }
        }

        [TestMethod]
        public void TestSort()
        {
            SysTrade trade0 = new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 3, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 3, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 1,
                    IsLong = true
                };
            SysTrade trade1 = new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 3, 12, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 3, 13, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 2,
                    IsLong = true
                };
            SysTrade trade2 = new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 2, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 2, 11, 0, 0),
                    EntryPrice = 100,
                    ExitPrice = 101,
                    Count = 2,
                    IsLong = false
                };
            SysTrade trade3 = new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 10, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 11, 0, 0),
                    EntryPrice = 387,
                    ExitPrice = 215,
                    Count = 2,
                    IsLong = false,
                    NewContract = true
                };
            SysTrade trade4 = new SysTrade()
                {
                    EntryDateTime = new DateTime(2014, 1, 1, 15, 0, 0),
                    ExitDateTime = new DateTime(2014, 1, 1, 16, 0, 0),
                    EntryPrice = 387.75,
                    ExitPrice = 215.16,
                    Count = 2,
                    IsLong = false
                };
            TradeSystem expSystem = new TradeSystem();
            expSystem.AddTrade(trade3);
            expSystem.AddTrade(trade4);
            expSystem.AddTrade(trade2);
            expSystem.AddTrade(trade0);
            expSystem.AddTrade(trade1);
            expSystem.CalcTradeProperties();

            TradeSystem sortSystem = new TradeSystem();
            sortSystem.AddTrade(trade0);
            sortSystem.AddTrade(trade1);
            sortSystem.AddTrade(trade2);
            sortSystem.AddTrade(trade3);
            sortSystem.AddTrade(trade4);
            sortSystem.Sort();
            sortSystem.CalcTradeProperties();

            CollectionAssert.AreEqual(expSystem.Trades, sortSystem.Trades);
        }
    }
}
