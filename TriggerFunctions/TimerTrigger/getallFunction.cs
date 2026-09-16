using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
namespace FunctionApp2;

public class getallFunction
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    public getallFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<getallFunction>();
        _httpClient = new HttpClient();
    }

    [Function("GetStudentsTimer")]
    public async Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"Timer triggered at: {DateTime.Now}");

        try
        {
            var response = await _httpClient.GetAsync("https://localhost:44319/api/Student");
            var data = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Student Data: {data}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Message: {ex.Message}");
            _logger.LogError($"Full Error: {ex}");
        }
    }
}