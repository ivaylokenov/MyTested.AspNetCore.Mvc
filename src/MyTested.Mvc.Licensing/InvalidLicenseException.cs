namespace MyTested.Mvc.Licensing
{
    using System;

    internal class InvalidLicenseException : Exception
    {
        public InvalidLicenseException(string message)
            : base(message)
        {
        }
    }
}
