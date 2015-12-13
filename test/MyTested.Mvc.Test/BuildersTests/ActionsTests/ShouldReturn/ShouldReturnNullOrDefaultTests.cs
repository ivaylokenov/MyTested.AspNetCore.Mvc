namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnNullOrDefaultTests
    {
        [Fact]
        public void ShouldReturnNullShouldNotThrowExceptionWhenReturnValueNull()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .Null();
        }

        [Fact]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueNotNullable()
        {
            Test.AssertException<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericStructAction())
                    .ShouldReturn()
                    .Null();
            }, "Boolean cannot be null.");
        }

        [Fact]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueNotNull()
        {
            Test.AssertException<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultAction())
                    .ShouldReturn()
                    .Null();
            }, "When calling OkResultAction action in MvcController expected action result to be null, but instead received IActionResult.");
        }

        [Fact]
        public void ShouldReturnNotNullShouldNotThrowExceptionWhenReturnValueNotNull()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .NotNull();
        }

        [Fact]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueNotNullable()
        {
            Test.AssertException<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericStructAction())
                    .ShouldReturn()
                    .NotNull();
            }, "Boolean cannot be null.");

        }

        [Fact]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueNotNull()
        {
            Test.AssertException<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.NullAction())
                    .ShouldReturn()
                    .NotNull();
            }, "When calling NullAction action in MvcController expected action result to be not null, but it was IActionResult object.");
        }

        [Fact]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIDefaultForClass()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Fact]
        public void ShouldReturnDefaultShouldNotThrowExceptionWhenReturnValueIsNotDefaultForStructs()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.DefaultStructAction())
                .ShouldReturn()
                .DefaultValue();
        }

        [Fact]
        public void ShouldReturnDefaultShouldThrowExceptionWhenReturnValueIsNotDefault()
        {
            Test.AssertException<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultAction())
                    .ShouldReturn()
                    .DefaultValue();
            }, "When calling OkResultAction action in MvcController expected action result to be the default value of IActionResult, but in fact it was not.");
        }
    }
}
