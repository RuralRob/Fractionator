namespace Fractionator.Tests
{
    [TestClass]
    public class StringParserTests
    {
        [TestMethod]
        public void ValidateStringParser()
        {
            // Test parsing of fraction.
            Fraction a = new("1/2");
            Assert.AreEqual(a.Numerator, 1);
            Assert.AreEqual(a.Denominator, 2);

            // Test parsing of negative fraction.
            a = new("-1/2");
            Assert.AreEqual(a.Numerator, -1);
            Assert.AreEqual(a.Denominator, 2);

            // Test parsing of mixed number.
            a = new("6&7/8");
            Assert.AreEqual(a.Numerator, 55);
            Assert.AreEqual(a.Denominator, 8);

            // Test parsing of negative mixed number.
            a = new("-6&7/8");
            Assert.AreEqual(a.Numerator, -55);
            Assert.AreEqual(a.Denominator, 8);

            // Test parsing of whole number.
            a = new("77");
            Assert.AreEqual(a.Numerator, 77);
            Assert.AreEqual(a.Denominator, 1);

            // Test parsing of negative whole number.
            a = new("-77");
            Assert.AreEqual(a.Numerator, -77);
            Assert.AreEqual(a.Denominator, 1);

            // Test parsing of zero.
            a = new("0");
            Assert.AreEqual(a.Numerator, 0);
            Assert.AreEqual(a.Denominator, 1);
        }
    }
}
