# Licensing

This section will describe all available My Tested ASP.NET Core MVC editions and their licensing options.

## Editions

The testing library has four different editions available for you to explore:

### Lite

The **"Lite"** edition of the library provides only a small subset of the available setups and assertions, but they are more than sufficient for controller and view component testing.

- Perfect for small MVC applications
- Completely free and unlimited
- Includes only **"Controllers"**, **"ViewActionResults"** and **"ViewComponents"**
- Cannot be used in combination with any other package
- Requires manual mock creation of commonly used services
- Can be used by everyone
- [Apache or Microsoft Public License](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc/blob/development/src/MyTested.AspNetCore.Mvc.Lite/LICENSE)

### Community

The **"Community"** edition of the library is fully unlocked, and the whole fluent testing API can be used for free. This version is exclusively available only for individuals, open-source projects, startups and educational institutions.

- Perfect for every MVC application complying with the special terms
- Completely free and unlimited
- Developers can include [any package they prefer](/guide/packages.html)
- Available only for individuals and special types of businesses and organizations
- Fully featured edition
- License codes can be requested from [HERE](https://mytestedasp.net/Core/Mvc#free-usage-modal) for free

### Trial

The **"Trial"** edition of the library is fully featured but allows up to 100 assertions (around 25 unit tests) per test project. You may use it in multiple projects to overcome this limitation or just write only a few tests with it (for example asserting a very important route in your application)

- Perfect for previewing the framework
- Completely free with limitations
- Allows up to 100 assertions (around 25 unit tests) per test project
- Developers can include [any package they prefer](/guide/packages.html)
- Fully featured edition
- Can be used by everyone
- The limitation can be removed by providing a license code

### Commercial

The **"Commercial"** edition of the library is fully unlocked and unlimited. This version requires a paid license code.

- Perfect for every MVC application
- No limitations
- Requires paid license code
- Developers can include [any package they prefer](/guide/packages.html)
- Fully featured edition
- Can be used by everyone
- License codes can be purchased from [HERE](https://mytestedasp.net/Core/Mvc#pricing)

Choose wisely! :)

## Registering a license code

If you followed the tutorial strictly, you should have reached the free trial version limitations of My Tested ASP.NET Core MVC. When you run a single test (no matter which one), it should pass. But if you try to run them all at once, you should receive the following exception:

```text
The free-quota limit of 100 assertions per test project has been reached. Please visit https://mytestedasp.net/core/mvc#pricing to request a free license or upgrade to a commercial one.
```

We will now register a license code in **"MusicStore.Test"** and remove this restriction. Create a **"testconfig.json"** file at the root of the project:

<img src="/images/tutorial/testconfigfile.jpg" alt="testconfig.json file at the root of the project" />

Then add the following JSON in it:

```json
{
  "License": "1-0GEPhlzJ+jgqzGg+hQPo19wbRvEN0C4LjGm9YDTErbLzcTROsj3fU177Unj7wlOCNE0ciZCB5aw8jt4EEDczpW6S/lW0PkU8ZBjqh6F2ev42hqcgtlEKmBRwomPKj/PUElAo1iIdkLn3/il3o8HAsum7bKMqv7QPpOSwy/TuAGYxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok11c2ljIFN0b3JlIFNhbXBsZSBUZXN0czpEZXZlbG9wZXI6TXVzaWNTdG9yZS4="
}
```

This license is tied to the **"MusicStore.Test"** project. To unlock the testing framework limitations, we need to do one more thing. Go to the **"MusicStore.Test.csproj"** file and add **"testconfig.json"** in the **"copyToOutput"** array:

```xml
<!-- Other ItemGroups -->

<ItemGroup>
    <Content Update="config.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="testconfig.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

<!-- Other ItemGroups -->
```

From now on the **"testconfig.json"** file will be copied to the build output directory, and My Tested ASP.NET Core MVC will be able to read it successfully. More information about the test configuration can be found [HERE](/guide/testconfig.html).

Rebuild the solution and run the tests again. All of them should pass. We have unlocked the full version of the library! :)

Additional details about the licensing of the testing framework can be found [HERE](/guide/licensing.html).

## Section summary

Since now we have used the **"Trial"** edition of My Tested ASP.NET Core MVC. It was working fine, but we have reached its limitations while still having a lot of tests to write for our web application. After providing a license code in the test project, we are now able to successfully assert with the fully-featured and unlimited **"Community"** edition. Now let's get back to the code and test some [Attributes](/tutorial/attributes.html)!