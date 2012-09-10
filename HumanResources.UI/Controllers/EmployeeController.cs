using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HumanResources.Application.Contracts;
using HumanResources.Application.DataTransferObjects;
using HumanResources.UI.Helpers;
using HumanResources.UI.ViewModels;
using StructureMap;

namespace HumanResources.UI.Controllers  
{
    /// <remarks>
    /// I'm well aware that there are some design issues here, such as the fact that it needs to get the employee entity
    /// twice, once to display holiday entitlement/remaining, and another time to get calendar entries. Also,
    /// when getting the list of employees, it gets all calendar entries for each one. This can be avoided but I'm leaving
    /// it for the sake of speed and simplicity. It's EDA I'm demonstrating here, not fine tuning NHibernate behavior.
    /// </remarks>
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ActionResult Index(Guid? employeeId)
        {
            var viewModel = new EmployeeViewModel();
            var employees = _employeeService.GetAll();
            viewModel.Employees = new SelectList(employees, "Id", "FullName", employeeId);
            viewModel.EmployeeId = employeeId;

            if (employeeId.HasValue)
            {
                var employeeService = ObjectFactory.GetInstance<IEmployeeService>();
                var employee = employeeService.GetById(employeeId.Value);
                viewModel.HolidayEntitlement = employee.HolidayEntitlement;
                viewModel.RemainingHoliday = employee.RemainingHoliday;
            }

            return View(viewModel);
        }

        public JsonResult CalendarData(Guid? employeeId)
        {
            var calendarEntries = new List<object>();

            if (employeeId.HasValue)
            {
                var employee = _employeeService.GetById(employeeId.Value);   
                foreach(var timeAllocation in employee.TimeAllocations)
                {
                    calendarEntries.Add(new 
                                            {
                                                title = timeAllocation.GetType() == typeof(HolidayDto) ? "Holiday" : "",
                                                start = DateHelper.ToUnixTimespan(timeAllocation.Start),
                                                end = DateHelper.ToUnixTimespan(timeAllocation.End),
                                                backgroundColor = timeAllocation.GetType() == typeof(HolidayDto) ? "cadetblue" : "lightgrey",
                                                borderColor = timeAllocation.GetType() == typeof(HolidayDto) ? "cadetblue" : "lightgrey",
                                            });
                }
            }

            return Json(calendarEntries, JsonRequestBehavior.AllowGet);
        }
    }
}
