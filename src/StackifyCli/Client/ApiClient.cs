using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StackifyCli.Client
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(ApiClientConfig config, string route);
    }

    public class ApiClient : IApiClient
    {
        private static readonly HttpClient _http = new HttpClient();

        public async Task<T> GetAsync<T>(ApiClientConfig config, string route)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{config.Host}/{route}");
            request.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", config.ApiKey);
            var response = await _http.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task PostAsync(ApiClientConfig config, string route, object content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{config.Host}/{route}");
            var json = JsonConvert.SerializeObject(content);
            request.Content = new StringContent(json);
            request.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", config.ApiKey);
            var response = await _http.SendAsync(request);

            if (response.IsSuccessStatusCode == false)
                throw new Exception($"Received {response.StatusCode} response from API.");
        }
    }
}