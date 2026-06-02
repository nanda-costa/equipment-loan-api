using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EquipmentLoan.Application.DTOs;
using EquipmentLoan.Application.Interfaces;

namespace EquipmentLoan.API.Controllers;

    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        [Authorize(Roles = "Admin")] 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryResponseDto>> Create(
            [FromBody] CategoryCreateRequestDto request,
            [FromServices] ICreateCategory service)
        {
            CategoryResponseDto result = await service.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll(
            [FromServices] IGetAllCategories service)
        {
            var result = await service.Execute();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryResponseDto>> GetById(
            Guid id,
            [FromServices] IGetCategoryById service)
        {
            CategoryResponseDto result = await service.Execute(id);
            if (result == null) return NotFound(new { message = "Categoria não encontrada." });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")] 
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] CategoryCreateRequestDto request,
            [FromServices] IUpdateCategory service)
        {
            var updated = await service.Execute(id, request);
            if (!updated) return NotFound(new { message = "Categoria não encontrada." });

            return NoContent();
        }

        [Authorize(Roles = "Admin")] 
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromServices] IDeleteCategory service)
        {
            var deleted = await service.Execute(id);
            if (!deleted) return NotFound(new { message = "Categoria não encontrada." });

            return NoContent();
        }
    }    