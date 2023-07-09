namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class FuncExtensions
    {
        public static FieldInfo GetTargetField<T, TResult>(
            this Func<T, TResult> func,
            string fieldName)
            => func
               .Target
               ?.GetType()
               .GetTypeInfo()
               .DeclaredFields
               .FirstOrDefault(m => m.Name == fieldName);
    }
}
