namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal.Application;
    using Internal.Razor;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void RazorRuntimeCompilationServicesShouldBeReplacedInTheTestApplication()
        {
            MyApplication.StartsFrom<TestStartup>();

            var actionDescriptorChangeProvider = Assert.Single(TestApplication.Services.GetServices<IActionDescriptorChangeProvider>());

            Assert.IsType<TestActionDescriptorChangeProvider>(actionDescriptorChangeProvider);
            Assert.DoesNotContain(TestApplication.Services.GetServices<IActionDescriptorProvider>(), p => p is PageActionDescriptorProvider);
        }
    }
}
