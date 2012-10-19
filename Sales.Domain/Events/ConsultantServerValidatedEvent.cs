using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class ConsultantServerValidatedEvent : DomainEvent<Consultant>
    {
        public ConsultantServerValidatedEvent(Consultant consultant)
            : base(consultant)
        {
        }
    }
}
