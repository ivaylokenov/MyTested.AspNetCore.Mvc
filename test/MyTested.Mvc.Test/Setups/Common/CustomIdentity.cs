namespace MyTested.Mvc.Test.Setups.Common
{
    using System.Security.Principal;

    public class CustomIdentity : IIdentity
    {
        private readonly string username;

        public CustomIdentity(string username)
        {
            this.username = username;
        }

        public string AuthenticationType => "CustomAuthenticationType";

        public bool IsAuthenticated => true;

        public string Name => this.username;
    }
}
