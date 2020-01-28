namespace MyTested.AspNetCore.Mvc.Licensing.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Licensing;
    using Microsoft.Extensions.Configuration;
    using Mvc.Test.Setups;
    using Mvc.Test.Setups.Controllers;
    using Xunit;

    public class MyTestedMvcLicensingTests
    {
        [Fact]
        public void WithNoLicenseExceptionShouldBeThrown()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithConfiguration(configuration => configuration
                    .Add("License", null))
                .WithTestAssembly(this);

            LicenseValidator.ClearLicenseDetails();
            TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.AspNetCore.Mvc.Tests");

            Task.Run(async () =>
            {
                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyController<MvcController>
                            .Instance()
                            .Calling(c => c.OkResultAction())
                            .ShouldReturn()
                            .Ok();
                    }));
                }

                await Assert.ThrowsAsync<InvalidLicenseException>(async () => await Task.WhenAll(tasks));
            })
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();
            
            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithLicenseNoExceptionShouldBeThrown()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithConfiguration(configuration => configuration
                    .Add("License", "1-3i7E5P3qX5IUWHIAfcXG6DSbOwUBidygp8bnYY/2Rd9zA15SwRWP6QDDp+m/dDTZNBFX2eIHcU/gdcdm83SL695kf3VyvMPw+iyPN6QBh/WnfQwGLqBecrQw+WNPJMz6UgXi2q4e4s/D8/iSjMlwCnzJvC2Yv3zSuADdWObQsygxOjk5OTktMTItMzE6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpGdWxsOk15VGVzdGVkLkFzcE5ldENvcmUuTXZjLg=="))
                .WithTestAssembly(this);

            LicenseValidator.ClearLicenseDetails();
            TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.AspNetCore.Mvc.Tests");

            Task.Run(async () =>
            {
                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyController<MvcController>
                            .Instance()
                            .Calling(c => c.OkResultAction())
                            .ShouldReturn()
                            .Ok();
                    }));
                }

                await Task.WhenAll(tasks);
            })
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithMultipleLicensesNoExceptionShouldBeThrown()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithConfiguration(config => config
                    .AddJsonFile("multilicenseconfig.json"))
                .WithTestAssembly(this);

            LicenseValidator.ClearLicenseDetails();
            TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.AspNetCore.Mvc.Tests");
            
            Task.Run(async () =>
            {
                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyController<MvcController>
                            .Instance()
                            .Calling(c => c.OkResultAction())
                            .ShouldReturn()
                            .Ok();
                    }));
                }

                await Task.WhenAll(tasks);
            })
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
