using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System.Globalization;

namespace UniTabler.Utils
{
    public class EstToUtcDateTimeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            string[] formats = { "MM/dd/yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt" };
            var culture = CultureInfo.InvariantCulture;

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(text, format, culture, DateTimeStyles.None, out var estDateTime))
                {
                    TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    return TimeZoneInfo.ConvertTimeToUtc(estDateTime, estZone);
                }
            }

            throw new Exception($"Unable to convert '{text}' to DateTime using the provided formats.");
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
