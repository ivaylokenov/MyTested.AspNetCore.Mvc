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
            TestCounter.SetLicenseData(null, DateTime.MinValue, DateTime.MinValue, "MyTested.AspNetCore.Mvc.Tests");

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
                    .Add("License", "1-1pKwrILvp8I6UEGplN/RvjVeUW8DX0G5V2UhskQUmOd46C5rO9Nb+FcWf/xqBaYljRtCydmSqvmv37PFvMD7PUrXI0lyDcvRKoCJywthqp0wqrjvfmJOWcsH4AHaPdWZXIAG2NP77A7EwhjbNvzQ6tR6HovSFv2S5qcJWB0Ht/4xOjIwOTktMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46U3Vic2NyaXB0aW9u"))
                .WithTestAssembly(this);

            LicenseValidator.ClearLicenseDetails();
            TestCounter.SetLicenseData(null, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

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
            TestCounter.SetLicenseData(null, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");
            
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
