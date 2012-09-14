using System;
using System.Threading;
using System.Web.Mvc;
using Calendar.Messages.Commands;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
using HumanResources.Domain.Global;
using HumanResources.Messages.Commands;
using HumanResources.UI.ViewModels;
using NServiceBus;

namespace HumanResources.UI.Controllers
{
    public class HolidayController : Controller
    {
        private readonly IBus _bus;
        private readonly IEmployeeService _employeeService;

        public HolidayController(IBus bus, IEmployeeService employeeService)
        {
            _bus = bus;
            _employeeService = employeeService;
        }

        public ActionResult Book(Guid employeeId)
        {
            var viewModel = new BookHolidayViewModel
                                {
                                    EmployeeId = employeeId,
                                    Start = new DateTime(2012, 08, 1),
                                    End = new DateTime(2012, 08, 1)
                                };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Book(BookHolidayViewModel viewModel)
        {
            var request = new BookHolidayRequest
                              {
                                  EmployeeId = viewModel.EmployeeId,
                                  Start = viewModel.Start,
                                  End = viewModel.End
                              };

            var validationResult = _employeeService.ValidateBookHoliday(request);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError(error.Field ?? "", error.Text);

                return View("Book", viewModel);
            }

            Guid id = Guid.NewGuid();

            var makeBookingCommand = new MakeBooking
            {
                Id = id,
                EmployeeId = viewModel.EmployeeId,
                Start = viewModel.Start,
                End = viewModel.End,
                BookingTypeId = Constants.HolidayBookingTypeId
            };

            var bookHolidayCommand = new BookHoliday
            {
                Id = id,
                EmployeeId = viewModel.EmployeeId,
                Start = viewModel.Start,
                End = viewModel.End
            };

            _bus.Send(makeBookingCommand);
            var ayncResult = _bus.Send(bookHolidayCommand).Register(EmptyCallBack, this);
            var asyncWaitHandle = ayncResult.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(50000);
            return RedirectToAction("Index", "Employee", new { employeeId = viewModel.EmployeeId });
        }

        private void EmptyCallBack(IAsyncResult asyncResult)
        {
        }
    }
}
