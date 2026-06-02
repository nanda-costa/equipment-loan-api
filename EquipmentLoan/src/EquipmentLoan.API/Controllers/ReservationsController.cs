using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.Dtos.Reservation;
using EquipmentLoan.Application.Interfaces.Reservation;

namespace EquipmentLoan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReservationResponseDto>> Create(
        [FromBody] ReservationCreateRequestDto request,
        [FromServices] ICreateReservation service)
    {
        try
        {
            ReservationResponseDto result = await service.Execute(request);
            return Created(string.Empty, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
        
    [HttpPut("cancel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReservationResponseDto>> Cancel(
        [FromBody] ReservationCancelRequestDto request,
        [FromServices] ICancelReservation service)
    {
        try
        {
            ReservationResponseDto result = await  service.Execute(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}