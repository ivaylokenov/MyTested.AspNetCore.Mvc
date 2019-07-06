namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Application;
    using Builders.Contracts.Application;
    using Builders.Contracts.Server;
    using Builders.Server;
    using Internal.Application;

    /// <summary>
    /// Provides methods to set up the ASP.NET Core MVC test application.
    /// </summary>
    public class MyApplication : ApplicationConfigurationBuilder
    {
        static MyApplication() => TestApplication.TryInitialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="MyApplication"/> class.
        /// </summary>
        public MyApplication()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyApplication"/> class.
        /// </summary>
        /// <param name="startupType">Startup class to bootstrap the test application from.</param>
        public MyApplication(Type startupType)
            : base(startupType)
        {
        }

        /// <summary>
        /// Specifies the Startup class from which the test application is bootstrapped. This method should be called only
        /// once per test project. If you need to use different test configurations with more than one Startup class in your 
        /// tests, you should separate them in independent assemblies.
        /// </summary>
        /// <typeparam name="TStartup">Startup class to bootstrap the test application from.</typeparam>
        /// <returns>Builder of <see cref="IApplicationConfigurationBuilder"/> type.</returns>
        public static IApplicationConfigurationBuilder StartsFrom<TStartup>()
            where TStartup : class 
            => new MyApplication(typeof(TStartup));

        /// <summary>
        /// Configures the test server on which the ASP.NET Core MVC test application is running on. This method should
        /// be called only once per test project. If you need to use different test server configurations in your tests,
        /// you should separate them in independent assemblies.
        /// </summary>
        /// <param name="testServerBuilder">Action setting the test server.</param>
        public static void IsRunningOn(Action<ITestServerBuilder> testServerBuilder) 
            => testServerBuilder(new TestServerBuilder());
    }
}
