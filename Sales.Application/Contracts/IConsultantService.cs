using System;
using System.Collections.Generic;
using Sales.Application.DataTransferObjects;
using Sales.Application.Requests;
using Sales.Domain.Common;

namespace Sales.Application.Contracts
{
    public interface IConsultantService
    {
        List<ConsultantDto> GetAll();
        ConsultantDto GetById(Guid id);
        ValidationMessageCollection ValidateBookAppointment(BookAppointmentRequest request);
        void BookAppointment(BookAppointmentRequest request);
        void BookTimeAllocation(BookTimeAllocationRequest request);
    }
}
