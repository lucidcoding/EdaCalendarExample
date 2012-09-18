using System;
using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.RepositoryContracts
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
    }
}
