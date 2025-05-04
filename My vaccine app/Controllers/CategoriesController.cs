using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using My_vaccine_app.Dtos.Categories;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoriRequestDto> _validator;
        public CategoriesController(ICategoryService categoryService, IValidator<CategoriRequestDto> validator)
        {
            _categoryService = categoryService;
            _validator = validator;

        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _categoryService.GetAll();
            return Ok(category);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var category = await _categoryService.GetById(Id);
            if (category == null) { return NotFound("not found category with id " + Id); }
            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoriRequestDto request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var category = await _categoryService.Add(request);
            if (category == null) return BadRequest("Check the associated categories, some do not exist");
            return Ok($"Create new category {category.Name} with id {category.Id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) {
                return NotFound("not found category with id " + id);
             }

           await _categoryService.Delete(id);

            return Ok($"Delete successfully {category.Name}");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(CategoriRequestDto request, int id)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var category = await _categoryService.Update(request, id);
            if (category == null) return NotFound("not found category with id " + id);
            return Ok($"Update Successfully the Category {category.Name} with id {category.Id}");
        }
    }
}
