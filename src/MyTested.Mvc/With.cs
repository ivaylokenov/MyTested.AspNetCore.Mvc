namespace MyTested.Mvc
{
    /// <summary>
    /// Provides easy replacing of expression method argument values.
    /// </summary>
    public static class With
    {
        /// <summary>
        /// Indicates that a argument should not be considered in method call lambda expression.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns><see cref="TParameter"/></returns>
        public static TParameter No<TParameter>()
        {
            return default(TParameter);
        }
    }
}
