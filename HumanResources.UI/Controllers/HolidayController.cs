using System;
using System.Web.Mvc;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
using HumanResources.Domain.Global;
using HumanResources.UI.ViewModels;
using NServiceBus;

namespace HumanResources.UI.Controllers
{
    public class HolidayController : Controller
    {
        private readonly IBus _bus;
        private readonly IHolidayService _holidayService;

        public HolidayController(IBus bus, IHolidayService holidayService)
        {
            _bus = bus;
            _holidayService = holidayService;
        }

        public ActionResult BookUpdate(bool updating, Guid? employeeId, Guid? holidayId, DateTime? start, DateTime? end)
        {
            var viewModel = new BookHolidayViewModel
            {
                Updating = updating,
                EmployeeId = employeeId,
                HolidayId = holidayId,
                Start = start.HasValue ? start.Value : new DateTime(2012, 08, 1),
                End = end.HasValue ? end.Value : new DateTime(2012, 08, 1),
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BookUpdate(BookHolidayViewModel viewModel)
        {
            if (viewModel.Updating)
            {
                var updateHolidayRequest = new UpdateHolidayRequest
                {
                    Id = viewModel.HolidayId.Value,
                    Start = viewModel.Start,
                    End = viewModel.End
                };

                var validationResult = _holidayService.ValidateUpdate(updateHolidayRequest);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        ModelState.AddModelError(error.Field ?? "", error.Text);

                    return View("BookUpdate", viewModel);
                }

                _holidayService.Update(updateHolidayRequest);
            }
            else
            {
                Guid id = Guid.NewGuid();

                var bookHolidayRequest = new BookHolidayRequest
                {
                    Id = id,
                    EmployeeId = viewModel.EmployeeId.Value,
                    Start = viewModel.Start,
                    End = viewModel.End
                };

                var validationResult = _holidayService.ValidateBook(bookHolidayRequest);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                        ModelState.AddModelError(error.Field ?? "", error.Text);

                    return View("BookUpdate", viewModel);
                }

                _holidayService.Book(bookHolidayRequest);
            }

            return RedirectToAction("Index", "Employee", new { employeeId = viewModel.EmployeeId });
        }
    }
}
