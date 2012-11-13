//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Calendar.Domain.Entities;
//using NUnit.Framework;

//todo: repair tests.
//namespace Calendar.Domain.Tests
//{
//    [TestFixture]
//    [Category("Unit")]
//    public class BookingTests
//    {
//        [Test]
//        public void Given_that_booking_does_not_clash_when_new_booking_is_validated_then_no_validation_errors_are_returned()
//        {
//            var employee = new Employee
//            {
//                Bookings = new List<Booking>(),
//                Forename = "Gary",
//                Surname = "Green"
//            };

//            var validationCollection = Booking.ValidateMake(
//                employee, 
//                new DateTime(2012, 09, 01),
//                new DateTime(2012, 09, 05, 23, 59, 59));

//            Assert.AreEqual(0, validationCollection.Count);
//        }

//        [Test]
//        public void Given_that_booking_does_clash_when_new_booking_is_validated_then_validation_errors_are_returned()
//        {
//            var employee = new Employee
//            {
//                Bookings = new List<Booking>(),
//                Forename = "Gary",
//                Surname = "Green"
//            };

//            Booking.Make(
//                Guid.NewGuid(),
//                employee,
//                new DateTime(2012, 09, 02),
//                new DateTime(2012, 09, 06, 23, 59, 59),
//                new BookingType());

//            var validationCollection = Booking.ValidateMake(
//                employee,
//                new DateTime(2012, 09, 01),
//                new DateTime(2012, 09, 05, 23, 59, 59));

//            Assert.AreEqual(1, validationCollection.Count);
//            Assert.AreEqual("Booking clashes with other bookings for employee.", validationCollection[0].Text);
//        }

//        [Test]
//        public void Given_that_booking_does_not_clash_when_booking_update_is_validated_then_no_validation_errors_are_returned()
//        {
//            var employee = new Employee
//            {
//                Bookings = new List<Booking>(),
//                Forename = "Gary",
//                Surname = "Green"
//            };

//            Booking.Make(
//                Guid.NewGuid(),
//                employee,
//                new DateTime(2012, 09, 01),
//                new DateTime(2012, 09, 03, 23, 59, 59),
//                new BookingType());

//            var booking = Booking.Make(
//                Guid.NewGuid(),
//                employee,
//                new DateTime(2012, 09, 05),
//                new DateTime(2012, 09, 07, 23, 59, 59),
//                new BookingType());

//            var validationCollection = booking.ValidateUpdate(
//                new DateTime(2012, 09, 10),
//                new DateTime(2012, 09, 12, 23, 59, 59));

//            Assert.AreEqual(0, validationCollection.Count);
//        }

//        [Test]
//        public void Given_that_booking_does_not_clash_but_does_overlap_previous_times_when_booking_update_is_validated_then_no_validation_errors_are_returned()
//        {
//            var employee = new Employee
//            {
//                Bookings = new List<Booking>(),
//                Forename = "Gary",
//                Surname = "Green"
//            };

//            Booking.Make(
//                Guid.NewGuid(),
//                employee,
//                new DateTime(2012, 09, 01),
//                new DateTime(2012, 09, 03, 23, 59, 59),
//                new BookingType());

//            var booking = Booking.Make(
//                Guid.NewGuid(),
//                employee,
//                new DateTime(2012, 09, 05),
//                new DateTime(2012, 09, 07, 23, 59, 59),
//                new BookingType());

//            var validationCollection = booking.ValidateUpdate(
//                new DateTime(2012, 09, 06),
//                new DateTime(2012, 09, 08, 23, 59, 59));

//            Assert.AreEqual(0, validationCollection.Count);
//        }

//        [Test]
//        public void Given_that_booking_does_when_booking_update_is_validated_then_validation_errors_are_returned()
//        {
//            var employee = new Employee
//            {
//                Bookings = new List<Booking>(),
//                Forename = "Gary",
//                Surname = "Green"
//            };

//            Booking.Make(
//                Guid.NewGuid(),
//                employee,
//                new DateTime(2012, 09, 01),
//                new DateTime(2012, 09, 03, 23, 59, 59),
//                new BookingType());

//            var booking = Booking.Make(
//                Guid.NewGuid(),
//                employee,
//                new DateTime(2012, 09, 05),
//                new DateTime(2012, 09, 07, 23, 59, 59),
//                new BookingType());

//            var validationCollection = booking.ValidateUpdate(
//                new DateTime(2012, 09, 02),
//                new DateTime(2012, 09, 04, 23, 59, 59));

//            Assert.AreEqual(1, validationCollection.Count);
//            Assert.AreEqual("Booking clashes with other bookings for employee.", validationCollection[0].Text);
//        }
//    }
//}
