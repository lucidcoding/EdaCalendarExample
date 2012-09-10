namespace Sales.Application.DataTransferObjects
{
    public class AppointmentDto : TimeAllocationDto
    {
        public string LeadName { get; set; }
        public string Address { get; set; }
    }
}
