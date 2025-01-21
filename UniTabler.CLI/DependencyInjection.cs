using UniTabler.Common.Interfaces.CSVParser;
using UniTabler.Common.Interfaces.CSVParserFactory;
using UniTabler.Common.Mappers;
using Microsoft.Extensions.DependencyInjection;
using UniTabler.BLL.CSVParserFactory;
using UniTabler.BLL.CSVParsers;
using UniTabler.DAL.CSVParserRepository;
using UniTabler.Common.Interfaces.RideInformation;
using UniTabler.BLL.RideInformation;

namespace UniTabler.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfig));

            services.AddScoped<ICSVParserFactory, CSVParserFactory>();
            services.AddScoped<ICSVParserManager, FileCSVParserManager>();
            services.AddScoped<ICSVParserRepository, CSVParserRepository>();
            services.AddScoped<IRideInformationManager, RideInformationManager>();
            services.AddScoped<IRideInformationRepository, RideInformationRepository>();

            return services;
        }
    }
}
