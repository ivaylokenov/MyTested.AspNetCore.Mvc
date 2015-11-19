namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    using Microsoft.AspNet.Mvc;

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
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericInterfaceAction())
                    .ShouldReturn()
                    .ResultOfType(typeof(ICollection<>));
            });

            Assert.Equal("When calling GenericInterfaceAction action in MvcController expected action result to be ICollection<T>, but instead received ResponseModel.", exception.Message);
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
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericActionWithListCollection())
                    .ShouldReturn()
                    .ResultOfType<HashSet<ResponseModel>>();
            });

            Assert.Equal("When calling GenericActionWithListCollection action in MvcController expected action result to be HashSet<ResponseModel>, but instead received List<ResponseModel>.", exception.Message);
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
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericActionWithListCollection())
                    .ShouldReturn()
                    .ResultOfType(typeof(HashSet<ResponseModel>));
            });

            Assert.Equal("When calling GenericActionWithListCollection action in MvcController expected action result to be HashSet<ResponseModel>, but instead received List<ResponseModel>.", exception.Message);
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
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericActionWithCollection())
                    .ShouldReturn()
                    .ResultOfType(typeof(HashSet<>));
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected action result to be HashSet<T>, but instead received List<ResponseModel>.", exception.Message);
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
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericActionWithCollection())
                    .ShouldReturn()
                    .ResultOfType<ICollection<IResponseModel>>();
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<IResponseModel>, but instead received List<ResponseModel>.", exception.Message);
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithCollectionOfClassTypesAndTypeOfWithInterface()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericActionWithCollection())
                    .ShouldReturn()
                    .ResultOfType(typeof(ICollection<IResponseModel>));
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<IResponseModel>, but instead received List<ResponseModel>.", exception.Message);
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
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ActionWithException())
                    .ShouldReturn()
                    .ResultOfType<IActionResult>();
            });

            Assert.Equal("NullReferenceException with 'Test exception message' message was thrown but was not caught or expected.", exception.Message);
        }

        [Fact]
        public void ShouldReturnWithAsyncShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .CallingAsync(c => c.ActionWithExceptionAsync())
                    .ShouldReturn()
                    .ResultOfType<IActionResult>();
            });

            Assert.Equal("AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.", exception.Message);
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithModelDetailsTestsWithGenericDefinitionAndIncorrectAssertion()
        {
            var exception = Assert.Throws<ResponseModelAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericActionWithCollection())
                    .ShouldReturn()
                    .ResultOfType<ICollection<ResponseModel>>()
                    .Passing(c => c.Count == 1);
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected response model ICollection<ResponseModel> to pass the given condition, but it failed.", exception.Message);
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentResult()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                   .Controller<MvcController>()
                   .Calling(c => c.GenericActionWithCollection())
                   .ShouldReturn()
                   .ResultOfType<ResponseModel>();
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected action result to be ResponseModel, but instead received List<ResponseModel>.", exception.Message);
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentResultAndTypeOf()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                   .Controller<MvcController>()
                   .Calling(c => c.GenericActionWithCollection())
                   .ShouldReturn()
                   .ResultOfType(typeof(ResponseModel));
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected action result to be ResponseModel, but instead received List<ResponseModel>.", exception.Message);
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResult()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericActionWithCollection())
                    .ShouldReturn()
                    .ResultOfType<ICollection<int>>();
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.", exception.Message);
        }

        [Fact]
        public void ShouldReturnShouldThrowExceptionWithDifferentGenericResultAndTypeOf()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                   .Controller<MvcController>()
                   .Calling(c => c.GenericActionWithCollection())
                   .ShouldReturn()
                   .ResultOfType(typeof(ICollection<int>));
            });

            Assert.Equal("When calling GenericActionWithCollection action in MvcController expected action result to be ICollection<Int32>, but instead received List<ResponseModel>.", exception.Message);
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
    }
}
