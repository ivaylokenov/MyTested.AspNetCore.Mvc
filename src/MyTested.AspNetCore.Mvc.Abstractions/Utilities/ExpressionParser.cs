namespace MyTested.AspNetCore.Mvc.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Internal.TestContexts;

    /// <summary>
    /// Utility class helping parsing expression trees.
    /// </summary>
    public static class ExpressionParser
    {
        public const string IgnoredExpressionArgument = "!__Ignored_Expression_Value__!";

        private static readonly Type TypeOfWith = typeof(With);
        private static readonly Type TypeOfFrom = typeof(From);

        /// <summary>
        /// Parses method info from method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Method info.</returns>
        public static MethodInfo GetMethodInfo(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpression(expression);
            return methodCallExpression.Method;
        }

        /// <summary>
        /// Parses method name from method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Method name as string.</returns>
        public static string GetMethodName(LambdaExpression expression)
        {
            var methodInfo = GetMethodInfo(expression);
            return methodInfo.Name;
        }

        /// <summary>
        /// Resolves arguments from method in method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Collection of method argument information.</returns>
        public static IEnumerable<MethodArgumentTestContext> ResolveMethodArguments(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpression(expression);
            if (!methodCallExpression.Arguments.Any())
            {
                return Enumerable.Empty<MethodArgumentTestContext>();
            }

            return methodCallExpression.Arguments
                .Zip(
                    methodCallExpression.Method.GetParameters(),
                    (m, a) => new
                    {
                        a.Name,
                        Value = ResolveExpressionValue(m)
                    })
                .Select(ma => new MethodArgumentTestContext
                {
                    Name = ma.Name,
                    Type = ma.Value?.GetType(),
                    Value = ma.Value
                })
                .ToList();
        }

        public static object ResolveExpressionValue(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
            {
                // Expression which contains converting from type to type
                var expressionArgumentAsUnary = (UnaryExpression)expression;
                expression = expressionArgumentAsUnary.Operand;
            }

            if (expression.NodeType == ExpressionType.Call)
            {
                // These expressions types should be ignored and can be skipped: 
                // - c => c.Action(With.No<int>()) 
                // - c => c.Action(With.Any<int>()) 
                // - c => c.Action(From.Services<IService>())
                var expressionArgumentAsMethodCall = (MethodCallExpression)expression;
                var expressionMethodDeclaringType = expressionArgumentAsMethodCall.Method.DeclaringType;

                if (expressionArgumentAsMethodCall.Object == null)
                {
                    var expressionArgumentMethodName = expressionArgumentAsMethodCall.Method.Name;

                    if (expressionMethodDeclaringType == TypeOfWith &&
                        expressionArgumentMethodName == nameof(With.Any))
                    {
                        return IgnoredExpressionArgument;
                    }

                    if ((expressionMethodDeclaringType == TypeOfWith &&
                         expressionArgumentMethodName != nameof(With.Default))
                        || expressionMethodDeclaringType == TypeOfFrom)
                    {
                        return null;
                    }
                }
            }

            if (expression.NodeType == ExpressionType.Constant)
            {
                // Expression of type c => c.Action({const})
                // Value can be extracted without compiling.
                return ((ConstantExpression)expression).Value;
            }

            if (expression.NodeType == ExpressionType.MemberAccess
                && ((MemberExpression)expression).Member is FieldInfo)
            {
                // Expression of type c => c.Action(id)
                // Value can be extracted without compiling.
                var memberAccessExpr = (MemberExpression)expression;
                var constantExpression = (ConstantExpression)memberAccessExpr.Expression;
                if (constantExpression != null)
                {
                    var innerMemberName = memberAccessExpr.Member.Name;
                    var compiledLambdaScopeField = constantExpression.Value.GetType().GetField(innerMemberName);
                    return compiledLambdaScopeField.GetValue(constantExpression.Value);
                }
            }

            // Expression needs compiling because it is not of constant type.
            var convertExpression = Expression.Convert(expression, typeof(object));
            return Expression.Lambda<Func<object>>(convertExpression).Compile().Invoke();
        }

        /// <summary>
        /// Parses member name from member lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Member name as string.</returns>
        public static string GetPropertyName(LambdaExpression expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("Provided expression is not a valid member expression.");
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Gets instance method call expression from a lambda expression.
        /// </summary>
        /// <param name="expression">The lambda expression as MethodCallExpression.</param>
        /// <returns>Method call expression.</returns>
        public static MethodCallExpression GetMethodCallExpression(LambdaExpression expression)
        {
            if (!(expression.Body is MethodCallExpression methodCallExpression))
            {
                throw new InvalidOperationException("Provided expression is not valid - expected instance method call but instead received other type of expression.");
            }

            var objectInstance = methodCallExpression.Object;
            if (objectInstance == null)
            {
                throw new InvalidOperationException("Provided expression is not valid - expected instance method call but instead received static method call.");
            }

            return methodCallExpression;
        }

        public static bool IsIgnoredArgument(object value) 
            => value == null || value.ToString() == IgnoredExpressionArgument;
    }
}
