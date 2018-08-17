using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UtilitySharp
{
    /// <summary>
    /// A static class that contains useful methods related to time.
    /// </summary>
    class TimeHelper
    {
        /// <summary>
        /// Returns the regex string for a given DateTime format string.
        /// </summary>
        /// <param name="str">The DateTime format string.</param>
        public static string DateTimeFormatStringToRegex(string str)
        {
            var original = str;
            var map = Resources.DateTimeFormatStringToRegexMap();
            var processed = new List<KeyValuePair<int, string>>();

            foreach (var format in map)
            {
                if (str.Contains(format.Key))
                {
                    for (int i = 0; i < Regex.Matches(original, format.Key).Count; i++)
                    {
                        var index = StringHelper.IndexOfNth(original, format.Key, i + 1);
                        str = StringHelper.ReplaceFirst(str, format.Key, new string('X', format.Key.Length));
                        processed.Add(new KeyValuePair<int, string>(index, format.Value));
                    }
                }
            }
            for (var m = Regex.Match(str, "[^X]"); m.Success; m = m.NextMatch())
            {
                str = StringHelper.ReplaceFirst(str, m.Value, new string('X', m.Length));
                var substr = StringHelper.EscapeRegexSpecialCharacters(m.Value);
                processed.Add(new KeyValuePair<int, string>(m.Index, substr));
            }

            if (processed.Any())
            {
                var regexList = processed.OrderBy(p => p.Key).Select(p => p.Value).ToList();
                return $"({string.Join(string.Empty, regexList)})";
            }
            else
            {
                throw new Exception($"'{str}' is an invalid DateTime format string!");
            }
        }

        /// <summary>
        /// Converts unix time to a DateTime.
        /// </summary>
        /// <param name="unixTime">The number of seconds past since 1970/01/01.</param>
        public static DateTime UnixTimeToDateTime(long unixTime)
        {
            return Resources.EpochTime().AddSeconds(unixTime);
        }

        /// <summary>
        /// Converts a DateTime to unix time.
        /// </summary>
        /// <param name="dateTime">The DateTime to be converted.</param>
        public static long DateTimeToUnixTime(DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Converts ISO 8601 time to a DateTime.
        /// </summary>
        /// <param name="isoTime">The ISO 8601 string to be converted.</param>
        public static DateTime ISO8601ToDateTime(string isoTime)
        {
            return DateTime.Parse(isoTime);
        }

        /// <summary>
        /// Converts a DateTime to ISO 8601 time.
        /// </summary>
        /// <param name="dateTime">The DateTime to be converted.</param>
        public static string DateTimeToISO8601(DateTime dateTime)
        {
            return dateTime.ToString("O");
        }
    }
}
