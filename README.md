# xFeatureTest
Test Framework ontop of xunit providing enforced structure for gherkin style tests.

Create a class in your xunit test project to configure the test run host by adding your services which will then be injected into your Given, When, Then classes. 

```c#
using XFeatureTest.Hosting;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestFramework(Startup.AssemblyQualifiedName, Startup.AssemblyName)]

namespace ExampleTestProject
{
    /// <summary>
    ///     Runs once at the assembly initialize point... i.e. start of the test run
    /// </summary>
    public class Startup : XunitTestFramework
    {
        public const string AssemblyQualifiedName = "ExampleTestProject.Startup";
        public const string AssemblyName = "ExampleTestProject";

        public Startup(IMessageSink messageSink) : base(messageSink)
        {
            // Will create a Host.CreateDefaultBuilder host so include appsettings.json etc.
            TestRunHost.ConfigureTestRunHost(ConfigureServices);
        }

        public void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Configure any services or options
        }
    }
}
```

Singletons will last the lifetime of the entire test run and scoped services will live for the lifetime of a single test/scenario.



Create a feature which will contain your scenarios.  

Scenarios are written using standard Fact/Theory xunit methods.   The feature class should inherit from `XFeatureTest.Features.Feature<>` with corresponding Given, When, Then Classes created.

```c#
public class ExampleFeature : Feature<ExampleFeatureGiven, ExampleFeatureWhen, ExampleFeatureThen>
{
	public ExampleFeature(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    { }
}
```

Preconditions and test data creation should be done in the `Given` class.

```c#
using XFeatureTest.Features;
using XFeatureTest.TextOutput;

public class ExampleGiven : FeatureGiven
{
    public ExampleGiven(ScenarioOutput output) : base(output)
    { }
}
```

Actions should be carried out and the outcomes captured in the `When` class.  The actions should use use preconditions from the `Given` class. 

```c#
using XFeatureTest.Features;
using XFeatureTest.TextOutput;

public class ExampleWhen : FeatureWhen<ExampleGiven>
{
    public ExampleWhen(ScenarioOutput output, ExampleGiven given) : base(output, given)
    { }
}
```

Assertions using the `Given` preconditions and `When` outcomes should be performed in the `Then` class.

```c#
using XFeatureTest.Features;
using XFeatureTest.TextOutput;

public class ExampleThen : FeatureThen<ExampleGiven, ExampleWhen>
{
    public ExampleThen(ScenarioOutput output, ExampleGiven given, ExampleWhen when) : base(output, given, when)
    {
    }
}
```

Any registered services can be injected into the the given when or then classes.

Register you feature and it's given, when, then class using the features `RegisterServices` method.

```c#
public Startup(IMessageSink messageSink) : base(messageSink)
{
    // Will create a Host.CreateDefaultBuilder host so include appsettings.json etc.
	TestRunHost.ConfigureTestRunHost(ConfigureServices);
}

public void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
	// Configure any services or options
    ExampleFeature.RegisterServices(services);
}
```