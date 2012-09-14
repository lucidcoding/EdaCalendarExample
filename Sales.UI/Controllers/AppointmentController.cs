using System;
using System.Web.Mvc;
using Calendar.Messages.Commands;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using Sales.Domain.Global;
using Sales.Messages.Commands;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IBus _bus;
        private readonly IConsultantService _consultantService;

        public AppointmentController(IBus bus, IConsultantService consultantService)
        {
            _bus = bus;
            _consultantService = consultantService;
        }

        public ActionResult Book(Guid consultantId)
        {
            var viewModel = new BookAppointmentViewModel
                                {
                                    ConsultantId = consultantId,
                                    Date = new DateTime(2012, 08, 01),
                                    StartTime = new TimeSpan(09, 00, 00),
                                    EndTime = new TimeSpan(09, 30, 00)
                                };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Book(BookAppointmentViewModel viewModel)
        {
            var request = new BookAppointmentRequest
                              {
                                  ConsultantId = viewModel.ConsultantId,
                                  Date = viewModel.Date,
                                  StartTime = viewModel.StartTime,
                                  EndTime = viewModel.EndTime,
                                  LeadName = viewModel.LeadName,
                                  Address = viewModel.Address
                              };

            var validationResult = _consultantService.ValidateBookAppointment(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.Field ?? "", error.Text);

                return View("Book", viewModel);
            }

            Guid id = Guid.NewGuid();

            //Todo: note some logic has slipped into here on how to raise a command to make a booking.
            var makeBookingCommand = new MakeBooking
            {
                Id = id,
                EmployeeId = viewModel.ConsultantId,
                Start = viewModel.Date + viewModel.StartTime,
                End = viewModel.Date + viewModel.EndTime,
                BookingTypeId = Constants.SalesAppointmentBookingTypeId
            };

            var bookAppointmentCommand = new BookAppointment
            {
                Id = id,
                ConsultantId = viewModel.ConsultantId,
                Date = viewModel.Date,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                LeadName = viewModel.LeadName,
                Address = viewModel.Address
            };

            _bus.Send(makeBookingCommand);
            var ayncResult = _bus.Send(bookAppointmentCommand).Register(EmptyCallBack, this);
            var asyncWaitHandle = ayncResult.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(50000);
            return RedirectToAction("Index", "Consultant", new { consultantId = viewModel.ConsultantId });
        }

        private void EmptyCallBack(IAsyncResult asyncResult)
        {
        }
    }
}
