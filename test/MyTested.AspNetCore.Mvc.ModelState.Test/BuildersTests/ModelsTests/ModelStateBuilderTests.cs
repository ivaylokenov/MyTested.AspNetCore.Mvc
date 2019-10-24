namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ModelsTests
{
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using System.Collections.Generic;
    using System.Linq;

    public class ModelStateBuilderTests
    {
        [Fact]
        public void WithModelStateWithErrorShouldWorkCorrectly()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithError("TestError", "Invalid value"))
                .Calling(c => c.BadRequestWithModelState(requestBody))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void WithModelStateWithErrorsDictionaryShouldWorkCorrectly()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();
            var errorsDictionary = new Dictionary<string, string>()
            {
                ["First"] = "SomeError",
                ["Second"] = "AnotherError",
            };

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithErrors(errorsDictionary))
                .Calling(c => c.BadRequestWithModelState(requestBody))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void WithModelStateWithErrorsObjectShouldWorkCorrectly()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();
            var errorsObject = new
            {
                First = "SomeError",
                Second = "AnotherError",
            };

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithErrors(errorsObject))
                .Calling(c => c.BadRequestWithModelState(requestBody))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void WithoutModelStateByProvidingExistingKeyShouldWorkCorrectly()
        {
            var keyValuePair = new KeyValuePair<string, string>("Key1", "Value1");

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithError(keyValuePair.Key, keyValuePair.Value)
                    .WithError("PseudoRandomKey", "value"))
                .WithoutModelState(keyValuePair.Key)
                .Calling(c => c.GetModelStateKeys())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<List<string>>()
                    .Passing(keys => keys.Count == 1 &&
                        keys.Any(key => !key.Equals(keyValuePair.Key))));
        }

        [Fact]
        public void WithoutModelStateByUsingTheBuilderShouldWorkCorrectly()
        {
            var keyValuePair = new KeyValuePair<string, string>("Key1", "Value1");

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithError(keyValuePair.Key, keyValuePair.Value)
                    .WithError("PseudoRandomKey", "value"))
                .WithoutModelState(modelState => modelState.WithoutModelState(keyValuePair.Key))
                .Calling(c => c.GetModelStateKeys())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<List<string>>()
                    .Passing(keys => keys.Count == 1 &&
                        keys.Any(key => !key.Equals(keyValuePair.Key))));
        }

        [Fact]
        public void WithoutModelStateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithError("PseudoRandomKey1", "value1")
                    .WithError("PseudoRandomKey2", "value2"))
                .WithoutModelState()
                .Calling(c => c.GetModelStateKeys())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<List<string>>()
                    .Passing(keys => keys.Count == 0));
        }

        [Fact]
        public void WithoutModelStateByRemovingNonExistingKeyShouldWorkCorrectly()
        {
            var keyValuePair = new KeyValuePair<string, string>("Key1", "Value1");

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithError(keyValuePair.Key, keyValuePair.Value))
                .WithoutModelState("NonExistingKey")
                .Calling(c => c.GetModelStateKeys())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<List<string>>()
                    .Passing(keys => keys.Count == 1 &&
                        keys.Any(key => key.Equals(keyValuePair.Key))));
        }
    }
}
