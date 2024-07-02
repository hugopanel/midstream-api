using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PredictionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<string>> GetRecommendedModulesAsync(string description)
        {
            var baseUrl = _configuration["FlaskApi:BaseUrl"];
            var endpoint = _configuration["FlaskApi:Endpoint"];

            var requestData = new { description = description };
            var jsonString = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var apiUrl = new Uri(new Uri(baseUrl ?? throw new ArgumentNullException(nameof(baseUrl))), endpoint).ToString();

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error calling the Flask API: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Response from Flask API: " + jsonResponse);
            var result = JsonSerializer.Deserialize<PredictionResult>(jsonResponse);
            
            Console.WriteLine("Predicted labels: " + string.Join(", ", result.PredictedLabels));

            if (result == null || result.PredictedLabels == null)
            {
                throw new Exception("Invalid response from Flask API");
            }

            return result.PredictedLabels;
        }

        private class PredictionResult
        {
            public required List<string> PredictedLabels { get; set; }
        }
    }
}
