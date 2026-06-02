namespace EquipmentLoan.Application.DTOs
{
    public class LoanReturnRequestDto
    {
        // Indica se o equipamento voltou danificado. Quando true, o empréstimo
        // é encerrado como ReturnedDamaged e o equipamento vai para Manutenção.
        public bool WasDamaged { get; set; } = false;
    }
}
