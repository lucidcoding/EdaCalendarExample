using HumanResources.Application.Requests;

namespace HumanResources.Application.Contracts
{
    public interface ITimeAllocationService
    {
        void Book(BookTimeAllocationRequest request);
        void Update(UpdateTimeAllocationRequest request);
    }
}
