using System.Threading.Tasks;
using WoltersKluwerTask.Domain.Entities;

namespace WoltersKluwerTask.Application.Contracts.Repositories
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
        Task<bool> ExistByPeselAsync(string pesel);

        Task<int> GetLastEvidenceNumberAsync();
    }
}
