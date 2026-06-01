using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface ICreateEquipment
{ 
    public Task<EquipmentResponseDto> Execute(EquipmentCreateRequestDto requestDto);
}