using System;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.RepositoryContracts
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
    }
}
