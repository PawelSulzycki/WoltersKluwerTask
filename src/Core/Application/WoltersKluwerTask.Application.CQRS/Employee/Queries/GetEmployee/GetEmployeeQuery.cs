using MediatR;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee
{
    public class GetEmployeeQuery : IRequest<GetEmployeeQueryResponse>
    {
        public EmployeeId EmployeeId { get; set; }
    }
}
