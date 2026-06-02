using EquipmentLoan.Application.DTOs;

namespace EquipmentLoan.Application.Interfaces;

public interface IGetMyLoans
{
    public Task<IEnumerable<LoanResponseDto>> Execute(Guid userId);
}
