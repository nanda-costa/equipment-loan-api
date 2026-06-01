using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        // POST /api/loans -> solicitar empréstimo (usuário comum)
        // Obs.: quando o JWT existir, proteger com [Authorize].
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoanResponseDto>> Create(
            [FromBody] LoanCreateRequestDto request,
            [FromServices] ICreateLoan service)
        {
            try
            {
                LoanResponseDto result = await service.Execute(request);
                return Created(string.Empty, result);
            }
            catch (InvalidOperationException ex)
            {
                // Violação de regra de negócio (ex.: equipamento indisponível).
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET /api/loans/my?userId=... -> histórico do próprio usuário (Regra 3)
        // Obs.: com JWT, o userId virá do token e não da query string.
        [HttpGet("my")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetMy(
            [FromQuery] Guid userId,
            [FromServices] IGetMyLoans service)
        {
            var result = await service.Execute(userId);
            return Ok(result);
        }

        // GET /api/loans?status=&userId=&equipmentId=&startDate=&endDate= (Regra 7, admin)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetWithFilters(
            [FromQuery] LoanStatus? status,
            [FromQuery] Guid? userId,
            [FromQuery] Guid? equipmentId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromServices] IGetLoansByFilter service)
        {
            var result = await service.Execute(status, userId, equipmentId, startDate, endDate);
            return Ok(result);
        }

        // POST /api/loans/{id}/approve -> aprovar (admin, Regra 4)
        [HttpPost("{id:guid}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Approve(
            Guid id,
            [FromServices] IApproveLoan service)
        {
            try
            {
                var ok = await service.Execute(id);
                if (!ok) return NotFound(new { message = "Empréstimo não encontrado." });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /api/loans/{id}/reject -> recusar (admin, Regra 4)
        [HttpPost("{id:guid}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Reject(
            Guid id,
            [FromServices] IRejectLoan service)
        {
            try
            {
                var ok = await service.Execute(id);
                if (!ok) return NotFound(new { message = "Empréstimo não encontrado." });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /api/loans/{id}/return -> registrar devolução (admin, Regra 5)
        [HttpPost("{id:guid}/return")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Return(
            Guid id,
            [FromBody] LoanReturnRequestDto request,
            [FromServices] IReturnLoan service)
        {
            try
            {
                var ok = await service.Execute(id, request);
                if (!ok) return NotFound(new { message = "Empréstimo não encontrado." });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
