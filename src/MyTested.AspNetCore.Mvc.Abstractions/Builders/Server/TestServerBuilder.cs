namespace MyTested.AspNetCore.Mvc.Builders.Server
{
    using System;
    using Base;
    using Contracts.Server;
    using Internal.Server;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc.Builders.Contracts.Application;

    /// <summary>
    /// Configures the test server.
    /// </summary>
    public class TestServerBuilder : BaseInitializationBuilder<IAndTestServerBuilder>, IAndTestServerBuilder
    {
        public TestServerBuilder()
            => TestWebServer.ResetSetup();

        /// <inheritdoc />
        protected override IAndTestServerBuilder InitializationBuilder => this;

        /// <inheritdoc />
        public IAndTestServerBuilder WithServices(Action<IServiceCollection> services)
        {
            TestWebServer.AdditionalServices += services;
            return this;
        }

        /// <inheritdoc />
        public void WithStartup<TStartup>()
            where TStartup : class
            => MyApplication.StartsFrom<TStartup>();

        /// <inheritdoc />
        public void WithStartup<TStartup>(Action<IApplicationConfigurationBuilder> applicationBuilder)
            where TStartup : class
            => applicationBuilder(MyApplication.StartsFrom<TStartup>());

        /// <inheritdoc />
        public ITestServerBuilder AndAlso() => this;
    }
}
