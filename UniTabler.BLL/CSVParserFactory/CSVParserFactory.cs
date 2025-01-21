using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using UniTabler.BLL.CSVParsers;
using UniTabler.Common.Enums;
using UniTabler.Common.Interfaces.CSVParser;
using UniTabler.Common.Interfaces.CSVParserFactory;

namespace UniTabler.BLL.CSVParserFactory
{
    public class CSVParserFactory : ICSVParserFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CSVParserFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICSVParserManager CreateParser(SourceTypeEnum sourceType, string source)
        {
            var repository = _serviceProvider.GetRequiredService<ICSVParserRepository>();
            var mapper = _serviceProvider.GetRequiredService<IMapper>();

            switch (sourceType)
            {
                case SourceTypeEnum.File:
                    return new FileCSVParserManager(source, repository, mapper);

                case SourceTypeEnum.Web:
                    return new WebCSVParserManager(source, repository, mapper);

                default:
                    throw new ArgumentException($"Unknown source type: {sourceType}");
            }
        }
    }
}
