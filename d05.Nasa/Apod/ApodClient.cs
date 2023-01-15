using d05.Nasa.Apod.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace d05.Nasa.Apod
{
    public class ApodClient : INasaClient<int, Task<MediaOfToday[]>>
    {
        const string _apodUrl = "https://api.nasa.gov/planetary/apod";
        private string _apiKey;
        private HttpClient _httpClient;

        public ApodClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("wrong api key");
            }

            _apiKey = apiKey;
            _httpClient = new();
        }

        public async Task<MediaOfToday[]> GetAsync(int elementsCount = 1)
        {
            string apiUrl = $"{_apodUrl}?api_key={_apiKey}&count={elementsCount}";

            var result = await _httpClient.GetAsync(apiUrl);

            var content = await result.Content.ReadAsStringAsync();

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"GET “{apiUrl}” returned {result.StatusCode}:\n{content}");
            }

            MediaOfToday[] mediasOfToday = JsonSerializer.Deserialize<MediaOfToday[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return mediasOfToday;
        }
    }
}