using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.Dtos.Maintenence;
using EquipmentLoan.Application.Interfaces.Maintenence;

namespace EquipmentLoan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenancesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaintenanceResponseDto>> Start(
        [FromBody] MaintenanceCreateRequestDto request,
        [FromServices] IStartMaintenance service)
    {
        try
        {
            MaintenanceResponseDto result = await service.Execute(request);
            return Created(string.Empty, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
        
    [HttpPut("finalize")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaintenanceResponseDto>> Finalize(
        [FromBody] MaintenanceFinalizeRequestDto request,
        [FromServices] IFinalizeMaintenance service)
    {
        try
        {
            MaintenanceResponseDto result = await service.Execute(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}