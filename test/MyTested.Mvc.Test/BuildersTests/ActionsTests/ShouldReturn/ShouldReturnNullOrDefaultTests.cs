namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnNullOrDefaultTests
    {
        [Fact]
        public void ShouldReturnNullShouldNotThrowExceptionWhenReturnValueIsNull()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NullAction())
                .ShouldReturn()
                .Null();
        }

        [Fact]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueIsNotNullable()
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
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueIsNotNull()
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
        public void ShouldReturnNotNullShouldNotThrowExceptionWhenReturnValueIsNotNull()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .NotNull();
        }

        [Fact]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueIsNotNullable()
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
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueIsNotNull()
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
