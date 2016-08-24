namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

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

                    Assert.Equal(false, controllerUser.IsInRole("Any"));
                    Assert.Equal("TestUser", controllerUser.Identity.Name);
                    Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
                    Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
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
                    Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
                    Assert.Equal(true, controllerUser.IsInRole("NormalUser"));
                    Assert.Equal(true, controllerUser.IsInRole("Moderator"));
                    Assert.Equal(true, controllerUser.IsInRole("Administrator"));
                    Assert.Equal(true, controllerUser.IsInRole("SuperUser"));
                    Assert.Equal(true, controllerUser.IsInRole("MegaUser"));
                    Assert.Equal(false, controllerUser.IsInRole("AnotherRole"));
                });
        }
        
        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValuesForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

                    Assert.Equal(false, controllerUser.IsInRole("Any"));
                    Assert.Equal("TestUser", controllerUser.Identity.Name);
                    Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
                    Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilderForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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
                    Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
                    Assert.Equal(true, controllerUser.IsInRole("NormalUser"));
                    Assert.Equal(true, controllerUser.IsInRole("Moderator"));
                    Assert.Equal(true, controllerUser.IsInRole("Administrator"));
                    Assert.Equal(true, controllerUser.IsInRole("SuperUser"));
                    Assert.Equal(true, controllerUser.IsInRole("MegaUser"));
                    Assert.Equal(false, controllerUser.IsInRole("AnotherRole"));
                });

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
