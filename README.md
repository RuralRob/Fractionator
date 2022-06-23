
# Fractionator
## Introduction
Simple console Windows application that evaluates operations on fractions, per the Code Challenge requirements.
## Compiling and Running
The application was written in C# and .NET 6, using Visual Studio 2022.
After loading the solution into Visual Studio, hit F5 to compile and run the console application.
## Code Structure
### Fraction class
The Fraction class encapsulates one fractional operand, and provides overloaded operators for performing addition, subtraction, multiplication, division, and modulo operations on Fractions.
### ExpressionEvaluator class
This is a static class that implements the Evaluate method, which accepts a string containing an expression (i.e. "1/2 + 3&4/5 - 2000") and returns a string containing the evaluated response.
## Unit Tests
The unit tests can be viewed and run using Visual Studio's Test Explorer. There are tests for the Fraction and ExpressionEvaluator classes described above.