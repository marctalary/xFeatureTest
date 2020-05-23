using System.Runtime.CompilerServices;
using XFeatureTest.TextOutput;

namespace XFeatureTest.Features
{
    public abstract class FeatureGiven : FeatureGivenWhenThen
    {
        protected FeatureGiven(ScenarioOutput output) : base(output)
        {
        }

        public void OutputScenarioText([CallerMemberName] string preconditionDescription = null)
        {
            Output.Given(preconditionDescription);
        }
    }
}