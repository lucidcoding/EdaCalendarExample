using System;
using NServiceBus;

namespace MasterData.Messages.Events
{
    public class EmployeeRegistered : IEvent
    {
        public Guid Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime Joined { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
