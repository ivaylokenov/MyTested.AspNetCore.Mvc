namespace MyTested.Mvc.Utilities.Validators
{
    using MyTested.Mvc.Common;

    public static class ServiceValidator
    {
        public static void ValidateServices()
        {
            CommonValidator.CheckForNullReference(
                TestServiceProvider.Current,
                string.Format("'IsUsing' method should be called before running this test case. MyMvc must be configured and services"));
        }

        public static void ValidateServiceExists<TService>(TService service)
        {
            CommonValidator.CheckForNullReference(
                service,
                $"{typeof(TService).Name} could not be resolved from the services provider. Before running this test case, the service should be registered in the 'IsUsing' method and");
        }
    }
}
