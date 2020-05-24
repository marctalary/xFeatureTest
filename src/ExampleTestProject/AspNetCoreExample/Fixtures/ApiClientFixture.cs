using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ExampleAspNetProject;
using ExampleAspNetProject.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExampleTestProject.AspNetCoreExample.Fixtures
{
    public interface IApiClientFixture
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }

    public class ApiClientFixture : IApiClientFixture
    {
        private readonly HttpClient _httpClient;
        private readonly IValuesDataService _valuesDataServiceFixture;

        public ApiClientFixture(IValuesDataService valuesDataServiceFixture)
        {
            _valuesDataServiceFixture = valuesDataServiceFixture;
            var hostBuilder = Program
                .CreateWebHostBuilder(new string[0])
                .ConfigureWebHost(o => o.UseTestServer());

            hostBuilder.ConfigureServices(ReplaceServices);

            var host = hostBuilder.Start();
            _httpClient = host.GetTestClient();
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _httpClient.SendAsync(request);
        }

        private void ReplaceServices(IServiceCollection services)
        {
            ReplaceService(typeof(IValuesDataService), _valuesDataServiceFixture, services);
        }

        private void ReplaceService(Type typeToReplace, object instance, IServiceCollection services)
        {
            foreach (var serviceDescriptor in services.Where(s => s.ImplementationType == typeToReplace))
                services.Remove(serviceDescriptor);

            services.Add(new ServiceDescriptor(typeToReplace, instance));
        }
    }
}