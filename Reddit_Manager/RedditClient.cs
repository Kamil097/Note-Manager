using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Manager
{
    public class RedditClient
    {
        private readonly HttpClient _httpClient;

        public RedditClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAccessTokenAsync(string username, string password, string clientId, string clientSecret)
        {
            var authHeaderValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.reddit.com/api/v1/access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            request.Content = new StringContent($"grant_type=password&username={username}&password={password}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
         
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {content}");
                var json = System.Text.Json.JsonDocument.Parse(content);
                return json.RootElement.GetProperty("access_token").GetString();
            }
            return null;
        }

        public async Task<string> GetUserInfoAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://oauth.reddit.com/api/v1/me");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Headers.UserAgent.ParseAdd("ChangeMeClient/0.1 by YourUsername");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
