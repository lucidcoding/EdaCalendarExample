using System;
using System.Collections.Generic;

namespace Sales.Application.DataTransferObjects
{
    public class ConsultantDto
    {
        public Guid? Id { get; set; }
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual IList<TimeAllocationDto> TimeAllocations { get; set; }
        public virtual string FullName { get; set; }
    }
}
