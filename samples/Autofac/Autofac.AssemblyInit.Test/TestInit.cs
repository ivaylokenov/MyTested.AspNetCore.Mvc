namespace Autofac.AssemblyInit.Test
{
    using Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.AspNetCore.Mvc;

    [TestClass]
    public class TestInit
    {
        // Prepare additional server configuration - add AutoFac here. 
        // Call 'IsRunningOn' either in the static constructor of the TestStartup class
        // or in an assembly initialization method if your test runner supports it.
        // Note that 'IsRunningOn' should be called only once per test project.
        [AssemblyInitialize]
        public static void Init(TestContext context)
            => MyApplication
                .IsRunningOn(server => server
                    .WithServices(services => services.AddAutofac()));
    }
}
