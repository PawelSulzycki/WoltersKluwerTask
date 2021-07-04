using System.Collections.Generic;
using WoltersKluwerTask.Application.Common;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryResponse : BaseResponse
    {
        public IEnumerable<Domain.Entities.Employee> Employees { get; set; }

        public GetAllEmployeesQueryResponse(IEnumerable<Domain.Entities.Employee> employees) : base()
        {
            Employees = employees;
        }
    }
}
