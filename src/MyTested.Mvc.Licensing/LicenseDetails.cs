namespace MyTested.Mvc.Licensing
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    internal class LicenseDetails
    {
        public int Id { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string User { get; set; }

        public string InformationDetails { get; set; }

        public string Type { get; set; }

        public string NamespacePrefix { get; set; }

        public byte[] GetSignificateData()
        {
            this.NamespacePrefix = this.NamespacePrefix ?? ".";
            if (!this.NamespacePrefix.EndsWith("."))
            {
                throw new InvalidLicenseException("Project namespace must end with the '.' character.");
            }

            var data = new[]
            {
                this.Id.ToString(CultureInfo.InvariantCulture),
                this.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                this.User,
                this.InformationDetails,
                this.Type,
                this.NamespacePrefix
            };

            if (data.Any(d => d == null || d.Any(s => s == ':')))
            {
                throw new InvalidLicenseException("License details cannot contain empty values or ':'.");
            }
            
            return Encoding.UTF8.GetBytes(string.Join(":", data));
        }
    }
}
