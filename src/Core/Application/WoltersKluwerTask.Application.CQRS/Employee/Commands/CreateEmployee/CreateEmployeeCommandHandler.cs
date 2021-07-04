using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var exist = await _employeeRepository.ExistByPeselAsync(request.Pesel.Value);

            if (!exist)
            {
                var lastEvidenceNumber = await _employeeRepository.GetLastEvidenceNumberAsync();

                var newEmplayee = await _employeeRepository
                    .AddAsync(new Domain.Entities.Employee(
                        new EmployeeId(0),
                        EvidenceNumber.NewEvidenceNumberByLast(lastEvidenceNumber),
                        request.Pesel,
                        request.DateOfBirth,
                        request.Name,
                        request.Gender));

                return new CreateEmployeeCommandResponse(newEmplayee.Id);
            }

            return new CreateEmployeeCommandResponse(ResponseStatus.BadQuery, "The employee exists with the given PESEL number");
        }
    }
}
