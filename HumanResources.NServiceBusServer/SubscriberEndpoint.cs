using NServiceBus;
using Sales.Messages.Events;

namespace HumanResources.NServiceBusServer
{
    public class SubscriberEndpoint : IWantToRunAtStartup
    {
        private readonly IBus _bus;

        public SubscriberEndpoint(IBus bus)
        {
            _bus = bus;
        }

        public void Run()
        {
            _bus.Subscribe<AppointmentBooked>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<AppointmentBooked>();
        }
    }
}
