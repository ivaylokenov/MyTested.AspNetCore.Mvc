namespace MyTested.Mvc.Tests.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    using Setups;

    public class ResponseModelTestBuilderTests
    {
        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectResponseModel()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithIncorrectInheritedTypeArgument()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IList<ResponseModel>>();
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectImplementatorTypeArgument()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithNoResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultAction())
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModelOfType<ResponseModel>();
            }, "When calling OkResultAction action in MvcController expected response model to be of ResponseModel type, but instead received null.");
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithIncorrectResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultWithInterfaceResponse())
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModelOfType<ResponseModel>();
            }, "When calling OkResultWithInterfaceResponse action in MvcController expected response model to be of ResponseModel type, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithIncorrectGenericTypeArgument()
        {
            Test.AssertException<ResponseModelAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultWithInterfaceResponse())
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModelOfType<ICollection<int>>();
            }, "When calling OkResultWithInterfaceResponse action in MvcController expected response model to be of ICollection<Int32> type, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPassedExpectedObject()
        {
            var controller = new MvcController();

            MyMvc
                .Controller(() => controller)
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(controller.ResponseModel);
        }

        [Fact]
        public void WithResponceModelShouldNotThrowExceptionWithDeeplyEqualPassedExpectedObject()
        {
            var controller = new MvcController();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(controller.ResponseModel);
        }

        [Fact]
        public void WithResponceModelShouldThrowExceptionWithDifferentPassedExpectedObject()
        {
            var controller = new MvcController();

            var another = controller.ResponseModel.ToList();
            another.Add(new ResponseModel());

            Test.AssertException<ResponseModelAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultWithResponse())
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModel(another);
            }, "When calling OkResultWithResponse action in MvcController expected response model List<ResponseModel> to be the given model, but in fact it was a different model.");
        }

        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionWhenNoResponseModel()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok()
                .WithNoResponseModel();
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionWhenResponseModelExists()
        {
            Test.AssertException<ResponseModelAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultWithResponse())
                    .ShouldReturn()
                    .Ok()
                    .WithNoResponseModel();
            }, "When calling OkResultWithResponse action in MvcController expected to not have response model but in fact response model was found.");
        }
    }
}
