namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.UnauthorizedTests
{
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    using Setups;
    using Utilities;

    public class UnauthorizedTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.UnauthorizedAction())
                .ShouldReturn()
                .Unauthorized(unauthorized => unauthorized
                    .WithStatusCode(401));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.UnauthorizedAction())
                .ShouldReturn()
                .Unauthorized(unauthorized => unauthorized
                    .WithStatusCode(HttpStatusCode.Unauthorized));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<UnauthorizedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.UnauthorizedAction())
                        .ShouldReturn()
                        .Unauthorized(unauthorized => unauthorized
                            .WithStatusCode(415));
                },
                "When calling UnauthorizedAction action in MvcController expected unauthorized result to have 415 (UnsupportedMediaType) status code, but instead received 401 (Unauthorized).");
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<UnauthorizedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.UnauthorizedAction())
                        .ShouldReturn()
                        .Unauthorized(unauthorized => unauthorized
                            .WithStatusCode(HttpStatusCode.UnsupportedMediaType));
                },
                "When calling UnauthorizedAction action in MvcController expected unauthorized result to have 415 (UnsupportedMediaType) status code, but instead received 401 (Unauthorized).");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullUnauthorizedAction())
                .ShouldReturn()
                .Unauthorized(unauthorized => unauthorized
                    .WithStatusCode(HttpStatusCode.Unauthorized)
                    .AndAlso()
                    .ContainingContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullUnauthorizedAction())
                .ShouldReturn()
                .Unauthorized(unauthorized => unauthorized
                    .Passing(c => c.ContentTypes.Contains(ContentType.ApplicationJson)));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullUnauthorizedAction())
                        .ShouldReturn()
                        .Unauthorized(unauthorized => unauthorized
                            .Passing(c => c.ContentTypes?.Count == 0));
                },
                $"When calling FullUnauthorizedAction action in MvcController expected the UnauthorizedObjectResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullUnauthorizedAction())
                .ShouldReturn()
                .Unauthorized(unauthorized => unauthorized
                    .Passing(c =>
                    {
                        const int expectedContentTypesCount = 2;
                        var actualContentTypesCount = c.ContentTypes?.Count;
                        if (actualContentTypesCount != expectedContentTypesCount)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} {2}, but it has {3}.",
                                    c.GetType().ToFriendlyTypeName(),
                                    expectedContentTypesCount,
                                    nameof(c.ContentTypes),
                                    actualContentTypesCount));
                        };
                    }));
        }
    }
}
