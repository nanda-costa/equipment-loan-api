using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class CreateCreateCategoryService : ICreateCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCreateCategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponseDto> Execute(CategoryCreateRequestDto requestDto)
    {
        Category category = new Category
        {
            Name = requestDto.Name,
            Description = requestDto.Description
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}