using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTabler.Common.DTOs;
using UniTabler.Common.Models;

namespace UniTabler.Common.Interfaces.CSVParser
{
    public interface ICSVParserRepository
    {
        List<TripRecordModel> Parse(string filePath);
        Task SaveAsync(List<TripRecordDTO> records, List<TripRecordDTO> dublicateData);
    }
}
