using System;
using System.Collections.Generic;
using System.Linq;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Consultant : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual IList<TimeAllocation> TimeAllocations { get; set; }
        public virtual bool ServiceValidated { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }

        public virtual IList<Appointment> Appointments
        {
            get
            {
                return (from timeAllocation in TimeAllocations
                        where (timeAllocation as Appointment) != null
                        select timeAllocation as Appointment)
                    .ToList();
            }
        }

        public static void Add(Guid id, string forename, string surname, DateTime joined)
        {
            var consultant = new Consultant
            {
                Id = id,
                Forename = forename,
                Surname = surname
            };

            DomainEvents.Raise(new ConsultantAddedEvent(consultant));
        }

        public virtual void ServerValidate()
        {
            ServiceValidated = true;
            DomainEvents.Raise(new ConsultantServerValidatedEvent(this));
        }
    }
}
