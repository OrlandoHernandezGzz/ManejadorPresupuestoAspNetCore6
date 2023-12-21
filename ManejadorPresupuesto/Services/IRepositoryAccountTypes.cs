using ManejadorPresupuesto.Models;

namespace ManejadorPresupuesto.Services
{
    public interface IRepositoryAccountTypes
    {
        Task Create(AccountType accountType);
        Task<bool> Exist(string name, int userId);
        Task<IEnumerable<AccountType>> Get(int userId);
        Task<AccountType> GetById(int id, int userId);
        Task Update(AccountType accountType);
    }
}
