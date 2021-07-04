using MediatR;
using System;
using WoltersKluwerTask.Domain.Enums;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeCommandResponse>
    {
        public Pesel Pesel { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Name Name { get; private set; }
        public Gender Gender { get; private set; }
    }
}
