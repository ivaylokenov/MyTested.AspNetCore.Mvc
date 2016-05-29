namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using Contracts.Controllers;
    using Contracts.Data;
    using Data;

    /// <content>
    /// Used for building the controller which will be tested.
    /// </content>
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithTempData(Action<ITempDataBuilder> tempDataBuilder)
        {
            this.tempDataBuilderAction = tempDataBuilder;
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
