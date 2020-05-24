using XFeatureTest.Features;
using XFeatureTest.TextOutput;

namespace ExampleTestProject.BasicExample.Features.BasicExampleFeature
{
    public class BasicExampleFeatureGiven : FeatureGiven
    {
        public BasicExampleFeatureGiven(ScenarioOutput output) : base(output)
        {
        }

        public BasicSubjectUnderTest SubjectUnderTest { get; } = new BasicSubjectUnderTest();

        public string FirstName { get; private set; }

        public string LastName { get; set; }

        public void AFirstName()
        {
            OutputScenarioText();

            SubjectUnderTest.FirstName = FirstName = "Dave";
        }

        public void ALastName()
        {
            OutputScenarioText();

            SubjectUnderTest.LastName = LastName = "Smith";
        }
    }
}