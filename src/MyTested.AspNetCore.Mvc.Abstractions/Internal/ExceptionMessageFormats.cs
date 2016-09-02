namespace MyTested.AspNetCore.Mvc.Internal
{
    public class ExceptionMessageFormats
    {
        public const string ResponseModel = "{0} response model {1} to be the given model, but in fact it was a different one.";
        public const string ResponseModelOfType = "{0} response model to be of {1} type, but instead received {2}.";

        public const string ContentResult = "{0} content result to contain '{1}', but instead received '{2}'.";
        public const string ContentResultPredicate = "{0} content result ('{1}') to pass the given predicate, but it failed.";
    }
}
