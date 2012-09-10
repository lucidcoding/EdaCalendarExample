using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class HolidayBookedEvent : DomainEvent<Holiday>
    {
        public HolidayBookedEvent(Holiday holiday) : base (holiday)
        {
        }
    }
}
