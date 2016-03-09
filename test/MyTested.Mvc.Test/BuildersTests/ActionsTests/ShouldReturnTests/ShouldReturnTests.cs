namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ShouldReturnTests
    {
        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithStructTypes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn()
                .ResultOfType<bool>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithStructTypesAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericStructAction())
                .ShouldReturn()
                .ResultOfType(typeof(bool));
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<ResponseModel>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithClassTypesAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType<IResponseModel>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypesAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericAction())
                .ShouldReturn()
                .ResultOfType(typeof(IResponseModel));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndInterfaceReturn()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType<ResponseModel>();
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndTypeOfAndInterfaceReturn()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType(typeof(ResponseModel));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithClassTypesAndTypeOfAndInterfaceReturnWithInterface()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericInterfaceAction())
                        .ShouldReturn()
                        .ResultOfType(typeof(ICollection<>));
                }, 
                "When calling GenericInterfaceAction action in MvcController expected action result to be ICollection<T>, but instead received ResponseModel.");
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypesAndInterfaceReturn()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType<IResponseModel>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithInterfaceTypesAndTypeOfAndInterfaceReturn()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericInterfaceAction())
                .ShouldReturn()
                .ResultOfType(typeof(IResponseModel));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentInheritedGenericResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<IList<ResponseModel>>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithNotInheritedGenericResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType<IList<ResponseModel>>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>();
        }

        [Fact]
        public void ShouldReturnShouldNotExceptionWithOtherGenericResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericActionWithListCollection())
                        .ShouldReturn()
                        .ResultOfType<HashSet<ResponseModel>>();
                }, 
                "When calling GenericActionWithListCollection action in MvcController expected action result to be HashSet<ResponseModel>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldNotExceptionWithConcreteGenericResultWithTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType(typeof(List<ResponseModel>));
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithNotInheritedGenericResultWithTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType(typeof(IList<ResponseModel>));
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithDifferentInheritedGenericResultWithTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithListCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<ResponseModel>));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithOtherGenericResultWithTypeOf()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericActionWithListCollection())
                        .ShouldReturn()
                        .ResultOfType(typeof(HashSet<ResponseModel>));
                },
                "When calling GenericActionWithListCollection action in MvcController expected action result to be HashSet<ResponseModel>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentInheritedGenericResultAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(IList<ResponseModel>));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericDefinitionAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(List<>));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentWrongGenericDefinitionAndTypeOf()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericActionWithCollection())
                        .ShouldReturn()
                        .ResultOfType(typeof(HashSet<>));
                }, 
                "When calling GenericActionWithCollection action in MvcController expected action result to be HashSet<T>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentInheritedGenericDefinitionResultAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(IList<>));
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithCollectionOfClassTypes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>();
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithCollectionOfClassTypesAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<ResponseModel>));
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithCollectionOfClassTypesWithInterface()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericActionWithCollection())
                        .ShouldReturn()
                        .ResultOfType<ICollection<IResponseModel>>();
                }, 
                "When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<IResponseModel>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithCollectionOfClassTypesAndTypeOfWithInterface()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericActionWithCollection())
                        .ShouldReturn()
                        .ResultOfType(typeof(ICollection<IResponseModel>));
                }, 
                "When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<IResponseModel>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldNotThrowExceptionWithClassGenericDefinitionTypesAndTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<>));
        }

        [Fact]
        public void ShouldReturnShouldWorkWithModelDetailsTestsWithTypeOf()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType(typeof(ICollection<>))
                .Passing(c => c.Count == 2);
        }

        [Fact]
        public void ShouldReturnShouldWorkWithModelDetailsTestsWithGenericDefinition()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.GenericActionWithCollection())
                .ShouldReturn()
                .ResultOfType<ICollection<ResponseModel>>()
                .Passing(c => c.Count == 2);
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ActionWithException())
                        .ShouldReturn()
                        .ResultOfType<IActionResult>();
                }, 
                "NullReferenceException with 'Test exception message' message was thrown but was not caught or expected.");
        }

        [Fact]
        public void ShouldReturnWithAsyncShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ActionWithExceptionAsync())
                        .ShouldReturn()
                        .ResultOfType<IActionResult>();
                }, 
                "AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithModelDetailsTestsWithGenericDefinitionAndIncorrectAssertion()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericActionWithCollection())
                        .ShouldReturn()
                        .ResultOfType<ICollection<ResponseModel>>()
                        .Passing(c => c.Count == 1);
                }, 
                "When calling GenericActionWithCollection action in MvcController expected response model ICollection<ResponseModel> to pass the given condition, but it failed.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.GenericActionWithCollection())
                       .ShouldReturn()
                       .ResultOfType<ResponseModel>();
                }, 
                "When calling GenericActionWithCollection action in MvcController expected action result to be ResponseModel, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentResultAndTypeOf()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.GenericActionWithCollection())
                       .ShouldReturn()
                       .ResultOfType(typeof(ResponseModel));
                },
                "When calling GenericActionWithCollection action in MvcController expected action result to be ResponseModel, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.GenericActionWithCollection())
                        .ShouldReturn()
                        .ResultOfType<ICollection<int>>();
                }, 
                "When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResultAndTypeOf()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.GenericActionWithCollection())
                       .ShouldReturn()
                       .ResultOfType(typeof(ICollection<int>));
                }, 
                "When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void DynamicResultShouldBeProperlyRecognised()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.DynamicResult())
                .ShouldReturn()
                .ResultOfType<List<ResponseModel>>();
        }

        [Fact]
        public void WithShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyActionWithParameters(With.No<int>(), With.No<RequestModel>()))
                .ShouldReturnEmpty();
        }
    }
}
