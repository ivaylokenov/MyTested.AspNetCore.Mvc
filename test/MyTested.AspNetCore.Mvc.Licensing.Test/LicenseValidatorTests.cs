namespace MyTested.AspNetCore.Mvc.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using Licensing;
    using Setups;
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
                    LicenseValidator.Validate(null, new DateTime(2016, 10, 10), string.Empty);
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
                    LicenseValidator.Validate(new string[] { null }, new DateTime(2016, 10, 10), string.Empty);
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
                    LicenseValidator.Validate(new[] { string.Empty }, new DateTime(2016, 10, 10), null);
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
                    LicenseValidator.Validate(new[] { "1-2-3" }, new DateTime(2016, 10, 10), string.Empty);
                },
                "License text is invalid");

            Assert.False(LicenseValidator.HasValidLicense);

            Test.AssertException<InvalidLicenseException>(
                () =>
                {
                    LicenseValidator.ClearLicenseDetails();
                    LicenseValidator.Validate(new[] { "1" }, new DateTime(2016, 10, 10), string.Empty);
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
                    LicenseValidator.Validate(new[] { "test-2" }, new DateTime(2016, 10, 10), string.Empty);
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
                    LicenseValidator.Validate(new[] { "1-A" }, new DateTime(2016, 10, 10), string.Empty);
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
                    LicenseValidator.Validate(new[] { new string(wrongLicense.ToArray()) }, new DateTime(2016, 10, 10), string.Empty);
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
                    LicenseValidator.Validate(new[] { new string(licenseArray) }, new DateTime(2016, 10, 10), string.Empty);
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
                    LicenseValidator.Validate(new[] { license }, new DateTime(2018, 10, 10), string.Empty);
                },
                "License is not valid for this version of My Tested ASP.NET Core MVC. License expired on 2017-10-15. This version of My Tested ASP.NET Core MVC was released on 2018-10-10");

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
                    LicenseValidator.Validate(new []{ license }, new DateTime(2016, 10, 10), "MyTested.WebApi.Tests");
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
                    LicenseValidator.Validate(new []{ license }, new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");
                },
                "License is for internal use only");

            Assert.False(LicenseValidator.HasValidLicense);
        }

        [Fact]
        public void ValidateShouldSetValidLicense()
        {
            var licenseDetails = new LicenseDetails
            {
                Id = 1,
                Type = LicenseType.Full,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc."
            };

            var license = "1-mKwoA4cwRkRKioJ70fh6WcL5Ty+K2Kn32oftC2KFqw5kimAMYrTeKwOX25e55GcTMy/cB8Ssa/SJmFFq6OUSXpgc7TsG6vNA7jE8lYbpzQcaIumX9tu0Kr2655zUne0GcPP0 + Y6OqLnRnOiq5keErGSyohcfZmzkRXoHAEEXR68xOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkZ1bGw6TXlUZXN0ZWQuQXNwTmV0Q29yZS5NdmMu";

            LicenseValidator.ClearLicenseDetails();
            LicenseValidator.Validate(new []{ license }, new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

            Assert.True(LicenseValidator.HasValidLicense);

            var registeredLicense = LicenseValidator.GetLicenseDetails().FirstOrDefault();

            Assert.NotNull(registeredLicense);
            Assert.Equal(licenseDetails.Id, registeredLicense.Id);
            Assert.Equal(licenseDetails.Type, registeredLicense.Type);
            Assert.Equal(licenseDetails.User, registeredLicense.User);
            Assert.Equal(licenseDetails.InformationDetails, registeredLicense.InformationDetails);
            Assert.Equal(licenseDetails.ExpiryDate, registeredLicense.ExpiryDate);
            Assert.Equal(licenseDetails.NamespacePrefix, registeredLicense.NamespacePrefix);
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
                    LicenseValidator.Validate(new[] { firstLicense, secondLicense }, new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");
                },
                "License is for internal use only");

            Assert.False(LicenseValidator.HasValidLicense);
        }


        [Fact]
        public void ValidateShouldSetValidLicenses()
        {
            var firstLicenseDetails = new LicenseDetails
            {
                Id = 1,
                Type = LicenseType.Full,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc."
            };

            var secondLicenseDetails = new LicenseDetails
            {
                Id = 2,
                Type = LicenseType.Full,
                User = "admin@mytestedasp.net",
                InformationDetails = "MyTested.AspNetCore.Mvc Tests",
                ExpiryDate = new DateTime(2017, 10, 15),
                NamespacePrefix = "MyTested.AspNetCore.Mvc."
            };

            var firstLicense = "1-mKwoA4cwRkRKioJ70fh6WcL5Ty+K2Kn32oftC2KFqw5kimAMYrTeKwOX25e55GcTMy/cB8Ssa/SJmFFq6OUSXpgc7TsG6vNA7jE8lYbpzQcaIumX9tu0Kr2655zUne0GcPP0 + Y6OqLnRnOiq5keErGSyohcfZmzkRXoHAEEXR68xOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkZ1bGw6TXlUZXN0ZWQuQXNwTmV0Q29yZS5NdmMu";
            var secondLicense = "2-dO7yyeqUFb2FRmtqqZbB+fYAThKkGftht8Ky7PB0Gi1GxW2ZlROg1a+fZjJnzBxEgBSup0rSxt4n73zKmcTv4SYrR9r0DtnWxX8DN8BLDk9hX1 + whB5UMYiQmf / uxDizUWn9pxdbMRrq9JXfxyRhOS4vxVhGIEPgH03vZsCWLOwyOjIwMTctMTAtMTU6YWRtaW5AbXl0ZXN0ZWRhc3AubmV0Ok15VGVzdGVkLkFzcE5ldENvcmUuTXZjIFRlc3RzOkZ1bGw6TXlUZXN0ZWQuQXNwTmV0Q29yZS5NdmMu";

            LicenseValidator.ClearLicenseDetails();
            LicenseValidator.Validate(new[] { firstLicense, secondLicense }, new DateTime(2016, 10, 10), "MyTested.AspNetCore.Mvc.Tests");

            Assert.True(LicenseValidator.HasValidLicense);

            var registeredFirstLicense = LicenseValidator.GetLicenseDetails().FirstOrDefault();
            var registeredSecondLicense = LicenseValidator.GetLicenseDetails().LastOrDefault();

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
        }

        [Fact]
        public void PublicKeyShouldNotBeEnoughToForgeLicense()
        {
            var licenseDetails = new LicenseDetails
            {
                Id = 1,
                Type = LicenseType.Full,
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
