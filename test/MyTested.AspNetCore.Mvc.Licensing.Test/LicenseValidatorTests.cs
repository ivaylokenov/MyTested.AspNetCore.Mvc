namespace MyTested.AspNetCore.Mvc.Licensing.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using Licensing;
    using Mvc.Test.Setups;
    using Xunit;

    public class LicenseValidatorTests
    {
        [Fact]
        public void NullLicenseCollectionShouldThrowException()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(null, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "No license provided");
        }
        
        [Fact]
        public void NullLicenseShouldThrowException()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new string[] { null }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "License and project namespace cannot be null or empty");
        }

        [Fact]
        public void NullProjectNamespaceShouldThrowException()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { string.Empty }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), null);
                },
                "License and project namespace cannot be null or empty");
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithIncorrectNumberOfParts()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { "1-2-3" }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "License text is invalid");

            Assert.False(LicenseValidator.HasValidLicense);

            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { "1" }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "License text is invalid");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidLicenseId()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { "test-2" }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "License text is invalid");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidSignatureString()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { "1-A" }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "License text is invalid");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidNumberOfLicenseParts()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(
                        new[] { "1-aIqZBgCHicW6QK8wpk+T6a1WDfgvk1+uUEYkwxnFw+Ucla5Pxmyxyn9JJJcczy3iXet0exCvBMrkFE2PqAkwNd1TnZLt+pxmZaEPlUYwzfVCIpdB2rZAaFmrRToQvqrUv4jHHUyWw9/4C1yntfOUUkcYmFYLuNC4rmqocrv1o7AxOjIwMTYtMDEtMDE6OjpJbnRlcm5hbDpNeVRlc3RlZC5NdmMu" },
                        new DateTime(2016, 10, 10),
                        new DateTime(2016, 10, 10),
                        string.Empty);
                },
                "License details are invalid");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidSigningData()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    var firstLicense = "1-Lh95BZUyA92DgQ9B8o10CtFX0/Nhykf6upFj8rOIyodNLLc6iMRD3loJVZv00jtkUkdYPHDHAKW4YGqd8ROc3ZXUyrzNmQl67eUQ1BIELx8QfHo2gwPnQgMgdh5tfm+gtNNBvim0dqki2AN6ABwK0Garnd0lU3BAxM2YJ2k/C4oxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpJbnRlcm5hbDpNeVRlc3RlZC5NdmMu";
                    var secondLicense = "2-EEY5vqcJgmhaAR5wJbQc54Oev4z77D8+QLrbbeL3637ip6I7f6F3LqAm/hID8Wmtqs0fNXMKLl+bc6CH+J++keE9gOACejO0QXHa2e2xPpuHDPoKCyaJm2quDwLgzS/KcmlZAzc8H4RzL4rFy8nCEXGfgC83EJyf3L0D+8X1/0kyOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpJbnRlcm5hbDpNeVRlc3RlZC5NdmMu";

                    var wrongLicense = new List<char> { '1', '-' };
                    for (int i = 2; i < 130; i++)
                    {
                        wrongLicense.Add(firstLicense[i + 2]);
                    }

                    for (int i = 130; i < secondLicense.Length; i++)
                    {
                        wrongLicense.Add(secondLicense[i]);
                    }

                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { new string(wrongLicense.ToArray()) }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "License text does not match signature");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidLicenseIdMatch()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    var license = "1-Lh95BZUyA92DgQ9B8o10CtFX0/Nhykf6upFj8rOIyodNLLc6iMRD3loJVZv00jtkUkdYPHDHAKW4YGqd8ROc3ZXUyrzNmQl67eUQ1BIELx8QfHo2gwPnQgMgdh5tfm+gtNNBvim0dqki2AN6ABwK0Garnd0lU3BAxM2YJ2k/C4oxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpJbnRlcm5hbDpNeVRlc3RlZC5NdmMu";

                    var licenseArray = license.ToArray();
                    licenseArray[0] = '2';

                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { new string(licenseArray) }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), string.Empty);
                },
                "License ID does not match signature license ID");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidReleaseDate()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    var license = "1-Lh95BZUyA92DgQ9B8o10CtFX0/Nhykf6upFj8rOIyodNLLc6iMRD3loJVZv00jtkUkdYPHDHAKW4YGqd8ROc3ZXUyrzNmQl67eUQ1BIELx8QfHo2gwPnQgMgdh5tfm+gtNNBvim0dqki2AN6ABwK0Garnd0lU3BAxM2YJ2k/C4oxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpJbnRlcm5hbDpNeVRlc3RlZC5NdmMu";

                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { license }, new DateTime(2016, 10, 10), new DateTime(2018, 10, 10), string.Empty);
                },
                "License is not valid for this version of My Tested ASP.NET Core MVC. License expired on 2017-10-15. This version of My Tested ASP.NET Core MVC was released on 2018-10-10");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidSubscriptionDate()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    var license = "1-TE6MO5GnjYwR3DcbT8rIXfjk9e0+ZPOb+c27A7pA83aNY4IQNBhgnIf4eUfy0MBvyXYrh9rkLa1hpGnrGu2TMZSoYxeZS07rM7WCqxzd2xXqfzuTAxsO1yNiEo/UwvVZUqz6s3nunKXn1m0b5dbKrsu7hxmWf8P8L2DhCDD09/sxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46U3Vic2NyaXB0aW9u";

                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { license }, new DateTime(2018, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.");
                },
                "License subscription expired on 2017-10-15");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInvalidNamespace()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    var license = "1-Lh95BZUyA92DgQ9B8o10CtFX0/Nhykf6upFj8rOIyodNLLc6iMRD3loJVZv00jtkUkdYPHDHAKW4YGqd8ROc3ZXUyrzNmQl67eUQ1BIELx8QfHo2gwPnQgMgdh5tfm+gtNNBvim0dqki2AN6ABwK0Garnd0lU3BAxM2YJ2k/C4oxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLk12YyBUZXN0czpJbnRlcm5hbDpNeVRlc3RlZC5NdmMu";

                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new []{ license }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), "MyTested.WebApi.Tests");
                },
                "License is not valid for 'MyTested.WebApi.Tests' test project");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldThrowExceptionWithInternalType()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    var license = "1-03rl5Irwh4ZsynriqaencPeH4C+e4JtKFx6CehuklqJ58/N8OEePi19h332aQ5DrYPS3715dVEbbWDxpkOm0e9wge9W / q01AfXEN2ytSGMPF8wEuLX6ZC0Lo698YKWhxQxtq4k6ls3XiHxwYl7XKD8n / rAntd6xY8lJYp8S3QtMxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkludGVybmFsOk15VGVzdGVkLkFzcE5ldENvcmUuTXZjLg==";

                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new []{ license }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");
                },
                "License is for internal use only");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldSetValidPerpetualLicense()
        {
            var licenseDetails = new LicenseDetails
            {
                Id = 1,
                Type = LicenseType.Developer,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc.",
                ExpirationType = ExpirationType.Perpetual
            };

            var license = "1-IRaNRwlovf7moJnDcQCJW8JDq++p8/1hTNsRnBRLDGkd6HidiJ3OEzpFdwmlDacikCv5oRBisRkJ8edjqx1R21VA+SxCgpGHJE2ftOBpV1OBysguNUSIKJyte2heP3xD4tY1BQNh0vcVhXJDcE3qImhodZmi1aXJ19SK5f4JRA8xOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46UGVycGV0dWFs";

            LicenseValidator.ClearLicenseDetails();
            LicenseValidator.Validate(new []{ license }, new DateTime(2018, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

            Assert.True(LicenseValidator.HasValidLicense);

            var registeredLicense = LicenseValidator.GetLicenseDetails().FirstOrDefault();

            Assert.NotNull(registeredLicense);
            Assert.Equal(licenseDetails.Id, registeredLicense.Id);
            Assert.Equal(licenseDetails.Type, registeredLicense.Type);
            Assert.Equal(licenseDetails.User, registeredLicense.User);
            Assert.Equal(licenseDetails.InformationDetails, registeredLicense.InformationDetails);
            Assert.Equal(licenseDetails.ExpiryDate, registeredLicense.ExpiryDate);
            Assert.Equal(licenseDetails.NamespacePrefix, registeredLicense.NamespacePrefix);
            Assert.Equal(licenseDetails.ExpirationType, registeredLicense.ExpirationType);
        }

        [Fact]
        public void ValidateShouldSetValidSubscriptionLicense()
        {
            var licenseDetails = new LicenseDetails
            {
                Id = 1,
                Type = LicenseType.Developer,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc.",
                ExpirationType = ExpirationType.Subscription
            };

            var license = "1-TE6MO5GnjYwR3DcbT8rIXfjk9e0+ZPOb+c27A7pA83aNY4IQNBhgnIf4eUfy0MBvyXYrh9rkLa1hpGnrGu2TMZSoYxeZS07rM7WCqxzd2xXqfzuTAxsO1yNiEo/UwvVZUqz6s3nunKXn1m0b5dbKrsu7hxmWf8P8L2DhCDD09/sxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46U3Vic2NyaXB0aW9u";

            LicenseValidator.ClearLicenseDetails();
            LicenseValidator.Validate(new[] { license }, new DateTime(2016, 10, 10), new DateTime(2018, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

            Assert.True(LicenseValidator.HasValidLicense);

            var registeredLicense = LicenseValidator.GetLicenseDetails().FirstOrDefault();

            Assert.NotNull(registeredLicense);
            Assert.Equal(licenseDetails.Id, registeredLicense.Id);
            Assert.Equal(licenseDetails.Type, registeredLicense.Type);
            Assert.Equal(licenseDetails.User, registeredLicense.User);
            Assert.Equal(licenseDetails.InformationDetails, registeredLicense.InformationDetails);
            Assert.Equal(licenseDetails.ExpiryDate, registeredLicense.ExpiryDate);
            Assert.Equal(licenseDetails.NamespacePrefix, registeredLicense.NamespacePrefix);
            Assert.Equal(licenseDetails.ExpirationType, registeredLicense.ExpirationType);
        }

        [Fact]
        public void ValidateShouldThrowExceptionIfTwoLicensesAreProvidedAndOneIsInvalid()
        {
            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    var firstLicense = "1-mKwoA4cwRkRKioJ70fh6WcL5Ty+K2Kn32oftC2KFqw5kimAMYrTeKwOX25e55GcTMy/cB8Ssa/SJmFFq6OUSXpgc7TsG6vNA7jE8lYbpzQcaIumX9tu0Kr2655zUne0GcPP0 + Y6OqLnRnOiq5keErGSyohcfZmzkRXoHAEEXR68xOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkZ1bGw6TXlUZXN0ZWQuQXNwTmV0Q29yZS5NdmMu";
                    var secondLicense = "1-03rl5Irwh4ZsynriqaencPeH4C+e4JtKFx6CehuklqJ58/N8OEePi19h332aQ5DrYPS3715dVEbbWDxpkOm0e9wge9W / q01AfXEN2ytSGMPF8wEuLX6ZC0Lo698YKWhxQxtq4k6ls3XiHxwYl7XKD8n / rAntd6xY8lJYp8S3QtMxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkludGVybmFsOk15VGVzdGVkLkFzcE5ldENvcmUuTXZjLg==";

                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { firstLicense, secondLicense }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");
                },
                "License is for internal use only");

            Assert.False(LicenseValidator.HasValidLicense);
        }
        
        [Fact]
        public void ValidateShouldSetValidLicenses()
        {
            // Legacy license.
            var firstLicenseDetails = new LicenseDetails
            {
                Id = 1,
                Type = LicenseType.Developer,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc."
            };

            // Perpetual license.
            var secondLicenseDetails = new LicenseDetails
            {
                Id = 2,
                Type = LicenseType.Developer,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc.",
                ExpirationType = ExpirationType.Perpetual
            };

            // Subscription license.
            var thirdLicenseDetails = new LicenseDetails
            {
                Id = 3,
                Type = LicenseType.Developer,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc.",
                ExpirationType = ExpirationType.Subscription
            };

            var firstLicense = "1-rXDHzH/rR8IN83Qmtpyf8vsAd4cPfSd/roXjngSxf12fuEY5+nk/evBTOD3xcOQSrEQLte3BcpH/RxIxDaSmZU11zV4jafnJ4N0u+yfNmTvRhVAtGuVCPj1UgYva64QK5fsPbOXBXq1c9+ccfWoWuB7nuRPaJvUlv/dcHQAy3cUxOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy4=";
            var secondLicense = "2-LRJiOgmTuD8r3kD2XWziWyBJ2UTk7bxCsWkEaSuJ4cMcFnvyCkMB1mqVeVVIOOZxiXlS5bmlDKDwtFzCKGckzbSmij1wdHVmbBHIGCw1bRU2IBTMIWrLzHgOXXEGE7GsQhOxzcivVgg6gc7UBYtolvX+9TtwTQLR50eYEgaEd/AyOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46UGVycGV0dWFs";
            var thirdLicense = "3-uBS/3IdYWKeMNBd2Gnvb6VKisq/wcNmGfayo+I5nCH33G2pBHCMO+EerVymQA6yiPUz2kcf/ioo0nh3BwmhWDSPNyt/7Fhoie8zKdbNTLc3ZUTISUZzVYRbbAYv6Bngb6vPjnqMvlXiAGxXC8algqaKEG47j7vVUV24DgfGgHO4zOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkRldmVsb3BlcjpNeVRlc3RlZC5Bc3BOZXRDb3JlLk12Yy46U3Vic2NyaXB0aW9u";

            LicenseValidator.ClearLicenseDetails();
            LicenseValidator.Validate(new[] { firstLicense, secondLicense, thirdLicense }, new DateTime(2016, 10, 10), new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

            Assert.True(LicenseValidator.HasValidLicense);

            var registeredFirstLicense = LicenseValidator.GetLicenseDetails().FirstOrDefault();
            var registeredSecondLicense = LicenseValidator.GetLicenseDetails().ElementAt(1);
            var registeredThirdLicense = LicenseValidator.GetLicenseDetails().LastOrDefault();

            Assert.NotNull(registeredFirstLicense);
            Assert.Equal(firstLicenseDetails.Id, registeredFirstLicense.Id);
            Assert.Equal(firstLicenseDetails.Type, registeredFirstLicense.Type);
            Assert.Equal(firstLicenseDetails.User, registeredFirstLicense.User);
            Assert.Equal(firstLicenseDetails.InformationDetails, registeredFirstLicense.InformationDetails);
            Assert.Equal(firstLicenseDetails.ExpiryDate, registeredFirstLicense.ExpiryDate);
            Assert.Equal(firstLicenseDetails.NamespacePrefix, registeredFirstLicense.NamespacePrefix);

            Assert.NotNull(registeredSecondLicense);
            Assert.Equal(secondLicenseDetails.Id, registeredSecondLicense.Id);
            Assert.Equal(secondLicenseDetails.Type, registeredSecondLicense.Type);
            Assert.Equal(secondLicenseDetails.User, registeredSecondLicense.User);
            Assert.Equal(secondLicenseDetails.InformationDetails, registeredSecondLicense.InformationDetails);
            Assert.Equal(secondLicenseDetails.ExpiryDate, registeredSecondLicense.ExpiryDate);
            Assert.Equal(secondLicenseDetails.NamespacePrefix, registeredSecondLicense.NamespacePrefix);

            Assert.NotNull(registeredThirdLicense);
            Assert.Equal(thirdLicenseDetails.Id, registeredThirdLicense.Id);
            Assert.Equal(thirdLicenseDetails.Type, registeredThirdLicense.Type);
            Assert.Equal(thirdLicenseDetails.User, registeredThirdLicense.User);
            Assert.Equal(thirdLicenseDetails.InformationDetails, registeredThirdLicense.InformationDetails);
            Assert.Equal(thirdLicenseDetails.ExpiryDate, registeredThirdLicense.ExpiryDate);
            Assert.Equal(thirdLicenseDetails.NamespacePrefix, registeredThirdLicense.NamespacePrefix);
        }

        [Fact]
        public void PublicKeyShouldNotBeEnoughToForgeLicense()
        {
            var licenseDetails = new LicenseDetails
            {
                Id = 1,
                Type = LicenseType.Developer,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc."
            };

            var licenseDetailsAsBytes = licenseDetails.GetSignificateData();

            var cryptoProvider = new RSACryptoServiceProvider(1024)
            {
                PersistKeyInCsp = false
            };

            cryptoProvider.ImportCspBlob(Convert.FromBase64String("BgIAAACkAABSU0ExAAQAAAEAAQD5Hv5iOBm7GKs7GRQBwlYlbNsJZOL8PfX+rQuKK+tO4JquMo0ScaQiz4duyfjp1/dsrNAsRnRoDfIvaL75YYezaEaoRXldI83CjDPU92chrLUkaQdFtY1XyiBt6lJREkD6LBSRSJD9Z9Aeaqssl8fbaJpTk5wppIImhEvHrJ3F6g=="));

            Exception caughtException = null;

            try
            {
                cryptoProvider.SignData(licenseDetailsAsBytes, SHA1.Create());
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<CryptographicException>(caughtException);
        }
    }
}
