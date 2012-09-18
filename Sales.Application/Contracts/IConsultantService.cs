using System;
using System.Collections.Generic;
using Sales.Application.DataTransferObjects;

namespace Sales.Application.Contracts
{
    public interface IConsultantService
    {
        List<ConsultantDto> GetAll();
        ConsultantDto GetById(Guid id);
    }
}
