namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System.Reflection;
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Utilities.Extensions;

    public class ControllerTestContext : ActionTestContext<ControllerContext>
    {
        public ModelStateDictionary ModelState => this.ComponentContext.ModelState;

        public override string ExceptionMessagePrefix => $"When calling {this.MethodName} action in {this.Component.GetName()} expected";

        protected override ControllerContext DefaultComponentContext
            => ControllerContextMock.Default(this);
    }
}
