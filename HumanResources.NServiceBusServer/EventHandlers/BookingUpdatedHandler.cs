using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calendar.Messages.Events;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
using HumanResources.Domain.Global;
using NServiceBus;

namespace HumanResources.NServiceBusServer.EventHandlers
{
    public class BookingUpdatedHandler : IHandleMessages<BookingUpdated>
    {
        private readonly ITimeAllocationService _timeAllocationService;

        public BookingUpdatedHandler(ITimeAllocationService timeAllocationService)
        {
            _timeAllocationService = timeAllocationService;
        }

        public void Handle(BookingUpdated @event)
        {
            //Don't add this if it is relevant to this system - this will already have been done locally.
            if (@event.BookingTypeId != Constants.HolidayBookingTypeId)
            {
                var request = new UpdateTimeAllocationRequest
                {
                    Id = @event.Id,
                    Start = @event.Start,
                    End = @event.End
                };

                _timeAllocationService.Update(request);
            }
        }
    }
}
