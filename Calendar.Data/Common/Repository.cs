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

        /// <summary>
        /// This reference does nothing except make sure NHibernate.ByteCode.Castle 
        /// is copied.
        /// </summary>
        private static ProxyFactoryFactory _pff;

        /// <summary>
        /// Saves an entity if it is new, or updates it if it is old.
        /// </summary>
        /// <param name="obj">The entity to save or update.</param>
        public virtual void SaveOrUpdate(TEntity obj)
        {
            _sessionFactory.GetCurrentSession().SaveOrUpdate(obj);
        }

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="obj">The entity to save.</param>
        public virtual void Save(TEntity obj)
        {
            _sessionFactory.GetCurrentSession().Save(obj);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="obj">The entity to update.</param>
        public virtual void Update(TEntity obj)
        {
            _sessionFactory.GetCurrentSession().Update(obj);
        }

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to return.</param>
        /// <returns>The entity with the specified ID.</returns>
        public virtual TEntity GetById(TId id)
        {
            return _sessionFactory.GetCurrentSession().Get<TEntity>(id);
        }

        /// <summary>
        /// Loads a proxy for an entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the proxy of the entity to return.</param>
        /// <returns>A proxy of the entity with the specified ID.</returns>
        public virtual TEntity LoadById(TId id)
        {
            return _sessionFactory.GetCurrentSession().Load<TEntity>(id);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>List of all entities.</returns>
        public virtual List<TEntity> GetAll()
        {
            var criteriaQuery = _sessionFactory.GetCurrentSession().CreateCriteria(typeof(TEntity));
            return (List<TEntity>)criteriaQuery.List<TEntity>();
        }

        /// <summary>
        /// Gets entities with specified Ids.
        /// </summary>
        /// <param name="ids">Ids of entities to get.</param>
        /// <returns>List of entities matching ids.</returns>
        public List<TEntity> GetByIds(List<TId> ids)
        {
            return _sessionFactory.GetCurrentSession().CreateCriteria<TEntity>()
                .Add(Expression.In("Id", ids))
                .List<TEntity>()
                .ToList();
        }
    }
}

