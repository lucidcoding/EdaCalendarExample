using NServiceBus;
using Sales.Application.Common;
using StructureMap.Configuration.DSL;

namespace Sales.UI.Common
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IBus>().Use(MvcApplication.Bus); ;
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}