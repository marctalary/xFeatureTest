# xFeatureTest
Test Framework on-top of xunit providing enforced structure for gherkin style tests.
[![NuGet version](https://img.shields.io/nuget/vpre/XFeatureTest.svg)](https://www.nuget.org/packages/XFeatureTest)

A Feature (test class) contains Scenarios (test methods) and they have simple steps which call methods on a  corresponding Given, When and Then class.  

A scenario looks something like this:

```c#
public class BasicExampleFeature
 : Feature<BasicExampleFeatureGiven, BasicExampleFeatureWhen, BasicExampleFeatureThen>
{
    [Fact]
    public void IGetFullNameFromNames()
    {
        OutputScenarioText();

        Given.AFirstName();
        Given.ALastName();
        When.IGetFullName();
        Then.FullNameIsFirstNamePlusLastName();
    }
}
```
A more application familiar version of Dependency is implemented for this framework by using `Host.CreateDefaultBuilder()` (you can also pass in your own `IHostBuilder`).  This means that there is automatically support for `appsettings.json` and other standard utilities.  

## Getting Started
1. Install the standard Nuget package into your xunit test project.
`Install-Package XFeatureTest`
2. Create a class in your xunit test project to configure the test run host, add any services to be injected into your Given, When, Then classes. 

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

3. Create a feature which will contain your scenarios.  

Scenarios are written using standard Fact/Theory xunit methods.   The feature class should inherit from `XFeatureTest.Features.Feature<>` with corresponding `Given`, `When`, `Then` Classes created.

```c#
public class ExampleFeature : Feature<ExampleFeatureGiven, ExampleFeatureWhen, ExampleFeatureThen>
{
	public ExampleFeature(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
	{ }
}
```

Preconditions and test data creation should be done in the `Given` class.

```c#
public class ExampleGiven : FeatureGiven
{
    public ExampleGiven(ScenarioOutput output) : base(output)
    { }
}
```

Actions should be carried out and the outcomes captured in the `When` class.  The actions should use preconditions from the `Given` class. 

```c#
public class ExampleWhen : FeatureWhen<ExampleGiven>
{
    public ExampleWhen(ScenarioOutput output, ExampleGiven given) : base(output, given)
    { }
}
```

Assertions using the `Given` preconditions and `When` outcomes should be performed in the `Then` class.

```c#
public class ExampleThen : FeatureThen<ExampleGiven, ExampleWhen>
{
    public ExampleThen(ScenarioOutput output, ExampleGiven given, ExampleWhen when) : base(output, given, when)
    { }
}
```

Any registered services can be injected into the given when or then classes.

4. Register your feature and it's `Given`, `When`, `Then` class using the `Feature` class `RegisterServices` method.

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

## Scenario Text Output

Given, When, Then, Feature, Scenario and Cleanup descriptions can be added to the test output.  In the `Feature` class scenario/test methods, Given, When or Then methods simply add `OutputScenarioText();` and the name of the current method will be output, formatted as a sentence or you can provide your own text.

Example output:

```
12:42:41 FEATURE basic example feature
12:42:41 SCENARIO i get full name from names
----------------
12:42:41 GIVEN a first name
12:42:41   AND a last name
12:42:41 WHEN  i get full name
12:42:41 THEN  full name is first name plus last name
----------------
12:42:41 CLEANUP cleaning up name data.
```

The output can be customised by adding a service providing `ScenarioOutputOptions` e.g.

```c#
public void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
  ScenarioOutputOptions options = ScenarioOutputOptionsFactory.DefaultOptions;
  services.AddSingleton(options);
  ExampleFeature.RegisterServices(services);
}
```

## Test Initialisation and Cleanup

As per standard xunit approach, test initialisation and cleanup for each scenario/test can be added as follows:

- put initalisation code in the class constructor

- implment the `IDisposable` interface in your feature/test class and put the cleanup code in the `Dispose()` method

For asynchronous initialisation and cleanup implement `IAsyncLifetime` in your feature/test class then:

- put initalisation code in the `InitializeAsync()` method 
- put the cleanup code in the `DisposeAsync()` method

`InitializeAsync` will be called right after the constructor is invoked, and `DisposeAsync` just before `Dispose`, if it exists, is called.

