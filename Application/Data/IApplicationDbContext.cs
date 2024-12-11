using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    public DbSet<Operacion> Operaciones { get; set; }
    public DbSet<TipoCredito> TipoCredito { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
