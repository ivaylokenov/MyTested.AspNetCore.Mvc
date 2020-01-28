namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.UnprocessableEntityTests
{
    using Xunit;
    using Setups;
    using Setups.Controllers;
    using Exceptions;
    using Utilities;

    public class UnprocessableEntityTestBuilderTests
    {
        [Fact]
        public void UnprocessableEntityShouldNotThrowExceptionWithCorrectActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.UnprocessableEntityAction())
                .ShouldReturn()
                .UnprocessableEntity();
        }

        [Fact]
        public void UnprocessableEntityShouldThrowExceptionWithIncorrectActionResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultAction())
                        .ShouldReturn()
                        .UnprocessableEntity();
                },
                "When calling OkResultAction action in MvcController expected result to be UnprocessableEntityResult, but instead received OkResult.");
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.UnprocessableEntityAction())
                .ShouldReturn()
                .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                    .WithStatusCode(422));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.UnprocessableEntityAction())
                .ShouldReturn()
                .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                    .WithStatusCode(HttpStatusCode.UnprocessableEntity));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<UnprocessableEntityResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.UnprocessableEntityAction())
                        .ShouldReturn()
                        .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                            .WithStatusCode(415));
                },
                "When calling UnprocessableEntityAction action in MvcController expected unprocessable entity result to have 415 (UnsupportedMediaType) status code, but instead received 422 (UnprocessableEntity).");
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<UnprocessableEntityResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.UnprocessableEntityAction())
                        .ShouldReturn()
                        .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                            .WithStatusCode(HttpStatusCode.UnsupportedMediaType));
                },
                "When calling UnprocessableEntityAction action in MvcController expected unprocessable entity result to have 415 (UnsupportedMediaType) status code, but instead received 422 (UnprocessableEntity).");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullUnprocessableEntityAction())
                .ShouldReturn()
                .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                    .WithStatusCode(HttpStatusCode.UnprocessableEntity)
                    .AndAlso()
                    .ContainingContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullUnprocessableEntityAction())
                .ShouldReturn()
                .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                    .Passing(ue => ue.ContentTypes.Contains(ContentType.ApplicationJson)));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullUnprocessableEntityAction())
                        .ShouldReturn()
                        .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                            .Passing(ue => ue.ContentTypes?.Count == 0));
                },
                "When calling FullUnprocessableEntityAction action in MvcController expected the UnprocessableEntityObjectResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullUnprocessableEntityAction())
                .ShouldReturn()
                .UnprocessableEntity(unprocessableEntity => unprocessableEntity
                    .Passing(ue =>
                    {
                        const int expectedContentTypesCount = 2;
                        var actualContentTypesCount = ue.ContentTypes?.Count;
                        if (actualContentTypesCount != expectedContentTypesCount)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} {2}, but it has {3}.",
                                    ue.GetType().ToFriendlyTypeName(),
                                    expectedContentTypesCount,
                                    nameof(ue.ContentTypes),
                                    actualContentTypesCount));
                        };
                    }));
        }
    }
}
