using Microsoft.Extensions.DependencyInjection;

namespace XFeatureTest.Features
{
    public class ScenarioContext<TGiven, TWhen, TThen>
        where TGiven : class
        where TWhen : class
        where TThen : class
    {
        public ScenarioContext(TGiven given, TWhen when, TThen then, ScenarioOutput output)
        {
            Given = given;
            When = when;
            Then = then;
            Output = output;
        }

        public TGiven Given { get; }
        public TWhen When { get; }
        public TThen Then { get; }
        public ScenarioOutput Output { get; }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ScenarioContext<TGiven, TWhen, TThen>>();
            services.AddScoped<TGiven>();
            services.AddScoped<TWhen>();
            services.AddScoped<TThen>();
        }
    }
}