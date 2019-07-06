namespace Test.NoAsync
{
    using System;
    using MyTested.AspNetCore.Mvc;
    using WebApplication.Core;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void AsyncTestsWithMultipleStartupsShouldThrowCorrectException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                MyApplication.StartsFrom<TestStartup>();
                MyApplication.StartsFrom<Startup>();
            });

            Assert.Equal("Multiple Startup types per test project while running asynchronous tests is not supported. Either set 'General:AsynchronousTests' in the test configuration ('testsettings.json' file by default) to 'false' or separate your tests into different test projects. The latter is recommended. If you choose the first option, you may need to disable asynchronous testing in your preferred test runner too.", exception.Message);
        }
    }
}
