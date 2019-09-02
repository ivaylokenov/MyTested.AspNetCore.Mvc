namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentShouldReturnHtmlContentTests
    {
        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithContentResult()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .HtmlContent();
        }

        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithContentResultAndValue()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .HtmlContent("<input type='button' />");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithIncorrectContent()
        {
            Test.AssertException<ContentViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<HtmlContentComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .HtmlContent("incorrect");
                },
                "When invoking HtmlContentComponent expected content result to contain 'incorrect', but instead received '<input type='button' />'.");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithBadRequestResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .HtmlContent("content");
                },
                "When invoking NormalComponent expected result to be IHtmlContent, but instead received ContentViewComponentResult.");
        }

        [Fact]
        public void ShouldReturnContentWithPredicateShouldNotThrowExceptionWithValidPredicate()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .HtmlContent(content => content.StartsWith("<input "));
        }

        [Fact]
        public void ShouldReturnContentWithPredicateShouldThrowExceptionWithInvalidPredicate()
        {
            Test.AssertException<ContentViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<HtmlContentComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .HtmlContent(content => content.StartsWith("invalid"));
                },
                "When invoking HtmlContentComponent expected content result ('<input type='button' />') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnContentWithAssertionsShouldNotThrowExceptionWithValidPredicate()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .HtmlContent(content =>
                {
                    Assert.StartsWith("<input ", content);
                });
        }
    }
}
