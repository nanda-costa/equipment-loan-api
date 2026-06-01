using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface IGetAllCategories
{
    public Task<IEnumerable<CategoryResponseDto>> Execute();

}