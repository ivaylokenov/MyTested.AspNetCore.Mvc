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
                .WithUser()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .ShouldPassForThe<UserComponent>(viewComponent =>
                {
                    var user = viewComponent.User as ClaimsPrincipal;

                    Assert.NotNull(user);
                    Assert.False(user.IsInRole("Any"));
                    Assert.Equal("TestUser", user.Identity.Name);
                    Assert.True(user.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", user.Identity.AuthenticationType);
                    Assert.True(user.Identity.IsAuthenticated);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilder()
        {
            MyViewComponent<UserComponent>
                .Instance()
                .WithUser(user => user
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
                    Assert.True(user.Identity.IsAuthenticated);
                    Assert.True(user.IsInRole("NormalUser"));
                    Assert.True(user.IsInRole("Moderator"));
                    Assert.True(user.IsInRole("Administrator"));
                    Assert.True(user.IsInRole("SuperUser"));
                    Assert.True(user.IsInRole("MegaUser"));
                    Assert.False(user.IsInRole("AnotherRole"));
                });
        }
        
    }
}
