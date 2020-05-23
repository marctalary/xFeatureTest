using XFeatureTest.TextOutput;

namespace XFeatureTest.Features
{
    public abstract class FeatureGivenWhenThen
    {
        protected FeatureGivenWhenThen(ScenarioOutput output)
        {
            Output = output;
        }

        protected ScenarioOutput Output { get; }
    }
}