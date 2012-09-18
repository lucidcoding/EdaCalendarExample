using Calendar.Messages.Events;
using NServiceBus;

namespace HumanResources.NServiceBusServer.Core
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
            _bus.Subscribe<BookingUpdated>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<BookingMade>();
            _bus.Unsubscribe<BookingUpdated>();
        }
    }
}
