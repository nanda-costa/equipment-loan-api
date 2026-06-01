using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services;

public class DeleteCategoryService : IDeleteCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Execute(Guid id)
    {
        Category category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return false;

        _categoryRepository.Delete(category);
        return await _categoryRepository.SaveChangesAsync();
    }
}


