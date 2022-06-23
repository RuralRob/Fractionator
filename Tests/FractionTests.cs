namespace Fractionator.Tests
{
    [TestClass]
    public class FractionTests
    {
        [TestMethod]
        public void TestNormalization()
        {
            // Validate that positive fraction is normalized.
            Fraction a = new("6/3");
            Assert.AreEqual(a.Numerator, 2);
            Assert.AreEqual(a.Denominator, 1);

            // Validate that negative fraction is normalized.
            a = new("-6/3");
            Assert.AreEqual(a.Numerator, -2);
            Assert.AreEqual(a.Denominator, 1);
        }

        [TestMethod]
        public void TestFractionToString()
        {
            // Positive fraction.
            Fraction a = new Fraction(1, 2);
            Assert.AreEqual(a.ToString(), "1/2");

            // Negative fraction.
            a = new Fraction(-1, 2);
            Assert.AreEqual(a.ToString(), "-1/2");

            // Positive whole value.
            a = new Fraction(10, 1);
            Assert.AreEqual(a.ToString(), "10");

            // Negative whole value.
            a = new Fraction(-10, 1);
            Assert.AreEqual(a.ToString(), "-10");

            // Positive mixed value.
            a = new Fraction(17, 2);
            Assert.AreEqual(a.ToString(), "8&1/2");

            // Negative mixed value.
            a = new Fraction(-17, 2);
            Assert.AreEqual(a.ToString(), "-8&1/2");

            // Zero.
            a = new Fraction(0, 1);
            Assert.AreEqual(a.ToString(), "0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VerifyThatZeroDenominatorIsNotAllowed()
        {
            Fraction _ = new("5/0");
        }
    }
}