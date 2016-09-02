namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public abstract class ActionTestContext : ComponentTestContext
    {
        public abstract ModelStateDictionary ModelState { get; }
    }
}
