using System.Collections.Generic;

namespace Sales.Domain.Common
{
    public interface IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        void SaveOrUpdate(TEntity obj);
        void Save(TEntity obj);
        void Update(TEntity obj);
        TEntity GetById(TId id);
        TEntity LoadById(TId id);
        List<TEntity> GetAll();
        List<TEntity> GetByIds(List<TId> ids);
        void Flush();
    }
}