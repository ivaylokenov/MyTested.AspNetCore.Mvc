namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ForbidTests
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .ContainingAuthenticationScheme(AuthenticationScheme.Basic));
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionIfResultIsNotForbidWithIncorrectAuthenticationScheme()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ForbidWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Forbid(forbid => forbid
                            .ContainingAuthenticationScheme(AuthenticationScheme.Digest));
                },
                "When calling ForbidWithAuthenticationSchemes action in MvcController expected forbid result authentication schemes to contain 'Digest', but none was found.");
        }

        [Fact]
        public void ShouldReturnForbidShouldNotThrowExceptionIfResultIsForbidWithCorrectMultipleAuthenticationSchemes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .ContainingAuthenticationSchemes(new List<string>
                    {
                        AuthenticationScheme.Basic,
                        AuthenticationScheme.NTLM
                    }));
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionIfResultIsNotForbidWithIncorrectMultipleAuthenticationSchemes()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ForbidWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Forbid(forbid => forbid
                            .ContainingAuthenticationSchemes(AuthenticationScheme.Digest, AuthenticationScheme.Basic));
                },
                "When calling ForbidWithAuthenticationSchemes action in MvcController expected forbid result authentication schemes to contain 'Digest', but none was found.");
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionIfResultIsNotForbidWithIncorrectMultipleAuthenticationSchemesCount()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ForbidWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Forbid(forbid => forbid
                            .ContainingAuthenticationSchemes(AuthenticationScheme.Digest));
                },
                "When calling ForbidWithAuthenticationSchemes action in MvcController expected forbid result authentication schemes to be 1, but instead found 2.");
        }

        [Fact]
        public void ShouldReturnForbidShouldNotThrowExceptionIfResultIsForbidWithCorrectMultipleAuthenticationSchemesAsParams()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .ContainingAuthenticationSchemes(AuthenticationScheme.Basic, AuthenticationScheme.NTLM));
        }

        [Fact]
        public void ShouldReturnForbidShouldNotThrowExceptionWithValidAuthenticationProperties()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationProperties())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .WithAuthenticationProperties(TestObjectFactory.GetAuthenticationProperties()));
        }

        [Fact]
        public void ShouldReturnForbidShouldThrowExceptionWithInvalidAuthenticationProperties()
        {
            Test.AssertException<ForbidResultAssertionException>(
                () =>
                {
                    var authenticationProperties = TestObjectFactory.GetAuthenticationProperties();

                    authenticationProperties.AllowRefresh = false;

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ForbidWithAuthenticationProperties())
                        .ShouldReturn()
                        .Forbid(forbid => forbid
                            .WithAuthenticationProperties(authenticationProperties));
                },
                "When calling ForbidWithAuthenticationProperties action in MvcController expected forbid result authentication properties to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyForbid()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .ContainingAuthenticationScheme(AuthenticationScheme.Basic)
                    .AndAlso()
                    .ContainingAuthenticationScheme(AuthenticationScheme.NTLM));
        }
    }
}
