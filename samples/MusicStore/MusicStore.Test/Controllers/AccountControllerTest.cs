namespace MusicStore.Test.Controllers
{
    using Microsoft.Extensions.Caching.Memory;
    using Mocks;
    using Models;
    using MusicStore.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using HttpMethod = System.Net.Http.HttpMethod;

    public class AccountControllerTest
    {
        [Fact]
        public void AccountControllerShouldHaveAuthorizeFilter()
        {
            MyMvc
                .Controller<AccountController>()
                .ShouldHave()
                .Attributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void GetLoginShouldHaveAllowAnonymousFilter()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(With.No<string>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .AllowingAnonymousRequests());
        }

        [Fact]
        public void GetLoginShouldHaveCorrectViewBagEntriesWithReturnUrlAndShouldReturnCorrectView()
        {
            const string returnUrl = "MyReturnUrl";

            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(returnUrl))
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry("ReturnUrl", returnUrl))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void PostLoginShouldHaveCorrectActionFilters()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    With.Default<LoginViewModel>(),
                    With.No<string>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .AllowingAnonymousRequests()
                    .ValidatingAntiForgeryToken());
        }

        [Fact]
        public void PostLoginShouldReturnDefaultViewWithInvalidModel()
        {
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    With.Default<LoginViewModel>(),
                    With.No<string>()))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<LoginViewModel>()
                    .ContainingErrorFor(m => m.Email)
                    .ContainingErrorFor(m => m.Password))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void PostLoginShouldReturnRedirectToActionWithValidUserName()
        {
            var model = new LoginViewModel
            {
                Email = SignInManagerMock.ValidUser,
                Password = SignInManagerMock.ValidUser
            };
            
            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    model,
                    With.No<string>()))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index(
                        With.No<MusicStoreContext>(),
                        With.No<IMemoryCache>())));
        }
        
        [Fact]
        public void PostLoginShouldReturnRedirectToLocalWithValidUserNameAndReturnUrl()
        {
            var model = new LoginViewModel
            {
                Email = SignInManagerMock.ValidUser,
                Password = SignInManagerMock.ValidUser
            };

            var returnUrl = "/Store/Index";

            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    model,
                    returnUrl))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .ToUrl(returnUrl));
        }
        
        [Fact]
        public void PostLoginShouldReturnRedirectWithTwoFactor()
        {
            var model = new LoginViewModel
            {
                Email = SignInManagerMock.TwoFactorRequired,
                Password = SignInManagerMock.TwoFactorRequired,
                RememberMe = true
            };

            var returnUrl = "/Store/Index";

            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    model,
                    returnUrl))
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<AccountController>(c => c.SendCode(model.RememberMe, returnUrl)));
        }
        
        [Fact]
        public void PostLoginShouldReturnViewWithLockout()
        {
            var model = new LoginViewModel
            {
                Email = SignInManagerMock.LockedOutUser,
                Password = SignInManagerMock.LockedOutUser
            };

            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    model,
                    With.No<string>()))
                .ShouldReturn()
                .View("Lockout");
        }

        [Fact]
        public void PostLoginShouldReturnReturnViewWithInvalidCredentials()
        {
            var model = new LoginViewModel
            {
                Email = "Invalid@invalid.com",
                Password = "Invalid"
            };

            MyMvc
                .Controller<AccountController>()
                .Calling(c => c.Login(
                    model,
                    With.No<string>()))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<ValidationSummary>()
                    .ContainingError(string.Empty)
                    .ThatEquals("Invalid login attempt."))
                .AndAlso()
                .ShouldReturn()
                .View(model);
        }
    }
}
