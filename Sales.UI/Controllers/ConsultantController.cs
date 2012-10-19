using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MasterData.Messages.Commands;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.DataTransferObjects;
using Sales.Domain.Global;
using Sales.UI.Helpers;
using Sales.UI.ViewModels;
using System.Transactions;

namespace Sales.UI.Controllers
{
    public class ConsultantController : Controller
    {
        private readonly IConsultantService _consultantService;
        private readonly IBus _bus;

        public ConsultantController(
            IConsultantService consultantService,
            IBus bus)
        {
            _consultantService = consultantService;
            _bus = bus;
        }

        public ActionResult Index(Guid? consultantId)
        {
            var viewModel = new ViewConsultantViewModel();
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

        [HttpPost]
        public JsonResult Add(string forename, string surname)
        {
            var employeeId = Guid.NewGuid();

            //Do I actually need this transaction here?
            //using (var transactionScope = new TransactionScope())
            //{
            //    var registerEmployee = new RegisterEmployee
            //    {
            //        Id = employeeId,
            //        DepartmentId = Constants.SalesDepartmentId,
            //        Forename = viewModel.Forename,
            //        Surname = viewModel.Surname
            //    };

            //    _bus.Send(registerEmployee);
            //    transactionScope.Complete();
            //}

            return Json(new { text = forename + " " + surname, value = employeeId }, JsonRequestBehavior.AllowGet);
        }
    }
}
