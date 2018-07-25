﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UtilitySharp
{
    public class StringHelper
    {
        /// <summary>
        /// Cleans str and converts it to the provided type.
        /// </summary>
        /// <param name="str">The string or object that supports .ToString() to be cleaned and converted.</param>
        /// <param name="regex">A regex string used to either remove unwanted text or select the desired text. Please NOTE that this method already uses [^0-9.] for numeric convertions.</param>
        /// <param name="action">An enum used to dictate what the regex string should do. It is RECOMMENDED that you use regex to select the exact substring you need, to reduce noise.</param>
        /// <param name="format">A formatting string used for DateTime.ParseExact(). RECOMMENDED if converting to DateTime.</param>
        public static T CleanAndConvert<T>(object str, string regex = "", Enums.RegexAction action = Enums.RegexAction.Select, string format = "")
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
                throw new Exception(str.GetType() + " does not support .ToString()!");
            }

            // Apply regex.
            if (!string.IsNullOrWhiteSpace(txt))
            {
                if (!string.IsNullOrWhiteSpace(regex))
                {
                    switch (action)
                    {
                        case Enums.RegexAction.Select:
                            txt = Regex.Match(txt, regex).Value;
                            break;
                        case Enums.RegexAction.Remove:
                            txt = Regex.Replace(txt, regex, string.Empty);
                            break;
                        case Enums.RegexAction.NonRegexSelect:
                            txt = regex;
                            break;
                        case Enums.RegexAction.NonRegexRemove:
                            txt = txt.Replace(regex, string.Empty);
                            break;
                    }
                }

                // Apply other string cleaning.
                if (!string.IsNullOrWhiteSpace(txt))
                {
                    txt = txt.Trim();
                }
            }

            switch (type)
            {
                // Number
                case ("System.Int16"):
                case ("System.Int32"):
                case ("System.Int64"):
                    {
                        var a = Regex.Replace(txt, "[^0-9]", string.Empty);
                        var b = string.IsNullOrWhiteSpace(a);
                        return (T)Convert.ChangeType(b ? 0 : Convert.ToInt64(a), typeof(T));
                    }
                case ("System.UInt16"):
                case ("System.UInt32"):
                case ("System.UInt64"):
                    {
                        var a = Regex.Replace(txt, "[^0-9]", string.Empty);
                        var b = string.IsNullOrWhiteSpace(a);
                        return (T)Convert.ChangeType(b ? 0 : Convert.ToUInt64(a), typeof(T));
                    }
                case ("System.Single"):
                case ("System.Double"):
                case ("System.Decimal"):
                    {
                        var a = Regex.Replace(txt, "[^0-9.]", string.Empty);
                        var b = string.IsNullOrWhiteSpace(a);
                        return (T)Convert.ChangeType(b ? 0 : Convert.ToDecimal(a), typeof(T));
                    }
                // Number Nullable
                case ("System.Nullable`1[System.Int16]"):
                case ("System.Nullable`1[System.Int32]"):
                case ("System.Nullable`1[System.Int64]"):
                    {
                        var a = Regex.Replace(txt, "[^0-9]", string.Empty);
                        var b = string.IsNullOrWhiteSpace(a);
                        return (T)Convert.ChangeType(b ? (long?)null : Convert.ToInt64(a), typeof(T));
                    }
                case ("System.Nullable`1[System.UInt16]"):
                case ("System.Nullable`1[System.UInt32]"):
                case ("System.Nullable`1[System.UInt64]"):
                {
                    var a = Regex.Replace(txt, "[^0-9]", string.Empty);
                    var b = string.IsNullOrWhiteSpace(a);
                    return (T)Convert.ChangeType(b ? (ulong?)null : Convert.ToUInt64(a), typeof(T));
                }
                case ("System.Nullable`1[System.Single]"):
                case ("System.Nullable`1[System.Double]"):
                case ("System.Nullable`1[System.Decimal]"):
                    {
                        var a = Regex.Replace(txt, "[^0-9.]", string.Empty);
                        var b = string.IsNullOrWhiteSpace(a);
                        return (T)Convert.ChangeType(b ? (decimal?)null : Convert.ToDecimal(a), typeof(T));
                    }
                // Other
                case ("System.String"):
                    {
                        return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(txt) ? null : txt, typeof(T));
                    }
                case ("System.DateTime"): // Currently only supports clean strings, it is recommended to use 'regex'.
                    {
                        var a = format != "" ? DateTime.ParseExact(txt, format, null) : DateTime.Parse(txt);
                        return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(txt) ? DateTime.MinValue : a, typeof(T));
                    }
                case ("System.Nullable`1[System.DateTime]"): // Currently only supports clean strings, it is recommended to use 'regex'.
                    {
                        var a = format != "" ? DateTime.ParseExact(txt, format, null) : DateTime.Parse(txt);
                        return (T)Convert.ChangeType(string.IsNullOrWhiteSpace(txt) ? (DateTime?)null : a, typeof(T));
                    }
                case ("System.Boolean"):
                    {
                        var _true = new List<string> { "TRUE", "T", "1", "Y", "YES" };
                        bool value = _true.Any(p => txt.ToUpper().Contains(p));

                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                case ("System.Nullable`1[System.Boolean]"):
                    {
                        var _true = new List<string> { "TRUE", "T", "1", "Y", "YES" };
                        var _false = new List<string> { "FALSE", "F", "0", "N", "NO" };
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
                        throw new Exception(type + " is not supported!");
                    }
            }
        }

        /// <summary>
        /// Replaces all but the first instance of a substring.
        /// </summary>
        /// <param name="str">The original string.</param>
        /// <param name="oldVal">The substring to be replaced.</param>
        /// <param name="newVal">The new substring.</param>
        public static string ReplaceAllButFirst(string str, string oldVal, string newVal = "")
        {
            string a = str;
            if (!string.IsNullOrEmpty(str) && str.Contains(oldVal))
            {
                var b = str.IndexOf(oldVal) + oldVal.Length;
                a = str.Substring(0, b) + str.Substring(b).Replace(oldVal, newVal);
            }
            return a;
        }
    }
}
