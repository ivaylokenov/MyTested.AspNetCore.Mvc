namespace MyTested.AspNetCore.Mvc.Internal.ViewComponents
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using Contracts;
    using Microsoft.AspNetCore.Mvc.ViewComponents;

    public class ViewComponentPropertyActivator : IViewComponentPropertyActivator
    {
        private readonly Func<Type, PropertyActivator<ViewComponentContext>[]> getPropertiesToActivate;
        private readonly ConcurrentDictionary<Type, PropertyActivator<ViewComponentContext>[]> propertyActivatorCache;

        public ViewComponentPropertyActivator()
        {
            this.getPropertiesToActivate = type => PropertyActivator<ViewComponentContext>
                .GetPropertiesToActivate(type, typeof(ViewComponentContextAttribute), CreateActivateInfo);

            this.propertyActivatorCache = new ConcurrentDictionary<Type, PropertyActivator<ViewComponentContext>[]>();
        }

        public void Activate(ViewComponentContext viewComponentContext, object viewComponent)
        {
            var propertiesToActivate = propertyActivatorCache.GetOrAdd(viewComponent.GetType(), getPropertiesToActivate);

            for (var i = 0; i < propertiesToActivate.Length; i++)
            {
                var activateInfo = propertiesToActivate[i];
                activateInfo.Activate(viewComponent, viewComponentContext);
            }
        }

        private static PropertyActivator<ViewComponentContext> CreateActivateInfo(PropertyInfo property)
            => new PropertyActivator<ViewComponentContext>(property, context => context);
    }
}
