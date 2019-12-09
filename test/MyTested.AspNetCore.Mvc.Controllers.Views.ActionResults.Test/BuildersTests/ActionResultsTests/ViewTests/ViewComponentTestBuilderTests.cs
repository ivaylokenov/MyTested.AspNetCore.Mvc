namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ViewTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ViewComponentTestBuilderTests
    {
        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgumentName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .ContainingArgumentWithName("model"));
        }

        [Fact]
        public void WithArgumentOfTypeAndNameShouldNotThrowExceptionWithCorrectArgumentName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .ContainingArgumentOfType<List<ResponseModel>>("model"));
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgumentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .ContainingArgumentWithName("id"));
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with 'id' key, but such was not found.");
        }

        [Fact]
        public void WithArgumentWithSameKeyAndDifferentValueShouldThrowException()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .ContainingArgument("model", new { model = 1 }));
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with 'model' key and the provided value, but the value was different. Expected a value of AnonymousType<Int32> type, but in fact it was List<ResponseModel>.");
        }

        [Fact]
        public void WithArgumentWithSameKeyAndDifferentNameShouldThrowException()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .ContainingArgument("id", new { model = 1 }));
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with 'id' key and the provided value, but such was not found.");
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgument()
        {
            var responseModels = TestObjectFactory.GetListOfResponseModels();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .ContainingArgument(responseModels));
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgumentAndName()
        {
            var responseModels = TestObjectFactory.GetListOfResponseModels();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .ContainingArgument("model", responseModels));
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgument()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .ContainingArgument(1));
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have entry with the provided value, but none was found.");
        }

        [Fact]
        public void WithArgumentShouldNotThrowExceptionWithCorrectArgumentOfType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .ContainingArgumentOfType<List<ResponseModel>>());
        }

        [Fact]
        public void WithArgumentShouldThrowExceptionWithIncorrectArgumentOfType()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByType())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .ContainingArgumentOfType<int>());
                },
                "When calling ViewComponentResultByType action in MvcController expected view component result arguments to have at least one entry of Int32 type, but none was found.");
        }

        [Fact]
        public void WithArgumentsShouldNotThrowExceptionWithCorrectArguments()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .ContainingArguments(new { id = 1, test = "text" }));
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithIncorrectArgumentsCount()
        {
            Test.AssertException<ViewResultAssertionException>(
                (() =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .ContainingArguments(new { id = 1, text = "text", incorrect = 15 }));
                }),
                "When calling ViewComponentResultByName action in MvcController expected view component result arguments to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithIncorrectArguments()
        {
            Test.AssertException<ViewResultAssertionException>(
                (() =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .ContainingArguments(new { id = 1, test = "incorrect" }));
                }),
                "When calling ViewComponentResultByName action in MvcController expected view component result arguments to have entry with 'test' key and the provided value, but the value was different. Expected a value of 'incorrect', but in fact it was 'text'.");
        }

        [Fact]
        public void WithArgumentsShouldThrowExceptionWithCorrectArgumentsAsDictionary()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .ContainingArguments(new Dictionary<string, object> { ["id"] = 1, ["test"] = "text" }));
        }
    }
}
