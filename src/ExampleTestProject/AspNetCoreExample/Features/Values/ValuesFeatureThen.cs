using XFeatureTest.Features;
using XFeatureTest.TextOutput;

namespace ExampleTestProject.AspNetCoreExample.Features.Values
{
    public class ValuesFeatureThen:FeatureThen<ValuesFeatureGiven,ValuesFeatureWhen>
    {
        public ValuesFeatureThen(ScenarioOutput output, ValuesFeatureGiven given, ValuesFeatureWhen when) : base(output, given, when)
        {
        }
    }
}