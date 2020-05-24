using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ExampleTestProject.AspNetCoreExample.Fixtures;
using XFeatureTest.Features;
using XFeatureTest.TextOutput;

namespace ExampleTestProject.AspNetCoreExample.Features.Values
{
    public class ValuesFeatureWhen : FeatureWhen<ValuesFeatureGiven>
    {
        private readonly ValuesApiFixture _valuesApiFixture;

        public ValuesFeatureWhen(ScenarioOutput output, ValuesFeatureGiven given, ValuesApiFixture valuesApiFixture)
            : base(output, given)
        {
            _valuesApiFixture = valuesApiFixture;
        }

        public HttpResponseMessage GetResponse { get; private set; }
        public List<HttpResponseMessage> PostResponses { get; } = new List<HttpResponseMessage>();
        public List<HttpResponseMessage> PutResponses { get; } = new List<HttpResponseMessage>();

        public async Task PostIsCalledForValues()
        {
            OutputScenarioText();
            foreach (var value in Given.ValuesToBeCreated)
            {
                var httpResponseMessage = await _valuesApiFixture.Post(value);
                PostResponses.Add(httpResponseMessage);
            }
        }

        public async Task GetIsCalled()
        {
            OutputScenarioText();
            GetResponse = await _valuesApiFixture.Get();
        }

        public async Task DeleteCreatedValues()
        {
            OutputScenarioCleanupText();
            if (PostResponses.Any())
                await _valuesApiFixture.Delete();
        }
    }
}