using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nueve.Test.Integration
{
    public abstract class TestsBase : IDisposable
    {
        public readonly TestServer Server;
        public readonly HttpClient Client;
        public readonly string BaseUrl = "api";

        protected TestsBase()
        {
            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> PostAsync(string path, Dictionary<string, string> data)
        {
            var request = JsonConvert.SerializeObject(data);
            var response = await Client.PostAsync($"{BaseUrl}/{path}", new StringContent(request, Encoding.UTF8, "application/json"));

            return response;
        }

        public async Task<string> GetAsync(string path)
        {
            var response = await Client.GetAsync($"{BaseUrl}/{path}");
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<HttpResponseMessage> AuthorizeUser(string username, string password, string clientId)
        {
            var authData = new Dictionary<string, string>
            {
               { "Name", username },
               { "Password", password },
               { "ClientId", clientId }
            };
            var authResult = await PostAsync("auth", authData);
            var token = JObject.Parse(await authResult.Content.ReadAsStringAsync());
            var accessToken = (string)token.SelectToken("token");

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return authResult;
        }

        public void Dispose()
        {
        }
    }
}
