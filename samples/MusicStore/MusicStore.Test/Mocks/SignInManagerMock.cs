namespace MusicStore.Test.Mocks
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Models;

    public class SignInManagerMock : SignInManager<ApplicationUser>
    {
        internal const string ValidUser = "Valid@valid.com";
        internal const string TwoFactorRequired = "TwoFactor@invalid.com";
        internal const string LockedOutUser = "Locked@invalid.com";

        public SignInManagerMock(
            UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<ApplicationUser>> logger, 
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (userName == ValidUser && password == ValidUser)
            {
                return Task.FromResult(SignInResult.Success);
            }

            if (userName == TwoFactorRequired && password == TwoFactorRequired)
            {
                return Task.FromResult(SignInResult.TwoFactorRequired);
            }

            if (userName == LockedOutUser && password == LockedOutUser)
            {
                return Task.FromResult(SignInResult.LockedOut);
            }

            return Task.FromResult(SignInResult.Failed);
        }
    }
}
