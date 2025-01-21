using UniTabler.BLL.CSVParserFactory;
using UniTabler.Common.Interfaces.CSVParser;
using UniTabler.Common.Interfaces.CSVParserFactory;
using UniTabler.Common.Mappers;
using UniTabler.BLL.CSVParsers;
using UniTabler.BLL.RideInformation;
using UniTabler.Common.Interfaces.RideInformation;
using UniTabler.DAL.CSVParserRepository;

namespace UniTabler.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfig));

            services.AddHttpClient();

            services.AddScoped<ICSVParserFactory, CSVParserFactory>();
            services.AddScoped<ICSVParserRepository, CSVParserRepository>();
            services.AddScoped<IRideInformationManager, RideInformationManager>();
            services.AddScoped<IRideInformationRepository, RideInformationRepository>();


            return services;
        }
    }
}
