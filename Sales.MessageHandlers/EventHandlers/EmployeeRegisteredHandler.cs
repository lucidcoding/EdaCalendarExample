using MasterData.Messages.Events;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.Global;
using Sales.Domain.RepositoryContracts;

namespace Sales.MessageHandlers.EventHandlers
{
    public class EmployeeRegisteredHandler : IHandleMessages<EmployeeRegistered>
    {
        private readonly IConsultantRepository _consultantRepository;

        public EmployeeRegisteredHandler(IConsultantRepository consultantRepository)
        {
            _consultantRepository = consultantRepository;
            DomainEvents.Register<ConsultantAddedEvent>(ConsultantAdded);
            DomainEvents.Register<ConsultantServerValidatedEvent>(ConsultantServerValidated);
        }

        public void Handle(EmployeeRegistered @event)
        {
            if (@event.DepartmentId != Constants.SalesDepartmentId)
            {
                Consultant.Add(@event.Id, @event.Forename, @event.Surname, @event.Joined);
            }
            else
            {
                var employee = _consultantRepository.GetById(@event.Id);
                employee.ServerValidate();
            }
        }

        private void ConsultantAdded(ConsultantAddedEvent @event)
        {
            _consultantRepository.Save(@event.Source);
        }

        private void ConsultantServerValidated(ConsultantServerValidatedEvent @event)
        {
            _consultantRepository.Save(@event.Source);
        }
    }
}
