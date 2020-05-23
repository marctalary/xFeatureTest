using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using XFeatureTest.TextOutput;

namespace XFeatureTest.Hosting
{
    /// <summary>
    ///     Runs once at the assembly initialize point... i.e. start of the test run
    /// </summary>
    public class TestRunHost : IDisposable
    {
        /// <summary>
        ///     Single host for all of the tests in the test run
        /// </summary>
        public static IHost RunHost { get; private set; }

        /// <summary>
        ///     Services configured for the test run
        /// </summary>
        public static IServiceProvider Services => RunHost.Services;

        public void Dispose()
        {
            RunHost.Dispose();
        }

        public static void ConfigureTestRunHost(Action<HostBuilderContext, IServiceCollection> configureServices)
        {
            ConfigureTestRunHost(Host.CreateDefaultBuilder().ConfigureServices(configureServices));
        }

        public static void ConfigureTestRunHost(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(AddXFeatureTest);
            RunHost = hostBuilder.Build();
        }

        private static void AddXFeatureTest(IServiceCollection services)
        {
            services.AddScoped<TestContext>();
            services.AddScoped<ScenarioOutput>();
            services.TryAddSingleton(ScenarioOutputOptionsFactory.DefaultOptions);
        }
    }
}