namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;

    public static class ActionTestContextExtensions
    {
        private static readonly Type ActionResultGenericType = typeof(ActionResult<>);
        private static readonly Type ObjectResultType = typeof(ObjectResult);

        public static ActionTestContext ConvertMethodResult(this ActionTestContext testContext)
        {
            var methodReturnType = testContext.Method.ReturnType;

            if (Reflection.AreAssignableByGeneric(ActionResultGenericType, methodReturnType)
                || Reflection.AreAssignableByTaskGeneric(ActionResultGenericType, methodReturnType))
            {
                var methodResultType = testContext.MethodResult.GetType();

                if (Reflection.AreSameTypes(ObjectResultType, methodResultType))
                {
                    var objectResult = testContext.MethodResult as ObjectResult;

                    testContext.MethodResult = objectResult?.Value;
                }
            }

            return testContext;
        }
    }
}
