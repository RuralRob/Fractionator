namespace Fractionator.Tests
{
    [TestClass]
    public class EvaluatorTests
    {
        [TestMethod]
        public void TestValidExpressions()
        {
            // Simple two-operand case.
            string response = ExpressionEvaluator.Evaluate("1/2 * 3&3/4");
            Assert.AreEqual(response, "1&7/8");

            // Three-operand case.
            response = ExpressionEvaluator.Evaluate("1/2 * 3&3/4 + 1");
            Assert.AreEqual(response, "2&7/8");

            // Just one operand (should return that value).
            response = ExpressionEvaluator.Evaluate("1/2");
            Assert.AreEqual(response, "1/2");
        }

        [TestMethod]
        public void TestBadExpressions()
        {
            // Dangling operator.
            string response = ExpressionEvaluator.Evaluate("1/2 *");
            Assert.AreEqual(response, "1/2 *<< Invalid expression");

            // Two operators in a row.
            response = ExpressionEvaluator.Evaluate("1/2 * + 3/4");
            Assert.AreEqual(response, "1/2 * +<< Invalid expression");

            // Two operands in a row.
            response = ExpressionEvaluator.Evaluate("1/2 * 3/4 5/6 + 7/8");
            Assert.AreEqual(response, "1/2 * 3/4 5/6<< Invalid expression");

            // Division by zero.
            response = ExpressionEvaluator.Evaluate("1/2 / 0");
            Assert.AreEqual(response, "1/2 / 0<< Denominator of 0 is not allowed.");

            // Garbage in the expression.
            response = ExpressionEvaluator.Evaluate("1/2 + BLECCH");
            Assert.AreEqual(response, "1/2 + BLECCH<< Invalid operand");
        }
    }
}
