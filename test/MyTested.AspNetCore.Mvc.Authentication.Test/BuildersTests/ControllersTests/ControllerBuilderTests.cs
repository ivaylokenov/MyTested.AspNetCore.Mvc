namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;
    using Setups;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValues()
        {
            MyController<MvcController>
                .Instance()
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<Controller>(controller =>
                {
                    var controllerUser = controller.User;

                    Assert.False(controllerUser.IsInRole("Any"));
                    Assert.Equal("TestUser", controllerUser.Identity.Name);
                    Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
                    Assert.True(controllerUser.Identity.IsAuthenticated);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilder()
        {
            MyController<MvcController>
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
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<Controller>(controller =>
                {
                    var controllerUser = controller.User;

                    Assert.Equal("NewUserName", controllerUser.Identity.Name);
                    Assert.Equal("Custom", controllerUser.Identity.AuthenticationType);
                    Assert.True(controllerUser.Identity.IsAuthenticated);
                    Assert.True(controllerUser.IsInRole("NormalUser"));
                    Assert.True(controllerUser.IsInRole("Moderator"));
                    Assert.True(controllerUser.IsInRole("Administrator"));
                    Assert.True(controllerUser.IsInRole("SuperUser"));
                    Assert.True(controllerUser.IsInRole("MegaUser"));
                    Assert.False(controllerUser.IsInRole("AnotherRole"));
                });
        }
        
        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValuesForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    var controllerUser = controller.CustomHttpContext.User;

                    Assert.False(controllerUser.IsInRole("Any"));
                    Assert.Equal("TestUser", controllerUser.Identity.Name);
                    Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
                    Assert.True(controllerUser.Identity.IsAuthenticated);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilderForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
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
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<FullPocoController>(controller =>
                {
                    var controllerUser = controller.CustomHttpContext.User;

                    Assert.Equal("NewUserName", controllerUser.Identity.Name);
                    Assert.Equal("Custom", controllerUser.Identity.AuthenticationType);
                    Assert.True(controllerUser.Identity.IsAuthenticated);
                    Assert.True(controllerUser.IsInRole("NormalUser"));
                    Assert.True(controllerUser.IsInRole("Moderator"));
                    Assert.True(controllerUser.IsInRole("Administrator"));
                    Assert.True(controllerUser.IsInRole("SuperUser"));
                    Assert.True(controllerUser.IsInRole("MegaUser"));
                    Assert.False(controllerUser.IsInRole("AnotherRole"));
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
