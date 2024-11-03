using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_vaccine_app.Dtos.Allergies;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Services.Contracts;
using AllergyRequestDto = My_vaccine_app.Dtos.Allergies.AllergyRequestDto;

namespace My_vaccine_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController : ControllerBase
    {
        private readonly IAllergyService _allergyService;
        private readonly IValidator<Dtos.Allergies.AllergyRequestDto> _validator;
        public AllergyController(IAllergyService allergyService, IValidator<Dtos.Allergies.AllergyRequestDto> validator)
        {
            _allergyService = allergyService;
            _validator = validator;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allergies = await _allergyService.GetAll();
            return Ok(allergies);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var allergies = await _allergyService.GetById(Id);
            if (allergies == null) { return NotFound("not found Allergy with id " + Id); }
            return Ok(allergies);
        }

        [HttpGet("ByUser/{Id}")]
        public async Task<IActionResult> GetAllergyByUserId(int Id)
        {
            var Allergies = await _allergyService.GetByUserID(Id);
            if (Allergies.IsNullOrEmpty()) { return NotFound("not found Allergies for the user " + Id); }
            return Ok(Allergies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AllergyRequestDto request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var Allergies = await _allergyService.Add(request);
            return Ok("Create new Allergies " + request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Allergy = await _allergyService.GetById(id);
            if (Allergy == null) return NotFound("not found Allergies with id " + id);
            await _allergyService.Delete(id);
            return Ok("Delete successfully " + Allergy.ToString());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(AllergyRequestDto request, int id)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var Allergy = await _allergyService.Update(request, id);
            if (Allergy == null) return NotFound("not found Allergies with id " + id);
            return Ok("Update Successfully " + request);
        }
    }
}
