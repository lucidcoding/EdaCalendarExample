using MasterData.Domain.Common;
using MasterData.Domain.Entities;
using MasterData.Domain.Events;
using MasterData.Domain.RepositoryContracts;
using MasterData.Messages.Commands;
using MasterData.Messages.Events;
using NServiceBus;

namespace MasterData.MessageHandlers.CommandHandlers
{
    public class RegisterEmployeeHandler : IHandleMessages<RegisterEmployee>
    {
        private readonly IBus _bus;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public RegisterEmployeeHandler(
            IBus bus,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _bus = bus;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            DomainEvents.Register<EmployeeRegisteredEvent>(EmployeeRegistered);
        }

        public void Handle(RegisterEmployee command)
        {
            var department = _departmentRepository.GetById(command.DepartmentId);
            Employee.Register(command.Id, command.Forename, command.Surname, department);
        }

        public void EmployeeRegistered(EmployeeRegisteredEvent @event)
        {
            _employeeRepository.Save(@event.Source);

            var employeeRegistered = new EmployeeRegistered
            {
                Id = @event.Source.Id.Value,
                Forename = @event.Source.Forename,
                Surname = @event.Source.Surname,
                Joined = @event.Source.Joined.Value,
                DepartmentId = @event.Source.Department.Id.Value
            };

            _bus.Publish(employeeRegistered);
        }
    }
}
