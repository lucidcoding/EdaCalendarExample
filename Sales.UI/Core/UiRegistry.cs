using NServiceBus;
using Sales.Application.Core;
using StructureMap.Configuration.DSL;

namespace Sales.UI.Core
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