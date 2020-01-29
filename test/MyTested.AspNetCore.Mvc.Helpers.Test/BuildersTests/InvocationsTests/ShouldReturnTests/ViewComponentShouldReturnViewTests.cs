namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldReturnTests
{
    using Setups.ViewComponents;
    using Setups.Models;
    using Xunit;

    public class ViewComponentShouldReturnViewTests
    {
        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("custom"))
                .ShouldReturn()
                .View("Custom");
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectModel()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("model"))
                .ShouldReturn()
                .View(new ResponseModel { StringValue = "TestValue" });
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectNameAndModel()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView", new ResponseModel { IntegerValue = 10 });
        }
    }
}
