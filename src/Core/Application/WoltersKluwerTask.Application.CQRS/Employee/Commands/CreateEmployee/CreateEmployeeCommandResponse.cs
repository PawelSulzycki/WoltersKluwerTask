using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandResponse : BaseResponse
    {
        public EmployeeId EmployeeId { get; set; }

        public CreateEmployeeCommandResponse() : base()
        {
        }

        public CreateEmployeeCommandResponse(ResponseStatus status, string message) : base(status, message)
        {

        }

        public CreateEmployeeCommandResponse(EmployeeId employeeId) : base()
        {
            EmployeeId = employeeId;
        }
    }
}
