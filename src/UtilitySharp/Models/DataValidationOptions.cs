using System.Collections.Generic;

namespace UtilitySharp.Models
{
    public class DataValidationOptions
    {
        public DataValidationOptions()
        {
            Delimiter = ',';
            ReportAllErrors = false;
            Columns = new List<Column>();
        }

        public char Delimiter { get; set; }
        public bool ReportAllErrors { get; set; }
        public List<Column> Columns { get; set; }

        public enum Type
        {
            String,
            Boolean,
            Int16,
            Int32,
            Int64,
            Single,
            Double,
            Decimal,
            UInt16,
            UInt32,
            UInt64,
            DateTime,
            TimeSpan,
            Guid,
            ByteArray
        }

        public enum SpecialType
        {
            Email,
            PhoneNumber
        }

        public class Column
        {
            public Column(string header)
            {
                Header = header;
                IsNullable = true;
                WhiteListValues = new List<string>();
                BlackListValues = new List<string>();
            }

            public string Header { get; set; }
            public int? Index { get; set; }
            public Type? Type { get; set; }
            public SpecialType? SpecialType { get; set; }
            public bool IsNullable { get; set; }
            public string RegexPattern { get; set; }
            public string Format { get; set; }
            public List<string> WhiteListValues { get; set; }
            public List<string> BlackListValues { get; set; }
            public string MinValue { get; set; }
            public string MaxValue { get; set; }
            public int? MinLength { get; set; }
            public int? MaxLength { get; set; }
        }
    }
}
