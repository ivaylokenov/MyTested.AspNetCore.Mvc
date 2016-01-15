namespace MyTested.Mvc
{
    /// <summary>
    /// Contains default authentication header schemes.
    /// </summary>
    public class AuthenticationScheme
    {
        /// <summary>
        /// Anonymous authentication header scheme.
        /// </summary>
        public const string Anonymous = "Anonymous";

        /// <summary>
        /// Basic authentication header scheme.
        /// </summary>
        public const string Basic = "Basic";

        /// <summary>
        /// Digest authentication header scheme.
        /// </summary>
        public const string Digest = "Digest";

        /// <summary>
        /// NTLM authentication header scheme.
        /// </summary>
        public const string NTLM = "NTLM";

        /// <summary>
        /// Negotiate authentication header scheme.
        /// </summary>
        public const string Negotiate = "Negotiate";
    }
}
