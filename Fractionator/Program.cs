// See https://aka.ms/new-console-template for more information
using Fractionator;

Console.WriteLine("Welcome to the FRACTIONATOR. Enter expressions to evaluate, or \"exit\" to quit.");

while (true)
{
    // Read the next expression from the keyboard, and quit if the user entered "exit".
    Console.Write(">");
    string expression = Console.ReadLine();
    if (string.Compare(expression, "exit", true) == 0)
        break;

    // Evaluate the expression and display the result.
    Console.WriteLine(ExpressionEvaluator.Evaluate(expression));
}

Console.WriteLine("Thank you for using the FRACTIONATOR.");