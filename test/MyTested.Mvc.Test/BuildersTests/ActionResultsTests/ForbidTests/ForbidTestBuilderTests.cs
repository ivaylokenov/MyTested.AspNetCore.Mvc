namespace MyTested.Mvc.Test.BuildersTests.ActionResultsTests.ForbidTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ForbidTestBuilderTests
    {
        [Fact]
        public void ShouldReturnForbidShouldNotThrowExceptionIfResultIsForbidWithCorrectAuthenticationScheme()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid()
                .ContainingAuthenticationScheme(AuthenticationScheme.Basic);
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionIfResultIsNotForbidWithIncorrectAuthenticationScheme()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ForbidWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Forbid()
                        .ContainingAuthenticationScheme(AuthenticationScheme.Digest);
                },
                "When calling ForbidWithAuthenticationSchemes action in MvcController expected forbid result authentication schemes to contain Digest, but none was found.");
        }

        [Fact]
        public void ShouldReturnForbidShouldNotThrowExceptionIfResultIsForbidWithCorrectMultipleAuthenticationSchemes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid()
                .ContainingAuthenticationSchemes(new List<string> { AuthenticationScheme.Basic, AuthenticationScheme.NTLM });
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionIfResultIsNotForbidWithIncorrectMultipleAuthenticationSchemes()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ForbidWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Forbid()
                        .ContainingAuthenticationSchemes(AuthenticationScheme.Digest, AuthenticationScheme.Basic);
                },
                "When calling ForbidWithAuthenticationSchemes action in MvcController expected forbid result authentication schemes to contain Digest, but none was found.");
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionIfResultIsNotForbidWithIncorrectMultipleAuthenticationSchemesCount()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ForbidWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Forbid()
                        .ContainingAuthenticationSchemes(new[] { AuthenticationScheme.Digest });
                },
                "When calling ForbidWithAuthenticationSchemes action in MvcController expected forbid result authentication schemes to be 1, but instead found 2.");
        }

        [Fact]
        public void ShouldReturnForbidShouldNotThrowExceptionIfResultIsForbidWithCorrectMultipleAuthenticationSchemesAsParams()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid()
                .ContainingAuthenticationSchemes(AuthenticationScheme.Basic, AuthenticationScheme.NTLM);
        }

        [Fact]
        public void ShouldReturnForbidShouldNotThrowExceptionWithValidAuthenticationProperties()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ForbidWithAuthenticationProperties())
                .ShouldReturn()
                .Forbid()
                .WithAuthenticationProperties(TestObjectFactory.GetAuthenticationProperties());
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionWithInvalidAuthenticationProperties()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    var authenticationProperties = TestObjectFactory.GetAuthenticationProperties();

                    authenticationProperties.AllowRefresh = false;

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ForbidWithAuthenticationProperties())
                        .ShouldReturn()
                        .Forbid()
                        .WithAuthenticationProperties(authenticationProperties);
                },
                "When calling ForbidWithAuthenticationProperties action in MvcController expected forbid result authentication properties to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid()
                .ContainingAuthenticationScheme(AuthenticationScheme.Basic)
                .AndAlso()
                .ContainingAuthenticationScheme(AuthenticationScheme.NTLM);
        }
    }
}
