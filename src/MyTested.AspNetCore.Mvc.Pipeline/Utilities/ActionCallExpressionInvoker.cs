namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;
    using Builders.Pipeline;

    public static class ActionCallExpressionInvoker<TController>
        where TController : class
    {
        private static readonly Type ActionCallExpressionInvokerType = typeof(ActionCallExpressionInvoker<TController>);

        private static readonly Type VoidType = typeof(void);
        private static readonly Type TaskType = typeof(Task);
        private static readonly Type GenericTaskType = typeof(Task<>);

        private static readonly Type BuilderType = typeof(WhichControllerInstanceBuilder<TController>);

        private static readonly MethodInfo InvokeResultMethod = Reflection.GetNonPublicMethod(BuilderType, "InvokeResult");
        private static readonly MethodInfo InvokeAsyncResultMethod = Reflection.GetNonPublicMethod(BuilderType, "InvokeAsyncResult");
        private static readonly MethodInfo InvokeVoidMethod = Reflection.GetNonPublicMethod(BuilderType, "InvokeVoid");
        private static readonly MethodInfo InvokeAsyncVoidMethod = Reflection.GetNonPublicMethod(BuilderType, "InvokeAsyncVoid");

        public static void Invoke(
            LambdaExpression methodCall,
            WhichControllerInstanceBuilder<TController> builder)
        {
            var methodInfo = ExpressionParser.GetMethodInfo(methodCall);
            var methodReturnType = methodInfo.ReturnType;

            if (Reflection.AreSameTypes(methodReturnType, VoidType))
            {
                var actionCall = Expression.Lambda<Action<TController>>(
                    methodCall.Body,
                    methodCall.Parameters);

                InvokeVoidMethod.Invoke(builder, new[] { actionCall });
            }
            else if (Reflection.AreSameTypes(methodReturnType, TaskType))
            {
                var actionCall = Expression.Lambda<Func<TController, Task>>(
                    methodCall.Body,
                    methodCall.Parameters);

                InvokeAsyncVoidMethod.Invoke(builder, new[] { actionCall });
            }
            else
            {
                var methodName = nameof(CallInvokeResult);
                var invokeMethod = InvokeResultMethod;

                if (Reflection.HasGenericTypeDefinition(methodReturnType, GenericTaskType))
                {
                    methodName = nameof(CallAsyncResult);
                    invokeMethod = InvokeAsyncResultMethod;
                    methodReturnType = methodReturnType.GetGenericArguments().First();
                }

                var invokeActionOpenGeneric = Reflection.GetNonPublicMethod(
                    ActionCallExpressionInvokerType, 
                    methodName);

                var invokeActionClosedGeneric = invokeActionOpenGeneric
                    .MakeGenericMethod(methodReturnType);

                invokeActionClosedGeneric
                    .Invoke(null, new object[] { methodCall, builder, invokeMethod });
            }
        }

        // Called via reflection.
        private static void CallInvokeResult<TActionResult>(
            LambdaExpression methodCall,
            WhichControllerInstanceBuilder<TController> builder,
            MethodInfo invokeResultMethod)
        {
            var actionCall = Expression.Lambda<Func<TController, TActionResult>>(
                methodCall.Body,
                methodCall.Parameters);

            invokeResultMethod
                .MakeGenericMethod(typeof(TActionResult))
                .Invoke(builder, new[] { actionCall });
        }

        // Called via reflection.
        private static void CallAsyncResult<TActionResult>(
            LambdaExpression methodCall,
            WhichControllerInstanceBuilder<TController> builder,
            MethodInfo invokeAsyncResultMethod)
        {
            var actionCall = Expression.Lambda<Func<TController, Task<TActionResult>>>(
                methodCall.Body,
                methodCall.Parameters);

            invokeAsyncResultMethod
                .MakeGenericMethod(typeof(TActionResult))
                .Invoke(builder, new[] { actionCall });
        }
    }
}
