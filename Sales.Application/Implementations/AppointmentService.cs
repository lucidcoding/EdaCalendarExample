using System;
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

namespace Sales.Application.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IBus _bus;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConsultantRepository _consultantRepository;

        public AppointmentService(
            ISessionFactory sessionFactory, 
            IBus bus,
            IAppointmentRepository appointmentRepository,
            IConsultantRepository consultantRepository)
        {
            _sessionFactory = sessionFactory;
            _bus = bus;
            _appointmentRepository = appointmentRepository;
            _consultantRepository = consultantRepository;
        }

        public AppointmentDto GetById(Guid id)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Appointment appointment;

                using (var transaction = session.BeginTransaction())
                {
                    appointment = _appointmentRepository.GetById(id);
                    transaction.Commit();
                }

                Mapper.Reset();
                Mapper.CreateMap<TimeAllocation, TimeAllocationDto>()
                    .ForMember(prop => prop.Consultant, opt => opt.Ignore())
                    .Include<Appointment, AppointmentDto>();
                Mapper.CreateMap<Appointment, AppointmentDto>();
                Mapper.CreateMap<Consultant, ConsultantDto>();
                var appointmentDto = Mapper.Map<Appointment, AppointmentDto>(appointment);
                return appointmentDto;
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }


        public ValidationMessageCollection ValidateBook(BookAppointmentRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Consultant consultant = null;

                using (var transaction = session.BeginTransaction())
                {
                    consultant = _consultantRepository.GetById(request.ConsultantId);
                    transaction.Commit();
                }

                return Appointment.ValidateBook(
                    consultant,
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

        public void Book(BookAppointmentRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var consultant = _consultantRepository.GetById(request.ConsultantId);
                    DomainEvents.Register<AppointmentBookedEvent>(AppointmentBooked);
                    Appointment.Book(consultant, request.Id, request.Date, request.StartTime, request.EndTime, request.LeadName, request.Address);
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
            _appointmentRepository.Save(@event.Source);
        }

        public ValidationMessageCollection ValidateUpdate(UpdateAppointmentRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Appointment appointment;

                using (var transaction = session.BeginTransaction())
                {
                    appointment = _appointmentRepository.GetById(request.Id);
                    transaction.Commit();
                }

                return appointment.ValidateUpdate(
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

        public void Update(UpdateAppointmentRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Appointment appointment;

                using (var transaction = session.BeginTransaction())
                {
                    appointment = _appointmentRepository.GetById(request.Id);
                    DomainEvents.Register<AppointmentUpdatedEvent>(AppointmentUpdated);

                    appointment.Update(
                        request.Date,
                        request.StartTime,
                        request.EndTime,
                        request.LeadName,
                        request.Address);

                    transaction.Commit();
                }
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        private void AppointmentUpdated(AppointmentUpdatedEvent @event)
        {
            _appointmentRepository.Update(@event.Source);
        }
    }
}
