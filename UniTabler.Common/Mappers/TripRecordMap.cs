using CsvHelper.Configuration;
using UniTabler.Common.Models;
using UniTabler.Utils;

namespace UniTabler.Common.Mappers
{
    public class TripRecordMap : ClassMap<TripRecordModel>
    {
        public TripRecordMap()
        {
            Map(m => m.PickUpDateTime)
                .Name("tpep_pickup_datetime")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<EstToUtcDateTimeConverter>();
            Map(m => m.DropOffDateTime)
                .Name("tpep_dropoff_datetime")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<EstToUtcDateTimeConverter>();
            Map(m => m.PassengerCount)
                .Name("passenger_count")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<IntConverterUtil>();
            Map(m => m.TripDistance)
                .Name("trip_distance")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<SafeDoubleConverterUtil>();
            Map(m => m.StoreAndForwardFlag)
                .Name("store_and_fwd_flag")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<BoolConverterUtil>();
            Map(m => m.PickUpLocationId)
                .Name("PULocationID")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<IntConverterUtil>();
            Map(m => m.DropOffLocationId)
                .Name("DOLocationID")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<IntConverterUtil>();
            Map(m => m.FareAmount)
                .Name("fare_amount")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<SafeDecimalConverterUtil>();
            Map(m => m.TipAmount)
                .Name("tip_amount")
                .TypeConverter<TrimmedStringConverterUtil>()
                .TypeConverter<SafeDecimalConverterUtil>();
        }
    }
}
