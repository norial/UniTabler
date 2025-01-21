using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniTabler.BLL.CSVParserFactory;
using UniTabler.Common;
using UniTabler.Common.Enums;
using UniTabler.Common.Interfaces.CSVParserFactory;
using UniTabler.Common.Interfaces.RideInformation;

namespace UniTabler.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.StartAsync();

            IRideInformationManager rideInformation;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var factory = services.GetRequiredService<ICSVParserFactory>();

                    string path = @"D:\CSVData\sample-cab-data.csv";

                    var csvParser = factory.CreateParser(SourceTypeEnum.File, path);

                    var records = csvParser.Parse();

                    csvParser.SaveAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                rideInformation = services.GetRequiredService<IRideInformationManager>();
            }

            await RunConsoleManager(rideInformation);

            await host.StopAsync();
        }

        private static async Task RunConsoleManager(IRideInformationManager rideInformation)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Console manager for working with travel data:");
                Console.WriteLine("1. Find the PULocationID with the highest average tip_amount value");
                Console.WriteLine("2. Find trips by PickUpLocationId");
                Console.WriteLine("3. Find the top 100 longest journeys by distance");
                Console.WriteLine("4. Find the top 100 longest trips in terms of time");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await GetPickUpLocationByAverageTipAmount(rideInformation);
                        break;

                    case "2":
                        await GetRideByPickUpId(rideInformation);
                        break;

                    case "3":
                        await GetTopLongestFaresByDistance(rideInformation);
                        break;

                    case "4":
                        await GetTopLongestFaresByTime(rideInformation);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Wrong choice. Try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static async Task GetPickUpLocationByAverageTipAmount(IRideInformationManager rideInformation)
        {
            var result = await rideInformation.GetPickUpLocationByAverageTipAmount();

            if (result != null)
            {
                Console.WriteLine($"PULocationID: {result.PickUpLocationId}, Average Tip: {result.TipAmount}");
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }

        private static async Task GetRideByPickUpId(IRideInformationManager rideInformation)
        {
            Console.Write("Enter PULocationId: ");
            if (int.TryParse(Console.ReadLine(), out int pickUpId))
            {
                var result = await rideInformation.GetRideByPickUpId(pickUpId);

                if (result.Count > 0)
                {
                    foreach (var record in result)
                    {
                        Console.WriteLine($"PickUpLocationId: {record.PickUpLocationId}, DropOffLocationId: {record.DropOffLocationId}, " +
                                          $"Start date and time: {record.PickUpDateTime}, End date and time: {record.DropOffDateTime}, " +
                                          $"Number of passengers: {record.PassengerCount}, Distance: {record.TripDistance}, " +
                                          $"FareAmount: {record.FareAmount}, TipAmount: {record.TipAmount}");
                    }
                }
                else
                {
                    Console.WriteLine("No trips were found for this PickUpLocationId.");
                }
            }
            else
            {
                Console.WriteLine("Invalid PULocationId format.");
            }
        }

        private static async Task GetTopLongestFaresByDistance(IRideInformationManager rideInformation)
        {
            var result = await rideInformation.GetTopLongestFaresByDistance();

            if (result.Count > 0)
            {
                foreach (var record in result)
                {
                    Console.WriteLine($"PickUpLocationId: {record.PickUpLocationId}, DropOffLocationId: {record.DropOffLocationId}, " +
                                      $"Distance: {record.TripDistance} km, StartDateTime: {record.PickUpDateTime}, " +
                                      $"End date and time: {record.DropOffDateTime}");
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }

        private static async Task GetTopLongestFaresByTime(IRideInformationManager rideInformation)
        {
            var result = await rideInformation.GetTopLongestFaresByTime();

            if (result.Count > 0)
            {
                foreach (var record in result)
                {
                    Console.WriteLine($"PickUpLocationId: {record.PickUpLocationId}, DropOffLocationId: {record.DropOffLocationId}, " +
                                      $"Duration: {record.DropOffDateTime - record.PickUpDateTime}, Start Date and Time: {record.PickUpDateTime}, " +
                                      $"End Date and Time: {record.DropOffDateTime}");
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddProjectServices();
                });
    }
}