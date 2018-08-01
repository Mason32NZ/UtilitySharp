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
        /// <param name="format">A formatting string used for DateTime.ParseExact(). REQUIRED if converting to DateTime.</param>
        public static T CleanAndConvert<T>(object str, string format = "")
        {
            T value = default(T);
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
                    value = (T)Convert.ChangeType(b, typeof(T));
                    break;
                }
                case ("System.UInt16"):
                case ("System.UInt32"):
                case ("System.UInt64"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "([0-9]([0-9,]+)?)").Value, "[^0-9]", string.Empty);
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? 0 : Convert.ToUInt64(a), typeof(T));
                    break;
                }
                case ("System.Single"):
                case ("System.Double"):
                case ("System.Decimal"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)").Value, "[^0-9.-]", string.Empty);
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? 0 : Convert.ToDecimal(a), typeof(T));
                    break;
                }
                // Number Nullable
                case ("System.Nullable`1[System.Int16]"):
                case ("System.Nullable`1[System.Int32]"):
                case ("System.Nullable`1[System.Int64]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)").Value, "[^0-9.-]", string.Empty);
                    var b = string.IsNullOrWhiteSpace(a) ? (Decimal?) null : Math.Round(Convert.ToDecimal(a));
                    value = (T)Convert.ChangeType(b, typeof(T));
                    break;
                }
                case ("System.Nullable`1[System.UInt16]"):
                case ("System.Nullable`1[System.UInt32]"):
                case ("System.Nullable`1[System.UInt64]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "([0-9]([0-9,]+)?)").Value, "[^0-9]", string.Empty);
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? (UInt64?) null : Convert.ToUInt64(a), typeof(T));
                    break;
                }
                case ("System.Nullable`1[System.Single]"):
                case ("System.Nullable`1[System.Double]"):
                case ("System.Nullable`1[System.Decimal]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)").Value, "[^0-9.-]", string.Empty);
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? (Decimal?) null : Convert.ToDecimal(a), typeof(T));
                    break;
                }
                // Other
                case ("System.String"):
                {
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(txt) ? null : txt, typeof(T));
                    break;
                }
                case ("System.DateTime"):
                {
                    if (format != "")
                    {
                        var regex = DateTimeFormatStringToRegex(format);
                        txt = Regex.Match(txt, regex).Value;
                        var a = string.IsNullOrWhiteSpace(txt) ? DateTime.MinValue : DateTime.ParseExact(txt, format, null);
                        value = (T)Convert.ChangeType(a, typeof(T));
                    }
                    else
                    {
                        throw new Exception($"'format' must be provided for DateTime!");
                    }
                    break;
                }
                case ("System.Nullable`1[System.DateTime]"):
                {
                    if (format != "")
                    {
                        var regex = DateTimeFormatStringToRegex(format);
                        txt = Regex.Match(txt, regex).Value;
                        var a = string.IsNullOrWhiteSpace(txt) ? (DateTime?)null : DateTime.ParseExact(txt, format, null);
                        value = (T)Convert.ChangeType(a, typeof(T));
                    }
                    else
                    {
                        throw new Exception($"'format' must be provided for DateTime!");
                    }
                    break;
                }
                case ("System.Boolean"):
                {
                    bool a = false;
                    var _true = new List<string> { "TRUE", "T", "YES", "Y", "1" };
                    var m = Regex.Match(txt.ToUpper(), "(TRUE|T|YES|Y|1|FALSE|F|NO|N|0)").Value;
                    if (_true.Contains(m)) { a = true; }
                    value = (T)Convert.ChangeType(a, typeof(T));
                    break;
                }
                case ("System.Nullable`1[System.Boolean]"):
                {
                    bool? a = null;
                    var _true = new List<string> {"TRUE", "T", "YES", "Y", "1"};
                    var _false = new List<string> {"FALSE", "F", "NO", "N", "0"};
                    var m = Regex.Match(txt.ToUpper(), "(TRUE|T|YES|Y|1|FALSE|F|NO|N|0)").Value;
                    if (_true.Contains(m)) { a = true; }
                    else if (_false.Contains(m)) { a = false; }
                    value = (T)Activator.CreateInstance(typeof(T), a);
                    break;
                }
                // List - Number
                case ("System.Collections.Generic.List`1[System.Int16]"):
                case ("System.Collections.Generic.List`1[System.Int32]"):
                case ("System.Collections.Generic.List`1[System.Int64]"):
                {
                    var list = new List<decimal>();
                    for (var m = Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)"); m.Success; m = m.NextMatch())
                    {
                        var a = Regex.Replace(m.Value, "[^0-9.-]", string.Empty);
                        if (!string.IsNullOrWhiteSpace(a))
                        {
                            var b = Math.Round(Convert.ToDecimal(a));
                            list.Add(b);
                        }
                    }
                    value = (T)Convert.ChangeType(list, typeof(T));
                    break;
                }
                case ("System.Collections.Generic.List`1[System.UInt16]"):
                case ("System.Collections.Generic.List`1[System.UInt32]"):
                case ("System.Collections.Generic.List`1[System.UInt64]"):
                {
                    var list = new List<ulong>();
                    for (var m = Regex.Match(txt, "([0-9]([0-9,]+)?)"); m.Success; m = m.NextMatch())
                    {
                        var a = Regex.Replace(m.Value, "[^0-9]", string.Empty);
                        if (!string.IsNullOrWhiteSpace(a))
                        {
                            var b = Convert.ToUInt64(a);
                            list.Add(b);
                        }
                    }
                    value = (T)Convert.ChangeType(list, typeof(T));
                    break;
                }
                case ("System.Collections.Generic.List`1[System.Single]"):
                case ("System.Collections.Generic.List`1[System.Double]"):
                case ("System.Collections.Generic.List`1[System.Decimal]"):
                {
                    var list = new List<decimal>();
                    for (var m = Regex.Match(txt, "(-?[0-9]([0-9,]+)?.?([0-9]+)?)"); m.Success; m = m.NextMatch())
                    {
                        var a = Regex.Replace(m.Value, "[^0-9.-]", string.Empty);
                        if (!string.IsNullOrWhiteSpace(a))
                        {
                            var b = Convert.ToDecimal(a);
                            list.Add(b);
                        }
                    }
                    value = (T)Convert.ChangeType(list, typeof(T));
                    break;
                }
                // List - Other
                case ("System.Collections.Generic.List`1[System.DateTime]"):
                {
                    if (format != "")
                    {
                        var list = new List<DateTime>();
                        var regex = DateTimeFormatStringToRegex(format);
                        for (var m = Regex.Match(txt, regex); m.Success; m = m.NextMatch())
                        {
                            var a = DateTime.ParseExact(m.Value, format, null);
                            list.Add(a);
                        }
                        value = (T)Convert.ChangeType(list, typeof(T));
                    }
                    else
                    {
                        throw new Exception($"'format' must be provided for DateTime!");
                    }
                    break;
                }
                case ("System.Collections.Generic.List`1[System.Boolean]"):
                {
                    var list = new List<bool>();
                    var _true = new List<string> { "TRUE", "T", "YES", "Y", "1" };
                    for (var m = Regex.Match(txt.ToUpper(), "(TRUE|T|YES|Y|1|FALSE|F|NO|N|0)"); m.Success; m = m.NextMatch())
                    {
                        var a = _true.Contains(m.Value);
                        list.Add(a);
                    }
                    value = (T)Convert.ChangeType(list, typeof(T));
                    break;
                }
                default:
                {
                    throw new Exception($"{type} is not supported!");
                }
            }

            return value;
        }

        /// <summary>
        /// Replaces all but the first instance of a substring. Supports regex.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        public static string ReplaceAllButFirst(string str, string oldVal, string newVal)
        {
            if (!string.IsNullOrEmpty(str) && str.Contains(oldVal))
            {
                var a = IndexOfNth(str, oldVal, 1) + oldVal.Length;
                str = $"{str.Substring(0, a)}{Regex.Replace(str.Substring(a), oldVal, newVal)}";
            }
            return str;
        }

        /// <summary>
        /// Replaces only the first instance of a substring. Supports regex.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        public static string ReplaceFirst(string str, string oldVal, string newVal)
        {
            return ReplaceTheNth(str, oldVal, newVal, 1);
        }

        /// <summary>
        /// Replaces the nth instance of a substring. Supports regex.
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
        /// Returns the index of the nth instance of a substring. Supports regex.
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
    }
}
