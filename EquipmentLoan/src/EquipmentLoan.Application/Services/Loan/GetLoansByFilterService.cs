using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class GetLoansByFilterService : IGetLoansByFilter
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoansByFilterService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        // Regra 7: filtra empréstimos por status, usuário, equipamento e período.
        public async Task<IEnumerable<LoanResponseDto>> Execute(
            LoanStatus? status,
            Guid? userId,
            Guid? equipmentId,
            DateTime? startDate,
            DateTime? endDate)
        {
            var loans = await _loanRepository.GetByFilterAsync(
                status,
                userId,
                equipmentId,
                startDate.HasValue ? ToUtc(startDate.Value) : null,
                endDate.HasValue ? ToUtc(endDate.Value) : null);

            return loans.Select(l => new LoanResponseDto
            {
                Id = l.Id,
                RequestDate = l.RequestDate,
                ApprovalDate = l.ApprovalDate,
                ExpectedReturnDate = l.ExpectedReturnDate,
                ActualReturnDate = l.ActualReturnDate,
                Status = l.Status,
                UserId = l.UserId,
                UserName = l.User?.Name ?? string.Empty,
                EquipmentId = l.EquipmentId,
                EquipmentName = l.Equipment?.Name ?? string.Empty
            });
        }

        // timestamptz exige Kind=Utc no Npgsql.
        private static DateTime ToUtc(DateTime value) =>
            value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value.ToUniversalTime();
    }
}
