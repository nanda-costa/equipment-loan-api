using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class ReturnLoanService : IReturnLoan
    {
        private readonly ILoanRepository _loanRepository;

        public ReturnLoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<bool> Execute(Guid id, LoanReturnRequestDto requestDto)
        {
            Loan? loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null) return false;

            // Regras 4 e 5: só é possível devolver um empréstimo Aprovado (em uso).
            if (loan.Status != LoanStatus.Approved)
            {
                throw new InvalidOperationException(
                    "Apenas empréstimos aprovados podem ser devolvidos.");
            }

            DateTime now = DateTime.UtcNow;
            loan.ActualReturnDate = now;

            // Regra 5: define o status final da devolução.
            // Prioridade adotada: dano > atraso > normal.
            if (requestDto.WasDamaged)
            {
                loan.Status = LoanStatus.ReturnedDamaged;
                // Regra 6: item danificado fica indisponível, vai para Manutenção.
                loan.Equipment.Status = EquipmentStatus.Maintenance;
            }
            else
            {
                loan.Status = now > loan.ExpectedReturnDate
                    ? LoanStatus.ReturnedLate
                    : LoanStatus.ReturnedNormal;
                // Devolução sem dano libera o equipamento de volta.
                loan.Equipment.Status = EquipmentStatus.Available;
            }

            return await _loanRepository.SaveChangesAsync();
        }
    }
}
