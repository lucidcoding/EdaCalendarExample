using System;
using MasterData.Domain.Common;
using MasterData.Domain.Events;

namespace MasterData.Domain.Entities
{
    public class Employee : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime? Joined { get; set; }
        public virtual DateTime? Left { get; set; }
        public Department Department { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }

        public static void Register(Guid id, string forename, string surname, Department department)
        {
            var employee = new Employee
            {
                Id = id,
                Forename = forename,
                Surname = surname,
                Department = department,
                Joined = DateTime.Now
            };

            DomainEvents.Raise(new EmployeeRegisteredEvent(employee));

        }
    }
}
