using System;

namespace UtilitySharp
{
    /// <summary>
    /// A static class that contains useful methods related to numbers.
    /// </summary>
    public static class NumberHelper
    {
        /// <summary>
        /// Checks if two numbers are equal within a provided range.
        /// </summary>
        /// <param name="a">The first number to be compared.</param>
        /// <param name="b">The second number to be compared.</param>
        /// <param name="epsilon">The maxium allowed range between the two numbers.</param>
        public static bool NearlyEqual(double a, double b, double epsilon)
        {
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double diff = Math.Abs(a - b);
            if (a == b)
            {
                return true;
            }
            if (a == 0 || b == 0 || diff < Double.Epsilon)
            {
                return diff < epsilon;
            }
            return diff / (absA + absB) < epsilon;
        }
    }
}
