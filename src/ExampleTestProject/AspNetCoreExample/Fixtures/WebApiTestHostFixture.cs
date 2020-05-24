using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExampleTestProject.AspNetCoreExample.Fixtures
{
    public class WebApiTestHostFixture
    {
        private HttpClient _httpClient;

        public async Task StartHost(Dictionary<Type, object> servicesToReplace = null)
        {

            var hostBuilder = ExampleAspNetProject.Program
                    .CreateWebHostBuilder(new string[0])
                    .ConfigureWebHost(o => o.UseTestServer());

            if (servicesToReplace != null)
                hostBuilder.ConfigureServices(c => Replace(c, servicesToReplace));

            // Build and start the IHost
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient to send requests to the TestServer
            _httpClient = host.GetTestClient();
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
            => _httpClient.SendAsync(request);


        private void Replace(IServiceCollection services, Dictionary<Type, object> servicesToReplace)
        {
            foreach (var (serviceTypeToReplace, replacementInstance) in servicesToReplace)
            {
                Replace(services, serviceTypeToReplace, replacementInstance);
            }
        }

        private void Replace(IServiceCollection services, Type typeToReplace, object instance)
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeToReplace);
            services.Remove(descriptorToRemove);
            var descriptorToAdd = new ServiceDescriptor(typeToReplace, instance);
            services.Add(descriptorToAdd);
        }
    }
}
