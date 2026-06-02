using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.Dtos.Reservation;
using EquipmentLoan.Application.Interfaces.Reservation;

namespace EquipmentLoan.API.Controllers
{
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