using BlogV4.Application.Extensions;
using BlogV4.Application.Notifications;
using BlogV4.Application.Repositories;
using BlogV4.Domain.DTOs.Input;
using BlogV4.Domain.DTOs.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogV4.Controllers
{
    [Route("v4")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet("categories")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _categoryService.GetCategories(cancellationToken);

                return Ok(new Notification<List<CategoryOutputDto>>(existing));
            }
            catch
            {
                return StatusCode(500, new Notification<List<CategoryOutputDto>>("Não foi possível localizar categorias"));
            }
        }

        [HttpGet("categories/{categoryId}")]
        public async Task<IActionResult> GetById(Guid categoryId, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _categoryService.GetCategoryId(categoryId, cancellationToken);

                if (existing is null)
                    return NotFound(new Notification<CreateCategoryDto>("Categoria não localizada"));

                return Ok(existing);

            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CreateCategoryDto>("Falha interna no servidor"));
            }

        }

        [HttpPost("categories")]
        public async Task<IActionResult> Post(CreateCategoryDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new Notification<CreateCategoryDto>(ModelState.GetErrors()));

                var category = await _categoryService.Add(model);

                return Created($"categories/{category.Id}", category);

            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CreateCategoryDto>("Não foi possível criar categoria"));
            }
            catch
            {
                return StatusCode(500, new Notification<CreateCategoryDto>("Erro interno no servidor"));
            }

        }

        [HttpPut("categories/{categoryId}")]
        public async Task<IActionResult> Put(Guid categoryId, CreateCategoryDto model, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _categoryService.GetCategoryId(categoryId, cancellationToken);

                if (existing is null)
                    return NotFound(new Notification<CreateCategoryDto>("Categoria não localizada"));

                await _categoryService.Update(categoryId, model);

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CreateCategoryDto>("Não foi possível atualizar a categoria"));
            }
            catch
            {
                return StatusCode(500, new Notification<CreateCategoryDto>("Erro interno no servidor"));
            }

        }

        [HttpDelete("categories/{categoryId}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            try
            {
                var existing = await _categoryService.Delete(categoryId);

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new Notification<CreateCategoryDto>("Não foi possível remover categoria"));
            }
            catch
            {
                return StatusCode(500, new Notification<CreateCategoryDto>("Erro interno no servidor"));
            }
        }
    }
}
