using XFeatureTest.Features;
using Xunit;
using Xunit.Abstractions;

namespace ExampleTestProject.BasicExample.Features.BasicExampleFeature
{
    public class BasicExampleFeature
        : Feature<BasicExampleFeatureGiven, BasicExampleFeatureWhen, BasicExampleFeatureThen>
    {
        public BasicExampleFeature(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void IGetFullNameFromNames()
        {
            OutputScenarioText();

            Given.AFirstName();
            Given.ALastName();
            When.IGetFullName();
            Then.FullNameIsFirstNamePlusLastName();
        }
    }
}