using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Services.Contracts;
using System.Runtime.CompilerServices;

namespace My_vaccine_app.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DependentsController : ControllerBase
    {
        private readonly IDependentService _dependentService;
        private readonly IValidator<DependentRequestDto> _validator;
        public DependentsController(IDependentService dependentService, IValidator<DependentRequestDto> validator)
        {
            _dependentService = dependentService;
            _validator=validator;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dependents = await _dependentService.GetAll();
            return Ok(dependents);
        }

        [HttpGet("{Id}")]
        public async Task <IActionResult> GetById(int Id)
        {
            var dependent = await _dependentService.GetById(Id);
            if(dependent == null) { return  NotFound("not found dependent with id "+Id); }
            return Ok(dependent);
        }

        [HttpGet("ByUser/{Id}")]
        public async Task<IActionResult> GetDependentsByUserId(int Id)
        {
            var dependent = await _dependentService.GetByUserID(Id);
            if (dependent.IsNullOrEmpty()) { return NotFound("not found dependent for the user " + Id); }
            return Ok(dependent);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DependentRequestDto request)
        {
            var validation= await _validator.ValidateAsync(request);
            if(!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var dependent = await _dependentService.Add(request);
            return Ok("Create new dependent "+request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dependent = await _dependentService.GetById(id);
            if (dependent == null) return NotFound("not found dependent with id " + id);
             await _dependentService.Delete(id);
            return Ok("Delete successfully " + dependent.ToString());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(DependentRequestDto request, int id)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var dependent = await _dependentService.Update(request, id);
            if (dependent == null) return NotFound("not found dependent with id " + id);
            return Ok("Update Successfully "+request);
        }
    }
}
