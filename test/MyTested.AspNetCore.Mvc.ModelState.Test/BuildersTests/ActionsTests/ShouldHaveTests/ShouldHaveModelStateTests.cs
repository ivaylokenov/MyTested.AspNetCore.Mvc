namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Plugins;
    using Setups;
    using Setups.Controllers;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveModelStateTests
    {
        [Fact]
        public void ShouldHaveValidModelStateShouldBeValidWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHave()
                .ValidModelState();
        }

        [Fact]
        public void ShouldHaveValidModelStateShouldThrowExceptionWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                        .ShouldHave()
                        .ValidModelState();
                }, 
                "When calling ModelStateCheck action in MvcController expected to have valid model state with no errors, but it had some.");
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState();
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidRequestModelAndCorrectNumberOfErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState(2);
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeInvalidWithInvalidRequestModelAndIncorrectNumberOfErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                        .ShouldHave()
                        .InvalidModelState(5);
                }, 
                "When calling ModelStateCheck action in MvcController expected to have invalid model state with 5 errors, but in fact contained 2.");
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();
            
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestModel))
                        .ShouldHave()
                        .InvalidModelState();
                }, 
                "When calling ModelStateCheck action in MvcController expected to have invalid model state, but was in fact valid.");
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModelAndProvidedNumberOfErrors()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestModel))
                        .ShouldHave()
                        .InvalidModelState(withNumberOfErrors: 5);
                }, 
                "When calling ModelStateCheck action in MvcController expected to have invalid model state with 5 errors, but in fact contained 0.");
        }

        [Fact]
        public void AndShouldWorkCorrectlyWithValidModelState()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AndShouldWorkCorrectlyWithInvalidModelState()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ShouldHaveValidModelStateShouldBeValidWithViewComponentValidModelState()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .ValidModelState();
        }

        [Fact]
        public void ShouldHaveValidModelStateShouldThrowExceptionWithInvalidViewComponentModelState()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .WithSetup(vc => vc.ModelState.AddModelError("Test", "InvalidTest"))
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .ValidModelState();
                },
                "When invoking NormalComponent expected to have valid model state with no errors, but it had some.");
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidViewComponentModelState()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .WithSetup(vc => vc.ModelState.AddModelError("Test", "InvalidTest"))
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .InvalidModelState();
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidViewComponentModelStateAndCorrectNumberOfErrors()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .WithSetup(vc =>
                {
                    vc.ModelState.AddModelError("Test", "InvalidTest");
                    vc.ModelState.AddModelError("Another", "InvalidAnotherTest");
                })
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .InvalidModelState(2);
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeInvalidWithInvalidViewComponentModelStateAndIncorrectNumberOfErrors()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .WithSetup(vc =>
                        {
                            vc.ModelState.AddModelError("Test", "InvalidTest");
                            vc.ModelState.AddModelError("Another", "InvalidAnotherTest");
                        })
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .InvalidModelState(5);
                },
                "When invoking NormalComponent expected to have invalid model state with 5 errors, but in fact contained 2.");
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidViewComponentModelState()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .InvalidModelState();
                },
                "When invoking NormalComponent expected to have invalid model state, but was in fact valid.");
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithViewComponentModelStateAndProvidedNumberOfErrors()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .InvalidModelState(withNumberOfErrors: 5);
                },
                "When invoking NormalComponent expected to have invalid model state with 5 errors, but in fact contained 0.");
        }

        [Fact]
        public void ShouldHaveDefaultServices()
        {
            var modelStateTestPlugin = new ModelStateTestPlugin();
            var serviceCollection = new ServiceCollection();
            
            modelStateTestPlugin.DefaultServiceRegistrationDelegate(serviceCollection);
          
            Assert.True(serviceCollection.Count == 60);
        }
    }
}
