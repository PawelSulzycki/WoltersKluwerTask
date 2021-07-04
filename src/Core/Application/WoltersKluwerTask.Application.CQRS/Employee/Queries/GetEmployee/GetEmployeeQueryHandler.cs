using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Contracts.Repositories;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Domain.Entities.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Entities.Employee> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);

            return employee;
        }
    }
}
