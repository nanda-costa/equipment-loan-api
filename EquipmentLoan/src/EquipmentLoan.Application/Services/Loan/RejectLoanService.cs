using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class RejectLoanService : IRejectLoan
    {
        private readonly ILoanRepository _loanRepository;

        public RejectLoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<bool> Execute(Guid id)
        {
            Loan? loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null) return false;

            // Regra 4: apenas empréstimos Pendentes podem ser recusados.
            if (loan.Status != LoanStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Apenas empréstimos pendentes podem ser recusados.");
            }

            loan.Status = LoanStatus.Rejected;
            // O equipamento permanece Disponível, pois nunca chegou a sair.

            return await _loanRepository.SaveChangesAsync();
        }
    }
}
