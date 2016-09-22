namespace MyTested.AspNetCore.Mvc.Licensing
{
    internal class LicenseType
    {
        public const string Internal = "Internal"; // testing purposes, throws exception
        public const string Contribution = "Contribution"; // given for free for various contributions
        public const string Developer = "Developer"; // given to a single developer, can be purchased through https://mytestedasp.net
        public const string Application = "Application"; // given to an application with unlimited developers, can be requested through https://mytestedasp.net
        public const string Enterprise = "Enterprise"; // given to a company with unlimited developers and applications, can be requested through https://mytestedasp.net
    }
}
