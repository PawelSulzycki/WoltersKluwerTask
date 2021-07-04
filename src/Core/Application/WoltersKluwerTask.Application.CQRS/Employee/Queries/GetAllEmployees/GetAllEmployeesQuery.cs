using MediatR;
using System.Collections.Generic;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<Domain.Entities.Employee>>
    {
    }
}
