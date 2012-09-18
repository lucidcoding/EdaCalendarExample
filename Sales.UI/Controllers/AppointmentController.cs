using System;
using System.Web.Mvc;
using Calendar.Messages.Commands;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using Sales.Domain.Global;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IBus _bus;
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(
            IBus bus,
            IAppointmentService appointmentService)
        {
            _bus = bus;
            _appointmentService = appointmentService;
        }

        public ActionResult BookUpdate(bool updating, Guid? consultantId, Guid? appointmentId)
        {
            //todo: note: there is a bit of logic in here to convert between a booking and an appointment.
            var viewModel = new BookAppointmentViewModel
            {
                Updating = updating,
                ConsultantId = consultantId,
                AppointmentId = appointmentId
            };

            if (updating)
            {
                var appointment = _appointmentService.GetById(appointmentId.Value);
                viewModel.LeadName = appointment.LeadName;
                viewModel.Address = appointment.Address;
                viewModel.Date = appointment.Start.Date;
                viewModel.StartTime = appointment.Start.TimeOfDay;
                viewModel.EndTime = appointment.End.TimeOfDay;
            }
            else
            {
                viewModel.Date = new DateTime(2012, 08, 01);
                viewModel.StartTime = new TimeSpan(09, 00, 00);
                viewModel.EndTime = new TimeSpan(09, 30, 00);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BookUpdate(BookAppointmentViewModel viewModel)
        {
            if (viewModel.Updating)
            {
                var updateBookingRequest = new UpdateAppointmentRequest
                {
                    Id = viewModel.AppointmentId.Value,
                    Date = viewModel.Date,
                    StartTime = viewModel.StartTime,
                    EndTime = viewModel.EndTime,
                    LeadName = viewModel.LeadName,
                    Address = viewModel.Address
                };

                var validationResult = _appointmentService.ValidateUpdate(updateBookingRequest);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        ModelState.AddModelError(error.Field ?? "", error.Text);

                    return View("BookUpdate", viewModel);
                }

                //todo: still a problem here.
                //Some logic has slipped into here on how to raise a command to make a booking.
                var updateBookingCommand = new UpdateBooking
                {
                    Id = viewModel.AppointmentId.Value,
                    Start = viewModel.Date + viewModel.StartTime,
                    End = viewModel.Date + viewModel.EndTime,
                };

                _appointmentService.Update(updateBookingRequest);
                _bus.Send(updateBookingCommand);
            }
            else
            {
                Guid id = Guid.NewGuid();

                var makeBookingRequest = new BookAppointmentRequest
                {
                    Id = id,
                    ConsultantId = viewModel.ConsultantId.Value,
                    Date = viewModel.Date,
                    StartTime = viewModel.StartTime,
                    EndTime = viewModel.EndTime,
                    LeadName = viewModel.LeadName,
                    Address = viewModel.Address
                };

                var validationResult = _appointmentService.ValidateBook(makeBookingRequest);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        ModelState.AddModelError(error.Field ?? "", error.Text);

                    return View("BookUpdate", viewModel);
                }

                //todo: still a problem here.
                //Some logic has slipped into here on how to raise a command to make a booking.
                var makeBookingCommand = new MakeBooking
                                             {
                                                 Id = id,
                                                 EmployeeId = viewModel.ConsultantId.Value,
                                                 Start = viewModel.Date + viewModel.StartTime,
                                                 End = viewModel.Date + viewModel.EndTime,
                                                 BookingTypeId = Constants.SalesAppointmentBookingTypeId
                                             };

                _appointmentService.Book(makeBookingRequest);
                _bus.Send(makeBookingCommand);
            }

            return RedirectToAction("Index", "Consultant", new { consultantId = viewModel.ConsultantId });
        }
    }
}
