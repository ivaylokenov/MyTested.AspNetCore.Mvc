namespace MyTested.AspNetCore.Mvc.Internal.Results
{
    /// <summary>
    /// Represents void method result in a generic test builder.
    /// </summary>
    public class VoidMethodResult : MethodResult
    {
        /// <summary>
        /// Gets an instance of <see cref="VoidMethodResult"/>.
        /// </summary>
        /// <returns>Void method result.</returns>
        /// <value>Instance of <see cref="VoidMethodResult"/>.</value>
        public static VoidMethodResult Instance { get; } = new VoidMethodResult();
    }
}
