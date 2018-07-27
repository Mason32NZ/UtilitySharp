using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UtilitySharp
{
    /// <summary>
    /// A static class that contains useful methods related to strings.
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// Cleans the provided string and converts it to the provided type.
        /// </summary>
        /// <param name="str">The string or object that supports .ToString() to be cleaned and converted.</param>
        /// <param name="format">A formatting string used for DateTime.ParseExact(). RECOMMENDED if converting to DateTime.</param>
        /// <remarks>
        /// All unsigned numeric conversions DO NOT currently support rounding. Also collections support is planned and coming soon.
        /// </remarks>
        public static T CleanAndConvert<T>(object str, string format = "")
        {
            string txt;
            string type = typeof(T).ToString();

            // Convert str to a string.
            try
            {
                txt = str.ToString();
            }
            catch
            {
                throw new Exception($"{str.GetType()} does not support .ToString()!");
            }

            // Pre-clean string.
            if (!string.IsNullOrWhiteSpace(txt))
            {
                txt = txt.Trim();
            }

            switch (type)
            {
                // Number
                case ("System.Int16"):
                case ("System.Int32"):
                case ("System.Int64"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)").Value, "[^0-9.-]", string.Empty);
                    var b = string.IsNullOrWhiteSpace(a) ? 0 : Math.Round(Convert.ToDecimal(a));
                    return (T)Convert.ChangeType(b, typeof(T));
                }
                case ("System.UInt16"):
                case ("System.UInt32"):
                case ("System.UInt64"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "([0-9]([0-9,]+)?)").Value, "[^0-9]", string.Empty);
                    return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? 0 : Convert.ToUInt64(a), typeof(T));
                }
                case ("System.Single"):
                case ("System.Double"):
                case ("System.Decimal"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)").Value, "[^0-9.-]", string.Empty);
                    return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? 0 : Convert.ToDecimal(a), typeof(T));
                }
                // Number Nullable
                case ("System.Nullable`1[System.Int16]"):
                case ("System.Nullable`1[System.Int32]"):
                case ("System.Nullable`1[System.Int64]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)").Value, "[^0-9.-]", string.Empty);
                    var b = string.IsNullOrWhiteSpace(a) ? (Decimal?)null : Math.Round(Convert.ToDecimal(a));
                    return (T)Convert.ChangeType(b, typeof(T));
                }
                case ("System.Nullable`1[System.UInt16]"):
                case ("System.Nullable`1[System.UInt32]"):
                case ("System.Nullable`1[System.UInt64]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "([0-9]([0-9,]+)?)").Value, "[^0-9]", string.Empty);
                    return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? (UInt64?)null : Convert.ToUInt64(a), typeof(T));
                }
                case ("System.Nullable`1[System.Single]"):
                case ("System.Nullable`1[System.Double]"):
                case ("System.Nullable`1[System.Decimal]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)").Value, "[^0-9.-]", string.Empty);
                    return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? (Decimal?)null : Convert.ToDecimal(a), typeof(T));
                }
                // Other
                case ("System.String"):
                {
                    return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(txt) ? null : txt, typeof(T));
                }
                case ("System.DateTime"):
                {
                    if (format != "")
                    {
                        var regex = DateTimeFormatStringToRegex(format);
                        txt = Regex.Match(txt, regex).Value;
                    }
                    var a = format != "" ? DateTime.ParseExact(txt, format, null) : DateTime.Parse(txt);
                    return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(txt) ? DateTime.MinValue : a, typeof(T));
                }
                case ("System.Nullable`1[System.DateTime]"):
                {
                    if (format != "")
                    {
                        var regex = DateTimeFormatStringToRegex(format);
                        txt = Regex.Match(txt, regex).Value;
                    }
                    var a = format != "" ? DateTime.ParseExact(txt, format, null) : DateTime.Parse(txt);
                    return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(txt) ? (DateTime?)null : a, typeof(T));
                }
                case ("System.Boolean"):
                {
                    var _true = new List<string> { "TRUE", "T", "YES", "Y", "1" };
                    bool value = _true.Any(p => txt.ToUpper().Contains(p));

                    return (T)Convert.ChangeType(value, typeof(T));
                }
            case ("System.Nullable`1[System.Boolean]"):
                {
                    var _true = new List<string> { "TRUE", "T", "YES", "Y", "1" };
                    var _false = new List<string> { "FALSE", "F", "NO", "N", "0" };
                    bool? value = null;

                    if (_true.Any(p => txt.ToUpper().Contains(p)))
                    {
                        value = true;
                    }
                    else if (_false.Any(p => txt.ToUpper().Contains(p)))
                    {
                        value = false;
                    }

                    return (T)Activator.CreateInstance(typeof(T), value);
                }
                default:
                {
                    throw new Exception($"{type} is not supported!");
                }
            }
        }

        /// <summary>
        /// Replaces all but the first instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        public static string ReplaceAllButFirst(string str, string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(str) && str.Contains(oldVal))
            {
                var a = str.IndexOf(oldVal) + oldVal.Length;
                str = $"{str.Substring(0, a)}{str.Substring(a).Replace(oldVal, newVal)}";
            }
            return str;
        }

        /// <summary>
        /// Replaces only the first instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        public static string ReplaceFirst(string str, string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(str) && str.Contains(oldVal))
            {
                var a = str.IndexOf(oldVal);
                str = $"{str.Substring(0, a)}{newVal}{str.Substring(a + oldVal.Length)}";
            }
            return str;
        }

        /// <summary>
        /// Replaces the nth instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        /// <param name="n">The nth occurrence of the substring.</param>
        public static string ReplaceTheNth(string str, string oldVal, string newVal, int n)
        {
            var i = 0;
            for (var m = Regex.Match(str, oldVal); m.Success; m = m.NextMatch())
            {
                i++;
                if (i == n)
                {
                    return $"{str.Substring(0, m.Index)}{newVal}{str.Substring(m.Index + m.Length)}";
                }
            }
            return str;
        }

        /// <summary>
        /// Returns the index of the nth instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="substr">The substring to look for.</param>
        /// <param name="n">The nth occurrence of the substring.</param>
        public static int IndexOfNth(string str, string substr, int n)
        {
            var i = 0;
            for (var m = Regex.Match(str, substr); m.Success; m = m.NextMatch())
            {
                i++;
                if (i == n)
                {
                    return m.Index;
                }
            }
            return -1;
        }

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
                        var index = IndexOfNth(original, format.Key, i + 1);
                        str = ReplaceFirst(str, format.Key, new string('X', format.Key.Length));
                        processed.Add(new KeyValuePair<int, string>(index, format.Value));
                    }
                }
            }

            var regexList = processed.Any() ? processed.OrderBy(p => p.Key).Select(p => p.Value).ToList() : new List<string>{""};
            return $"({string.Join(string.Empty, regexList)})";
        }
    }
}
