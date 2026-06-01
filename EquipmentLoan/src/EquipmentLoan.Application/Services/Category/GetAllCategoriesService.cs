using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class GetAllCategoriesService : IGetAllCategories
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryResponseDto>> Execute()
    {
        var categories = await _categoryRepository.GetAllAsync();
            
        return categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        });
    }
}