using MediatR;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<GetAllEmployeesQueryResponse>
    {
    }
}
