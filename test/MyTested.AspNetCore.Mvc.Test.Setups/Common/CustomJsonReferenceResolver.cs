namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Newtonsoft.Json.Serialization;

    public class CustomJsonReferenceResolver : IReferenceResolver
    {
        public void AddReference(object context, string reference, object value)
        {
        }

        public string GetReference(object context, object value)
        {
            return string.Empty;
        }

        public bool IsReferenced(object context, object value)
        {
            return true;
        }

        public object ResolveReference(object context, string reference)
        {
            return new object();
        }
    }
}
