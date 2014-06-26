using elp87.Finance.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Test.elp87.Finance
{
    [TestClass]
    public class LinqExtsTest
    {
        private List<int> testList;

        public LinqExtsTest()
        {
            testList = new List<int>() { 0, 2, 5, -3, 1, -2, 5, 7, 3, 8, 10, -1, 5, 3, -4 };
        }

        [TestMethod]
        public void TestMaxCountInRow()
        {
            int expMaxPositiveInRow = 5;

            int result = testList.MaxCountInRow(num => num > 0);

            Assert.AreEqual(expMaxPositiveInRow, result);
        }
    }
}
