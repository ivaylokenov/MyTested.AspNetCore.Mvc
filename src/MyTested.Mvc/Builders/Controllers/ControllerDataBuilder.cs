namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using Contracts.Controllers;
    using Contracts.Data;
    using Data;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithTempData(Action<ITempDataBuilder> tempDataBuilder)
        {
            this.tempDataBuilderAction = tempDataBuilder;
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithSession(Action<ISessionBuilder> sessionBuilder)
        {
            sessionBuilder(new SessionBuilder(this.HttpContext.Session));
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithMemoryCache(Action<IMemoryCacheBuilder> memoryCacheBuilder)
        {
            memoryCacheBuilder(new MemoryCacheBuilder(this.Services));
            return this;
        }
    }
}
