using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Dtos.FamilyGroup;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyGroupController : ControllerBase
    {
        private readonly IFamilyGroupService _familyService;
        private readonly IValidator<GroupRequestDto> _validator;
        public FamilyGroupController(IFamilyGroupService familiService, IValidator<GroupRequestDto> validator)
        {
            _familyService = familiService;
            _validator = validator;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var family = await _familyService.GetAll();
            return Ok(family);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var family = await _familyService.GetById(Id);
            if (family == null) { return NotFound("not found family with id " + Id); }
            return Ok(family);
        }

        [HttpGet("ByUser/{Id}")]
        public async Task<IActionResult> GetFamilyGroupByUserId(int Id)
        {
            var family = await _familyService.GetByUserID(Id);
            if (family.IsNullOrEmpty()) { return NotFound("not found family for the user " + Id); }
            return Ok(family);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupRequestDto request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var family = await _familyService.Add(request);
            if (family == null) return BadRequest("some user to add, not found");
            return Ok("Create new family " + request.Name);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var family = await _familyService.GetById(id);
            if (family == null) return NotFound("not found family with id " + id);
            await _familyService.Delete(id);
            return Ok("Delete successfully " + family.Name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(GroupRequestDto request, int id)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var family = await _familyService.Update(request, id);
            if (family == null) return NotFound("not found family with id " + id+" or not found some users to add");
            return Ok("Update Successfully " + request.Name);
        }
    }
}

