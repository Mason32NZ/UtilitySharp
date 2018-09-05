using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitySharp.Models
{
    public class DataValidationError
    {
        public int? Row { get; set; }
        public int? Column { get; set; }
        public string Message { get; set; }
    }
}
