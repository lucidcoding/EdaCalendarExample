using System;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.RepositoryContracts
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
    }
}
