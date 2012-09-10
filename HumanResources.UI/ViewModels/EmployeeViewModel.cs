using System;
using System.Web.Mvc;

namespace HumanResources.UI.ViewModels
{
    public class EmployeeViewModel
    {
        public SelectList Employees { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FullName { get; set; }
        public int? HolidayEntitlement { get; set; }
        public int? RemainingHoliday { get; set; }
    }
}