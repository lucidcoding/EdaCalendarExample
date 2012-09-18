using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class HolidayUpdatedEvent : DomainEvent<Holiday>
    {
        public HolidayUpdatedEvent(Holiday holiday) : base(holiday)
        {
        }
    }
}
