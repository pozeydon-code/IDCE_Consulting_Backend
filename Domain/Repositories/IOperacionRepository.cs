using Domain.Models;

namespace Domain.Repositories;

public interface IOperacionRepository
{
    Task<List<Operacion>> GetAllAsync();
    Task<Operacion?> GetByIdAsync(int id);
    Task<bool> ExistAsync(int id);
    void AddAsync(Operacion category);
    void Update(Operacion category);
    void Delete(Operacion category);
}
