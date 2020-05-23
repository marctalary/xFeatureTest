using System.Runtime.CompilerServices;
using XFeatureTest.TextOutput;

namespace XFeatureTest.Features
{
    public class FeatureWhen<TGiven> : FeatureGivenWhenThen
        where TGiven : FeatureGiven
    {
        protected TGiven Given { get; }

        protected FeatureWhen(ScenarioOutput output, TGiven given) : base(output)
        {
            Given = given;
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