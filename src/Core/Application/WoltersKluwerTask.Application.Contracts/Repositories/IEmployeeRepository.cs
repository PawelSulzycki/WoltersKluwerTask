using WoltersKluwerTask.Domain.Entities;

namespace WoltersKluwerTask.Application.Contracts.Repositories
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
    }
}
