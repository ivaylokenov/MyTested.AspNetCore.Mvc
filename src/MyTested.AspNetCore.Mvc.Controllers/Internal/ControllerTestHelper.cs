namespace MyTested.AspNetCore.Mvc.Internal
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Services;

    public class ControllerTestHelper
    {
        /// <summary>
        /// Tries to create instance of the provided type. Returns null if not successful.
        /// </summary>
        /// <typeparam name="TInstance">Type to create.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
        public static TInstance TryCreateInstance<TInstance>()
            where TInstance : class
        {
            var instance = TestServiceProvider.GetService<TInstance>();
            if (instance != null)
            {
                return instance;
            }

            try
            {
                var typeActivatorCache = TestServiceProvider.GetRequiredService<ITypeActivatorCache>();
                return typeActivatorCache.CreateInstance<TInstance>(TestServiceProvider.Current, typeof(TInstance));
            }
            catch
            {
                return null;
            }
        }

        public static void SetActionContextToAccessor(ActionContext actionContext)
        {
            var actionContextAccessor = TestServiceProvider.GetService<IActionContextAccessor>();
            if (actionContextAccessor != null)
            {
                actionContextAccessor.ActionContext = actionContext;
            }
        }
    }
}
