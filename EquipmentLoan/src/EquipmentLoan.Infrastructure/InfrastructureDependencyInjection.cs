using Microsoft.Extensions.DependencyInjection;
using EquipmentLoan.Domain.Interfaces;
using EquipmentLoan.Infrastructure.Repositories;

namespace EquipmentLoan.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureHierarchy(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();

            services.AddScoped<ILoanRepository, LoanRepository>();
            
            return services;
        }
    }
}