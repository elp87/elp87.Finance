using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Finance.Helpers;

namespace Test.elp87.Finance
{
    [TestClass]
    public class CompareTest
    {
        [TestMethod]
        public void TestIsInteger()
        {
            double[] test = new double[] { 156, 156.17 };

            bool[] expResult = new bool[] { true, false };

            for (int i = 0; i < test.Length; i++)
            {
                Assert.AreEqual(expResult[i], Compare.IsInteger(test[i]));
            }
        }
    }
}
