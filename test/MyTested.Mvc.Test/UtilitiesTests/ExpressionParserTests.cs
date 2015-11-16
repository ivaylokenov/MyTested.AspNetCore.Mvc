namespace MyTested.Mvc.Test.UtilitiesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Common;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Utilities;
    using Microsoft.AspNet.Mvc;
    using Xunit;
    
    public class ExpressionParserTests
    {
        [Fact]
        public void GetMethodNameShouldReturnCorrectMethodNameWithValidMethodCallExpression()
        {
            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultAction();
            var methodName = ExpressionParser.GetMethodName(expression);
            Assert.Equal("OkResultAction", methodName);
        }

        [Fact]
        public void GetMethodNameShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;
            var exception = Assert.Throws<ArgumentException>(() => ExpressionParser.GetMethodName(expression));
            Assert.Equal("Provided expression is not a valid method call.", exception.Message);
        }

        [Fact]
        public void ResolveMethodArgumentsShouldReturnCorrectCollectionOfArgumentsInformation()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultActionWithRequestBody(1, requestModel);

            var result = ExpressionParser.ResolveMethodArguments(expression);

            var arguments = result as IList<MethodArgumentInfo> ?? result.ToArray();
            var firstArgument = arguments.First();
            var secondArgument = arguments.Last();
            var secondArgumentAsRequestModel = secondArgument.Value as RequestModel;

            Assert.Equal("id", firstArgument.Name);
            Assert.Equal(typeof(int), firstArgument.Type);
            Assert.Equal(1, firstArgument.Value);

            Assert.Equal("model", secondArgument.Name);
            Assert.Equal(typeof(RequestModel), secondArgument.Type);
            Assert.NotNull(secondArgumentAsRequestModel);
            Assert.Equal(2, secondArgumentAsRequestModel.Integer);
            Assert.Equal("Test", secondArgumentAsRequestModel.RequiredString);
        }

        [Fact]
        public void ResolveMethodArgumentsShouldReturnEmptyCollectionIfMethodDoesNotHaveArguments()
        {
            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultAction();
            var result = ExpressionParser.ResolveMethodArguments(expression);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void ResolveMethodArgumentsShouldReturnProperArgumentsWithNullValues()
        {
            Expression<Func<MvcController, IActionResult>> expression = c => c.OkResultActionWithRequestBody(1, null);
            var result = ExpressionParser.ResolveMethodArguments(expression).ToList();

            var first = result[0];
            var second = result[1];

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, first.Value);
            Assert.Null(second.Value);
        }

        [Fact]
        public void ResolveMethodArgumentsShouldThrowArgumentExceptionWithInvalidMethodCallExpression()
        {
            Expression<Func<int>> expression = () => 0;
            var exception = Assert.Throws<ArgumentException>(() => ExpressionParser.ResolveMethodArguments(expression));
            Assert.Equal("Provided expression is not a valid method call.", exception.Message);
        }

        [Fact]
        public void GetPropertyNameShouldReturnProperMemberNameWithValidExpression()
        {
            Expression<Func<MvcController, object>> expression = c => c.InjectedService;
            var result = ExpressionParser.GetPropertyName(expression);

            Assert.Equal("InjectedService", result);
        }

        [Fact]
        public void GetPropertyNameShouldThrowExceptionWithInvalidMemberExpression()
        {
            Expression<Func<MvcController, object>> expression = c => c.OkResultWithResponse();
            var exception = Assert.Throws<ArgumentException>(() => ExpressionParser.GetPropertyName(expression));
            Assert.Equal("Provided expression is not a valid member expression.", exception.Message);
        }

        [Fact]
        public void GetMethodAttributesShouldReturnProperAttributes()
        {
            Expression<Func<MvcController, object>> expression = c => c.VariousAttributesAction();
            var attributes = ExpressionParser.GetMethodAttributes(expression).Select(a => a.GetType()).ToList();

            var expectedTypes = new List<Type>
            {
                typeof(AllowAnonymousAttribute),
                typeof(RouteAttribute),
                typeof(ActionNameAttribute),
                typeof(NonActionAttribute),
                typeof(AcceptVerbsAttribute),
                typeof(HttpDeleteAttribute)
            };

            Assert.NotNull(attributes);
            Assert.Equal(6, attributes.Count());

            var allAttributesArePresent = expectedTypes.All(attributes.Contains);
            Assert.True(allAttributesArePresent);
        }
    }
}
