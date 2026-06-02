using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Application.Interfaces;

public interface IGetLoansByFilter
{
    public Task<IEnumerable<LoanResponseDto>> Execute(
        LoanStatus? status,
        Guid? userId,
        Guid? equipmentId,
        DateTime? startDate,
        DateTime? endDate);
}
