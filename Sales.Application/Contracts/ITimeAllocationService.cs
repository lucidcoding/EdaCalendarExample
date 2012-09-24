using Sales.Application.Requests;

namespace Sales.Application.Contracts
{
    public interface ITimeAllocationService
    {
        void Book(BookTimeAllocationRequest request);
        void Update(UpdateTimeAllocationRequest request);
        void Invalidate(InvalidateTimeAllocationRequest request);
    }
}
