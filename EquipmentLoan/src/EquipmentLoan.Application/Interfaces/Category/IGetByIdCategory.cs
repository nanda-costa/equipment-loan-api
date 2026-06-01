using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface IGetCategoryById
{
    public Task<CategoryResponseDto?> Execute(Guid id);
}