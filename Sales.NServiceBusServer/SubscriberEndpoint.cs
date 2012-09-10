using HumanResources.Messages.Events;
using NServiceBus;

namespace Sales.NServiceBusServer
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
            _bus.Subscribe<HolidayBooked>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<HolidayBooked>();
        }
    }
}
