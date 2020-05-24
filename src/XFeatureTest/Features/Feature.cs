using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using XFeatureTest.Hosting;
using Xunit.Abstractions;

namespace XFeatureTest.Features
{
    public abstract class Feature<TGiven, TWhen, TThen> : IDisposable
        where TGiven : FeatureGiven
        where TWhen : FeatureWhen<TGiven>
        where TThen : FeatureThen<TGiven,TWhen>
    {
        private readonly ScenarioContext<TGiven, TWhen, TThen> _scenarioContext;
        private readonly IServiceScope _scenarioServicesScope;

        protected Feature(ITestOutputHelper output)
        {
            _scenarioServicesScope = TestRunHost.Services.CreateScope();
            _scenarioServicesScope.ServiceProvider.GetService<TestContext>().Output = output;
            _scenarioContext = GetService<ScenarioContext<TGiven, TWhen, TThen>>();
            if (_scenarioContext == null)
                throw new Exception(
                    "Feature not registered, ensure you have registered services in ConfigureServices, i.e. \n" +
                    $"{GetType().Name}.RegisterServices(services);");
        }

        protected TGiven Given => _scenarioContext.Given;
        protected TWhen When => _scenarioContext.When;
        protected TThen Then => _scenarioContext.Then;

        public virtual void Dispose()
        {
            _scenarioServicesScope.Dispose();
        }

        public void OutputScenarioText([CallerMemberName] string scenarioText = null, bool includeFeatureText = true)
        {
            if (includeFeatureText)
                _scenarioContext.Output.Feature(GetType().Name);

            _scenarioContext.Output.Scenario(scenarioText);
        }

        public static void RegisterServices(IServiceCollection services)
        {
            ScenarioContext<TGiven, TWhen, TThen>.RegisterServices(services);
        }

        protected TService GetService<TService>()
        {
            return _scenarioServicesScope.ServiceProvider.GetService<TService>();
        }
    }
}