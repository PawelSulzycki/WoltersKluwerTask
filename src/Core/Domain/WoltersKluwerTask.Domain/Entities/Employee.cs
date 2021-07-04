﻿using WoltersKluwerTask.Domain.Ddd;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Domain.Entities
{
    public class Employee : Entity<EmployeeId>
    {
        public EvidenceNumber EvidenceNumber { get; private set; }
    }
}