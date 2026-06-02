using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class ApproveLoanService : IApproveLoan
    {
        private readonly ILoanRepository _loanRepository;

        public ApproveLoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<bool> Execute(Guid id)
        {
            Loan? loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null) return false; // vira 404 no controller

            // Regra 4: apenas empréstimos Pendentes podem ser aprovados.
            if (loan.Status != LoanStatus.Pending)
            {
                throw new InvalidOperationException(
                    "Apenas empréstimos pendentes podem ser aprovados.");
            }

            // Regra 1: revalida a disponibilidade no momento da aprovação,
            // pois o equipamento pode ter sido emprestado em outra aprovação.
            if (loan.Equipment.Status != EquipmentStatus.Available)
            {
                throw new InvalidOperationException(
                    "O equipamento não está mais disponível para empréstimo.");
            }

            loan.Status = LoanStatus.Approved;
            loan.ApprovalDate = DateTime.UtcNow;
            loan.Equipment.Status = EquipmentStatus.Loaned; // sai de circulação

            return await _loanRepository.SaveChangesAsync();
        }
    }
}
