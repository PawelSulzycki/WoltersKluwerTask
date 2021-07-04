using MediatR;
using System;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<UpdateEmployeeCommandResponse>
    {
        public EmployeeId EmployeeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Name Name { get; set; }
        public Gender Gender { get; set; }
    }
}
