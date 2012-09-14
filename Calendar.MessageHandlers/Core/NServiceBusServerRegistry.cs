using Calendar.Data.Core;
using Calendar.Domain.Common;
using Calendar.Domain.Events;
using Calendar.MessageHandlers.DomainEventHandlers;
using StructureMap.Configuration.DSL;

namespace Calendar.MessageHandlers.Core
{
    class NServiceBusServerRegistry : Registry
    {
        public NServiceBusServerRegistry()
        {
            Configure(x =>
            {
                For<IDomainEventHandler<BookingMadeEvent>>().Use<BookingMadeDomainEventHandler>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}
