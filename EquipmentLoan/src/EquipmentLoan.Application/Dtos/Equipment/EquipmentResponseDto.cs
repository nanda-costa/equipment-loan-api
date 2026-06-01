using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.Application.DTOs
{
    public class EquipmentResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public EquipmentStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; 
    }
}