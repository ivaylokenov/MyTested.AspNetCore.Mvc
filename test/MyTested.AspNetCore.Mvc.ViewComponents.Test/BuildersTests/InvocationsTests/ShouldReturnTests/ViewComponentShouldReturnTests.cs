namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldReturnTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Setups;
    using Setups.Models;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentShouldReturnTests
    {
        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypes()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .ResultOfType<ContentViewComponentResult>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypesAndTypeOf()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .ResultOfType(typeof(ContentViewComponentResult));
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypes()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .ResultOfType<IHtmlContent>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypesAndTypeOf()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .ResultOfType(typeof(IHtmlContent));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndInterfaceReturn()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .ResultOfType<HtmlContentBuilder>();
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndTypeOfAndInterfaceReturn()
        {
            MyViewComponent<HtmlContentComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .ResultOfType(typeof(HtmlContentBuilder));
        }
        
        [Fact]
        public void ShouldReturnShouldWorkWithModelDetailsTestsWithGenericDefinition()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .ResultOfType<ContentViewComponentResult>(result => result
                    .Passing(c => c.Content == "Test"));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    MyViewComponent<ExceptionComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .ResultOfType<IViewComponentResult>();
                },
                "When invoking ExceptionComponent expected no exception but IndexOutOfRangeException with 'Test exception message' message was thrown without being caught.");
        }

        [Fact]
        public void ShouldReturnWithAsyncShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    MyViewComponent<AggregateExceptionComponent>
                        .InvokedWith(c => c.InvokeAsync())
                        .ShouldReturn()
                        .ResultOfType<IViewComponentResult>();
                },
                "When invoking AggregateExceptionComponent expected no exception but AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown without being caught.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithModelDetailsTestsWithGenericDefinitionAndIncorrectAssertion()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .ResultOfType<ContentViewComponentResult>(result => result
                            .Passing(c => c.Content == "Incorrect"));
                },
                "When invoking NormalComponent expected response model ContentViewComponentResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                       .InvokedWith(c => c.Invoke())
                       .ShouldReturn()
                       .ResultOfType<string>();
                },
                "When invoking NormalComponent expected result to be String, but instead received ContentViewComponentResult.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentResultAndTypeOf()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                       .InvokedWith(c => c.Invoke())
                       .ShouldReturn()
                       .ResultOfType(typeof(string));
                },
                "When invoking NormalComponent expected result to be String, but instead received ContentViewComponentResult.");
        }
        
        [Fact]
        public void ShouldReturnResultShouldWorkCorrectlyWithCorrectModel()
        {
            MyViewComponent<StringComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Result("TestString");
        }

        [Fact]
        public void ShouldReturnResultShouldThrowExceptionWithIncorrectModelType()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<StringComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Result(TestObjectFactory.GetListOfResponseModels());
                },
                "When invoking StringComponent expected result to be List<ResponseModel>, but instead received String.");
        }

        [Fact]
        public void ShouldReturnResultShouldThrowExceptionWithDifferentModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<StringComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Result("Incorrect");
                },
                "When invoking StringComponent expected the response model to be the given model, but in fact it was a different one. Expected a value of 'Incorrect', but in fact it was 'TestString'.");
        }
        
        [Fact]
        public void WithShouldWorkCorrectly()
        {
            MyViewComponent<ArgumentsComponent>
                .InvokedWith(c => c.Invoke(With.No<int>(), With.No<RequestModel>()))
                .ShouldReturn()
                .Content("0,");
        }
    }
}
