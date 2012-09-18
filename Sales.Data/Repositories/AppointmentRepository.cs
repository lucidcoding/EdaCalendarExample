using System;
using NHibernate;
using Sales.Data.Common;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

namespace Sales.Data.Repositories
{
    public class  AppointmentRepository : Repository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
