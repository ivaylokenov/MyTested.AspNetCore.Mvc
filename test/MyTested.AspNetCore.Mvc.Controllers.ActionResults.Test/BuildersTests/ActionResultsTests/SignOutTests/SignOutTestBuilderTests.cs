namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.SignOutTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using System.Collections.Generic;
    using Utilities;
    using Xunit;

    public class SignOutTestBuilderTests
    {
        [Fact]
        public void SignInShouldNotThrowExceptionWithCorrectActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationProperties())
                .ShouldReturn()
                .SignOut();
        }

        [Fact]
        public void ConflictShouldThrowExceptionWithIncorrectActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultAction())
                        .ShouldReturn()
                        .SignOut();
                },
                "When calling OkResultAction action in MvcController expected result to be SignOutResult, but instead received OkResult.");
        }

        [Fact]
        public void ShouldReturnSignOutShouldNotThrowExceptionIfResultIsSignOutWithCorrectAuthenticationScheme()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationSchemes())
                .ShouldReturn()
                .SignOut(signOut => signOut
                    .ContainingAuthenticationScheme(AuthenticationScheme.Basic));
        }

        [Fact]
        public void ShouldReturnSignOutShouldThrowExceptionIfResultIsSignOutWithIncorrectAuthenticationScheme()
        {
            Test.AssertException<SignOutResultAssertionException>(
               () =>
               {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignOutWithAuthenticationSchemes())
                        .ShouldReturn()
                        .SignOut(signOut => signOut
                        .ContainingAuthenticationScheme(AuthenticationScheme.Digest));
               },
               "When calling SignOutWithAuthenticationSchemes action in MvcController expected sign out result authentication schemes to contain 'Digest', but none was found.");
        }

        [Fact]
        public void ShouldReturnSignOutShouldNotThrowExceptionIfResultIsSignOutWithCorrectMultipleAuthenticationSchemes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationSchemes())
                .ShouldReturn()
                .SignOut(signOut => signOut
                    .ContainingAuthenticationSchemes(new List<string>
                    {
                        AuthenticationScheme.Basic,
                        AuthenticationScheme.NTLM
                    }));
        }

        [Fact]
        public void ShouldReturnSignOutShouldNotThrowExceptionIfResultIsSignOutWithCorrectMultipleAuthenticationSchemesAsParams()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationSchemes())
                .ShouldReturn()
                .SignOut(signOut => signOut
                    .ContainingAuthenticationSchemes(AuthenticationScheme.Basic, AuthenticationScheme.NTLM));
        }

        [Fact]
        public void ShouldReturnSignOutShouldThrowExceptionIfResultIsNotSignOutWithIncorrectMultipleAuthenticationSchemes()
        {
            Test.AssertException<SignOutResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignOutWithAuthenticationSchemes())
                        .ShouldReturn()
                        .SignOut(signOut => signOut
                            .ContainingAuthenticationSchemes(AuthenticationScheme.Digest, AuthenticationScheme.Basic));
                },
                "When calling SignOutWithAuthenticationSchemes action in MvcController expected sign out result authentication schemes to contain 'Digest', but none was found.");
        }

        [Fact]
        public void ShouldReturnSignOutShouldThrowExceptionIfResultIsNotSignOutWithIncorrectMultipleAuthenticationSchemesCount()
        {
            Test.AssertException<SignOutResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignOutWithAuthenticationSchemes())
                        .ShouldReturn()
                        .SignOut(signOut => signOut
                            .ContainingAuthenticationSchemes(AuthenticationScheme.Digest));
                },
                "When calling SignOutWithAuthenticationSchemes action in MvcController expected sign out result authentication schemes to be 1, but instead found 2.");
        }

        [Fact]
        public void ShouldReturnSignOutShouldNotThrowExceptionIfResultIsSignOutWithCorrectAuthenticationProperties()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationProperties())
                .ShouldReturn()
                .SignOut(signOut => signOut
                    .WithAuthenticationProperties(TestObjectFactory.GetAuthenticationProperties()));
        }

        [Fact]
        public void ShouldReturnSignOutShouldThrowExceptionWithInvalidAuthenticationProperties()
        {
            Test.AssertException<SignOutResultAssertionException>(
                () =>
                {
                    var authenticationProperties = TestObjectFactory.GetAuthenticationProperties();

                    authenticationProperties.AllowRefresh = false;

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignOutWithAuthenticationProperties())
                        .ShouldReturn()
                        .SignOut(signOut => signOut
                            .WithAuthenticationProperties(authenticationProperties));
                },
                "When calling SignOutWithAuthenticationProperties action in MvcController expected sign out result authentication properties to be the same as the provided one, but instead received different result. Difference occurs at 'AuthenticationProperties.Items[.refresh].Value'. Expected a value of 'False', but in fact it was 'True'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationSchemes())
                .ShouldReturn()
                .SignOut(signOut => signOut
                    .ContainingAuthenticationScheme(AuthenticationScheme.Basic)
                    .AndAlso()
                    .ContainingAuthenticationScheme(AuthenticationScheme.NTLM));
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationSchemes())
                .ShouldReturn()
                .SignOut(signOut => signOut
                    .Passing(so => so.AuthenticationSchemes?.Count == 2));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignOutWithAuthenticationSchemes())
                        .ShouldReturn()
                        .SignOut(signOut => signOut
                            .Passing(so => so.AuthenticationSchemes?.Count == 0));
                },
                "When calling SignOutWithAuthenticationSchemes action in MvcController expected the SignOutResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignOutWithAuthenticationSchemes())
                .ShouldReturn()
                .SignOut(signOut => signOut
                    .Passing(so =>
                    {
                        const int expectedAuthSchemes = 2;
                        var actualAuthSchemes = so.AuthenticationSchemes?.Count;
                        if (actualAuthSchemes != expectedAuthSchemes)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} {2}, but it has {3}.",
                                    so.GetType().ToFriendlyTypeName(),
                                    expectedAuthSchemes,
                                    nameof(so.AuthenticationSchemes),
                                    actualAuthSchemes));
                        };
                    }));
        }
    }
}
