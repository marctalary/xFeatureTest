using XFeatureTest.Features;
using XFeatureTest.TextOutput;
using Xunit;

namespace ExampleTestProject.BasicExample.Features.BasicExampleFeature
{
    public class BasicExampleFeatureThen : FeatureThen<BasicExampleFeatureGiven, BasicExampleFeatureWhen>
    {
        public BasicExampleFeatureThen(ScenarioOutput output, BasicExampleFeatureGiven given,
            BasicExampleFeatureWhen when) : base(output, given, when)
        {
        }

        public void FullNameIsFirstNamePlusLastName()
        {
            OutputScenarioText();

            Assert.Equal($"{Given.FirstName} {Given.LastName}", When.FullNameResult);
        }
    }
}