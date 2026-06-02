using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class GetMyLoansService : IGetMyLoans
    {
        private readonly ILoanRepository _loanRepository;

        public GetMyLoansService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        // Regra 3: o usuário comum só enxerga os próprios empréstimos.
        public async Task<IEnumerable<LoanResponseDto>> Execute(Guid userId)
        {
            var loans = await _loanRepository.GetByUserAsync(userId);

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
    }
}
