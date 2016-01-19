namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.ChallengeTests
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge()
                .ContainingAuthenticationScheme(AuthenticationScheme.Basic);
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallengeWithIncorrectAuthenticationScheme()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Challenge()
                        .ContainingAuthenticationScheme(AuthenticationScheme.Digest);
                },
                "When calling ChallengeWithAuthenticationSchemes action in MvcController expected challenge result authentication schemes to contain Digest, but none was found.");
        }

        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionIfResultIsChallengeWithCorrectMultipleAuthenticationSchemes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge()
                .ContainingAuthenticationSchemes(new List<string> { AuthenticationScheme.Basic, AuthenticationScheme.NTLM });
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallengeWithIncorrectMultipleAuthenticationSchemes()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Challenge()
                        .ContainingAuthenticationSchemes(AuthenticationScheme.Digest, AuthenticationScheme.Basic);
                },
                "When calling ChallengeWithAuthenticationSchemes action in MvcController expected challenge result authentication schemes to contain Digest, but none was found.");
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallengeWithIncorrectMultipleAuthenticationSchemesCount()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationSchemes())
                        .ShouldReturn()
                        .Challenge()
                        .ContainingAuthenticationSchemes(new[] { AuthenticationScheme.Digest });
                },
                "When calling ChallengeWithAuthenticationSchemes action in MvcController expected challenge result authentication schemes to be 1, but instead found 2.");
        }

        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionIfResultIsChallengeWithCorrectMultipleAuthenticationSchemesAsParams()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge()
                .ContainingAuthenticationSchemes(AuthenticationScheme.Basic, AuthenticationScheme.NTLM);
        }

        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionWithValidAuthenticationProperties()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .WithAuthenticationProperties(TestObjectFactory.GetAuthenticationProperties());
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionWithInvalidAuthenticationProperties()
        {
            Test.AssertException<ChallengeResultAssertionException>(
                () =>
                {
                    var authenticationProperties = TestObjectFactory.GetAuthenticationProperties();

                    authenticationProperties.AllowRefresh = false;

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ChallengeWithAuthenticationProperties())
                        .ShouldReturn()
                        .Challenge()
                        .WithAuthenticationProperties(authenticationProperties);
                },
                "When calling ChallengeWithAuthenticationProperties action in MvcController expected challenge result authentication properties to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeWithAuthenticationSchemes())
                .ShouldReturn()
                .Challenge()
                .ContainingAuthenticationScheme(AuthenticationScheme.Basic)
                .AndAlso()
                .ContainingAuthenticationScheme(AuthenticationScheme.NTLM);
        }
    }
}
