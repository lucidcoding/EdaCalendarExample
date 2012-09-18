using System;
using System.ComponentModel.DataAnnotations;

namespace Sales.UI.ViewModels
{
    public class BookAppointmentViewModel
    {
        public bool Updating { get; set; }
        public Guid? ConsultantId { get; set; }
        public Guid? AppointmentId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date in the format dd/mm/yyyy")]
        public DateTime Date { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Please enter a valid time in the format hh:mm:ss")]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Please enter a valid time in the format hh:mm:ss")]
        public TimeSpan EndTime { get; set; }

        [Required]
        public string LeadName { get; set; }

        [Required]
        public string Address { get; set; }
    }
}