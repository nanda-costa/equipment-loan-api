using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Dtos.Maintenence;
using EquipmentLoan.Application.Interfaces;
using EquipmentLoan.Application.Interfaces.Maintenence;

namespace EquipmentLoan.API.Controllers
{
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
                var result = await service.Execute(request);
                return Created(string.Empty, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}