using System.Net;
using System.Threading.Tasks;
using ExampleAspNetProject.Models;
using ExampleTestProject.AspNetCoreExample.Models;
using XFeatureTest.Assertions;
using XFeatureTest.Features;
using XFeatureTest.TextOutput;
using Xunit;

namespace ExampleTestProject.AspNetCoreExample.Features.Values
{
    public class ValuesFeatureThen : FeatureThen<ValuesFeatureGiven, ValuesFeatureWhen>
    {
        public ValuesFeatureThen(ScenarioOutput output, ValuesFeatureGiven given, ValuesFeatureWhen when) : base(output,
            given, when)
        {
        }

        public void PutIsCalledSuccessfully()
        {
            OutputScenarioText();

            Assert.Equal(Given.ValuesToBeCreated.Count, When.PutResponses.Count);
            foreach (var response in When.PutResponses) Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void PostIsCalledSuccessfully()
        {
            OutputScenarioText();

            Assert.Equal(Given.ValuesToBeCreated.Count, When.PostResponses.Count);
            foreach (var response in When.PostResponses) Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void GetIsCalledSuccessfully()
        {
            OutputScenarioText();

            Assert.NotNull(When.GetResponse);
        }

        public async Task GetReturnedValuesToBeCreated()
        {
            OutputScenarioText();

            var jsonBody = await When.GetResponse.Content.ReadAsStringAsync();
            var responseModels = AssertJson.Valid<ValueJsonModel[]>(jsonBody);

            Assert.Equal(Given.ValuesToBeCreated.Count, responseModels.Length);
            foreach (var valueToBeCreated in Given.ValuesToBeCreated)
                Assert.Single(responseModels, r => r.Value == valueToBeCreated);
        }
    }
}