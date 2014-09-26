using System;
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
            expList.Add(new TsLabTrade() { IsLong = false, Count = 1, EntryDateTime = new DateTime(2014, 09, 16, 10, 00, 00), EntryPrice = 117410, ExitDateTime = new DateTime(2014, 09, 16, 11, 00, 00), ExitPrice = 117780 });
        }

        [TestMethod]
        public void TestReadReport()
        {
            List<ISysTrade> trades = TSLabReport.ReadReport(@"files\trades.csv");

            for (int i = 0; i < expList.Count; i++)
            {
                Assert.AreEqual((Trade)expList[i], (Trade)trades[i]);
            }
        }
    }
}
