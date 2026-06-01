
using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;
public interface IUpdateEquipment
{
    public Task<bool> Execute(Guid id, EquipmentCreateRequestDto requestDto);
}