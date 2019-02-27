namespace MyTested.AspNetCore.Mvc.Internal
{
    using Configuration;

    public class ExceptionMessages
    {
        public const string ResponseModelFormat = "{0} response model {1} to be the given model, but in fact it was a different one.";
        public const string ResponseModelOfTypeFormat = "{0} response model to be of {1} type, but instead received {2}.";

        public const string ContentResultFormat = "{0} content result to contain '{1}', but instead received '{2}'.";
        public const string ContentResultPredicateFormat = "{0} content result ('{1}') to pass the given predicate, but it failed.";

        public static readonly string RouteTestingUnavailable = $"Testing routes without a Startup class is not supported. Set the '{GeneralTestConfiguration.PrefixKey}:{GeneralTestConfiguration.NoStartupKey}' option in the test configuration ('{ServerTestConfiguration.DefaultConfigurationFile}' file by default) to 'true' and provide a Startup class.";
    }
}
