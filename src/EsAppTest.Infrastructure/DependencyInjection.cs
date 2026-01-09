using EsAppTest.Core.Services.Interfaces.Payments;
using EsAppTest.Infrastructure.Data;
using EsAppTest.Infrastructure.Services.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EsAppTest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<EsAppTestDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("SqlServer")));

        services.AddScoped<IPaymentsRepository, PaymentsRepository>();

        return services;
    }
}
