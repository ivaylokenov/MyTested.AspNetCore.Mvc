namespace MyTested.Mvc.Utilities.Validators
{
    using Internal.Application;

    /// <summary>
    /// Validator class containing application services validation logic.
    /// </summary>
    public static class ServiceValidator
    {
        /// <summary>
        /// Validates whether application services are registered.
        /// </summary>
        public static void ValidateServices()
        {
            CommonValidator.CheckForNullReference(
                TestServiceProvider.Current,
                string.Format("'StartsFrom' method should be called before running this test case. MyMvc must be configured and services"));
        }

        /// <summary>
        /// Validates whether service exists.
        /// </summary>
        /// <typeparam name="TService">Type of service to validate.</typeparam>
        /// <param name="service">Service object to validate.</param>
        public static void ValidateServiceExists<TService>(TService service)
        {
            CommonValidator.CheckForNullReference(
                service,
                $"{typeof(TService).Name} could not be resolved from the services provider. Before running this test case, the service should be registered in the 'IsUsing' method and");
        }
    }
}
