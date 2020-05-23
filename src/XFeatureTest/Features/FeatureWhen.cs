using System.Runtime.CompilerServices;
using XFeatureTest.TextOutput;

namespace XFeatureTest.Features
{
    public class FeatureWhen : FeatureGivenWhenThen
    {
        protected FeatureWhen(ScenarioOutput output) : base(output)
        {
        }

        public void OutputScenarioText([CallerMemberName] string actionDescription = null)
        {
            Output.When(actionDescription);
        }

        public void OutputScenarioCleanupText([CallerMemberName] string cleanupDescription = null)
        {
            Output.Cleanup(cleanupDescription);
        }
    }
}