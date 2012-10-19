using System;
using System.Collections.Generic;
using Calendar.Domain.Common;
using Calendar.Domain.Events;

namespace Calendar.Domain.Entities
{
    public class Employee : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual IList<Booking> Bookings { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }

        public static void Register(string forename, string surname)
        {
            var employee = new Employee
            {
                Forename = forename,
                Surname = surname
            };

            DomainEvents.Raise(new EmployeeRegisteredEvent(employee));
        }
    }
}
