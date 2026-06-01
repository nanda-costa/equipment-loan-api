using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Application.DTOs
{
    public class LoanResponseDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public LoanStatus Status { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
    }
}
