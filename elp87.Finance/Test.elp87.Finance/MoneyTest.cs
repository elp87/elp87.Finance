using Microsoft.VisualStudio.TestTools.UnitTesting;
using elp87.Finance;
using System;

namespace Test.elp87.Finance
{
    [TestClass]
    public class MoneyTest
    {
        private double[] dValues = new double[] 
            { 9923.878864, 385.343606, 316977.7498, 32.53238665, 5603.254167, 679.8344853, 9059.801647, 55251.78166, 5463869.642, .7547050281 };

        private decimal[] decValues = new decimal[] 
            { 9923.878864m, 385.343606m, 316977.7498m, 32.53238665m, 5603.254167m, 679.8344853m, 9059.801647m, 55251.78166m, 5463869.642m, .7547050281m };

        private int[] intValues = new int[] 
            { 84420, 23665, 30900, 30232, 39674, 22495, 9505, 31612, 98792, 80619, 68191, 11499, 21827, 80059, 91526, 24352, 28564, 95985, 48777, 48831 };

        private decimal[] decValues2 = new decimal[] 
            { 84420, 23665, 30900, 30232, 39674, 22495, 9505, 31612, 98792, 80619, 68191, 11499, 21827, 80059, 91526, 24352, 28564, 95985, 48777, 48831 };

        [TestMethod]
        public void TestClassicSum()
        {
            decimal expValue = 100;

            Money money = new Money(0);
            for (int i = 0; i < 1000; i++)
            {
                money += 0.1;
            }

            Assert.AreEqual(expValue, money.Value);
        }

        [TestMethod]
        public void TestCtorDouble()
        {
            Money[] moneyArray = new Money[dValues.Length];
            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = new Money(dValues[i]);
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(decValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestCtorDecimal()
        {
            Money[] moneyArray = new Money[decValues.Length];
            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = new Money(decValues[i]);
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(decValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestCtorInt()
        {
            Money[] moneyArray = new Money[intValues.Length];
            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = new Money(intValues[i]);
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(decValues2[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestPlus1()
        {
            decimal[] expValues = new decimal[] { 10309.22247m, 317010.28218665m, 6283.0886523m, 64311.583307m, 5463870.3967050281m };

            Money[] moneyArray = new Money[expValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Money money1 = new Money(dValues[i * 2]);
                Money money2 = new Money(dValues[(i * 2) + 1]);

                moneyArray[i] = money1 + money2;
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(expValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestPlus2()
        {
            decimal[] expValues = new decimal[] { 108085, 61132, 62169, 41117, 179411, 79690, 101886, 115878, 124549, 97608};

            Money[] moneyArray = new Money[expValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Money money1 = new Money(intValues[i * 2]);
                Money money2 = new Money(intValues[(i * 2) + 1]);

                moneyArray[i] = money1 + money2;
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(expValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestMinusDouble()
        {
            decimal[] expValues = new decimal[] { 9538.535258m, 316945.21741335m, 4923.4196817m, -46191.980013m, 5463868.8872949719m };

            Money[] moneyArray = new Money[expValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Money money1 = new Money(dValues[i * 2]);
                Money money2 = new Money(dValues[(i * 2) + 1]);

                moneyArray[i] = money1 - money2;
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(expValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestMinusInt()
        {
            decimal[] expValues = new decimal[] { 60755, 668, 17179, -22107, 18173, 56692, -58232, 67174, -67421, -54 };

            Money[] moneyArray = new Money[expValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Money money1 = new Money(intValues[i * 2]);
                Money money2 = new Money(intValues[(i * 2) + 1]);

                moneyArray[i] = money1 - money2;
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(expValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestMultiplyDoubleWithInteger()
        {
            int multiplier = 3;
            decimal[] expValues = new decimal[] 
                { 29771.636592m, 1156.030818m, 950933.2494m, 97.59715995m, 16809.762501m, 2039.5034559m, 27179.404941m, 165755.34498m, 16391608.926m, 2.2641150843m };

            Money[] moneyArray = new Money[expValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = dValues[i] * multiplier;
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(expValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestDivideDoubleWithDouble()
        {
            double divisor = 2.7;
            double delta = 0.00001;

            double[] expValues = new double[] 
                { 3675.51069, 142.71985, 117399.16659, 12.04903, 2075.27932, 251.79055, 3355.48209, 20463.62283, 2023655.42296, 0.27952 };

            Money[] moneyArray = new Money[expValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = dValues[i] / divisor;
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(expValues[i], Convert.ToDouble(moneyArray[i].Value), delta);
            }            
        }

        [TestMethod]
        public void TestPercentage()
        {
            double delta = 0.00001;

            double[] expValues = new double[] { 2475.33243, 974245.20624, 724.20858, -83.60269, 723974093.69995};

            double[] values = new double[expValues.Length];

            for (int i = 0; i < values.Length; i++)
            {
                Money money1 = new Money(dValues[i * 2]);
                Money money2 = new Money(dValues[(i * 2) + 1]);

                values[i] = ((money1 - money2) / money2) * 100;
            }

            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(expValues[i], values[i], delta);
            }
        }

        [TestMethod]
        public void TestImplicitInt()
        {
            Money[] moneyArray = new Money[intValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = intValues[i];
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(decValues2[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestImplicitDecimal()
        {
            Money[] moneyArray = new Money[decValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = decValues[i];
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(decValues[i], moneyArray[i].Value);
            }
        }

        [TestMethod]
        public void TestImplicitDouble()
        {
            Money[] moneyArray = new Money[dValues.Length];

            for (int i = 0; i < moneyArray.Length; i++)
            {
                moneyArray[i] = dValues[i];
            }

            for (int i = 0; i < moneyArray.Length; i++)
            {
                Assert.AreEqual(decValues[i], moneyArray[i].Value);
            }
        }

        public void TestMoreThan()
        {
            Money a = 100;
            Money b = 100.5;
            Money c = 100;

            bool[] expResults = new bool[] {true, false, false};

            bool[] Results = new bool[]
            {
                b > a,
                a > b, 
                a > c
            };

            CollectionAssert.AreEqual(expResults, Results);
        }
    }
}
