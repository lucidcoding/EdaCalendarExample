using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class ConsultantAddedEvent : DomainEvent<Consultant>
    {
        public ConsultantAddedEvent(Consultant consultant)
            : base(consultant)
        {
        }
    }
}
