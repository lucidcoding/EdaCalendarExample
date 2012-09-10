using System;
using System.Threading;
using System.Web.Mvc;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
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

            var command = new BookHoliday
            {
                EmployeeId = viewModel.EmployeeId,
                Start = viewModel.Start,
                End = viewModel.End
            };

            IAsyncResult ayncResult = _bus.Send(command).Register(EmptyCallBack, this);
            WaitHandle asyncWaitHandle = ayncResult.AsyncWaitHandle;
            asyncWaitHandle.WaitOne(50000);
            return RedirectToAction("Index", "Employee", new { employeeId = viewModel.EmployeeId });
        }

        private void EmptyCallBack(IAsyncResult asyncResult)
        {
        }
    }
}
