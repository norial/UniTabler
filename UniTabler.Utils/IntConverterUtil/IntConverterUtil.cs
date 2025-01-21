using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using static System.Net.Mime.MediaTypeNames;

namespace UniTabler.Utils
{
    public class IntConverterUtil : ITypeConverter
    {
        public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            try
            {
            int.TryParse(text, out var result);
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
