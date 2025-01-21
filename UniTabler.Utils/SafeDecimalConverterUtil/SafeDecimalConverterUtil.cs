using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace UniTabler.Utils
{
    public class SafeDecimalConverterUtil : ITypeConverter
    {
        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            try
            {
                var cultureInfo = new CultureInfo("en-US");
                cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

                decimal.TryParse(text, cultureInfo, out var result);
                return result;
            }
            catch
            {
                throw new InvalidCastException();
            }
        }

        public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            return value.ToString();
        }
    }
}
