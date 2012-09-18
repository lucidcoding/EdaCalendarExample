using System;

//todo: note not sure if 'update' is correct verb? Should it be alter or amend? Make is a good verb for creating, but for updating I have stuck to the default.
//todo: note I've decided that you cannot change the employee or type, only the times. Otherwise it makes more sense to delete it and make a new one.
namespace Calendar.Messages.Commands
{
    public class UpdateBooking
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
