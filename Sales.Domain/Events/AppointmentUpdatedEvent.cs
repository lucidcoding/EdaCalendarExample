using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class AppointmentUpdatedEvent : DomainEvent<Appointment>
    {
        public AppointmentUpdatedEvent(Appointment appointment) : base(appointment)
        {
        }
    }
}
