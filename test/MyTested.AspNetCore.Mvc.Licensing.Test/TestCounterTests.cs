namespace MyTested.AspNetCore.Mvc.Licensing.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Licensing;
    using Xunit;

    public class TestCounterTests
    {
        [Fact]
        public void IncrementAndValidateShouldThrowExceptionWithNullLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(null, new DateTime(2018, 10, 10), new DateTime(2018, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<InvalidLicenseException>(caughtException);
            Assert.Equal("The free-quota limit of 100 assertions per test project has been reached. Please visit https://mytestedasp.net/core/mvc#pricing to request a free license or upgrade to a commercial one.", caughtException.Message);
        }

        [Fact]
        public void IncrementAndValidateShouldThrowExceptionWithNoLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new string[0], new DateTime(2018, 10, 10), new DateTime(2018, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<InvalidLicenseException>(caughtException);
            Assert.Equal("The free-quota limit of 100 assertions per test project has been reached. Please visit https://mytestedasp.net/core/mvc#pricing to request a free license or upgrade to a commercial one.", caughtException.Message);
        }

        [Fact]
        public void IncrementAndValidateShouldThrowExceptionWithInvalidLegacyLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-0WUoGNBmCpgJ+ktp3BObsUjsaX5XKc4Ed4LoeJBUPgTacqG2wUw9iKAMG4jdDIaiU+AnoTvrXwwLuvfvn57oukhw6HwTqp8hJ2I0vmNZFisQGyD4sjTDlKCBaOXJwXzifCIty2UuGUeo3KNqKoM+5MF1D0i/kEg/LKztnAN312gxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpGdWxsOk15VGVzdGVkLk12Yy4=";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new[] { license }, new DateTime(2018, 10, 10), new DateTime(2018, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<InvalidLicenseException>(caughtException);
            Assert.Equal("You have invalid license: 'License is not valid for this version of My Tested ASP.NET Core MVC. License expired on 2017-10-15. This version of My Tested ASP.NET Core MVC was released on 2018-10-10'. The free-quota limit of 100 assertions per test project has been reached. Please visit https://mytestedasp.net/core/mvc#pricing to request a free license or upgrade to a commercial one.", caughtException.Message);
        }

        [Fact]
        public void IncrementAndValidateShouldNotThrowExceptionWithValidLegacyLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-rXDHzH/rR8IN83Qmtpyf8vsAd4cPfSd/roXjngSxf12fuEY5+nk/evBTOD3xcOQSrEQLte3BcpH/RxIxDaSmZU11zV4jafnJ4N0u+yfNmTvRhVAtGuVCPj1UgYva64QK5fsPbOXBXq1c9+ccfWoWuB7nuRPaJvUlv/dcHQAy3cUxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy4=";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new[] { license }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.Null(caughtException);
        }

        [Fact]
        public void IncrementAndValidateShouldThrowExceptionWithInvalidPerpetualLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-IRaNRwlovf7moJnDcQCJW8JDq++p8/1hTNsRnBRLDGkd6HidiJ3OEzpFdwmlDacikCv5oRBisRkJ8edjqx1R21VA+SxCgpGHJE2ftOBpV1OBysguNUSIKJyte2heP3xD4tY1BQNh0vcVhXJDcE3qImhodZmi1aXJ19SK5f4JRA8xOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46UGVycGV0dWFs";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new[] { license }, new DateTime(2016, 10, 10), new DateTime(2018, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<InvalidLicenseException>(caughtException);
            Assert.Equal("You have invalid license: 'License is not valid for this version of My Tested ASP.NET Core MVC. License expired on 2017-10-15. This version of My Tested ASP.NET Core MVC was released on 2018-10-10'. The free-quota limit of 100 assertions per test project has been reached. Please visit https://mytestedasp.net/core/mvc#pricing to request a free license or upgrade to a commercial one.", caughtException.Message);
        }

        [Fact]
        public void IncrementAndValidateShouldNotThrowExceptionWithValidPerpetualLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-IRaNRwlovf7moJnDcQCJW8JDq++p8/1hTNsRnBRLDGkd6HidiJ3OEzpFdwmlDacikCv5oRBisRkJ8edjqx1R21VA+SxCgpGHJE2ftOBpV1OBysguNUSIKJyte2heP3xD4tY1BQNh0vcVhXJDcE3qImhodZmi1aXJ19SK5f4JRA8xOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46UGVycGV0dWFs";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new[] { license }, new DateTime(2018, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.Null(caughtException);
        }

        [Fact]
        public void IncrementAndValidateShouldThrowExceptionWithInvalidSubscriptionLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-TE6MO5GnjYwR3DcbT8rIXfjk9e0+ZPOb+c27A7pA83aNY4IQNBhgnIf4eUfy0MBvyXYrh9rkLa1hpGnrGu2TMZSoYxeZS07rM7WCqxzd2xXqfzuTAxsO1yNiEo/UwvVZUqz6s3nunKXn1m0b5dbKrsu7hxmWf8P8L2DhCDD09/sxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46U3Vic2NyaXB0aW9u";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new[] { license }, new DateTime(2018, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<InvalidLicenseException>(caughtException);
            Assert.Equal("You have invalid license: 'License subscription expired on 2017-10-15'. The free-quota limit of 100 assertions per test project has been reached. Please visit https://mytestedasp.net/core/mvc#pricing to request a free license or upgrade to a commercial one.", caughtException.Message);
        }

        [Fact]
        public void IncrementAndValidateShouldNotThrowExceptionWithValidSubscriptionLicense()
        {
            Exception caughtException = null;

            Task.Run(async () =>
            {
                var license = "1-TE6MO5GnjYwR3DcbT8rIXfjk9e0+ZPOb+c27A7pA83aNY4IQNBhgnIf4eUfy0MBvyXYrh9rkLa1hpGnrGu2TMZSoYxeZS07rM7WCqxzd2xXqfzuTAxsO1yNiEo/UwvVZUqz6s3nunKXn1m0b5dbKrsu7hxmWf8P8L2DhCDD09/sxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46U3Vic2NyaXB0aW9u";

                LicenseValidator.ClearLicenseDetails();
                TestCounter.SetLicenseData(new[] { license }, new DateTime(2016, 10, 10), new DateTime(2018, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

                var tasks = new List<Task>();

                for (int i = 0; i < 200; i++)
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
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

            Assert.Null(caughtException);
        }
    }
}
