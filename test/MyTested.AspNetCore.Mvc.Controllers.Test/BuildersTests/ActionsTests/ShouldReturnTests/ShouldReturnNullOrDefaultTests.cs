namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using System;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnNullOrDefaultTests
    {
        [Fact]
        public void ShouldReturnNullShouldNotThrowExceptionWhenReturnValueNull()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .Null();
        }

        [Fact]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueNotNullable()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.GenericStructAction())
                        .ShouldReturn()
                        .Null();
                },
                "Boolean cannot be null.");
        }

        [Fact]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueNotNull()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultAction())
                        .ShouldReturn()
                        .Null();
                }, 
                "When calling OkResultAction action in MvcController expected action result to be null, but instead received IActionResult.");
        }

        [Fact]
        public void ShouldReturnNotNullShouldNotThrowExceptionWhenReturnValueNotNull()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .NotNull();
        }

        [Fact]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueNotNullable()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.GenericStructAction())
                        .ShouldReturn()
                        .NotNull();
                }, 
                "Boolean cannot be null.");
        }

        [Fact]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueNotNull()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NullAction())
                        .ShouldReturn()
                        .NotNull();
                }, 
                "When calling NullAction action in MvcController expected action result to be not null, but it was IActionResult object.");
        }

        [Fact]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIDefaultForClass()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Fact]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIsNotDefaultForStructs()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultStructAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Fact]
        public void ShouldReturnDefaultShouldThrowExceptionWhenReturnValueIsNotDefault()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultAction())
                        .ShouldReturn()
                        .DefaultValue();
                }, 
                "When calling OkResultAction action in MvcController expected action result to be the default value of IActionResult, but in fact it was not.");
        }
    }
}
