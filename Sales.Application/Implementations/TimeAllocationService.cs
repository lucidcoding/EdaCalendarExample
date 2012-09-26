//using NHibernate;
//using NHibernate.Context;
//using Sales.Application.Contracts;
//using Sales.Application.Requests;
//using Sales.Domain.Common;
//using Sales.Domain.Entities;
//using Sales.Domain.Events;
//using Sales.Domain.RepositoryContracts;

//namespace Sales.Application.Implementations
//{
//    public class TimeAllocationService : ITimeAllocationService
//    {
//        private readonly ISessionFactory _sessionFactory;
//        private readonly ITimeAllocationRepository _timeAllocationRepository;
//        private readonly IConsultantRepository _consultantRepository;

//        public TimeAllocationService(
//            ISessionFactory sessionFactory, 
//            ITimeAllocationRepository timeAllocationRepository,
//            IConsultantRepository consultantRepository)
//        {
//            _sessionFactory = sessionFactory;
//            _timeAllocationRepository = timeAllocationRepository;
//            _consultantRepository = consultantRepository;
//        }

//        public void Book(BookTimeAllocationRequest request)
//        {
//            var session = _sessionFactory.OpenSession();
//            CurrentSessionContext.Bind(session);

//            try
//            {
//                using (var transaction = session.BeginTransaction())
//                {
//                    var consultant = _consultantRepository.GetById(request.ConsultantId);
//                    DomainEvents.Register<TimeAllocationBookedEvent>(TimeAllocationBooked);
//                    TimeAllocation.Book(consultant, request.Id, request.Start, request.End);
//                    transaction.Commit();
//                }
//            }
//            finally
//            {
//                CurrentSessionContext.Unbind(_sessionFactory);
//                DomainEvents.ClearCallbacks();
//                session.Dispose();
//            }
//        }

//        private void TimeAllocationBooked(TimeAllocationBookedEvent @event)
//        {
//            _consultantRepository.Save(@event.Source.Consultant);
//        }

//        public void Update(UpdateTimeAllocationRequest request)
//        {
//            var session = _sessionFactory.OpenSession();
//            CurrentSessionContext.Bind(session);

//            try
//            {
//                using (var transaction = session.BeginTransaction())
//                {
//                    var timeAllocation = _timeAllocationRepository.GetById(request.Id);
//                    DomainEvents.Register<TimeAllocationUpdatedEvent>(TimeAllocationUpdated);
//                    timeAllocation.Update(request.Start, request.End);
//                    transaction.Commit();
//                }
//            }
//            finally
//            {
//                CurrentSessionContext.Unbind(_sessionFactory);
//                DomainEvents.ClearCallbacks();
//                session.Dispose();
//            }
//        }

//        private void TimeAllocationUpdated(TimeAllocationUpdatedEvent @event)
//        {
//            _timeAllocationRepository.Update(@event.Source);
//        }

//        public void Invalidate(InvalidateTimeAllocationRequest request)
//        {
//            var session = _sessionFactory.OpenSession();
//            CurrentSessionContext.Bind(session);

//            try
//            {
//                using (var transaction = session.BeginTransaction())
//                {
//                    var timeAllocation = _timeAllocationRepository.GetById(request.Id);

//                    if (timeAllocation != null)
//                    {
//                        DomainEvents.Register<TimeAllocationInvalidatedEvent>(TimeAllocationInvalidated);
//                        timeAllocation.Invalidate(request.Message);
//                        transaction.Commit();
//                    }
//                }
//            }
//            finally
//            {
//                CurrentSessionContext.Unbind(_sessionFactory);
//                DomainEvents.ClearCallbacks();
//                session.Dispose();
//            }
//        }

//        private void TimeAllocationInvalidated(TimeAllocationInvalidatedEvent @event)
//        {
//            _timeAllocationRepository.Save(@event.Source);
//        }
//    }
//}
