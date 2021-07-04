using WoltersKluwerTask.Application.Common;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee
{
    public class GetEmployeeQueryResponse : BaseResponse
    {
        public Domain.Entities.Employee Employee { get; set; }

        public GetEmployeeQueryResponse(ResponseStatus status, string message) : base(status, message)
        {

        }

        public GetEmployeeQueryResponse(Domain.Entities.Employee employee) : base()
        {
            Employee = employee;
        }
    }
}
