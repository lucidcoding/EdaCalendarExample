using System;
using Sales.Application.DataTransferObjects;
using Sales.Application.Requests;
using Sales.Domain.Common;

namespace Sales.Application.Contracts
{
    public interface IAppointmentService
    {
        AppointmentDto GetById(Guid id);
        ValidationMessageCollection ValidateBook(BookAppointmentRequest request);
        void Book(BookAppointmentRequest request);
        ValidationMessageCollection ValidateUpdate(UpdateAppointmentRequest request);
        void Update(UpdateAppointmentRequest request);
    }
}
