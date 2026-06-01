using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Application.Interfaces;

public interface IGetEquipmentsByFilter
{
    public Task<IEnumerable<EquipmentResponseDto>> Execute(EquipmentStatus? status, Guid? categoryId);
}