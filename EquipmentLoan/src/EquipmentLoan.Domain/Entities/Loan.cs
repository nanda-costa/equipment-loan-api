using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ApprovalDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Pending;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;
    }
}