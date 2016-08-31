namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using System.Linq.Expressions;
    using System.Reflection;

    public partial class ViewComponentBuilder<TViewComponent>
    {
        protected override void ProcessAndValidateMethod(LambdaExpression methodCall, MethodInfo methodInfo)
        {
        }
    }
}
