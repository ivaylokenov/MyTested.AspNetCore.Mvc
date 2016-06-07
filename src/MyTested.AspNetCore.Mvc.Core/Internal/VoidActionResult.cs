namespace MyTested.AspNetCore.Mvc.Internal
{
    /// <summary>
    /// Represents void action result in generic test builder.
    /// </summary>
    public class VoidActionResult
    {
        /// <summary>
        /// Gets an instance of <see cref="VoidActionResult"/>.
        /// </summary>
        /// <returns>Void action result.</returns>
        /// <value>Instance of <see cref="VoidActionResult"/>.</value>
        public static VoidActionResult Instance { get; } = new VoidActionResult();
    }
}
