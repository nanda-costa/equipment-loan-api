using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services; 

public class UpdateCategoryService : IUpdateCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Execute(Guid id, CategoryCreateRequestDto requestDto)
    {
        Category category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return false;

        category.Name = requestDto.Name;
        category.Description = requestDto.Description;

        _categoryRepository.Update(category);
        return await _categoryRepository.SaveChangesAsync();
    }
}