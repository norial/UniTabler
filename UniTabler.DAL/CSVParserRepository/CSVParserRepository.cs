using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;
using UniTabler.Common.DTOs;
using UniTabler.Common.Interfaces.CSVParser;
using UniTabler.Common.Mappers;
using UniTabler.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace UniTabler.DAL.CSVParserRepository
{
    public class CSVParserRepository : ICSVParserRepository
    {
        private const string _connectionString = "Server=localhost;Database=UniTabler;Integrated Security=True;TrustServerCertificate=True;";

        public List<TripRecordModel> Parse(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<TripRecordMap>();
                var records = csv.GetRecords<TripRecordModel>().ToList();
                return records;
            }
        }

        public async Task SaveAsync(List<TripRecordDTO> records, List<TripRecordDTO> dublicates)
        {
            var dublicateData = dublicates.Select(dto => $"{dto.PickUpDateTime}," +
                                                        $"{dto.DropOffDateTime}," +
                                                        $"{dto.PassengerCount}," +
                                                        $"{dto.TripDistance}," +
                                                        $"{(dto.StoreAndForwardFlag == false ? "n" : "y")}," +
                                                        $"{dto.PickUpLocationId}," +
                                                        $"{dto.DropOffLocationId}," +
                                                        $"{dto.FareAmount}," +
                                                        $"{dto.TipAmount}").ToList();

            await File.WriteAllTextAsync("duplicates.csv", string.Join(Environment.NewLine, dublicateData));
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();
                    using (var bulkCopy = new SqlBulkCopy(_connectionString))
                    {
                        bulkCopy.DestinationTableName = "TripRecords";

                        var table = new DataTable();
                        table.Columns.Add("tpep_pickup_datetime", typeof(DateTime));
                        table.Columns.Add("tpep_dropoff_datetime", typeof(DateTime));
                        table.Columns.Add("passenger_count", typeof(int));
                        table.Columns.Add("trip_distance", typeof(float));
                        table.Columns.Add("store_and_fwd_flag", typeof(string));
                        table.Columns.Add("PULocationID", typeof(int));
                        table.Columns.Add("DOLocationID", typeof(int));
                        table.Columns.Add("fare_amount", typeof(decimal));
                        table.Columns.Add("tip_amount", typeof(decimal));

                        foreach (var record in records)
                        {
                            table.Rows.Add(
                                record.PickUpDateTime,
                                record.DropOffDateTime,
                                record.PassengerCount,
                                record.TripDistance,
                                record.StoreAndForwardFlag ? "Yes" : "No",
                                record.PickUpLocationId,
                                record.DropOffLocationId,
                                record.FareAmount,
                                record.TipAmount
                            );
                        }

                        bulkCopy.WriteToServer(table);
                    }
                }
            }
            catch (Exception ex) { }

            Console.WriteLine($"Inserted {records.Count} records into the database.");
        }
    }
}
