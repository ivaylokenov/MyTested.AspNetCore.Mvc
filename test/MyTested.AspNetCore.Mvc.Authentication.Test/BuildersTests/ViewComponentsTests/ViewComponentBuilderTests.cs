namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Setups.ViewComponents;
    using System.Security.Claims;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValues()
        {
            MyViewComponent<UserComponent>
                .Instance()
                .WithAuthenticatedUser()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .ShouldPassForThe<UserComponent>(viewComponent =>
                {
                    var user = viewComponent.User as ClaimsPrincipal;

                    Assert.NotNull(user);
                    Assert.Equal(false, user.IsInRole("Any"));
                    Assert.Equal("TestUser", user.Identity.Name);
                    Assert.True(user.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", user.Identity.AuthenticationType);
                    Assert.Equal(true, user.Identity.IsAuthenticated);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilder()
        {
            MyViewComponent<UserComponent>
                .Instance()
                .WithAuthenticatedUser(user => user
                    .WithUsername("NewUserName")
                    .WithAuthenticationType("Custom")
                    .InRole("NormalUser")
                    .AndAlso()
                    .InRoles("Moderator", "Administrator")
                    .InRoles(new[]
                    {
                        "SuperUser",
                        "MegaUser"
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .ShouldPassForThe<UserComponent>(viewComponent =>
                {
                    var user = viewComponent.User as ClaimsPrincipal;

                    Assert.Equal("NewUserName", user.Identity.Name);
                    Assert.Equal("Custom", user.Identity.AuthenticationType);
                    Assert.Equal(true, user.Identity.IsAuthenticated);
                    Assert.Equal(true, user.IsInRole("NormalUser"));
                    Assert.Equal(true, user.IsInRole("Moderator"));
                    Assert.Equal(true, user.IsInRole("Administrator"));
                    Assert.Equal(true, user.IsInRole("SuperUser"));
                    Assert.Equal(true, user.IsInRole("MegaUser"));
                    Assert.Equal(false, user.IsInRole("AnotherRole"));
                });
        }
        
    }
}
