using System;
using System.Collections.Generic;
using AutoMapper;
using HumanResources.Application.Contracts;
using HumanResources.Application.DataTransferObjects;
using HumanResources.Application.Requests;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Context;

namespace HumanResources.Application.Implementations
{
    public class TimeAllocationService : ITimeAllocationService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ITimeAllocationRepository _timeAllocationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public TimeAllocationService(
            ISessionFactory sessionFactory,
            ITimeAllocationRepository timeAllocationRepository,
            IEmployeeRepository employeeRepository)
        {
            _sessionFactory = sessionFactory;
            _timeAllocationRepository = timeAllocationRepository;
            _employeeRepository = employeeRepository;
        }

        public void Book(BookTimeAllocationRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var employee = _employeeRepository.GetById(request.ConsultantId);
                    DomainEvents.Register<TimeAllocationBookedEvent>(TimeAllocationBooked);
                    TimeAllocation.Book(employee, request.Id, request.Start, request.End);
                    transaction.Commit();
                }
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                DomainEvents.ClearCallbacks();
                session.Dispose();
            }
        }

        private void TimeAllocationBooked(TimeAllocationBookedEvent @event)
        {
            _timeAllocationRepository.Save(@event.Source);
        }

        public void Update(UpdateTimeAllocationRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var timeAllocation = _timeAllocationRepository.GetById(request.Id);
                    DomainEvents.Register<TimeAllocationUpdatedEvent>(TimeAllocationUpdated);
                    timeAllocation.Update(request.Start, request.End);
                    transaction.Commit();
                }
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                DomainEvents.ClearCallbacks();
                session.Dispose();
            }
        }

        private void TimeAllocationUpdated(TimeAllocationUpdatedEvent @event)
        {
            _timeAllocationRepository.Update(@event.Source);
        }
        
        public void Invalidate(InvalidateTimeAllocationRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var timeAllocation = _timeAllocationRepository.GetById(request.Id);
                    DomainEvents.Register<TimeAllocationInvalidatedEvent>(TimeAllocationInvalidated);
                    timeAllocation.Invalidate(request.Message);
                    transaction.Commit();
                }
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                DomainEvents.ClearCallbacks();
                session.Dispose();
            }
        }

        private void TimeAllocationInvalidated(TimeAllocationInvalidatedEvent @event)
        {
            _timeAllocationRepository.Save(@event.Source);
        }
    }
}
