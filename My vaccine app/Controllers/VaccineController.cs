using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_vaccine_app.Dtos.Allergies;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;
        private readonly IValidator<VaccineRequestDto> _validator;
        public VaccineController(IVaccineService vaccineService, IValidator<VaccineRequestDto> validator)
        {
            _vaccineService = vaccineService;
            _validator = validator;

        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccine = await _vaccineService.GetAll();
            return Ok(vaccine);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var vaccines = await _vaccineService.GetById(Id);
            if (vaccines == null) { return NotFound("not found vaccines with id " + Id); }
            return Ok(vaccines);
        }


        [HttpPost]
        public async Task<IActionResult> Create(VaccineRequestDto request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var vaccines = await _vaccineService.Add(request);
            if (vaccines == null) return BadRequest("Check the associated categories, some do not exist");
            return Ok("Create new vaccines " + vaccines.Name+ " with categories "+vaccines.Categories);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vaccine = await _vaccineService.GetById(id);
            if (vaccine == null) {
                return NotFound("not found vaccines with id " + id);
             }

           await _vaccineService.Delete(id);

            return Ok("Delete successfully " + vaccine.Name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(VaccineRequestDto request, int id)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var Vaccine = await _vaccineService.Update(request, id);
            if (Vaccine == null) return NotFound("not found vaccines with id " + id);
            return Ok("Update Successfully the vaccine: " + request.Name);
        }
    }
}
