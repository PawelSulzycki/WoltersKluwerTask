using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WoltersKluwerTask.Application.Common;
using WoltersKluwerTask.Application.Contracts.Repositories;

namespace WoltersKluwerTask.Application.CQRS.Employee.Queries.GetEmployee
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<GetEmployeeQueryResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);

            if(employee != null)
                return new GetEmployeeQueryResponse(employee);

            return new GetEmployeeQueryResponse(ResponseStatus.NotFound, "The employee does not exist");
        }
    }
}
