using XFeatureTest.Features;
using Xunit.Abstractions;

namespace ExampleTestProject.AspNetCoreExample.Features.Values
{
    public class ValuesFeature:Feature<ValuesFeatureGiven,ValuesFeatureWhen,ValuesFeatureThen>
    {
        public ValuesFeature(ITestOutputHelper output) : base(output)
        {
        }
    }
}
