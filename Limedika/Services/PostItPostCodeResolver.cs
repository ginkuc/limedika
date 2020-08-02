using Limedika.Data.Dtos.PostIt;
using Limedika.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Limedika.Services
{
    public class PostItPostCodeResolver : IPostCodeResolver
    {
        private HttpClient Client { get; set; }
        private readonly string _apiKey;
        private readonly JsonSerializerOptions _serializerOptions;

        public PostItPostCodeResolver(
            HttpClient client,
            IConfiguration configuration
            )
        {
            _apiKey = configuration["PostItApi:Key"];
            client.BaseAddress = new Uri(configuration["PostItApi:Url"]);

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Client = client;
        }

        public async Task<string> GetPostCode(string address)
        {
            var encodedAddress = WebUtility.UrlEncode(address);
            var response = await Client.GetAsync($"?term={encodedAddress}&key={_apiKey}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            
            var postItApiResponse = JsonSerializer.Deserialize<PostItResponseDto>(responseString, _serializerOptions);

            return postItApiResponse.Data.First().Post_Code;
        }
    }
}