using Application.Data;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IOperacionRepository, OperacionRepository>();
        services.AddScoped<ITipoCreditoRepository, TipoCreditoRepository>();

        return services;
    }
}
