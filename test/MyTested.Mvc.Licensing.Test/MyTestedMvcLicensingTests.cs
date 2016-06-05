namespace MyTested.Mvc.Test
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
                TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.Mvc.Tests");
                
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
            .GetAwaiter()
            .GetResult();
        }

        [Fact]
        public void WithLicenseNoExceptionShouldBeThrown()
        {
            Task.Run(async () =>
            {
                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.Mvc.Tests");

                MyMvc
                    .IsUsingDefaultConfiguration()
                    .WithTestConfiguration(config =>
                    {
                        config.AddInMemoryCollection(new[]
                        {
                            new KeyValuePair<string, string>("License", "1-zIM2e+HWAKjSk0J62N16NTkDwCn3wt31aqH25kZQzjj+9zsHJzLkzTWqWXI0D/znxrzT5lt+rrkWrBJednuSf9kM5BxBpz4zoiBJZW3TtPSjEXRghu87Pr2QLiQoS8BJw/S3MNUHoO09AJZvf0UJoa0+Wm3Vh0U2ZEEK0OYttQUxOjk5OTktMTItMzE6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpGdWxsOk15VGVzdGVkLk12Yy4="),
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
                TestCounter.SetLicenseData(null, DateTime.MinValue, "MyTested.Mvc.Tests");

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
            .GetAwaiter()
            .GetResult();

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
