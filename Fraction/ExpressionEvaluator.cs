using System.Text;

namespace Fractionator
{
    public static class ExpressionEvaluator
    {
        private static readonly string[] _operands = { "+", "-", "*", "/", "%" };

        /// <summary>
        /// Simple expression evaluator (does not support parentheses or operator precedence).
        /// Evaluates expression containing one or more operands and operators, and returns a
        /// string containing the result. Supported operators are *, /, +, -, and %.
        /// 
        /// On error, the expression is returned up to the point of error, with the element
        /// that caused the error indicated by "<< [Error text]".
        /// </summary>
        /// <param name="expression">The expression to evaluate</param>
        /// <returns>Result of the evaluation</returns>
        public static string Evaluate(string expression)
        {
            StringBuilder sb = new StringBuilder();
            Fraction lastOperand = null;
            string lastOperator = null;
            bool error = false;

            // Loop through the whitespace-delimited elements of the expression.
            foreach (string element in expression.Split().Where(x => x != string.Empty))
            {
                // Build up copy of the expression up to this point, so we can return it
                // in error response, if needed.
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(element);

                if (element.Length == 1 && !char.IsDigit(element[0]))
                {
                    // This element is single character and not a digit, so assume it's an operator.
                    if (_operands.Contains(element))
                    {
                        // This is a valid operator. Make sure it's not following another operator.
                        if (lastOperator != null)
                        {
                            // The last element was also an operator. Error!
                            sb.Append("<< Invalid expression");
                            error = true;
                            break;
                        }
                        else
                        {
                            // All good, so remember this operator and continue parsing.
                            lastOperator = element;
                            continue;
                        }
                    }
                    else
                    {
                        // This is not a valid operator. Error!
                        sb.Append("<< Invalid operator");
                        error = true;
                        break;
                    }
                }
                else
                {
                    // This element should be an operand.
                    Fraction operand;
                    try
                    {
                        operand = new Fraction(element);
                    }
                    catch (Exception)
                    {
                        // Bad operand - error!
                        sb.Append("<< Invalid operand");
                        error = true;
                        break;
                    }

                    // If this is our first operand, save it and continue parsing.
                    if (lastOperand == null)
                    {
                        lastOperand = operand;
                        continue;
                    }

                    // If we don't have an operator already, error!
                    if (lastOperator == null)
                    {
                        sb.Append("<< Invalid expression");
                        error = true;
                        break;
                    }

                    // Perform the operation on this value and the last saved value, then save the result as the last saved value.
                    try
                    {
                        switch (lastOperator)
                        {
                            case "+":
                                lastOperand = lastOperand + operand;
                                break;
                            case "-":
                                lastOperand = lastOperand - operand;
                                break;
                            case "*":
                                lastOperand = lastOperand * operand;
                                break;
                            case "/":
                                lastOperand = lastOperand / operand;
                                break;
                            case "%":
                                lastOperand = lastOperand % operand;
                                break;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        // Math error!
                        sb.Append("<< ");
                        sb.Append(ex.Message);
                        error = true;
                        break;
                    }

                    // We "consumed" the last operator, so clear this.
                    lastOperator = null;
                }
            }

            // Check for dangling operator.
            if (!error && lastOperator != null)
            {
                sb.Append("<< Invalid expression");
                error = true;
            }

            return error
                ? sb.ToString()
                : lastOperand.ToString();
        }
    }
}
