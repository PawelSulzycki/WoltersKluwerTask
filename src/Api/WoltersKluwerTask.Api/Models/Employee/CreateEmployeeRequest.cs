using System;
using WoltersKluwerTask.Domain.Enums;

namespace WoltersKluwerTask.Api.Models.Employee
{
    public class CreateEmployeeRequest
    {
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
