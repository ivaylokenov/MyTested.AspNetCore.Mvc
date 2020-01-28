namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;

    public static class DelegateExtensions
    {
        public static T ConvertTo<T>(this Delegate source) where T : Delegate
            => Delegate.CreateDelegate(typeof(T), source.Target, source.Method) as T;
    }
}
