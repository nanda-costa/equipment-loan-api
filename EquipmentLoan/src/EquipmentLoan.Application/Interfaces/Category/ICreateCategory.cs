
using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;
public interface ICreateCategory
{
    public Task<CategoryResponseDto> Execute(CategoryCreateRequestDto requestDto);
}
