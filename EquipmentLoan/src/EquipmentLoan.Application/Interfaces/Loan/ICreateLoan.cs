using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface ICreateLoan
{
    public Task<LoanResponseDto> Execute(LoanCreateRequestDto requestDto);
}
