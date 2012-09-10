using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class AppointmentBookedEvent : DomainEvent<Appointment>
    {
        public AppointmentBookedEvent(Appointment appointment) : base(appointment)
        {
        }
    }
}
