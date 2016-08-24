namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithoutValidationShouldNotValidateTheRequestModel()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.ModelStateCheck(TestObjectFactory.GetRequestModelWithErrors()))
                .ShouldHave()
                .ValidModelState();
        }

        [Fact]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrors()
        {
            var requestModel = TestObjectFactory.GetRequestModelWithErrors();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<MvcController>(controller =>
                {
                    var modelState = (controller as Controller).ModelState;

                    Assert.False(modelState.IsValid);
                    Assert.Equal(2, modelState.Values.Count());
                    Assert.Equal("Integer", modelState.Keys.First());
                    Assert.Equal("RequiredString", modelState.Keys.Last());
                });
        }

        [Fact]
        public void UsingTryValidateModelInsideControllerShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.TryValidateModelAction())
                .ShouldReturn()
                .BadRequest();
        }
    }
}
