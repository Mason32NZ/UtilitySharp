using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public static T CleanAndConvert<T>(object str, string format = "") // TODO: Add support for number words and symbols (eg. ^,e).
        {
            T value;
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
                    var a = Regex.Replace(Regex.Match(txt, Resources.NumberRegex()).Value, "[^0-9\\.-]", string.Empty);
                    var b = string.IsNullOrWhiteSpace(a) ? 0 : Math.Round(Convert.ToDecimal(a));
                    value = (T)Convert.ChangeType(b, typeof(T));
                    break;
                }
                case ("System.UInt16"):
                case ("System.UInt32"):
                case ("System.UInt64"):
                {
                    var a = Regex.Replace(Regex.Match(txt, Resources.UnsignedNumberRegex()).Value, "[^0-9]", string.Empty);
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? 0 : Convert.ToUInt64(a), typeof(T));
                    break;
                }
                case ("System.Single"):
                case ("System.Double"):
                case ("System.Decimal"):
                {
                    var a = Regex.Replace(Regex.Match(txt, Resources.NumberRegex()).Value, "[^0-9\\.-]", string.Empty);
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? 0 : Convert.ToDecimal(a), typeof(T));
                    break;
                }
                // Number Nullable
                case ("System.Nullable`1[System.Int16]"):
                case ("System.Nullable`1[System.Int32]"):
                case ("System.Nullable`1[System.Int64]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, Resources.NumberRegex()).Value, "[^0-9\\.-]", string.Empty);
                    var b = string.IsNullOrWhiteSpace(a) ? (Decimal?) null : Math.Round(Convert.ToDecimal(a));
                    value = (T)Convert.ChangeType(b, typeof(T));
                    break;
                }
                case ("System.Nullable`1[System.UInt16]"):
                case ("System.Nullable`1[System.UInt32]"):
                case ("System.Nullable`1[System.UInt64]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, Resources.UnsignedNumberRegex()).Value, "[^0-9]", string.Empty);
                    value = (T)Convert.ChangeType(string.IsNullOrWhiteSpace(a) ? (UInt64?) null : Convert.ToUInt64(a), typeof(T));
                    break;
                }
                case ("System.Nullable`1[System.Single]"):
                case ("System.Nullable`1[System.Double]"):
                case ("System.Nullable`1[System.Decimal]"):
                {
                    var a = Regex.Replace(Regex.Match(txt, Resources.NumberRegex()).Value, "[^0-9\\.-]", string.Empty);
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
                        var regex = TimeHelper.DateTimeFormatStringToRegex(format);
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
                        var regex = TimeHelper.DateTimeFormatStringToRegex(format);
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
                    var m = Regex.Match(txt.ToUpper(), Resources.TrueFalseRegex()).Value;
                    if (Resources.TrueList().Contains(m)) { a = true; }
                    value = (T)Convert.ChangeType(a, typeof(T));
                    break;
                }
                case ("System.Nullable`1[System.Boolean]"):
                {
                    bool? a = null;
                    var m = Regex.Match(txt.ToUpper(), Resources.TrueFalseRegex()).Value;
                    if (Resources.TrueList().Contains(m)) { a = true; }
                    else if (Resources.FalseList().Contains(m)) { a = false; }
                    value = (T)Activator.CreateInstance(typeof(T), a);
                    break;
                }
                // List - Number
                case ("System.Collections.Generic.List`1[System.Int16]"):
                case ("System.Collections.Generic.List`1[System.Int32]"):
                case ("System.Collections.Generic.List`1[System.Int64]"):
                {
                    var list = new List<decimal>();
                    for (var m = Regex.Match(txt, Resources.NumberRegex()); m.Success; m = m.NextMatch())
                    {
                        var a = Regex.Replace(m.Value, "[^0-9\\.-]", string.Empty);
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
                    for (var m = Regex.Match(txt, Resources.UnsignedNumberRegex()); m.Success; m = m.NextMatch())
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
                    for (var m = Regex.Match(txt, Resources.NumberRegex()); m.Success; m = m.NextMatch())
                    {
                        var a = Regex.Replace(m.Value, "[^0-9\\.-]", string.Empty);
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
                        var regex = TimeHelper.DateTimeFormatStringToRegex(format);
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
                    for (var m = Regex.Match(txt.ToUpper(), Resources.TrueFalseRegex()); m.Success; m = m.NextMatch())
                    {
                        var a = Resources.TrueList().Contains(m.Value);
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
        /// Removes all non-ASCII compatible characters from the provided string.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="replacement">The string used to replace non-ASCII characters.</param>
        public static string StringToASCII(string str, string replacement = "")
        {
            return Encoding.ASCII.GetString(
                Encoding.Convert(
                    Encoding.UTF8, 
                    Encoding.GetEncoding(Encoding.ASCII.EncodingName, new EncoderReplacementFallback(replacement), new DecoderExceptionFallback()), 
                    Encoding.UTF8.GetBytes(str))
                );
        }

        /// <summary>
        /// Removes all non-human typable characters from the provided string.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="replacement">The string used to replace non-human typable characters.</param>
        public static string StringToHumanTypable(string str, string replacement = "") // TODO: Test.
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = StringToASCII(str, replacement);
                if (!string.IsNullOrEmpty(str))
                {
                    return Regex.Replace(str, Resources.NonHumanTypableRegex(), replacement);
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// Removes irregular characters from a filename.
        /// </summary>
        /// <param name="str">The string to be cleaned.</param>
        /// <param name="replacement">The string used to replace irregular characters.</param>
        public static string CleanFilename(string str, string replacement = "") // TODO: Test.
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegex = $@"([{invalidChars}\\]*\.+$)|([{invalidChars}\\]+)";

            return Regex.Replace(str, invalidRegex, replacement).Trim();
        }

        /// <summary>
        /// Replaces all but the first instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        /// <param name="useRegex">If oldVal should be considered as regex.</param>
        public static string ReplaceAllButFirst(string str, string oldVal, string newVal, bool useRegex = false)
        {
            return ReplaceAllButNth(str, oldVal, newVal, 1, useRegex);
        }

        /// <summary>
        /// Replaces all but the nth instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        /// <param name="n">The nth occurrence of the substring.</param>
        /// <param name="useRegex">If oldVal should be considered as regex.</param>
        public static string ReplaceAllButNth(string str, string oldVal, string newVal, int n, bool useRegex = false)
        {
            if (!useRegex)
            {
                oldVal = EscapeRegexSpecialCharacters(oldVal);
            }
            if (!string.IsNullOrEmpty(str) && Regex.IsMatch(str, oldVal))
            {
                var a = IndexOfNth(str, oldVal, n);
                var b = Regex.Match(str, oldVal).Length;
                str = $"{Regex.Replace(str.Substring(0, a), oldVal, newVal)}{str.Substring(a, b)}{Regex.Replace(str.Substring(a + b), oldVal, newVal)}";
            }
            return str;
        }

        /// <summary>
        /// Replaces only the first instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        /// <param name="useRegex">If oldVal should be considered as regex.</param>
        public static string ReplaceFirst(string str, string oldVal, string newVal, bool useRegex = false)
        {
            return ReplaceTheNth(str, oldVal, newVal, 1, useRegex);
        }

        /// <summary>
        /// Replaces the nth instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        /// <param name="n">The nth occurrence of the substring.</param>
        /// <param name="useRegex">If oldVal should be considered as regex.</param>
        public static string ReplaceTheNth(string str, string oldVal, string newVal, int n, bool useRegex = false)
        {
            if (!useRegex)
            {
                oldVal = EscapeRegexSpecialCharacters(oldVal);
            }
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
        /// <param name="useRegex">If oldVal should be considered as regex.</param>
        public static int IndexOfNth(string str, string substr, int n, bool useRegex = false)
        {
            if (!useRegex)
            {
                substr = EscapeRegexSpecialCharacters(substr);
            }
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
        /// Returns if the provided string is a vaild true or false string.
        /// </summary>
        /// <param name="str">The string to be checked.</param>
        public static bool IsTrueFalse(string str)
        {
            return Resources.TrueFalseList().Any(p => p == str.ToUpper());
        }

        /// <summary>
        /// Returns if the provided string is a vaild email string.
        /// </summary>
        /// <param name="str">The string to be checked.</param>
        public static bool IsEmail(string str)
        {
            var success = true;
            try
            {
                var email = new System.Net.Mail.MailAddress(str);
            }
            catch
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Returns if the provided string contains HTML.
        /// </summary>
        /// <param name="str">The string to be checked.</param>
        public static bool IsHtml(string str)
        {
            return Regex.IsMatch(str, Resources.HtmlTagRegex(), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Returns if the provided string is a number.
        /// </summary>
        /// <param name="str">The string to be checked.</param>
        public static bool IsNumeric(string str)
        {
            var a = Regex.Match(str, Resources.NumericRegex());
            return a.Length == str.Length;
        }

        /// <summary>
        /// Escapes all special regex characters in a string.
        /// </summary>
        /// <param name="str">The string to be escaped.</param>
        public static string EscapeRegexSpecialCharacters(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                foreach (var c in Resources.RegexSpecialCharactersList())
                {
                    if (str.Contains(c))
                    {
                        str = Regex.Replace(str, c == "\\" ? "((?<!\\\\)\\\\[^\\\\^\\[$.|?*+(){}])" : $"((?<!\\\\)\\{c})", $"\\{c}");
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// Converts a given CSV style string to a list.
        /// </summary>
        /// <param name="str">The string to be converted.</param>
        /// <param name="delimiter">The char to split the string by.</param>
        public static List<T> StringToList<T>(string str, char delimiter = ',') // TODO: Test.
        {
            return new List<T>(str.Split(delimiter).Select(p => CleanAndConvert<T>(p)));
        }

        /// <summary>
        /// Converts a given list to a CSV style string.
        /// </summary>
        /// <param name="list">The list to be converted.</param>
        /// <param name="delimiter">The string to join the list by.</param>
        public static string ListToString<T>(List<T> list, string delimiter = ",")
        {
            return string.Join(delimiter, list);
        }
    }
}
