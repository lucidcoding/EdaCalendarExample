using NServiceBus;

namespace Calendar.MessageHandlers.Core
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
            //_bus.Subscribe<EmployeeRegistered>();
        }

        public void Stop()
        {
            //_bus.Unsubscribe<EmployeeRegistered>();
        }
    }
}
