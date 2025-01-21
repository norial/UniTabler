using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using UniTabler.Common.Interfaces.CSVParser;
using UniTabler.Common.Mappers;
using UniTabler.Common.Models;
using AutoMapper;
using UniTabler.Common.DTOs;
using System.Collections.Generic;

namespace UniTabler.BLL.CSVParsers
{
    public class FileCSVParserManager : ICSVParserManager
    {
        private string _filePath;
        private readonly ICSVParserRepository _parserRepository;
        private readonly IMapper _mapper;
        private List<TripRecordModel> _parsedData;

        public FileCSVParserManager(string filePath, ICSVParserRepository parserRepository, IMapper mapper)
        {
            _filePath = filePath;
            _parserRepository = parserRepository;
            _mapper = mapper;
        }

        public List<TripRecordModel> Parse()
        {
            _parsedData = _parserRepository.Parse(_filePath);
            return _parsedData;
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
