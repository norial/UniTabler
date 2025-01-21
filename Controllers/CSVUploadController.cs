using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using UniTabler.BLL.CSVParserFactory;
using UniTabler.Common.Enums;
using UniTabler.Common.Interfaces.CSVParserFactory;

public class CsvUploadController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceProvider _serviceProvider;

    public CsvUploadController(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
    {
        _httpClientFactory = httpClientFactory;
        _serviceProvider = serviceProvider;
    }

    [HttpPost("upload-csv-from-url")]
    public async Task<IActionResult> UploadCsvFromUrl([FromBody] UploadRequest request)
    {
        if (string.IsNullOrEmpty(request.CsvUrl))
        {
            return BadRequest("CSV URL is required.");
        }

        try
        {
            string downloadLink = GoogleDriveHelper.GetDownloadLink(request.CsvUrl);
            Console.WriteLine("Ссылка для скачивания: " + downloadLink);

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(downloadLink);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500, "Failed to download the CSV file.");
            }

            var fileContent = await response.Content.ReadAsStringAsync();

            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var tempFilePath = Path.Combine(uploadsDirectory, "temp.csv");
            await System.IO.File.WriteAllTextAsync(tempFilePath, fileContent);

            var csvParserFactory = _serviceProvider.GetRequiredService<ICSVParserFactory>();
            var csvParser = csvParserFactory.CreateParser(SourceTypeEnum.File, tempFilePath);

            var records = csvParser.Parse();
            await csvParser.SaveAsync();

            return Ok(new { Message = "File uploaded and processed successfully.", FilePath = tempFilePath });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    public class UploadRequest
    {
        public string CsvUrl { get; set; }
    }
}