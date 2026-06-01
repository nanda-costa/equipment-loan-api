using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class GetCategoryByIdService : IGetCategoryById
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponseDto?> Execute(Guid id)
    {
        Category category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}