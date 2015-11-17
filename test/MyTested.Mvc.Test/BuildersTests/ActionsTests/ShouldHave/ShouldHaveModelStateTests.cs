namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldHave
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    
    public class ShouldHaveModelStateTests
    {
        [Fact]
        public void ShouldHaveModelStateForShouldChainCorrectly()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(r => r.NonRequiredString)
                .ContainingModelStateErrorFor(r => r.Integer)
                .ContainingModelStateErrorFor(r => r.RequiredString);
        }

        [Fact]
        public void ShouldHaveValidModelStateShouldBeValidWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModel))
                .ShouldHave()
                .ValidModelState();
        }

        [Fact]
        public void ShouldHaveValidModelStateShouldThrowExceptionWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            
            var exception = Assert.Throws<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                    .ShouldHave()
                    .ValidModelState();
            });

            Assert.Equal("When calling ModelStateCheck action in MvcController expected to have valid model state with no errors, but it had some.", exception.Message);
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidRequestModel()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState();
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeValidWithInvalidRequestModelAndCorrectNumberOfErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState(2);
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldBeInvalidWithInvalidRequestModelAndIncorrectNumberOfErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            var exception = Assert.Throws<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                    .ShouldHave()
                    .InvalidModelState(5);
            });

            Assert.Equal("When calling ModelStateCheck action in MvcController expected to have invalid model state with 5 errors, but in fact contained 2.", exception.Message);
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModel()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();
            
            var exception = Assert.Throws<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModel))
                    .ShouldHave()
                    .InvalidModelState();
            });

            Assert.Equal("When calling ModelStateCheck action in MvcController expected to have invalid model state, but was in fact valid.", exception.Message);
        }

        [Fact]
        public void ShouldHaveInvalidModelStateShouldThrowExceptionWithValidRequestModelAndProvidedNumberOfErrors()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            var exception = Assert.Throws<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModel))
                    .ShouldHave()
                    .InvalidModelState(withNumberOfErrors: 5);
            });

            Assert.Equal("When calling ModelStateCheck action in MvcController expected to have invalid model state with 5 errors, but in fact contained 0.", exception.Message);
        }

        [Fact]
        public void AndShouldWorkCorrectlyWithValidModelState()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyMvc
                .Controller<MvcController>()
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

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AndProvideModelShouldThrowExceptionWhenIsCalledOnTheRequest()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            var exception = Assert.Throws<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                    .ShouldHave()
                    .ModelStateFor<RequestModel>()
                    .ContainingNoModelStateErrorFor(r => r.NonRequiredString)
                    .ContainingModelStateErrorFor(r => r.Integer)
                    .ContainingModelStateErrorFor(r => r.RequiredString)
                    .AndProvideTheModel();
            });

            Assert.Equal("AndProvideTheModel can be used when there is response model from the action.", exception.Message);

        }
    }
}
