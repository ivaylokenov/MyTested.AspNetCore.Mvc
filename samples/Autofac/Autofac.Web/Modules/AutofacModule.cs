namespace Autofac.Web.Modules
{
    using Services;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder) 
            => builder.RegisterType<DataService>().As<IDataService>();
    }
}
