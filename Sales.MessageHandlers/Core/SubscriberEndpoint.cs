using Calendar.Messages.Events;
using MasterData.Messages.Events;
using NServiceBus;

namespace Sales.MessageHandlers.Core
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
            _bus.Subscribe<MakeBookingInvalidated>();
            _bus.Subscribe<UpdateBookingInvalidated>();
            _bus.Subscribe<EmployeeRegistered>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<BookingMade>();
            _bus.Unsubscribe<BookingUpdated>();
            _bus.Unsubscribe<MakeBookingInvalidated>();
            _bus.Unsubscribe<UpdateBookingInvalidated>();
            _bus.Unsubscribe<EmployeeRegistered>();
        }
    }
}
