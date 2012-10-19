using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.Events;
using Calendar.Domain.RepositoryContracts;
using MasterData.Messages.Events;
using NServiceBus;

namespace Calendar.MessageHandlers.EventHandlers
{
    public class EmployeeRegisteredHandler : IHandleMessages<EmployeeRegistered>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeRegisteredHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            DomainEvents.Register<EmployeeRegisteredEvent>(EmployeeRegistered);
        }

        public void Handle(EmployeeRegistered @event)
        {
            Employee.Register(@event.Forename, @event.Surname);
        }

        private void EmployeeRegistered(EmployeeRegisteredEvent @event)
        {
            _employeeRepository.Save(@event.Source);
        }
    }
}
