namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
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
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericStructAction())
                    .ShouldReturn()
                    .Null();
            });

            Assert.Equal("Boolean cannot be null.", exception.Message);
        }

        [Fact]
        public void ShouldReturnNullShouldThrowExceptionWhenReturnValueIsNotNull()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultAction())
                    .ShouldReturn()
                    .Null();
            });

            Assert.Equal("When calling OkResultAction action in MvcController expected action result to be null, but instead received IActionResult.", exception.Message);
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
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.GenericStructAction())
                    .ShouldReturn()
                    .NotNull();
            });

            Assert.Equal("Boolean cannot be null.", exception.Message);

        }

        [Fact]
        public void ShouldReturnNotNullShouldThrowExceptionWhenReturnValueIsNotNull()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.NullAction())
                    .ShouldReturn()
                    .NotNull();
            });

            Assert.Equal("When calling NullAction action in MvcController expected action result to be not null, but it was IActionResult object.", exception.Message);
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
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultAction())
                    .ShouldReturn()
                    .DefaultValue();
            });

            Assert.Equal("When calling OkResultAction action in MvcController expected action result to be the default value of IActionResult, but in fact it was not.", exception.Message);
        }
    }
}
