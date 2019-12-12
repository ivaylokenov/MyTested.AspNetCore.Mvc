namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;
    using Xunit.Sdk;

    public class ViewComponentShouldReturnContentTests
    {
        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithContentResult()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content();
        }

        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithContentResultAndValue()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Test");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithIncorrectContent()
        {
            Test.AssertException<ContentViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Content("incorrect");
                },
                "When invoking NormalComponent expected content result to contain 'incorrect', but instead received 'Test'.");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithEmptyContent()
        {
            Test.AssertException<ContentViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Content(string.Empty);
                },
                "When invoking NormalComponent expected content result to contain '', but instead received 'Test'.");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithNull()
        {
            Test.AssertException<ContentViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Content(content: null);
                },
                "When invoking NormalComponent expected content result to contain '', but instead received 'Test'.");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithBadRequestResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<AsyncComponent>
                        .InvokedWith(c => c.InvokeAsync())
                        .ShouldReturn()
                        .Content("content");
                },
                "When invoking AsyncComponent expected result to be ContentViewComponentResult, but instead received ViewViewComponentResult.");
        }

        [Fact]
        public void ShouldReturnContentWithPredicateShouldNotThrowExceptionWithValidPredicate()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content(content => content.StartsWith("Te"));
        }

        [Fact]
        public void ShouldReturnContentWithPredicateShouldThrowExceptionWithInvalidPredicate()
        {
            Test.AssertException<ContentViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Content(content => content.StartsWith("invalid"));
                },
                "When invoking NormalComponent expected content result ('Test') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnContentWithAssertionsShouldNotThrowExceptionWithValidPredicate()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content(content =>
                {
                    Assert.StartsWith("Te", content);
                });
        }

        [Fact]
        public void ShouldReturnContentWithAssertionsShouldThrowExceptionWithInvalidPredicate()
        {
            Assert.Throws<StartsWithException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Content(content =>
                        {
                            Assert.StartsWith("te", content);
                        });
                });
        }
    }
}
