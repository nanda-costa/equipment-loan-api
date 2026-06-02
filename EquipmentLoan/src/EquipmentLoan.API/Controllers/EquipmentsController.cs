using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Domain.Enums;

namespace EquipmentLoan.API.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentsController : ControllerBase
    {
        [Authorize(Roles = "Admin")] 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EquipmentResponseDto>> Create(
            [FromBody] EquipmentCreateRequestDto request,
            [FromServices] ICreateEquipment service)
        {
            EquipmentResponseDto result = await service.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EquipmentResponseDto>>> GetWithFilters(
            [FromQuery] EquipmentStatus? status,
            [FromQuery] Guid? categoryId,
            [FromServices] IGetEquipmentsByFilter service)
        {
            var result = await service.Execute(status, categoryId);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipmentResponseDto>> GetById(
            Guid id,
            [FromServices] IGetEquipmentById service)
        {
            EquipmentResponseDto result = await service.Execute(id);
            if (result == null) return NotFound(new { message = "Equipamento não encontrado." });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")] 
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] EquipmentCreateRequestDto request,
            [FromServices] IUpdateEquipment service)
        {
            var updated = await service.Execute(id, request);
            if (!updated) return NotFound(new { message = "Equipamento não encontrado para atualização." });

            return NoContent();
        }

        [Authorize(Roles = "Admin")] 
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromServices] IDeleteEquipment service)
        {
            var deleted = await service.Execute(id);
            if (!deleted) return NotFound(new { message = "Equipamento não encontrado." });

            return NoContent();
        }
    }
}