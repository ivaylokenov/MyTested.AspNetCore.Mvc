namespace MyTested.AspNetCore.Mvc.Internal.Actions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    public class ActionContextAccessorMock : ActionContextAccessor
    {
        internal static readonly IActionContextAccessor Null = new NullActionContextAccessor();

        private class NullActionContextAccessor : IActionContextAccessor
        {
            public ActionContext ActionContext
            {
                get => null;
                set { }
            }
        }
    }
}
