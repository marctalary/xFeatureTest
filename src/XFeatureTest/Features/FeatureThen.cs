using System.Runtime.CompilerServices;
using XFeatureTest.TextOutput;

namespace XFeatureTest.Features
{
    public abstract class FeatureThen<TGiven, TWhen> : FeatureGivenWhenThen
        where TGiven : FeatureGiven
        where TWhen : FeatureWhen<TGiven>
    {
        public TGiven Given { get; }
        public TWhen When { get; }

        protected FeatureThen(ScenarioOutput output, TGiven given, TWhen when) : base(output)
        {
            Given = given;
            When = when;
        }

        public void OutputScenarioText([CallerMemberName] string assertionDescription = null)
        {
            Output.Then(assertionDescription);
        }
    }
}