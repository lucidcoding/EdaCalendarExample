using System;
using System.ComponentModel.DataAnnotations;

namespace HumanResources.UI.ViewModels
{
    public class BookHolidayViewModel
    {
        public bool Updating { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? HolidayId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")] 
        public DateTime Start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")] 
        public DateTime End { get; set; }
    }
}