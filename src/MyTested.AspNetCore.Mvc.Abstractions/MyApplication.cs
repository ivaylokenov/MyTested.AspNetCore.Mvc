namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Application;
    using Builders.Contracts.Application;
    using Internal.Application;

    /// <summary>
    /// Provides methods to set up the ASP.NET Core MVC test application.
    /// </summary>
    public class MyApplication : ApplicationConfigurationBuilder
    {
        static MyApplication()
        {
            TestApplication.TryInitialize();
        }
        
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
        /// Specifies the Startup class from which the test application is bootstrapped.
        /// </summary>
        /// <typeparam name="TStartup">Startup class to bootstrap the test application from.</typeparam>
        /// <returns>Builder of <see cref="IApplicationConfigurationBuilder"/> type.</returns>
        public static IApplicationConfigurationBuilder StartsFrom<TStartup>()
            where TStartup : class
        {
            return new MyApplication(typeof(TStartup));
        }
    }
}
