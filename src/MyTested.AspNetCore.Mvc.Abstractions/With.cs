namespace MyTested.AspNetCore.Mvc
{
    /// <summary>
    /// Provides easy replacing of expression method argument values.
    /// </summary>
    public static class With
    {
        /// <summary>
        /// Returns an instance of the provided parameter type created with its default constructor.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns>The created instance.</returns>
        public static TParameter Default<TParameter>()
            where TParameter : new() 
            => new TParameter();

        /// <summary>
        /// Returns an instance of the provided parameter type created with its default constructor.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns>The created instance.</returns>
        public static TParameter Empty<TParameter>()
            where TParameter : new()
            => new TParameter();

        /// <summary>
        /// Indicates that an argument should not be considered in method call lambda expression.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns>Default value of the parameter.</returns>
        public static TParameter No<TParameter>() 
            => default(TParameter);

        /// <summary>
        /// Indicates that an argument should not be considered in method call lambda expression.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns>Default value of the parameter.</returns>
        /// <remarks>Using this method in route testing will indicate that the route value should be ignored during the test.</remarks>
        public static TParameter Any<TParameter>() 
            => default(TParameter);

        /// <summary>
        /// Indicates that a argument should not be considered during a route test but used in the actual action call. This method is the same as <see cref="With.IgnoredRouteValue{TParameter}(TParameter)"/>
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns>The provided value.</returns>
        /// <remarks>Using this method in pipeline testing will indicate that the route value should be ignored during the route test but used during the action execution.</remarks>
        public static TParameter Value<TParameter>(TParameter value)
            => value;

        /// <summary>
        /// Indicates that a argument should not be considered during a route test but used in the actual action call. This method is the same as <see cref="With.Value{TParameter}(TParameter)"/>.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <returns>The provided value.</returns>
        /// <remarks>Using this method in pipeline testing will indicate that the route value should be ignored during the route test but used during the action execution.</remarks>
        public static TParameter IgnoredRouteValue<TParameter>(TParameter value)
            => value;
    }
}
