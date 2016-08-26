namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using Utilities.Extensions;

    public class ViewComponentTestContext : ComponentTestContext
    {
        public override string ExceptionMessagePrefix => $"When invoking {this.Component.GetName()} expected";
    }
}
