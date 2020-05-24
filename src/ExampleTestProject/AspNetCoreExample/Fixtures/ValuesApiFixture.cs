using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExampleTestProject.AspNetCoreExample.Models;
using Newtonsoft.Json;

namespace ExampleTestProject.AspNetCoreExample.Fixtures
{
    public class ValuesApiFixture
    {
        private const string RelativeUri = "/api/values";
        private readonly IApiClientFixture _apiClientFixture;

        public ValuesApiFixture(IApiClientFixture apiClientFixture)
        {
            _apiClientFixture = apiClientFixture;
        }

        public async Task<HttpResponseMessage> Put(ValueJsonModel valueJsonModel)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{RelativeUri}/{valueJsonModel.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(valueJsonModel.Value), Encoding.Default,
                    "application/json")
            };

            return await _apiClientFixture.SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> Post(string value)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, RelativeUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.Default,
                    "application/json")
            };

            return await _apiClientFixture.SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> Get(int id)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{RelativeUri}/{id}");
            return await _apiClientFixture.SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> Get()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{RelativeUri}");
            return await _apiClientFixture.SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> Delete()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{RelativeUri}");
            return await _apiClientFixture.SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{RelativeUri}/{id}");
            return await _apiClientFixture.SendAsync(httpRequest);
        }
    }
}