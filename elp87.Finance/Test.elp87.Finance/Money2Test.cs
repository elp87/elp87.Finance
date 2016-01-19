using System.Diagnostics.CodeAnalysis;
using elp87.Finance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.elp87.Finance
{
    [TestClass]
    public class Money2Test
    {
        [TestMethod]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void OperatorEqual_OneNullReference()
        {
            Money2 a = null; 
            Money2 b = new Money2(100);
            Assert.AreEqual((a == b), false);
            Assert.AreEqual((b==a), false);
        }

        [TestMethod]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void OperatorEqual_BothNullReference()
        {
            Money2 a = null;
            Money2 b = null;
            Assert.AreEqual((a == b), true);
        }

        [TestMethod]
        public void OperatorEqual_Values()
        {
            Money2 a = new Money2(100);
            Money2 b = new Money2(100);
            Money2 c = new Money2(200);
            Assert.AreEqual((a == b), true);
            Assert.AreEqual((a == c), false);
        }

    }
}
