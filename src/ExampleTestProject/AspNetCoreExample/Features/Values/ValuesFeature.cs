using System.Threading.Tasks;
using XFeatureTest.Features;
using Xunit;
using Xunit.Abstractions;

namespace ExampleTestProject.AspNetCoreExample.Features.Values
{
    public class ValuesFeature : Feature<ValuesFeatureGiven, ValuesFeatureWhen, ValuesFeatureThen>, IAsyncLifetime
    {
        public ValuesFeature(ITestOutputHelper output) : base(output)
        {
        }


        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            await When.DeleteCreatedValues();
        }

        [Fact]
        public async Task RecordsCreatedAreRetrieved()
        {
            OutputScenarioText();

            Given.MultipleValuesToBeCreated();
            await When.PostIsCalledForValues();
            await When.GetIsCalled();
            Then.PostIsCalledSuccessfully();
            Then.GetIsCalledSuccessfully();
            await Then.GetReturnedValuesToBeCreated();
        }
    }
}