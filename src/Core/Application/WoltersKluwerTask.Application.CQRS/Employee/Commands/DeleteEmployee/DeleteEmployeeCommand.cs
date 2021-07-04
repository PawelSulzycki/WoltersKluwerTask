using MediatR;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<DeleteEmployeeCommandResponse>
    {
        public EmployeeId EmployeeId { get; set; }
    }
}
