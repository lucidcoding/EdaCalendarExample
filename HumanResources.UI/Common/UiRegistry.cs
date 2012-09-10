using HumanResources.Application.Common;
using NServiceBus;
using StructureMap.Configuration.DSL;

namespace HumanResources.UI.Common
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IBus>().Use(MvcApplication.Bus);
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}