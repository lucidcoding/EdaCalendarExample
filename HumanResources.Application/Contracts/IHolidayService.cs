using HumanResources.Application.Requests;
using HumanResources.Domain.Common;

namespace HumanResources.Application.Contracts
{
    public interface IHolidayService
    {
        ValidationMessageCollection ValidateBook(BookHolidayRequest request);
        void Book(BookHolidayRequest request);
        ValidationMessageCollection ValidateUpdate(UpdateHolidayRequest request);
        void Update(UpdateHolidayRequest request);
    }
}
