
using Domain.Models;
using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class OperacionRepository : IOperacionRepository
{
    private readonly ApplicationDbContext _context;

    public OperacionRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddAsync(Operacion operacion) => _context.Operaciones.Add(operacion);
    public void Update(Operacion operacion) => _context.Operaciones.Update(operacion);
    public void Delete(Operacion operacion) => _context.Operaciones.Remove(operacion);
    public async Task<bool> ExistAsync(int id) => await _context.Operaciones.AnyAsync(operacion => operacion.OperacionID == id);
    public async Task<Operacion?> GetByIdAsync(int id) => await _context.Operaciones.SingleOrDefaultAsync(operacion => operacion.OperacionID == id);
    public async Task<List<Operacion>> GetAllAsync() => await _context.Operaciones.ToListAsync();
    // public async Task<Operacion?> GetByIdAsync(int id) => await _context.Operaciones.Include(o => o.TipoCreditoNavigation).SingleOrDefaultAsync(operacion => operacion.OperacionID == id);
    // public async Task<List<Operacion>> GetAllAsync() => await _context.Operaciones.Include(o => o.TipoCreditoNavigation).ToListAsync();
}
