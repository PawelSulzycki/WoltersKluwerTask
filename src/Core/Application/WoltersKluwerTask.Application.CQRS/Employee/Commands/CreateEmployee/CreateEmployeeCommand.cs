using MediatR;
using System;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeCommandResponse>
    {
        public Pesel Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Name Name { get; set; }
        public Gender Gender { get; set; }
    }
}
