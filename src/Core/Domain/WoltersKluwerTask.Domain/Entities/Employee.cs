using System;
using WoltersKluwerTask.Domain.Ddd;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Domain.Entities
{
    public class Employee : Entity<EmployeeId>
    {
        public EvidenceNumber EvidenceNumber { get; private set; }
        public Pesel Pesel { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Name Name { get; private set; }
        public Gender Gender { get; private set; }

        public Employee(EmployeeId employeeId, EvidenceNumber evidenceNumber, Pesel pesel, DateTime dateOfBirth, Name name, Gender gender)
        {
            Id = employeeId;
            EvidenceNumber = evidenceNumber;
            Pesel = pesel;
            DateOfBirth = dateOfBirth;
            Name = name;
            Gender = gender;
        }
    }
}
