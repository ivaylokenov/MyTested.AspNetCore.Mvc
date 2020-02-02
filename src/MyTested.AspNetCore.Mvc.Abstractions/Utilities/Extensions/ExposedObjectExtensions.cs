namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;

    public static class ExposedObjectExtensions
    {
        public static dynamic Exposed(this object instance)
            => new ExposedObject(instance);

        public static dynamic Exposed(this Type type)
            => new ExposedObject(type);
    }
}
