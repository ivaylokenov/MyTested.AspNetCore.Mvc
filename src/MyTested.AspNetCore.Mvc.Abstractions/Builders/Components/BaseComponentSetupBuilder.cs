namespace MyTested.AspNetCore.Mvc.Builders.Components
{
    using System;

    public partial class BaseComponentBuilder<TComponent, TTestContext, TBuilder>
    {
        private Action<TComponent> componentSetupAction;
        
        /// <inheritdoc />
        public TBuilder WithSetup(Action<TComponent> componentSetup)
        {
            this.componentSetupAction += componentSetup;
            return this.Builder;
        }

        protected abstract void PrepareComponentContext();
    }
}
