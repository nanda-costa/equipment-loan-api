using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface IReturnLoan
{
    public Task<bool> Execute(Guid id, LoanReturnRequestDto requestDto);
}
