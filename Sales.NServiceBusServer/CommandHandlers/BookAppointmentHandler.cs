using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using Sales.Domain.Entities;
using Sales.Messages.Commands;

namespace Sales.NServiceBusServer.CommandHandlers
{
    public class BookAppointmentHandler : IHandleMessages<BookAppointment>
    {
        private readonly IBus _bus;
        private readonly IConsultantService _consultantService;

        public BookAppointmentHandler(IBus bus, IConsultantService consultantService)
        {
            _bus = bus;
            _consultantService = consultantService;
        }

        public void Handle(BookAppointment command)
        {
            var request = new BookAppointmentRequest
            {
                Id = command.Id,
                ConsultantId = command.ConsultantId,
                Date = command.Date,
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                LeadName = command.LeadName,
                Address = command.Address
            };

            _consultantService.BookAppointment(request);
            _bus.Return(0);
        }
    }
}
