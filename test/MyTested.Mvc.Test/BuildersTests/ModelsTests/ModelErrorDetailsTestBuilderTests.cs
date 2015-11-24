namespace MyTested.Mvc.Tests.BuildersTests.ModelsTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    
    public class ModelErrorDetailsTestBuilderTests
    {
        [Fact]
        public void ThatEqualsShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).ThatEquals("The RequiredString field is required.")
                .AndAlso()
                .ContainingModelStateErrorFor(m => m.RequiredString)
                .AndAlso()
                .ContainingNoModelStateErrorFor(m => m.NotValidateInteger)
                .AndAlso()
                .ContainingModelStateError("RequiredString")
                .ContainingModelStateErrorFor(m => m.Integer).ThatEquals(string.Format("The field Integer must be between {0} and {1}.", 1, int.MaxValue))
                .ContainingModelStateError("RequiredString")
                .ContainingModelStateError("Integer")
                .ContainingNoModelStateErrorFor(m => m.NotValidateInteger);
        }

        [Fact]
        public void ThatEqualsShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                    .ShouldHave()
                    .ModelStateFor<RequestModel>()
                    .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                    .AndAlso()
                    .ContainingModelStateErrorFor(m => m.RequiredString).ThatEquals("RequiredString field is required.")
                    .ContainingModelStateErrorFor(m => m.Integer).ThatEquals(string.Format("Integer must be between {0} and {1}.", 1, int.MaxValue));
            }, "When calling ModelStateCheck action in MvcController expected error message for key RequiredString to be 'RequiredString field is required.', but instead found 'The RequiredString field is required.'.");
        }

        [Fact]
        public void BeginningWithShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                .ContainingModelStateErrorFor(m => m.Integer).BeginningWith("The field Integer");
        }

        [Fact]
        public void BeginningWithShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                    .ShouldHave()
                    .ModelStateFor<RequestModel>()
                    .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                    .ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("RequiredString")
                    .ContainingModelStateErrorFor(m => m.Integer).BeginningWith("Integer");
            }, "When calling ModelStateCheck action in MvcController expected error message for key 'RequiredString' to begin with 'RequiredString', but instead found 'The RequiredString field is required.'.");
        }

        [Fact]
        public void EngingWithShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required.")
                .ContainingModelStateErrorFor(m => m.Integer).EndingWith(string.Format("{0} and {1}.", 1, int.MaxValue));
        }

        [Fact]
        public void EngingWithShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                    .ShouldHave()
                    .ModelStateFor<RequestModel>()
                    .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                    .ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required!")
                    .ContainingModelStateErrorFor(m => m.Integer).EndingWith(string.Format("{0} and {1}!", 1, int.MaxValue));
            }, "When calling ModelStateCheck action in MvcController expected error message for key 'RequiredString' to end with 'required!', but instead found 'The RequiredString field is required.'.");
        }

        [Fact]
        public void ContainingShouldNotThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                .ShouldHave()
                .ModelStateFor<RequestModel>()
                .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                .ContainingModelStateErrorFor(m => m.RequiredString).Containing("required")
                .ContainingModelStateErrorFor(m => m.Integer).Containing("between");
        }

        [Fact]
        public void ContainingShouldThrowExceptionWhenProvidedMessageIsValid()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestModelWithErrors))
                    .ShouldHave()
                    .ModelStateFor<RequestModel>()
                    .ContainingNoModelStateErrorFor(m => m.NonRequiredString)
                    .ContainingModelStateErrorFor(m => m.RequiredString).Containing("invalid")
                    .ContainingModelStateErrorFor(m => m.Integer).Containing("invalid");
            }, "When calling ModelStateCheck action in MvcController expected error message for key 'RequiredString' to contain 'invalid', but instead found 'The RequiredString field is required.'.");
        }
    }
}
