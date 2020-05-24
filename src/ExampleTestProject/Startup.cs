using ExampleTestProject;
using ExampleTestProject.BasicExample.Features.BasicExampleFeature;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XFeatureTest.Hosting;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestFramework(Startup.AssemblyQualifiedName, Startup.AssemblyName)]

namespace ExampleTestProject
{
    /// <summary>
    ///     Runs once at the assembly initialize point... i.e. start of the test run
    /// </summary>
    public class Startup : XunitTestFramework
    {
        public const string AssemblyQualifiedName = "ExampleTestProject.Startup";
        public const string AssemblyName = "ExampleTestProject";

        public Startup(IMessageSink messageSink) : base(messageSink)
        {
            // Will create a Host.CreateDefaultBuilder host so include appsettings.json etc.
            TestRunHost.ConfigureTestRunHost(ConfigureServices);
        }

        public void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            BasicExampleFeature.RegisterServices(services);
        }
    }
}