using System.ComponentModel.DataAnnotations;

namespace EquipmentLoan.Application.DTOs
{
    public class LoanCreateRequestDto
    {
        // Enquanto a autenticação JWT (Pessoa 1) não estiver pronta, o Id do
        // usuário solicitante chega no corpo da requisição. Quando o token
        // existir, este valor deve ser extraído do JWT e o campo removido daqui.
        [Required(ErrorMessage = "O usuário solicitante é obrigatório.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "O equipamento é obrigatório.")]
        public Guid EquipmentId { get; set; }

        [Required(ErrorMessage = "A data prevista de devolução é obrigatória.")]
        public DateTime ExpectedReturnDate { get; set; }
    }
}
