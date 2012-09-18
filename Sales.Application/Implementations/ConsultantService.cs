using System;
using System.Collections.Generic;
using AutoMapper;
using NHibernate;
using NHibernate.Context;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.DataTransferObjects;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

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
    }
}
