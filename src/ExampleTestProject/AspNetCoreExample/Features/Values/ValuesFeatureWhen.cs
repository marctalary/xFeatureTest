using XFeatureTest.Features;
using XFeatureTest.TextOutput;

namespace ExampleTestProject.AspNetCoreExample.Features.Values
{
    public class ValuesFeatureWhen:FeatureWhen<ValuesFeatureGiven>
    {
        protected ValuesFeatureWhen(ScenarioOutput output, ValuesFeatureGiven given) : base(output, given)
        {
        }
    }
}