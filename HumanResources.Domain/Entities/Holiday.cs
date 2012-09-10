namespace HumanResources.Domain.Entities
{
    public class Holiday : TimeAllocation
    {
        public virtual bool Approved { get; set; }
        
        public virtual int TotalDays
        {
            get { return (End.Date.AddDays(1) - Start.Date).Days; }
        }
    }
}
