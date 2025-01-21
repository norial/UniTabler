using UniTabler.Common.Models;

namespace UniTabler.Common.Interfaces.CSVParser
{
    public interface ICSVParserManager
    {
        List<TripRecordModel> Parse();

        Task SaveAsync();
    }
}
