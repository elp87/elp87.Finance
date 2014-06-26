using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Finance.Helpers;

namespace Test.elp87.Finance
{
    [TestClass]
    public class ConvertTest
    {
        [TestMethod]
        public void TestToStringInt()
        {
            double test = 100500;
            string expResult = "100 500";

            string result = test.ToStringInt();

            StringAssert.Equals(expResult, result);
        }

        [TestMethod]
        public void TestToStringFloat()
        {
            double test = 157813.532687;
            string expResult = "157 813,533";

            string result = test.ToStringFloat();

            StringAssert.Equals(expResult, result);

        }
    }
}
