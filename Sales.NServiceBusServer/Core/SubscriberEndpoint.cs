//using HumanResources.Messages.Events;

using Calendar.Messages.Events;
using NServiceBus;

namespace Sales.NServiceBusServer.Core
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
            _bus.Subscribe<BookingMade>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<BookingMade>();
        }
    }
}
