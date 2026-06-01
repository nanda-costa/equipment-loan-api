using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface IGetEquipmentById
{
    public Task<EquipmentResponseDto> Execute(Guid id);
}