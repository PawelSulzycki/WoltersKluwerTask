using System;
using WoltersKluwerTask.Domain.Enums;

namespace WoltersKluwerTask.Api.Models.Employee
{
    public class UpdateEmployeeRequest
    {
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
