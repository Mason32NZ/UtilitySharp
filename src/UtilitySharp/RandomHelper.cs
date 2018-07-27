using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace UtilitySharp
{
    /// <summary>
    /// A static class that contains useful methods related to randomness.
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// Returns a random int between the provided min and max values.
        /// </summary>
        /// <param name="min">The minimum value allowed.</param>
        /// <param name="max">The maximum value allowed.</param>
        public static int RandomInt(int min = 1, int max = 100)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                byte[] seed = new byte[4];
                rng.GetBytes(seed);

                scale = BitConverter.ToUInt32(seed, 0);
            }
            return (int)(min + (max - min) * (scale / (double)uint.MaxValue));
        }

        /// <summary>
        /// Returns a random float between the provided min and max values.
        /// </summary>
        /// <param name="min">The minimum value allowed.</param>
        /// <param name="max">The maximum value allowed.</param>
        public static float RandomFloat(float min = 0, float max = 1)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                byte[] seed = new byte[4];
                rng.GetBytes(seed);

                scale = BitConverter.ToUInt32(seed, 0);
            }
            return (float)(min + (max - min) * (scale / (double)uint.MaxValue));
        }

        /// <summary>
        /// Returns a random double between the provided min and max values.
        /// </summary>
        /// <param name="min">The minimum value allowed.</param>
        /// <param name="max">The maximum value allowed.</param>
        public static double RandomDouble(double min = 0, double max = 1)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                byte[] seed = new byte[4];
                rng.GetBytes(seed);

                scale = BitConverter.ToUInt32(seed, 0);
            }
            return (min + (max - min) * (scale / (double)uint.MaxValue));
        }

        /// <summary>
        /// Returns a random decimal between the provided min and max values.
        /// </summary>
        /// <param name="min">The minimum value allowed.</param>
        /// <param name="max">The maximum value allowed.</param>
        public static decimal RandomDecimal(decimal min = 0, decimal max = 1)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                byte[] seed = new byte[4];
                rng.GetBytes(seed);

                scale = BitConverter.ToUInt32(seed, 0);
            }
            return (min + (max - min) * (scale / (decimal)uint.MaxValue));
        }

        /// <summary>
        /// Returns a random lowercase char.
        /// </summary>
        public static char RandomLowerChar()
        {
            return (char)RandomInt(97, 122);
        }

        /// <summary>
        /// Returns a random uppercase char.
        /// </summary>
        public static char RandomUpperChar()
        {
            return (char)RandomInt(65, 90);
        }

        /// <summary>
        /// Returns a random alphanumeric string of the provided length.
        /// </summary>
        /// <param name="length">The length of the output string.</param>
        public static string RandomString(int length)
        {
            List<char> chars = new List<char>();

            for (int i = 0; i < length; i++)
            {
                int type = RandomInt(0, 2);
                switch (type)
                {
                    case 0:
                        {
                            chars.Add(RandomLowerChar());
                            break;
                        }
                    case 1:
                        {
                            chars.Add(RandomUpperChar());
                            break;
                        }
                    case 2:
                        {
                            chars.Add((char)(RandomInt(0, 9) + 48));
                            break;
                        }
                }
            }
            return (chars.ToString());
        }
    }
}
