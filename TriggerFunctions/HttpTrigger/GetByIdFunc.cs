using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp2;

public class GetByIdFunc        
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    public GetByIdFunc(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<GetByIdFunc>();

        // 🔥 Handle HTTPS localhost issue
        var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _httpClient = new HttpClient(handler);
    }

    [Function("GetStudentById")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        _logger.LogInformation("GetStudentById function triggered");

        try
        {
            // 🔹 Read ID from query
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var id = query["id"];

            if (string.IsNullOrEmpty(id))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Please provide id");
                return badResponse;
            }

            // 🔗 Call backend API
            var response = await _httpClient.GetAsync($"https://localhost:44319/api/Student/{id}");

            var data = await response.Content.ReadAsStringAsync();

            var result = req.CreateResponse(HttpStatusCode.OK);
            result.Headers.Add("Content-Type", "application/json");
            await result.WriteStringAsync(data);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");

            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("Error fetching student");

            return errorResponse;
        }
    }
}