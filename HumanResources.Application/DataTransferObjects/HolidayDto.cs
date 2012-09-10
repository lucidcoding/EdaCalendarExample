namespace HumanResources.Application.DataTransferObjects
{
    public class HolidayDto : TimeAllocationDto
    {
        public virtual bool Approved { get; set; }
    }
}
