using DTQ.Domain.Interfaces;
using DTQ.Infrastructure.Data;
using DTQ.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace DTQ.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddToInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
