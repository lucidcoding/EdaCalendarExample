using System;

namespace MasterData.Messages.Commands
{
    public class RegisterEmployee
    {
        public Guid Id { get; set; } 
        public string Forename { get; set; }
        public string Surname { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
