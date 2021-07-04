using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);

            if (employee != null)
            {
                var updateEmployee = new Domain.Entities.Employee(
                    request.EmployeeId,
                    employee.EvidenceNumber,
                    employee.Pesel,
                    request.DateOfBirth,
                    request.Name,
                    request.Gender);

                await _employeeRepository.UpdateAsync(updateEmployee);
            }

            return new UpdateEmployeeCommandResponse(ResponseStatus.NotFound, "The employee does not exist");
        }
    }
}
