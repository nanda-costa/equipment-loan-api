using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces.User;

namespace EquipmentLoan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponseDto>> Create(
            [FromBody] UserCreateRequestDto request,
            [FromServices] ICreateUser service)
        {
            try
            {
                UserResponseDto result = await service.Execute(request);
                return Created(string.Empty, result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll(
            [FromServices] IGetAllUsers service)
        {
            var result = await service.Execute();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetById(
            Guid id,
            [FromServices] IGetUserById service)
        {
            var result = await service.Execute(id);
            if (result == null) return NotFound(new { message = "Usuário não encontrado." });

            return Ok(result);
        }
    }
}