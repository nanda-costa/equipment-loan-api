using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces.User;
using Microsoft.AspNetCore.Authorization;

namespace EquipmentLoan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [AllowAnonymous] 
    [HttpPost("register")]
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
        
    [Authorize(Roles = "Admin")] 
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll(
        [FromServices] IGetAllUsers service)
    {
        IEnumerable<UserResponseDto> result = await  service.Execute();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")] 
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetById(
        Guid id,
        [FromServices] IGetUserById service)
    {
        UserResponseDto result = await service.Execute(id);
        if (result == null) return NotFound(new { message = "Usuário não encontrado." });

        return Ok(result);
    }
}