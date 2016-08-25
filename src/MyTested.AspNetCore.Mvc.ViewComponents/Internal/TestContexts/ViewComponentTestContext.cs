namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;

    public class ViewComponentTestContext : HttpTestContext
    {
        internal Func<object> ViewComponentConstruction { get; set; }

        public override string ExceptionMessagePrefix => $"When calling invoking ViewComponent expected";
    }
}
