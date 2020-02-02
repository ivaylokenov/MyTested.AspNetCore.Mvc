namespace MyTested.AspNetCore.Mvc.Internal.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using Microsoft.AspNetCore.Mvc;

    public class ControllerPropertyHelper : PropertyHelper
    {
        private static readonly ConcurrentDictionary<Type, ControllerPropertyHelper> ControllerPropertiesCache =
            new ConcurrentDictionary<Type, ControllerPropertyHelper>();

        private Func<object, ControllerContext> controllerContextGetter;
        private Func<object, ActionContext> actionContextGetter;

        public ControllerPropertyHelper(Type controllerType)
            : base (controllerType)
        {
        }

        public Func<object, ControllerContext> ControllerContextGetter
        {
            get
            {
                if (this.controllerContextGetter == null)
                {
                    this.TryCreateControllerContextDelegates();
                }

                return this.controllerContextGetter;
            }
        }

        public Func<object, ActionContext> ActionContextGetter
        {
            get
            {
                if (this.actionContextGetter == null)
                {
                    this.TryCreateActionContextDelegates();
                }

                return this.actionContextGetter;
            }
        }

        public static ControllerPropertyHelper GetProperties<TController>()
            where TController : class 
            => GetProperties(typeof(TController));

        public static ControllerPropertyHelper GetProperties(Type type) 
            => ControllerPropertiesCache.GetOrAdd(type, _ => new ControllerPropertyHelper(type));

        private void TryCreateControllerContextDelegates()
        {
            var controllerContextProperty = this.FindPropertyWithAttribute<ControllerContextAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(controllerContextProperty, nameof(ControllerContext));

            this.controllerContextGetter = MakeFastPropertyGetter<ControllerContext>(controllerContextProperty);
        }

        private void TryCreateActionContextDelegates()
        {
            var actionContextProperty = this.FindPropertyWithAttribute<ActionContextAttribute>();
            this.ThrowNewInvalidOperationExceptionIfNull(actionContextProperty, nameof(ActionContext));

            this.actionContextGetter = MakeFastPropertyGetter<ActionContext>(actionContextProperty);
        }
    }
}
