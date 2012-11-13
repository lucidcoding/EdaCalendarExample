using System;
using System.Collections.Generic;
using Calendar.Application.Contracts;
using Calendar.Application.Requests;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;

namespace Calendar.Application.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public ValidationMessageCollection ValidateMake(MakeBookingRequest request)
        {
            var otherBookings = _bookingRepository.GetByEmployeeId(request.EmployeeId);
            return Booking.ValidateMake(otherBookings, request.Start, request.End);
        }

        public IEnumerable<Booking> Search(SearchBookingsRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Booking GetById(Guid id)
        {
            throw new System.NotImplementedException();
        }
    }
}
