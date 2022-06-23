namespace Fractionator.Tests
{
    [TestClass]
    public class ArithmeticTests
    {
        [TestMethod]
        public void ValidateArithmeticOperators()
        {
            // 1/2 * 3&3/4 = 1&7/8
            Fraction a = new("1/2");
            Fraction b = new("3&3/4");
            Assert.AreEqual((a * b).ToString(), "1&7/8");

            // 1/2 * -3&3/4 = -1&7/8
            a = new("1/2");
            b = new("-3&3/4");
            Assert.AreEqual((a * b).ToString(), "-1&7/8");

            // 2&3/8 + 9/8 = 3&1/2
            a = new("2&3/8");
            b = new("9/8");
            Assert.AreEqual((a + b).ToString(), "3&1/2");

            // 1&3/4 - 2 = -1/4
            a = new("1&3/4");
            b = new("2");
            Assert.AreEqual((a - b).ToString(), "-1/4");

            // 11 / 3 = 3&2/3
            a = new("11");
            b = new("3");
            Assert.AreEqual((a / b).ToString(), "3&2/3");

            // 11 / -3 = -3&2/3
            a = new("11");
            b = new("-3");
            Assert.AreEqual((a / b).ToString(), "-3&2/3");

            // 11 % 3 = 2
            a = new("11");
            b = new("3");
            Assert.AreEqual((a % b).ToString(), "2");

            // -11 % 3 = -2
            a = new("-11");
            b = new("3");
            Assert.AreEqual((a % b).ToString(), "-2");

            // 5&1/5 % 3 = 2&1/5
            a = new("5&1/5");
            b = new("3");
            Assert.AreEqual((a % b).ToString(), "2&1/5");

            // -5&1/5 % 3 = -2&1/5
            a = new("-5&1/5");
            b = new("3");
            Assert.AreEqual((a % b).ToString(), "-2&1/5");
        }
    }
}
