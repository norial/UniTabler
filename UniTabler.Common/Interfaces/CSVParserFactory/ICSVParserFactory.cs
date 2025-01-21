using UniTabler.Common.Enums;
using UniTabler.Common.Interfaces.CSVParser;

namespace UniTabler.Common.Interfaces.CSVParserFactory
{
    public interface ICSVParserFactory
    {
        ICSVParserManager CreateParser(SourceTypeEnum sourceType, string source);
    }
}
