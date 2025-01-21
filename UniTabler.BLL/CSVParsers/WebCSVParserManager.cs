using UniTabler.Common.Interfaces.CSVParser;
using UniTabler.Common.Models;
using CsvHelper;
using System.Globalization;
using System.IO;
using UniTabler.Common.DTOs;
using AutoMapper;

namespace UniTabler.BLL.CSVParsers
{
    public class WebCSVParserManager : ICSVParserManager
    {
        private readonly string _sourceFilePath;
        private List<TripRecordModel> _parsedData;
        private readonly IMapper _mapper;
        private readonly ICSVParserRepository _parserRepository;
        public WebCSVParserManager(string sourceFilePath, ICSVParserRepository parserRepository, IMapper mapper)
        {
            _sourceFilePath = sourceFilePath;
            _parserRepository = parserRepository;
            _mapper = mapper;
        }

        public List<TripRecordModel> Parse()
        {
            var records = new List<TripRecordModel>();

            using (var reader = new StreamReader(_sourceFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var csvRecords = csv.GetRecords<TripRecordModel>().ToList();
                records.AddRange(csvRecords);
            }

            return records;
        }

        public async Task SaveAsync()
        {
            await SaveAsync(_parsedData);
        }

        private async Task SaveAsync(List<TripRecordModel> parsedData)
        {
            var distinctRecords = parsedData
                .GroupBy(r => new { r.PickUpDateTime, r.DropOffDateTime, r.PassengerCount })
                .Select(g => g.First())
                .ToList();

            var duplicates = _mapper.Map<List<TripRecordDTO>>(parsedData.Except(distinctRecords).ToList());
            var records = _mapper.Map<List<TripRecordDTO>>(distinctRecords);


            await _parserRepository.SaveAsync(records, duplicates);
        }
    }
}