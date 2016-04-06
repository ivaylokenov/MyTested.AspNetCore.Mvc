namespace MyTested.Mvc.Test.BuildersTests.AuthenticationTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class ClaimsPrincipalBuilderTests
    {
        [Fact]
        public void WithNameTypeShouldOverrideDefaultClaimType()
        {
            MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithNameType("CustomUsername")
                    .WithUsername("MyUsername"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var claim = controller.User.Claims.FirstOrDefault(c => c.Type == "CustomUsername");

                    Assert.NotNull(claim);
                    Assert.Equal("MyUsername", claim.Value);
                });
        }
        
        [Fact]
        public void WithRoleTypeShouldOverrideDefaultClaimType()
        {
            MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithRoleType("CustomRole")
                    .InRole("MyRole"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var claim = controller.User.Claims.FirstOrDefault(c => c.Type == "CustomRole");

                    Assert.NotNull(claim);
                    Assert.Equal("MyRole", claim.Value);
                });
        }

        [Fact]
        public void WithoutIdentifierShouldSetCorrectDefaultIdentifier()
        {
            MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var claim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    Assert.NotNull(claim);
                    Assert.Equal("TestId", claim.Value);
                });
        }

        [Fact]
        public void WithIdentifierShouldSetCorrectIdentifier()
        {
            MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithIdentifier("TestingId"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var claim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    Assert.NotNull(claim);
                    Assert.Equal("TestingId", claim.Value);
                });
        }

        [Fact]
        public void WithClaimShouldSetCorrectClaim()
        {
            MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithClaim("MyClaim", "MyValue"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var claim = controller.User.Claims.FirstOrDefault(c => c.Type == "MyClaim");

                    Assert.NotNull(claim);
                    Assert.Equal("MyValue", claim.Value);
                });
        }

        [Fact]
        public void WithClaimAsTypeShouldSetCorrectClaim()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithClaim(new Claim("MyClaim", "MyValue")))
                .AndProvideTheController();

            var claim = controller.User.Claims.FirstOrDefault(c => c.Type == "MyClaim");

            Assert.NotNull(claim);
            Assert.Equal("MyValue", claim.Value);
        }

        [Fact]
        public void WithClaimsShouldSetCorrectClaim()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithClaims(new Claim("MyClaim", "MyValue"), new Claim("MySecondClaim", "MySecondValue")))
                .AndProvideTheController();

            var claim = controller.User.Claims.FirstOrDefault(c => c.Type == "MyClaim");
            var secondClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "MySecondClaim");

            Assert.NotNull(claim);
            Assert.NotNull(secondClaim);
            Assert.Equal("MyValue", claim.Value);
            Assert.Equal("MySecondValue", secondClaim.Value);
        }
        
        [Fact]
        public void WithClaimsAsEnumerableShouldSetCorrectClaim()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithClaims(new List<Claim> { new Claim("MyClaim", "MyValue"), new Claim("MySecondClaim", "MySecondValue") }))
                .AndProvideTheController();

            var claim = controller.User.Claims.FirstOrDefault(c => c.Type == "MyClaim");
            var secondClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "MySecondClaim");

            Assert.NotNull(claim);
            Assert.NotNull(secondClaim);
            Assert.Equal("MyValue", claim.Value);
            Assert.Equal("MySecondValue", secondClaim.Value);
        }

        [Fact]
        public void WithClaimsIdentityShouldSetProperClaims()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithIdentity(new ClaimsIdentity(new List<Claim> { new Claim("MyClaim", "MyValue"), new Claim("MySecondClaim", "MySecondValue") })))
                .AndProvideTheController();
            
            var claim = controller.User.Claims.FirstOrDefault(c => c.Type == "MyClaim");
            var secondClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "MySecondClaim");

            Assert.NotNull(claim);
            Assert.NotNull(secondClaim);
            Assert.Equal("MyValue", claim.Value);
            Assert.Equal("MySecondValue", secondClaim.Value);
        }

        [Fact]
        public void WithIdentityShouldSetProperClaims()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithIdentity(new GenericIdentity("GenericName")))
                .AndProvideTheController();

            var claim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            
            Assert.NotNull(claim);
            Assert.Equal("GenericName", claim.Value);
        }

        [Fact]
        public void WithMultipleIdentitiesShouldSetProperClaims()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithIdentity(new CustomIdentity("GenericName"))
                    .WithIdentity(new CustomIdentity("SecondGenericName")))
                .AndProvideTheController();

            var claim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            Assert.NotNull(claim);
            Assert.Equal("SecondGenericName", claim.Value);
        }

        [Fact]
        public void WithIdentityBuilderShouldSetProperClaims()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithIdentity(identity => identity
                        .WithIdentifier("IdentityIdentifier")
                        .WithUsername("IdentityUsername")
                        .InRole("IdentityRole")))
                .AndProvideTheController();

            var usernameClaim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var userIdClaim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userRoleClaim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            Assert.NotNull(usernameClaim);
            Assert.NotNull(userIdClaim);
            Assert.NotNull(userRoleClaim);
            Assert.Equal("IdentityUsername", usernameClaim.Value);
            Assert.Equal("IdentityIdentifier", userIdClaim.Value);
            Assert.Equal("IdentityRole", userRoleClaim.Value);
        }

        [Fact]
        public void WithFullIdentityBuilderShouldSetProperClaims()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser(user => user
                    .WithIdentity(identity => identity
                        .WithNameType("CustomName")
                        .WithRoleType("CustomRole")
                        .WithIdentifier("IdentityIdentifier")
                        .WithUsername("IdentityUsername")
                        .WithClaim("First", "FirstValue")
                        .WithClaim(new Claim("Second", "SecondValue"))
                        .WithClaims(new Claim("Third", "ThirdValue"), new Claim("Fourth", "FourthValue"))
                        .WithClaims(new List<Claim> { new Claim("Fifth", "FifthValue"), new Claim("Sixth", "SixthValue") })
                        .WithAuthenticationType("MyAuthenticationType")
                        .AndAlso()
                        .InRole("IdentityRole")
                        .InRoles("AnotherRole", "ThirdRole")
                        .InRoles(new List<string> { "ListRole", "AnotherListRole" })))
                .AndProvideTheController();

            var usernameClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "CustomName");
            var userIdClaim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userRoleClaims = controller.User.Claims.Where(c => c.Type == "CustomRole").Select(c => c.Value).ToList();
            var firstClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "First");
            var secondClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "Second");
            var thirdClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "Third");
            var fourthClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "Fourth");
            var fifthClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "Fifth");
            var sixthClaim = controller.User.Claims.FirstOrDefault(c => c.Type == "Sixth");

            Assert.NotNull(usernameClaim);
            Assert.NotNull(userIdClaim);
            Assert.NotNull(firstClaim);
            Assert.NotNull(secondClaim);
            Assert.NotNull(thirdClaim);
            Assert.NotNull(fourthClaim);
            Assert.NotNull(fifthClaim);
            Assert.NotNull(sixthClaim);
            Assert.Equal(5, userRoleClaims.Count);
            Assert.Equal("IdentityUsername", usernameClaim.Value);
            Assert.Equal("IdentityIdentifier", userIdClaim.Value);
            Assert.Equal("FirstValue", firstClaim.Value);
            Assert.Equal("SecondValue", secondClaim.Value);
            Assert.Equal("ThirdValue", thirdClaim.Value);
            Assert.Equal("FourthValue", fourthClaim.Value);
            Assert.Equal("FifthValue", fifthClaim.Value);
            Assert.Equal("SixthValue", sixthClaim.Value);
            Assert.Contains("IdentityRole", userRoleClaims);
            Assert.Contains("AnotherRole", userRoleClaims);
            Assert.Contains("ThirdRole", userRoleClaims);
            Assert.Contains("ListRole", userRoleClaims);
            Assert.Contains("AnotherListRole", userRoleClaims);
        }
    }
}
