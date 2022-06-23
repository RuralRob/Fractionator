using System.Text.RegularExpressions;

namespace Fractionator
{
    public class Fraction
    {
        #region Properties
        public long Numerator { get; private set; }
        public long Denominator { get; private set; }
        #endregion Properties

        #region .ctor
        public Fraction(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
            ValidateAndNormalize();
        }

        public Fraction(string value)
        {
            ParseFraction(value);
            ValidateAndNormalize();
        }
        #endregion .ctor

        #region Operators
        // Overloaded arithmetic operators.
        public static Fraction operator -(Fraction a) => new(-a.Numerator, a.Denominator);
        public static Fraction operator +(Fraction a, Fraction b) => new(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        public static Fraction operator -(Fraction a, Fraction b) => a + (-b);
        public static Fraction operator *(Fraction a, Fraction b) => new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        public static Fraction operator /(Fraction a, Fraction b) => new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        public static Fraction operator %(Fraction a, Fraction b)
        {
            // Only allow whole number for modulo value.
            if (b.Numerator < 0 || b.Denominator != 1)
                throw new ArgumentException("Modulo value must be positve integer.");

            // If first operand is integer, do integer modulo operation.
            if (a.Denominator == 1)
            {
                return new Fraction(a.Numerator % b.Numerator, 1);
            }

            // First operand is fractional, so we gotta do this the hard way (loop to subtract the scaled modulo, while preserving the original sign).
            long scaledModulo = a.Denominator * b.Numerator;
            long numerator = Math.Abs(a.Numerator);
            while (numerator / a.Denominator > b.Numerator)
            {
                numerator -= scaledModulo;
            }
            return new Fraction(numerator * Math.Sign(a.Numerator), a.Denominator);
        }
        #endregion Operators

        #region Overrides
        public override string ToString()
        {
            string value;

            if (Numerator % Denominator == 0)
            {
                // Whole number.
                value = (Numerator / Denominator).ToString();
            }
            else if (Math.Abs(Numerator) > Denominator)
            {
                // Mixed number.
                value = string.Format($"{Numerator/Denominator}&{Math.Abs(Numerator)%Denominator}/{Denominator}");
            }
            else
            {
                // Proper fraction.
                value = string.Format($"{Numerator}/{Denominator}");
            }

            return value;
        }
        #endregion Overrides

        #region Private Helper Methods
        /// <summary>
        /// Return the greatest common devisor of the two specified values.
        /// </summary>
        private static long GCD(long value1, long value2)
        {
            // Do our GCD calculation on the absolute values.
            value1 = Math.Abs(value1);
            value2 = Math.Abs(value2);

            // Compute GCD.
            while (value1 != 0 && value2 != 0)
            {
                if (value1 > value2)
                    value1 %= value2;
                else
                    value2 %= value1;
            }

            // Return GCD.
            return value1 == 0 ? value2 : value1;
        }

        /// <summary>
        /// Parse the fraction from the passed-in string, and store the numerator and
        /// denominator in this object's Numerator and Denominator properties.
        /// 
        /// The fraction is expected to be one of the following:
        ///   * Whole number, with optional minus sign
        ///   * Fraction in the format N/D, with optional preceding minus sign
        ///   * Mixed number in the format W/N/D, with optional preceding minus sign.
        ///   
        /// </summary>
        /// <param name="value">String containing the fraction to parse.</param>
        private void ParseFraction(string value)
        {
            // Look for a mixed number (W&N/D).
            Match match = Regex.Matches(value, @"(-?\d+)&(\d+)\/(\d+)").FirstOrDefault();
            if (match != null)
            {
                // Found a mixed number.
                long whole = Int64.Parse(match.Groups[1].Value);
                long numerator = Int64.Parse(match.Groups[2].Value);
                long denominator = Int64.Parse(match.Groups[3].Value);
                Numerator = Math.Sign(whole) * (Math.Abs(whole) * denominator + numerator);
                Denominator = denominator;
            }
            else
            {
                // Look for a fraction (N/D).
                match = Regex.Matches(value, @"(-?\d+)\/(\d+)").FirstOrDefault();
                if (match != null)
                {
                    // Found a fraction.
                    Numerator = Int64.Parse(match.Groups[1].Value);
                    Denominator = Int64.Parse(match.Groups[2].Value);
                }
                else
                {
                    // Look for whole number.
                    Numerator = Int64.Parse(value);
                    Denominator = 1;
                }
            }
        }

        /// <summary>
        /// Validate the currently stored fraction (make sure the denominator is not 0),
        /// and normalize it so the sign is in the numerator and both numerator and
        /// denominator are the smallest integral values necessary to represent this
        /// fractional value.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateAndNormalize()
        {
            // Don't allow zero denominator.
            if (Denominator == 0)
                throw new ArgumentException("Denominator of 0 is not allowed.");

            // Normalize the sign so that the denominator is always positive.
            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }

            // Reduce the numerator and denominator to the smallest integral values
            // necessary to represent this fraction.
            long gcd = GCD(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;
        }
        #endregion Private Helper Methods
    }
}