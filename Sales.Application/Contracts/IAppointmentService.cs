using System;
using Sales.Application.Requests;
using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Application.Contracts
{
    public interface IAppointmentService
    {
        Appointment GetById(Guid id);
        ValidationMessageCollection ValidateBook(BookAppointmentRequest request);
        void Book(BookAppointmentRequest request);
        ValidationMessageCollection ValidateUpdate(UpdateAppointmentRequest request);
        void Update(UpdateAppointmentRequest request);
    }
}
