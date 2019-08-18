namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ConflictTests
{
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    using Setups;
    using Utilities;

    public class ConflictTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ConflictAction())
                .ShouldReturn()
                .Conflict(conflict => conflict
                    .WithStatusCode(409));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ConflictAction())
                .ShouldReturn()
                .Conflict(conflict => conflict
                    .WithStatusCode(HttpStatusCode.Conflict));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<ConflictResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ConflictAction())
                        .ShouldReturn()
                        .Conflict(conflict => conflict
                            .WithStatusCode(415));
                },
                "When calling ConflictAction action in MvcController expected conflict result to have 415 (UnsupportedMediaType) status code, but instead received 409 (Conflict).");
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<ConflictResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ConflictAction())
                        .ShouldReturn()
                        .Conflict(conflict => conflict
                            .WithStatusCode(HttpStatusCode.UnsupportedMediaType));
                },
                "When calling ConflictAction action in MvcController expected conflict result to have 415 (UnsupportedMediaType) status code, but instead received 409 (Conflict).");
        }



        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullConflictAction())
                .ShouldReturn()
                .Conflict(conflict => conflict
                    .WithStatusCode(HttpStatusCode.Conflict)
                    .AndAlso()
                    .ContainingContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullConflictAction())
                .ShouldReturn()
                .Conflict(conflict => conflict
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
                        .Calling(c => c.FullConflictAction())
                        .ShouldReturn()
                        .Conflict(conflict => conflict
                            .Passing(c => c.ContentTypes?.Count == 0));
                },
                $"When calling FullConflictAction action in MvcController expected the ConflictObjectResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullConflictAction())
                .ShouldReturn()
                .Conflict(conflict => conflict
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
