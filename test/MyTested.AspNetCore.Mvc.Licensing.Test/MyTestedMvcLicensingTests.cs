namespace MyTested.AspNetCore.Mvc.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Licensing;
    using Microsoft.Extensions.Configuration;
    using Setups.Controllers;
    using Xunit;

    public class MyTestedMvcLicensingTests
    {
        [Fact]
        public void WithNoLicenseExceptionShouldBeThrown()
        {
            Task.Run(async () =>
            {
                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.AspNetCore.Mvc.Tests");
                
                var tasks = new List<Task>();

                for (int i = 0; i < 500; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyMvc
                            .Controller<MvcController>()
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
        }

        [Fact]
        public void WithLicenseNoExceptionShouldBeThrown()
        {
            Task.Run(async () =>
            {
                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.AspNetCore.Mvc.Tests");

                MyMvc
                    .IsUsingDefaultConfiguration()
                    .WithTestConfiguration(config =>
                    {
                        config.AddInMemoryCollection(new[]
                        {
                            new KeyValuePair<string, string>("License", "1-3i7E5P3qX5IUWHIAfcXG6DSbOwUBidygp8bnYY/2Rd9zA15SwRWP6QDDp+m/dDTZNBFX2eIHcU/gdcdm83SL695kf3VyvMPw+iyPN6QBh/WnfQwGLqBecrQw+WNPJMz6UgXi2q4e4s/D8/iSjMlwCnzJvC2Yv3zSuADdWObQsygxOjk5OTktMTItMzE6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpGdWxsOk15VGVzdGVkLkFzcE5ldENvcmUuTXZjLg=="),
                        });
                    });

                var tasks = new List<Task>();

                for (int i = 0; i < 500; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyMvc
                            .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithMultipleLicensesNoExceptionShouldBeThrown()
        {
            Task.Run(async () =>
            {
                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.AspNetCore.Mvc.Tests");

                MyMvc
                    .IsUsingDefaultConfiguration()
                    .WithTestConfiguration(config =>
                    {
                        config.AddJsonFile("multilicenseconfig.json");
                    });

                var tasks = new List<Task>();

                for (int i = 0; i < 500; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        MyMvc
                            .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
