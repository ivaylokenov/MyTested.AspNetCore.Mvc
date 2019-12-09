namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.SignInTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Utilities;
    using Xunit;

    public class SignInTestBuilderTests
    {
        [Fact]
        public void ShouldReturnSignInShouldNotThrowExceptionIfResultIsSignInWithCorrectAuthenticationScheme()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignInWithEmptyAuthenticationPropertiesAndScheme())
                .ShouldReturn()
                .SignIn(signIn => signIn
                    .WithAuthenticationScheme(AuthenticationScheme.Basic));
        }

        [Fact]
        public void ShouldReturnSignInShouldThrowExceptionIfResultIsSignInWithIncorrectAuthenticationScheme()
        {
            Test.AssertException<SignInResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignInWithEmptyAuthenticationPropertiesAndScheme())
                        .ShouldReturn()
                        .SignIn(signIn => signIn
                            .WithAuthenticationScheme(AuthenticationScheme.Digest));
                },
                "When calling SignInWithEmptyAuthenticationPropertiesAndScheme action in MvcController expected sign in result authentication scheme to be 'Digest', but instead received 'Basic'.");
        }

        [Fact]
        public void ShouldReturnSignInShouldNotThrowExceptionIfResultIsSignInWithCorrectAuthenticationProperties()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignInWithAuthenticationPropertiesAndScheme())
                .ShouldReturn()
                .SignIn(signIn => signIn
                    .WithAuthenticationProperties(TestObjectFactory.GetAuthenticationProperties()));
        }

        [Fact]
        public void ShouldReturnSignInShouldThrowExceptionWithInvalidAuthenticationProperties()
        {
            Test.AssertException<SignInResultAssertionException>(
                () =>
                {
                    var authenticationProperties = TestObjectFactory.GetAuthenticationProperties();

                    authenticationProperties.AllowRefresh = false;

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignInWithAuthenticationPropertiesAndScheme())
                        .ShouldReturn()
                        .SignIn(signIn => signIn
                            .WithAuthenticationProperties(authenticationProperties));
                },
                "When calling SignInWithAuthenticationPropertiesAndScheme action in MvcController expected sign in result authentication properties to be the same as the provided one, but instead received different result. Difference occurs at 'AuthenticationProperties.Items[.refresh].Value'. Expected a value of 'False', but in fact it was 'True'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignInWithAuthenticationPropertiesAndScheme())
                .ShouldReturn()
                .SignIn(signIn => signIn
                    .WithAuthenticationProperties(TestObjectFactory.GetAuthenticationProperties())
                    .AndAlso()
                    .WithAuthenticationScheme(AuthenticationScheme.Basic));
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignInWithAuthenticationPropertiesAndScheme())
                .ShouldReturn()
                .SignIn(signIn => signIn
                    .Passing(si => si.AuthenticationScheme == AuthenticationScheme.Basic));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.SignInWithAuthenticationPropertiesAndScheme())
                        .ShouldReturn()
                        .SignIn(signIn => signIn
                            .Passing(si => si.AuthenticationScheme == AuthenticationScheme.Digest));
                },
                "When calling SignInWithAuthenticationPropertiesAndScheme action in MvcController expected the SignInResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.SignInWithAuthenticationPropertiesAndScheme())
                .ShouldReturn()
                .SignIn(signIn => signIn
                    .Passing(si =>
                    {
                        var expectedAuthScheme = AuthenticationScheme.Basic;
                        var actualAuthScheme = si.AuthenticationScheme;
                        if (actualAuthScheme != expectedAuthScheme)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} equal to {2}, but it has {3}.",
                                    si.GetType().ToFriendlyTypeName(),
                                    nameof(si.AuthenticationScheme),
                                    expectedAuthScheme,
                                    actualAuthScheme));
                        };
                    }));
        }
    }
}
