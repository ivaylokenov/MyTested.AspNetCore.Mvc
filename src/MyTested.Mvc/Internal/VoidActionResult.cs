namespace MyTested.Mvc.Internal
{
    /// <summary>
    /// Represents void action result in generic test builder.
    /// </summary>
    public class VoidActionResult
    {
        private static VoidActionResult defaultVoidActionResult = new VoidActionResult();

        /// <summary>
        /// Creates new instance of <see cref="VoidActionResult"/>.
        /// </summary>
        /// <returns>Void action result.</returns>
        public static VoidActionResult Instance => defaultVoidActionResult;
    }
}
