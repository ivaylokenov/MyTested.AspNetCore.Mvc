﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ForbidTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Utilities;
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
                "When calling ForbidWithAuthenticationProperties action in MvcController expected forbid result authentication properties to be the same as the provided one, but instead received different result. Difference occurs at 'AuthenticationProperties.Items[.refresh].Value'. Expected a value of 'False', but in fact it was 'True'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
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

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .Passing(f => f.AuthenticationSchemes?.Count == 2));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ForbidWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Forbid(forbid => forbid
                            .Passing(f => f.AuthenticationSchemes?.Count == 0));
                },
                $"When calling ForbidWithAuthenticationSchemes action in MvcController expected the ForbidResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ForbidWithAuthenticationSchemes())
                .ShouldReturn()
                .Forbid(forbid => forbid
                    .Passing(f =>
                    {
                        const int expectedAuthSchemesCount = 2;
                        var actualAuthSchemesCount = f.AuthenticationSchemes?.Count;
                        if (actualAuthSchemesCount != expectedAuthSchemesCount)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} {2}, but it was {3}.",
                                    f.GetType().ToFriendlyTypeName(),
                                    expectedAuthSchemesCount,
                                    nameof(f.AuthenticationSchemes),
                                    actualAuthSchemesCount));
                        };
                    }));
        }
    }
}
