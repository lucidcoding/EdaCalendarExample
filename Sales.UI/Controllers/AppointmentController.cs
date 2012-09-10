using System;
using System.Threading;
using System.Web.Mvc;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
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

            var command = new BookAppointment
            {
                ConsultantId = viewModel.ConsultantId,
                Date = viewModel.Date,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                LeadName = viewModel.LeadName,
                Address = viewModel.Address
            };

            IAsyncResult ayncResult = _bus.Send(command).Register(EmptyCallBack, this);
            WaitHandle asyncWaitHandle = ayncResult.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(50000);
            return RedirectToAction("Index", "Consultant", new { consultantId = viewModel.ConsultantId });
        }

        private void EmptyCallBack(IAsyncResult asyncResult)
        {
        }
    }
}
