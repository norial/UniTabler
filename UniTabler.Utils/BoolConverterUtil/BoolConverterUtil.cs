using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace UniTabler.Utils
{
    public class BoolConverterUtil : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text?.ToLower() == "n" ? false : true;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value is bool boolValue && boolValue == false ? "n" : "y";
        }
    }
}
