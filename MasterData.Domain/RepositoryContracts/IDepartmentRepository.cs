using System;
using MasterData.Domain.Common;
using MasterData.Domain.Entities;

namespace MasterData.Domain.RepositoryContracts
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
    }
}
