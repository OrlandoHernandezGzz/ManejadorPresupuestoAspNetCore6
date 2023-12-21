using ManejadorPresupuesto.Models;

namespace ManejadorPresupuesto.Services
{
    public interface IRepositoryAccountTypes
    {
        Task Create(AccountType accountType);
    }
}
