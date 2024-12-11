using Domain.Models;

namespace Domain.Repositories;

public interface ITipoCreditoRepository
{
    Task<List<TipoCredito>> GetAllAsync();
    Task<TipoCredito?> GetByIdAsync(string codigo);
    Task<bool> ExistAsync(string codigo);
    void AddAsync(TipoCredito category);
    void Update(TipoCredito category);
    void Delete(TipoCredito category);
}
