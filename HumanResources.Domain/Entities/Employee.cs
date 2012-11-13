using System;
using System.Collections.Generic;
using System.Linq;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;

namespace HumanResources.Domain.Entities
{
    public class Employee : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime? Joined { get; set; }
        public virtual DateTime? Left { get; set; }
        public virtual int HolidayEntitlement { get; set; }
        public virtual IList<Holiday> Holidays { get; set; }
        public Department Department { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }

        public virtual int RemainingHoliday
        {
            get { return HolidayEntitlement - Holidays.Sum(x => x.TotalDays); }
        }

        public static void Register(Guid id, string forename, string surname, Department department, DateTime joined)
        {
            var employee = new Employee
            {
                Id = id,
                Forename = forename,
                Surname = surname,
                Joined = joined,
                HolidayEntitlement = 25, //Default for this system
                Department = department
            };

            DomainEvents.Raise(new EmployeeRegisteredEvent(employee));
        }
    }
}
