
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Domain.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
    }
}