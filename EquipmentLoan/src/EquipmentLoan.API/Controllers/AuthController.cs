using EquipmentLoan.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.Dtos.Login;
using EquipmentLoan.Application.Interfaces.Login;
using EquipmentLoan.Application.Interfaces.User;

namespace EquipmentLoan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDto>> Login(
            [FromBody] LoginRequestDto request,
            [FromServices] ILoginUser service)
        {
            try
            {
                LoginResponseDto result = await service.Execute(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponseDto>> Register(
            [FromBody] UserCreateRequestDto request,
            [FromServices] ICreateUser service) 
        {
            try
            {
                UserResponseDto result = await service.Execute(request);
                return Created(string.Empty, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}