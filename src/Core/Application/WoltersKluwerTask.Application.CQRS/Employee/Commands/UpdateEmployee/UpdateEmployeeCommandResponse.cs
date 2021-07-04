using WoltersKluwerTask.Application.Common;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandResponse : BaseResponse
    {
        public UpdateEmployeeCommandResponse() : base()
        {

        }

        public UpdateEmployeeCommandResponse(ResponseStatus status, string message) : base(status, message)
        {

        }
    }
}
