using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class StaticApiService
{
    private static readonly HttpClient _httpClient;

    // Static constructor to initialize HttpClient
    static StaticApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.example.com/")
        };

        // Set default headers if needed
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp");
    }

    public static async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public static async Task<T?> PostAsync<T>(string endpoint, object payload)
    {
        var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
