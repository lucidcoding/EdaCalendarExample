using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Sales.Application.Contracts;
using Sales.Application.DataTransferObjects;
using Sales.UI.Helpers;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class ConsultantController : Controller
    {
        private readonly IConsultantService _consultantService;

        public ConsultantController(IConsultantService consultantService)
        {
            _consultantService = consultantService;
        }

        public ActionResult Index(Guid? consultantId)
        {
            var viewModel = new ConsultantViewModel();
            var consultants = _consultantService.GetAll();
            viewModel.Consultants = new SelectList(consultants, "Id", "FullName", consultantId);
            viewModel.ConsultantId = consultantId;
            return View(viewModel);
        }

        public JsonResult CalendarData(Guid? consultantId)
        {
            var calendarEntries = new List<object>();

            if (consultantId.HasValue)
            {
                var employee = _consultantService.GetById(consultantId.Value);

                foreach (var timeAllocation in employee.TimeAllocations)
                {
                    string title = "";
                    string address = "";
                    var appointment = timeAllocation as AppointmentDto;
                    if (appointment != null) title = appointment.LeadName;
                    if (appointment != null) address = appointment.Address;

                    calendarEntries.Add(new
                                            {
                                                id = timeAllocation.Id,
                                                consultantId,
                                                isAppointment = (appointment != null),
                                                title,
                                                start = DateHelper.ToUnixTimespan(timeAllocation.Start),
                                                end = DateHelper.ToUnixTimespan(timeAllocation.End),
                                                isoStart = timeAllocation.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                                                isoEnd = timeAllocation.End.ToString("yyyy-MM-dd HH:mm:ss"),
                                                address,
                                                backgroundColor = (appointment != null) ? "indianred" : "lightgrey",
                                                borderColor = (appointment != null) ? "indianred" : "lightgrey",
                                            });
                }
            }

            return Json(calendarEntries, JsonRequestBehavior.AllowGet);
        }
    }
}
