using System.Runtime.CompilerServices;

namespace XFeatureTest.Features
{
    public abstract class FeatureThen : FeatureGivenWhenThen
    {
        protected FeatureThen(ScenarioOutput output) : base(output)
        {
        }

        public void OutputScenarioText([CallerMemberName] string assertionDescription = null) =>
            Output.Then(assertionDescription);
    }
}