namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ChallengeTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ChallengeTestBuilderTests
    {
        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionIfResultIsChallengeWithCorrectAuthenticationScheme()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .ContainingAuthenticationScheme(AuthenticationScheme.Basic));
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallengeWithIncorrectAuthenticationScheme()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .ContainingAuthenticationScheme(AuthenticationScheme.Digest));
                },
                "When calling ChallengeWithAuthenticationSchemes action in MvcController expected challenge result authentication schemes to contain 'Digest', but none was found.");
        }

        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionIfResultIsChallengeWithCorrectMultipleAuthenticationSchemes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .ContainingAuthenticationSchemes(new List<string>
                    {
                        AuthenticationScheme.Basic,
                        AuthenticationScheme.NTLM
                    }));
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallengeWithIncorrectMultipleAuthenticationSchemes()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .ContainingAuthenticationSchemes(
                                AuthenticationScheme.Digest, 
                                AuthenticationScheme.Basic));
                },
                "When calling ChallengeWithAuthenticationSchemes action in MvcController expected challenge result authentication schemes to contain 'Digest', but none was found.");
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallengeWithIncorrectMultipleAuthenticationSchemesCount()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .ContainingAuthenticationSchemes(AuthenticationScheme.Digest));
                },
                "When calling ChallengeWithAuthenticationSchemes action in MvcController expected challenge result authentication schemes to be 1, but instead found 2.");
        }

        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionIfResultIsChallengeWithCorrectMultipleAuthenticationSchemesAsParams()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .ContainingAuthenticationSchemes(
                        AuthenticationScheme.Basic, 
                        AuthenticationScheme.NTLM));
        }

        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionWithValidAuthenticationProperties()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .WithAuthenticationProperties(TestObjectFactory.GetAuthenticationProperties()));
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionWithInvalidAuthenticationProperties()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    var authenticationProperties = TestObjectFactory.GetAuthenticationProperties();

                    authenticationProperties.AllowRefresh = false;

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge(challenge => challenge
                            .WithAuthenticationProperties(authenticationProperties));
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected challenge result authentication properties to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge(challenge => challenge
                    .ContainingAuthenticationScheme(AuthenticationScheme.Basic)
                    .AndAlso()
                    .ContainingAuthenticationScheme(AuthenticationScheme.NTLM));
        }
    }
}
