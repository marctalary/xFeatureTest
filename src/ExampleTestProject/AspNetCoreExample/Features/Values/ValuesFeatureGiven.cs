using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using XFeatureTest.Features;
using XFeatureTest.TextOutput;

namespace ExampleTestProject.AspNetCoreExample.Features.Values
{
    public class ValuesFeatureGiven : FeatureGiven
    {
        private readonly IFixture _fixture;

        public ValuesFeatureGiven(ScenarioOutput output, IFixture fixture) : base(output)
        {
            _fixture = fixture;
        }

        public IReadOnlyList<string> ValuesToBeCreated { get; private set; }

        public void MultipleValuesToBeCreated()
        {
            OutputScenarioText();
            ValuesToBeCreated = _fixture.CreateMany<string>(3).ToList();
        }
    }
}