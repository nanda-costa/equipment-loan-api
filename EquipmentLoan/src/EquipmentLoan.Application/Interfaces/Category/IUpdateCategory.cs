using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface IUpdateCategory
{
    public Task<bool> Execute(Guid id, CategoryCreateRequestDto requestDto);
}