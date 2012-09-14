using HumanResources.Application.Core;
using NServiceBus;
using StructureMap.Configuration.DSL;

namespace HumanResources.UI.Core
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