namespace MyTested.AspNetCore.Mvc.Licensing
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    internal static class LicenseValidator
    {
        private const string PublicKey = "BgIAAACkAABSU0ExAAQAAAEAAQD5Hv5iOBm7GKs7GRQBwlYlbNsJZOL8PfX+rQuKK+tO4JquMo0ScaQiz4duyfjp1/dsrNAsRnRoDfIvaL75YYezaEaoRXldI83CjDPU92chrLUkaQdFtY1XyiBt6lJREkD6LBSRSJD9Z9Aeaqssl8fbaJpTk5wppIImhEvHrJ3F6g==";
        private const int SigningDataLength = 128;

        private static ICollection<LicenseDetails> registeredLicenses;

        public static bool HasValidLicense => registeredLicenses != null && registeredLicenses.Any();

        public static bool Validate(IEnumerable<string> licenses, DateTime releaseDate, string projectNamespace)
        {
            if (licenses == null || !licenses.Any())
            {
                throw new InvalidLicenseException("No license provided");
            }

            registeredLicenses = new List<LicenseDetails>();

            foreach (var license in licenses)
            {
                try
                {
                    Validate(license, releaseDate, projectNamespace);
                }
                catch (Exception)
                {
                    registeredLicenses = null;
                    throw;
                }
            }

            return true;
        }

        public static IEnumerable<LicenseDetails> GetLicenseDetails()
        {
            if (registeredLicenses == null || !registeredLicenses.Any())
            {
                return registeredLicenses;
            }

            var licenses = new List<LicenseDetails>();

            foreach (var registeredLicense in registeredLicenses)
            {
                licenses.Add(new LicenseDetails
                {
                    Id = registeredLicense.Id,
                    ExpiryDate = registeredLicense.ExpiryDate,
                    User = registeredLicense.User,
                    Type = registeredLicense.Type,
                    InformationDetails = registeredLicense.InformationDetails,
                    NamespacePrefix = registeredLicense.NamespacePrefix
                });
            }

            return licenses;
        }

        public static void ClearLicenseDetails()
        {
            registeredLicenses = null;
        }

        private static void Validate(string license, DateTime releaseDate, string projectNamespace)
        {
            if (license == null || projectNamespace == null)
            {
                throw new InvalidLicenseException("License and project namespace cannot be null or empty");
            }

            var licenseParts = license.Trim().Split('-');
            if (licenseParts.Length != 2)
            {
                throw new InvalidLicenseException("License text is invalid");
            }

            var licenseIdData = licenseParts[0];
            var licenseDetailsData = licenseParts[1];

            int licenseId;
            if (!int.TryParse(licenseIdData, NumberStyles.Integer, CultureInfo.InvariantCulture, out licenseId))
            {
                throw new InvalidLicenseException("License text is invalid");
            }

            byte[] licenseAsBytes;
            try
            {
                licenseAsBytes = Convert.FromBase64String(licenseDetailsData);
            }
            catch
            {
                throw new InvalidLicenseException("License text is invalid");
            }

            var stream = new MemoryStream(licenseAsBytes, SigningDataLength, licenseAsBytes.Length - SigningDataLength);
            var parsedLicenseDetails = new StreamReader(stream).ReadToEnd();

            var parsedLicenseParts = parsedLicenseDetails.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (parsedLicenseParts.Length != 6)
            {
                throw new InvalidLicenseException("License details are invalid");
            }

            DateTime expiryDate;
            if (!DateTime.TryParseExact(parsedLicenseParts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate))
            {
                throw new InvalidLicenseException("License expiry date is invalid");
            }

            int parsedLicenseId;
            if (!int.TryParse(parsedLicenseParts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedLicenseId))
            {
                throw new InvalidLicenseException("License ID is invalid");
            }

            var licenseDetails = new LicenseDetails
            {
                Id = parsedLicenseId,
                ExpiryDate = expiryDate,
                User = parsedLicenseParts[2],
                InformationDetails = parsedLicenseParts[3],
                Type = parsedLicenseParts[4],
                NamespacePrefix = parsedLicenseParts[5]
            };

            var parsedSigningData = licenseDetails.GetSignificateData();

            var signingData = new byte[SigningDataLength];
            Array.Copy(licenseAsBytes, signingData, SigningDataLength);
            
#if NETSTANDARD1_4
            var cryptoProvider = RSA.Create();
            cryptoProvider.KeySize = 1024;

            var parameters = CryptographyHelpers.ToRSAParameters(Convert.FromBase64String(PublicKey), false);
            cryptoProvider.ImportParameters(parameters);

            var dataVerified = cryptoProvider.VerifyData(parsedSigningData, signingData, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
#else
            var cryptoProvider = new RSACryptoServiceProvider(1024)
            {
                PersistKeyInCsp = false
            };

            cryptoProvider.ImportCspBlob(Convert.FromBase64String(PublicKey));

            var dataVerified = cryptoProvider.VerifyData(parsedSigningData, SHA1.Create(), signingData);
#endif
            if (!dataVerified)
            {
                throw new InvalidLicenseException("License text does not match signature");
            }

            if (licenseDetails.Id != licenseId)
            {
                throw new InvalidLicenseException("License ID does not match signature license ID");
            }

            if (licenseDetails.ExpiryDate < releaseDate)
            {
                throw new InvalidLicenseException($"License is not valid for this version of My Tested ASP.NET Core MVC. License expired on {licenseDetails.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}. This version of My Tested ASP.NET Core MVC was released on {releaseDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}");
            }

            if (licenseDetails.Type != LicenseType.Enterprise && licenseDetails.NamespacePrefix != "." && !projectNamespace.StartsWith(licenseDetails.NamespacePrefix))
            {
                throw new InvalidLicenseException($"License is not valid for '{projectNamespace}' test project");
            }

            if (licenseDetails.Type == LicenseType.Internal)
            {
                throw new InvalidLicenseException("License is for internal use only");
            }

            registeredLicenses.Add(licenseDetails);
        }
    }
}
