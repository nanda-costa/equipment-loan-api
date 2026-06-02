using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Entities;
using EquipmentLoan.Domain.Enums;
using EquipmentLoan.Domain.Interfaces;

namespace EquipmentLoan.Application.Services
{
    public class CreateLoanService : ICreateLoan
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public CreateLoanService(
            ILoanRepository loanRepository,
            IEquipmentRepository equipmentRepository)
        {
            _loanRepository = loanRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<LoanResponseDto> Execute(LoanCreateRequestDto requestDto)
        {
            Equipment? equipment = await _equipmentRepository.GetByIdAsync(requestDto.EquipmentId);
            if (equipment == null)
            {
                throw new InvalidOperationException("O equipamento informado não existe.");
            }

            // Regras 1 e 6: só é possível solicitar um equipamento que esteja
            // Disponível. Se estiver Emprestado ou em Manutenção, é bloqueado.
            if (equipment.Status != EquipmentStatus.Available)
            {
                throw new InvalidOperationException(
                    "O equipamento não está disponível para empréstimo.");
            }

            DateTime expectedReturn = ToUtc(requestDto.ExpectedReturnDate);
            if (expectedReturn <= DateTime.UtcNow)
            {
                throw new InvalidOperationException(
                    "A data prevista de devolução deve ser futura.");
            }

            Loan loan = new Loan
            {
                UserId = requestDto.UserId,
                EquipmentId = requestDto.EquipmentId,
                RequestDate = DateTime.UtcNow,
                ExpectedReturnDate = expectedReturn,
                Status = LoanStatus.Pending // Toda solicitação nasce como Pendente
            };

            await _loanRepository.AddAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return new LoanResponseDto
            {
                Id = loan.Id,
                RequestDate = loan.RequestDate,
                ApprovalDate = loan.ApprovalDate,
                ExpectedReturnDate = loan.ExpectedReturnDate,
                ActualReturnDate = loan.ActualReturnDate,
                Status = loan.Status,
                UserId = loan.UserId,
                UserName = loan.User?.Name ?? string.Empty,
                EquipmentId = loan.EquipmentId,
                EquipmentName = equipment.Name
            };
        }

        // As colunas de data são timestamptz no PostgreSQL e o Npgsql exige Kind=Utc.
        private static DateTime ToUtc(DateTime value) =>
            value.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(value, DateTimeKind.Utc)
                : value.ToUniversalTime();
    }
}
