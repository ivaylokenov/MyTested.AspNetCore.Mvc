namespace MyTested.Mvc.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Licensing;
    using Xunit;

    public class TestCounterTests
    {
        [Fact]
        public void IncrementAndValidateShouldThrowExceptionWithNoLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(null, new DateTime(2018, 10, 10), "MyTested.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 500; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        TestCounter.IncrementAndValidate();
                    }));
                }

                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    caughtException = ex;
                }
            })
            .GetAwaiter()
            .GetResult();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<InvalidLicenseException>(caughtException);
        }

        [Fact]
        public void IncrementAndValidateShouldThrowExceptionWithInvalidLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-0WUoGNBmCpgJ+ktp3BObsUjsaX5XKc4Ed4LoeJBUPgTacqG2wUw9iKAMG4jdDIaiU+AnoTvrXwwLuvfvn57oukhw6HwTqp8hJ2I0vmNZFisQGyD4sjTDlKCBaOXJwXzifCIty2UuGUeo3KNqKoM+5MF1D0i/kEg/LKztnAN312gxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpGdWxsOk15VGVzdGVkLk12Yy4=";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new []{ license }, new DateTime(2018, 10, 10), "MyTested.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 500; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        TestCounter.IncrementAndValidate();
                    }));
                }

                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    caughtException = ex;
                }
            })
            .GetAwaiter()
            .GetResult();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<InvalidLicenseException>(caughtException);
        }

        [Fact]
        public void IncrementAndValidateShouldNotThrowExceptionWithValidLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-0WUoGNBmCpgJ+ktp3BObsUjsaX5XKc4Ed4LoeJBUPgTacqG2wUw9iKAMG4jdDIaiU+AnoTvrXwwLuvfvn57oukhw6HwTqp8hJ2I0vmNZFisQGyD4sjTDlKCBaOXJwXzifCIty2UuGUeo3KNqKoM+5MF1D0i/kEg/LKztnAN312gxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpGdWxsOk15VGVzdGVkLk12Yy4=";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new []{ license }, new DateTime(2016, 10, 10), "MyTested.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 500; i++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        TestCounter.IncrementAndValidate();
                    }));
                }

                try
                {
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    caughtException = ex;
                }
            })
            .GetAwaiter()
            .GetResult();

            Assert.Null(caughtException);
        }
    }
}
