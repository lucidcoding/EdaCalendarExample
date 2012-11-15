using System.Collections.Generic;
using System.Linq;
using Calendar.Domain.Common;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Criterion;

namespace Calendar.Data.Common
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct
    {
        protected readonly ISessionFactory _sessionFactory;

        public Repository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        private static ProxyFactoryFactory _pff;

        public virtual void SaveOrUpdate(TEntity obj)
        {
            _sessionFactory.GetCurrentSession().SaveOrUpdate(obj);
        }

        public virtual void Save(TEntity obj)
        {
            _sessionFactory.GetCurrentSession().Save(obj);
        }

        public virtual void Update(TEntity obj)
        {
            _sessionFactory.GetCurrentSession().Update(obj);
        }

        public virtual TEntity GetById(TId id)
        {
            return _sessionFactory.GetCurrentSession().Get<TEntity>(id);
        }

        public virtual TEntity LoadById(TId id)
        {
            return _sessionFactory.GetCurrentSession().Load<TEntity>(id);
        }

        public virtual List<TEntity> GetAll()
        {
            var criteriaQuery = _sessionFactory.GetCurrentSession().CreateCriteria(typeof(TEntity));
            return (List<TEntity>)criteriaQuery.List<TEntity>();
        }

        public List<TEntity> GetByIds(List<TId> ids)
        {
            return _sessionFactory.GetCurrentSession().CreateCriteria<TEntity>()
                .Add(Expression.In("Id", ids))
                .List<TEntity>()
                .ToList();
        }

        public void Flush()
        {
            _sessionFactory.GetCurrentSession().Flush();
        }
    }
}

