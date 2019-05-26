namespace MyTested.AspNetCore.Mvc
{
    using System.Security.Claims;

    public static class TestUser
    {
        /// <summary>
        /// Default <see cref="ClaimsIdentity"/> identifier (Id) - "TestId".
        /// </summary>
        public const string Identifier = "TestId";

        /// <summary>
        /// Default <see cref="ClaimsIdentity"/> username - "TestUser".
        /// </summary>
        public const string Username = "TestUser";

        /// <summary>
        /// Default <see cref="ClaimsIdentity"/> authentication type - "Passport".
        /// </summary>
        public const string AuthenticationType = "Passport";
    }
}
