namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using Microsoft.AspNetCore.Mvc.Filters;

    public static class IntegerExtensions
    {
        public static string ToFilterScopeName(this int integer)
            => integer switch
            {
                10 => nameof(FilterScope.Global),
                20 => nameof(FilterScope.Controller),
                30 => nameof(FilterScope.Action),
                _ => "Undefined Scope",
            };
    }
}
