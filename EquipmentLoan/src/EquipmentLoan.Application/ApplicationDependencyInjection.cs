using Microsoft.Extensions.DependencyInjection;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Application.Services;

namespace EquipmentLoan.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationHierarchy(this IServiceCollection services)
        {
            services.AddScoped<ICreateCategory, CreateCreateCategoryService>();
            services.AddScoped<IGetAllCategories, GetAllCategoriesService>();
            services.AddScoped<IGetCategoryById, GetCategoryByIdService>();
            services.AddScoped<IUpdateCategory, UpdateCategoryService>();
            services.AddScoped<IDeleteCategory, DeleteCategoryService>();
            
            
            services.AddScoped<ICreateEquipment, CreateEquipmentService>();
            services.AddScoped<IGetEquipmentsByFilter, GetEquipmentsByFilterService>();
            
            
            services.AddScoped<IGetEquipmentById, GetEquipmentByIdService>();
            services.AddScoped<IUpdateEquipment, UpdateEquipmentService>();
            services.AddScoped<IDeleteEquipment, DeleteEquipmentService>();
            
            return services;
        }
    }
}