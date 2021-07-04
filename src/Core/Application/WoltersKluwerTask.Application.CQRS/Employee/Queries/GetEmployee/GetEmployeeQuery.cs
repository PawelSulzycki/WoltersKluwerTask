using MediatR;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee
{
    public class GetEmployeeQuery : IRequest<Domain.Entities.Employee>
    {
        public EmployeeId EmployeeId { get; set; }
    }
}
