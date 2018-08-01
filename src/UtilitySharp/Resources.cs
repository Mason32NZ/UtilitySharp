using System.Collections.Generic;

namespace UtilitySharp
{
    /// <summary>
    /// A static class that contains useful enums, maps, and data.
    /// </summary>
    public static class Resources
    {
        /// <summary>
        /// A map of DateTime format segments and their corresponding regex strings.
        /// </summary>
        public static List<KeyValuePair<string, string>> DateTimeFormatStringToRegexMap()
        {
            var map = new List<KeyValuePair<string, string>> // NOTE: The order of this list is important, DO NOT change!
            {
                new KeyValuePair<string, string>( " ", "\\s" ),
                new KeyValuePair<string, string>( "/", "[/.-]" ),
                new KeyValuePair<string, string>( ":", "[:.]" ),
                new KeyValuePair<string, string>( "zzz", "[+-][0-9]{2}:[0-9]{2}" ),
                new KeyValuePair<string, string>( "zz", "[+-][0-9]{2}" ),
                new KeyValuePair<string, string>( "z", "[+-][0-9]" ),
                new KeyValuePair<string, string>( "yyyy", "[0-9]{4}" ),
                new KeyValuePair<string, string>( "yyy", "[0-9]{3,4}" ),
                new KeyValuePair<string, string>( "yy", "[0-9]{2}" ),
                new KeyValuePair<string, string>( "y", "([1-9][0-9]|[0-9])" ),
                new KeyValuePair<string, string>( "tt", "(PM|AM)" ),
                new KeyValuePair<string, string>( "t", "[PA]" ),
                new KeyValuePair<string, string>( "ss", "[0-5][0-9]" ),
                new KeyValuePair<string, string>( "s", "([1-5][0-9]|[0-9])" ),
                new KeyValuePair<string, string>( "MMMM", "([A-Za-z]{3,9})?" ),
                new KeyValuePair<string, string>( "MMM", "([A-Za-z]{3})?" ),
                new KeyValuePair<string, string>( "MM", "(1[0-2]|0[1-9])" ),
                new KeyValuePair<string, string>( "M", "(1[0-2]|[1-9])" ),
                new KeyValuePair<string, string>( "mm", "[0-5][0-9]" ),
                new KeyValuePair<string, string>( "m", "([1-5][0-9]|[0-9])" ),
                new KeyValuePair<string, string>( "K", "[+-][0-9]{2}:[0-9]{2}" ),
                new KeyValuePair<string, string>( "HH", "(2[0-3]|1[0-9]|0[0-9])" ),
                new KeyValuePair<string, string>( "H", "(2[0-3]|1[0-9]|[0-9])" ),
                new KeyValuePair<string, string>( "hh", "(1[0-2]|0[1-9])" ),
                new KeyValuePair<string, string>( "h", "(1[0-2]|[1-9])" ),
                new KeyValuePair<string, string>( "gg", "[A-B].?[C-D].?" ),
                new KeyValuePair<string, string>( "g", "[A-B].?[C-D].?" ),
                new KeyValuePair<string, string>( "FFFFFFF", "[1-9][0-9]{5}[1-9]" ),
                new KeyValuePair<string, string>( "FFFFFF", "[1-9][0-9]{4}[1-9]" ),
                new KeyValuePair<string, string>( "FFFFF", "[1-9][0-9]{3}[1-9]" ),
                new KeyValuePair<string, string>( "FFFF", "[1-9][0-9]{2}[1-9]" ),
                new KeyValuePair<string, string>( "FFF", "[1-9][0-9][1-9]" ),
                new KeyValuePair<string, string>( "FF", "[1-9]{2}" ),
                new KeyValuePair<string, string>( "F", "[1-9]" ),
                new KeyValuePair<string, string>( "fffffff", "[0-9]{7}" ),
                new KeyValuePair<string, string>( "ffffff", "[0-9]{6}" ),
                new KeyValuePair<string, string>( "fffff", "[0-9]{5}" ),
                new KeyValuePair<string, string>( "ffff", "[0-9]{4}" ),
                new KeyValuePair<string, string>( "fff", "[0-9]{3}" ),
                new KeyValuePair<string, string>( "ff", "[0-9]{2}" ),
                new KeyValuePair<string, string>( "f", "[0-9]" ),
                new KeyValuePair<string, string>( "dddd", "[A-Za-z]{6,9}" ),
                new KeyValuePair<string, string>( "ddd", "[A-Za-z]{3}" ),
                new KeyValuePair<string, string>( "dd", "[0-3][0-9]" ),
                new KeyValuePair<string, string>( "d", "([1-3][0-9]|[1-9])")
            };
            return map;
        }
    }
}
