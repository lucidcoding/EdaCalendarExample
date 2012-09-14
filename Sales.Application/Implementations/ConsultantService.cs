using System;
using System.Collections.Generic;
using AutoMapper;
using NHibernate;
using NHibernate.Context;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.DataTransferObjects;
using Sales.Application.Requests;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Events;

namespace Sales.Application.Implementations
{
    public class ConsultantService : IConsultantService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IBus _bus;
        private readonly IConsultantRepository _consultantRepository;

        public ConsultantService(
            ISessionFactory sessionFactory, 
            IBus bus,
            IConsultantRepository consultantRepository)
        {
            _sessionFactory = sessionFactory;
            _bus = bus;
            _consultantRepository = consultantRepository;
        }

        public List<ConsultantDto> GetAll()
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                List<Consultant> consultants;

                using (var transaction = session.BeginTransaction())
                {
                    consultants = _consultantRepository.GetAll();
                    transaction.Commit();
                }

                Mapper.Reset();
                Mapper.CreateMap<Consultant, ConsultantDto>()
                    .ForMember(prop => prop.TimeAllocations, opt => opt.Ignore());

                var consultantDtos = Mapper.Map<List<Consultant>, List<ConsultantDto>>(consultants);
                return consultantDtos;
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public ConsultantDto GetById(Guid id)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Consultant consultant;

                using (var transaction = session.BeginTransaction())
                {
                    consultant = _consultantRepository.GetById(id);
                    transaction.Commit();
                }

                Mapper.Reset();
                Mapper.CreateMap<Consultant, ConsultantDto>();
                Mapper.CreateMap<TimeAllocation, TimeAllocationDto>()
                    .ForMember(prop => prop.Consultant, opt => opt.Ignore())
                    .Include<Appointment, AppointmentDto>();
                Mapper.CreateMap<Appointment, AppointmentDto>();
                var consultantDto = Mapper.Map<Consultant, ConsultantDto>(consultant);
                return consultantDto;
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public ValidationMessageCollection ValidateBookAppointment(BookAppointmentRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Consultant consultant;

                using (var transaction = session.BeginTransaction())
                {
                    consultant = _consultantRepository.GetById(request.ConsultantId);
                    transaction.Commit();
                }

                return consultant.ValidateBookAppointment(
                    request.Date, 
                    request.StartTime, 
                    request.EndTime,
                    request.LeadName,
                    request.Address);
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public void BookAppointment(BookAppointmentRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var consultant = _consultantRepository.GetById(request.ConsultantId);
                    DomainEvents.Register<AppointmentBookedEvent>(AppointmentBooked);
                    consultant.BookAppointment(request.Id, request.Date, request.StartTime, request.EndTime, request.LeadName, request.Address);
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

        private void AppointmentBooked(AppointmentBookedEvent @event)
        {
            _consultantRepository.SaveOrUpdate(@event.Source.Consultant);

            var appointmentBooked = new AppointmentBooked
            {
                AppointmentId = @event.Source.Id.Value,
                EmployeeId = @event.Source.Consultant.Id.Value,
                Date = @event.Source.Date,
                StartTime = @event.Source.StartTime,
                EndTime = @event.Source.EndTime,
                LeadName = @event.Source.LeadName,
                Address = @event.Source.Address
            };

            _bus.Publish(appointmentBooked);
        }

        public void BookTimeAllocation(BookTimeAllocationRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var consultant = _consultantRepository.GetById(request.ConsultantId);
                    DomainEvents.Register<TimeAllocationBookedEvent>(TimeAllocationBooked);
                    consultant.BookTimeAllocation(request.TimeAllocationId, request.Start, request.End);
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
            _consultantRepository.SaveOrUpdate(@event.Source.Consultant);
        }
    }
}
