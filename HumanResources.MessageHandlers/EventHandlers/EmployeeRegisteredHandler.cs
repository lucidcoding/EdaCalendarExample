using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Global;
using HumanResources.Domain.RepositoryContracts;
using MasterData.Messages.Events;
using NServiceBus;

namespace HumanResources.MessageHandlers.EventHandlers
{
    public class EmployeeRegisteredHandler : IHandleMessages<EmployeeRegistered>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeRegisteredHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            DomainEvents.Register<EmployeeAddedEvent>(EmployeeAdded);
            DomainEvents.Register<EmployeeServerValidatedEvent>(EmployeeServerValidated);
        }

        public void Handle(EmployeeRegistered @event)
        {
            if (@event.DepartmentId != Constants.HumanResoursesDepartmentId)
            {
                Employee.Add(@event.Id, @event.Forename, @event.Surname, @event.Joined);
            }
            else
            {
                var employee = _employeeRepository.GetById(@event.Id);
                employee.ServerValidate();
            }
        }

        private void EmployeeAdded(EmployeeAddedEvent @event)
        {
            _employeeRepository.Save(@event.Source);
        }

        private void EmployeeServerValidated(EmployeeServerValidatedEvent @event)
        {
            _employeeRepository.Save(@event.Source);
        }
    }
}
