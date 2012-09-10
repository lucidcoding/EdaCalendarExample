using HumanResources.Application.Common;
using StructureMap.Configuration.DSL;

namespace HumanResources.NServiceBusServer.Common
{
    class NServiceBusServerRegistry : Registry
    {
        public NServiceBusServerRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}
