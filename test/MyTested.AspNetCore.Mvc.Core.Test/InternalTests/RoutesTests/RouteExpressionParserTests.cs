namespace MyTested.AspNetCore.Mvc.Test.InternalTests.RoutesTests
{
    using Internal.Routes;
    using Setups.Routes;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Xunit;

    public class RouteExpressionParserTests
    {
        [Theory]
        [MemberData(nameof(NormalActionsWithNoParametersData))]
        public void ParseControllerAndActionWithoutParametersControllerAndActionNameAreParsed(
            Expression<Action<NormalController>> action, string controllerName, string actionName)
        {
            var result = RouteExpressionParser.Parse(action);

            Assert.Equal(controllerName, result.ControllerName);
            Assert.Equal(actionName, result.Action);
            Assert.Equal(0, result.ActionArguments.Count);
        }

        [Theory]
        [MemberData(nameof(NormalActionsWithParametersData))]
        public void ParseControllerAndActionWithPrimitiveParametersControllerActionNameAndParametersAreParsed(
            Expression<Action<NormalController>> action, string controllerName, string actionName, IDictionary<string, object> routeValues)
        {
            var result = RouteExpressionParser.Parse(action);

            Assert.Equal(controllerName, result.ControllerName);
            Assert.Equal(actionName, result.Action);
            Assert.Equal(routeValues.Count, result.ActionArguments.Count);

            foreach (var routeValue in routeValues)
            {
                Assert.True(result.ActionArguments.ContainsKey(routeValue.Key));
                Assert.Equal(routeValue.Value, result.ActionArguments[routeValue.Key].Value);
            }
        }

        [Fact]
        public void ParseControllerAndActionWithRouteAttributeControllerActionNameAndParametersAreParsed()
        {
            Expression<Action<RouteController>> expr = c => c.Index();
            var result = RouteExpressionParser.Parse(expr);

            Assert.Equal("Route", result.ControllerName);
            Assert.Equal("Index", result.Action);
            Assert.Equal(0, result.ActionArguments.Count);
        }

        [Fact]
        public void ParseControllerAndActionWithObjectParametersControllerActionNameAndParametersAreParsed()
        {
            Expression<Action<NormalController>> expr = c => c.ActionWithMultipleParameters(1, "string", new RequestModel { Integer = 1, String = "Text" });
            var result = RouteExpressionParser.Parse(expr);

            Assert.Equal("Normal", result.ControllerName);
            Assert.Equal("ActionWithMultipleParameters", result.Action);
            Assert.Equal(3, result.ActionArguments.Count);
            Assert.Equal(1, result.ActionArguments["id"].Value);
            Assert.Equal("string", result.ActionArguments["text"].Value);
            Assert.IsAssignableFrom<RequestModel>(result.ActionArguments["model"].Value);

            var model = (RequestModel)result.ActionArguments["model"].Value;
            Assert.Equal(1, model.Integer);
            Assert.Equal("Text", model.String);
        }

        [Fact]
        public void ParsePocoControllerControllerActionNameAndParametersAreParsed()
        {
            Expression<Action<PocoController>> expr = c => c.Action(1);
            var result = RouteExpressionParser.Parse(expr);

            Assert.Equal("Poco", result.ControllerName);
            Assert.Equal("Action", result.Action);
            Assert.Equal(1, result.ActionArguments.Count);
            Assert.True(result.ActionArguments.ContainsKey("id"));
            Assert.Equal(1, result.ActionArguments["id"].Value);
        }

        [Fact]
        public void ParseInAreaControllerControllerActionNameAndAreaAreParsed()
        {
            Expression<Action<InAreaController>> expr = c => c.Action(1);
            var result = RouteExpressionParser.Parse(expr);

            Assert.Equal("InArea", result.ControllerName);
            Assert.Equal("Action", result.Action);
            Assert.Equal(2, result.ActionArguments.Count);
            Assert.True(result.ActionArguments.ContainsKey("id"));
            Assert.Equal(1, result.ActionArguments["id"].Value);
            Assert.True(result.ActionArguments.ContainsKey("area"));
            Assert.Equal("MyArea", result.ActionArguments["area"].Value);
        }

        [Fact]
        public void ParseActionWithCustomRouteConstraintsRouteConstraintsAreParsed()
        {
            Expression<Action<RouteConstraintController>> expr = c => c.Action(1, 2);
            var result = RouteExpressionParser.Parse(expr);

            Assert.Equal("CustomController", result.ControllerName);
            Assert.Equal("CustomAction", result.Action);
            Assert.Equal(3, result.ActionArguments.Count);
            Assert.True(result.ActionArguments.ContainsKey("id"));
            Assert.Equal("5", result.ActionArguments["id"].Value);
            Assert.True(result.ActionArguments.ContainsKey("key"));
            Assert.Equal("value", result.ActionArguments["key"].Value);
            Assert.True(result.ActionArguments.ContainsKey("anotherId"));
            Assert.Equal(2, result.ActionArguments["anotherId"].Value);
        }

        [Fact]
        public void ParseCustomConventionsCustomConventionsAreParsed()
        {
            Expression<Action<ConventionsController>> expr = c => c.ConventionsAction(1);
            var result = RouteExpressionParser.Parse(expr, considerParameterDescriptors: true);

            Assert.Equal("ChangedController", result.ControllerName);
            Assert.Equal("ChangedAction", result.Action);
            Assert.Equal(1, result.ActionArguments.Count);
            Assert.True(result.ActionArguments.ContainsKey("ChangedParameter"));
            Assert.Equal(1, result.ActionArguments["ChangedParameter"].Value);
        }

        [Fact]
        public void ParseCustomConventionsCustomConventionsAreParsedWithNoParameters()
        {
            Expression<Action<ConventionsController>> expr = c => c.ConventionsAction(1);
            var result = RouteExpressionParser.Parse(expr);

            Assert.Equal("ChangedController", result.ControllerName);
            Assert.Equal("ChangedAction", result.Action);
            Assert.Equal(1, result.ActionArguments.Count);
            Assert.True(result.ActionArguments.ContainsKey("id"));
            Assert.Equal(1, result.ActionArguments["id"].Value);
        }

        [Fact]
        public void ParseStaticMethodCallThrowsInvalidOperationException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Expression<Action<NormalController>> expr = c => NormalController.StaticCall();
                RouteExpressionParser.Parse(expr);
            });

            Assert.Equal("Provided expression is not valid - expected instance method call but instead received static method call.", exception.Message);
        }

        [Fact]
        public void ParseNonMethodCallExceptionThrowsInvalidOperationException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Expression<Action<NormalController>> expr = c => new object();
                RouteExpressionParser.Parse(expr);
            });

            Assert.Equal("Provided expression is not valid - expected instance method call but instead received other type of expression.", exception.Message);
        }

        [Fact]
        public void ParseNonControllerActionThrowsInvalidOperationException()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Expression<Action<RequestModel>> expr = c => c.SomeMethod();
                RouteExpressionParser.Parse(expr);
            });

            Assert.Equal("Method SomeMethod in class RequestModel is not a valid controller action.", exception.Message);
        }

        public static TheoryData<Expression<Action<NormalController>>, string, string> NormalActionsWithNoParametersData
        {
            get
            {
                var data = new TheoryData<Expression<Action<NormalController>>, string, string>();

                const string controllerName = "Normal";
                data.Add(c => c.ActionWithoutParameters(), controllerName, "ActionWithoutParameters");
                data.Add(c => c.ActionWithOverloads(), controllerName, "ActionWithOverloads");
                data.Add(c => c.VoidAction(), controllerName, "VoidAction");
                data.Add(c => c.ActionWithChangedName(), controllerName, "AnotherName");

                return data;
            }
        }

        public static TheoryData<
            Expression<Action<NormalController>>,
            string,
            string,
            IDictionary<string, object>> NormalActionsWithParametersData
        {
            get
            {
                var data = new TheoryData<Expression<Action<NormalController>>, string, string, IDictionary<string, object>>();

                const string controllerName = "Normal";
                string actionName = "ActionWithOverloads";
                data.Add(
                    c => c.ActionWithOverloads(1),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { ["id"] = 1 });
                
                data.Add(
                    c => c.ActionWithOverloads(With.No<int?>()),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { });
                
                data.Add(
                    c => c.ActionWithOverloads(With.No<int>()),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { });

                data.Add(
                    c => c.ActionWithOverloads(GetInt()),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { ["id"] = 1 });

                actionName = "ActionWithMultipleParameters";
                data.Add(
                    c => c.ActionWithMultipleParameters(1, "string", null),
                    controllerName,
                    actionName,
                    new Dictionary<string, object> { ["id"] = 1, ["text"] = "string" });

                return data;
            }
        }

        private static int GetInt()
        {
            return 1;
        }
    }
}
