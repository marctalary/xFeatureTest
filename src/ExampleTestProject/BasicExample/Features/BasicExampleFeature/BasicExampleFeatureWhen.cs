using XFeatureTest.Features;
using XFeatureTest.TextOutput;

namespace ExampleTestProject.BasicExample.Features.BasicExampleFeature
{
    public class BasicExampleFeatureWhen : FeatureWhen<BasicExampleFeatureGiven>
    {
        public BasicExampleFeatureWhen(ScenarioOutput output, BasicExampleFeatureGiven given) : base(output, given)
        {
        }

        public string FullNameResult { get; private set; }

        public void IGetFullName()
        {
            OutputScenarioText();

            FullNameResult = Given.SubjectUnderTest.GetFullName();
        }
    }
}