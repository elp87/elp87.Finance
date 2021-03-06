﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Finance;

namespace Test.elp87.Finance
{
    [TestClass]
    public class TsLabReportTest
    {
        List<ISysTrade> expList;

        public TsLabReportTest()
        {
            expList = new List<ISysTrade>();
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 16, 10, 00, 00), EntryPrice = 117410, ExitDateTime = new DateTime(2014, 09, 16, 11, 00, 00), ExitPrice = 117780 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 16, 11, 15, 00), EntryPrice = 118120, ExitDateTime = new DateTime(2014, 09, 16, 15, 45, 00), ExitPrice = 118980 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 16, 18, 30, 00), EntryPrice = 119740, ExitDateTime = new DateTime(2014, 09, 16, 20, 45, 00), ExitPrice = 120760 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 17, 10, 00, 00), EntryPrice = 119130, ExitDateTime = new DateTime(2014, 09, 17, 14, 00, 00), ExitPrice = 118100 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 17, 18, 00, 00), EntryPrice = 118440, ExitDateTime = new DateTime(2014, 09, 17, 19, 00, 00), ExitPrice = 117570 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 17, 21, 45, 00), EntryPrice = 118480, ExitDateTime = new DateTime(2014, 09, 17, 22, 00, 00), ExitPrice = 118350 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 17, 22, 00, 00), EntryPrice = 117410, ExitDateTime = new DateTime(2014, 09, 17, 22, 30, 00), ExitPrice = 118590 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 18, 10, 00, 00), EntryPrice = 117080, ExitDateTime = new DateTime(2014, 09, 18, 12, 00, 00), ExitPrice = 118160 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 19, 12, 30, 00), EntryPrice = 115920, ExitDateTime = new DateTime(2014, 09, 19, 15, 30, 00), ExitPrice = 115720 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 19, 17, 15, 00), EntryPrice = 117080, ExitDateTime = new DateTime(2014, 09, 19, 17, 30, 00), ExitPrice = 116790 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 22, 10, 00, 00), EntryPrice = 115190, ExitDateTime = new DateTime(2014, 09, 22, 10, 30, 00), ExitPrice = 115710 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 22, 10, 30, 00), EntryPrice = 116230, ExitDateTime = new DateTime(2014, 09, 22, 10, 45, 00), ExitPrice = 115620 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 22, 11, 45, 00), EntryPrice = 114860, ExitDateTime = new DateTime(2014, 09, 22, 14, 00, 00), ExitPrice = 114860 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 22, 18, 15, 00), EntryPrice = 114200, ExitDateTime = new DateTime(2014, 09, 22, 21, 30, 00), ExitPrice = 113920 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 23, 10, 15, 00), EntryPrice = 114650, ExitDateTime = new DateTime(2014, 09, 23, 10, 30, 00), ExitPrice = 114120 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 23, 10, 45, 00), EntryPrice = 114700, ExitDateTime = new DateTime(2014, 09, 23, 14, 30, 00), ExitPrice = 114660 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 23, 17, 45, 00), EntryPrice = 115340, ExitDateTime = new DateTime(2014, 09, 23, 21, 15, 00), ExitPrice = 116030 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 24, 10, 00, 00), EntryPrice = 116690, ExitDateTime = new DateTime(2014, 09, 24, 12, 30, 00), ExitPrice = 117370 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 24, 15, 00, 00), EntryPrice = 118070, ExitDateTime = new DateTime(2014, 09, 24, 16, 45, 00), ExitPrice = 117770 });
            expList.Add(new SysTrade() { IsLong = true, Count = 1, EntryDateTime = new DateTime(2014, 09, 24, 19, 00, 00), EntryPrice = 118420, ExitDateTime = new DateTime(2014, 09, 25, 10, 00, 00), ExitPrice = 118560 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 25, 11, 00, 00), EntryPrice = 118020, ExitDateTime = new DateTime(2014, 09, 25, 16, 00, 00), ExitPrice = 117970 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 25, 17, 45, 00), EntryPrice = 117250, ExitDateTime = new DateTime(2014, 09, 25, 22, 30, 00), ExitPrice = 115620 });
            expList.Add(new SysTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 26, 10, 00, 00), EntryPrice = 115230, ExitDateTime = new DateTime(2014, 09, 26, 10, 30, 00), ExitPrice = 115850 });
        }

        [TestMethod]
        public void TestReadReport()
        {
            List<ISysTrade> trades = TSLabReport.ReadReport(@"files\trades.csv");

            CollectionAssert.AreEqual(expList, trades);

            
        }
    }
}
