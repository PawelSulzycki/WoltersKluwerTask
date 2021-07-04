using System.Threading.Tasks;
using WoltersKluwerTask.Domain.Entities;
using WoltersKluwerTask.Domain.ValueObjects;

namespace WoltersKluwerTask.Application.Contracts.Repositories
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
        Task<bool> ExistByPeselAsync(Pesel pesel);

        Task<int> GetLastEvidenceNumberAsync();
    }
}
