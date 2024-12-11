
using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class TipoCreditoRepository : ITipoCreditoRepository
{
    private readonly ApplicationDbContext _context;

    public TipoCreditoRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddAsync(TipoCredito tipoCredito) => _context.TipoCredito.Add(tipoCredito);
    public void Update(TipoCredito tipoCredito) => _context.TipoCredito.Update(tipoCredito);
    public void Delete(TipoCredito tipoCredito) => _context.TipoCredito.Remove(tipoCredito);
    public async Task<bool> ExistAsync(string codigo) => await _context.TipoCredito.AnyAsync(tipoCredito => tipoCredito.Codigo == codigo);
    public async Task<TipoCredito?> GetByIdAsync(string codigo) => await _context.TipoCredito.SingleOrDefaultAsync(tipoCredito => tipoCredito.Codigo == codigo);
    public async Task<List<TipoCredito>> GetAllAsync() => await _context.TipoCredito.ToListAsync();
}
