namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentShouldReturnViewTests
    {
        [Fact]
        public void ShouldReturnViewShouldNotThrowExceptionWithDefaultView()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke(null))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsNotViewResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                       .InvokedWith(c => c.Invoke())
                       .ShouldReturn()
                       .View();
                },
                "When invoking NormalComponent expected result to be ViewViewComponentResult, but instead received ContentViewComponentResult.");
        }
    }
}
