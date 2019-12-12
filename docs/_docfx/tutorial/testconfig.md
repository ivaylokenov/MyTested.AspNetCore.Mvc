# Test Configuration

In this section we are going to examine the test configuration or the **"testsettings.json"** file we used to provide a license key for the library.

## Startup class

One of the features you can control from the test configuration is how you register the application services. There are three ways:

- **Automatic Startup class** - the default option, we used it during this tutorial

- **Manual Startup class** - if the test runner of your choice supports assembly initialization you may choose to use this option like in the [ApplicationParts](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/development/samples/ApplicationParts) sample

```json
{
  "General": {
    "AutomaticStartup": false
  }
}
```

- **No Startup class** - you may choose not to use global services at all and provide them in each test explicitly like in the [NoStartup](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/tree/development/samples/NoStartup) sample

```json
{
  "General": {
    "NoStartup": true
  }
}
```

More information is available [HERE](/guide/startuptypes.html)!

## General settings

Various general settings are available for you to explore:

```json
{
  "General": {
    "Environment": "CustomEnvironment",
    "ApplicationName": "Custom Application Name",
    "StartupType": "CustomStartupType",
    "AsynchronousTests": false,
    "WebAssemblyName": "MyApp",
    "TestAssemblyName": "MyApp.Test",
	"AutomaticApplicationParts": true
  }
}
```

More information is available [HERE](/guide/testconfig.html).

## Specific settings

Different components have different test configuration. For example, you may disable the automatic model validation in every action call:

```json
{
  "Controllers": {
    "ModelStateValidation": false
  }
}
```

More information is available [HERE](/guide/testconfig.html).

## Section summary

This section introduced you to the test configuration. We have one last part - custom [Extension Methods](/tutorial/extensionmethods.html)!
