using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using My_vaccine_app.Dtos.Dependent;
using My_vaccine_app.Dtos.VaccineRecord;
using My_vaccine_app.Services.Contracts;
using My_vaccine_app.Services.Implements;
using System.Security.Claims;

namespace My_vaccine_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineRecordController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IValidator<RecordRequestDto> _validator;
        public VaccineRecordController(IRecordService recordService, IValidator<RecordRequestDto> validator) 
        { 
            _recordService = recordService;
            _validator = validator;
        }
        [HttpGet("records-user")]
        public async Task<IActionResult> GetInfoCurrentUser()
        {
            var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
            if (!claimIdentity.IsAuthenticated)
                return Unauthorized();
            var response = await _recordService.GetByCurrentUser(claimIdentity.Name);
            if (response == null) return NotFound("Not found records for the current user");

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _recordService.GetAll();
            return Ok(records);
        }


        [HttpGet("ByUser/{Id}")]
        public async Task<IActionResult> GetRecordsByUserId(int Id)
        {
            var records = await _recordService.GetByUserID(Id);
            if (records.IsNullOrEmpty()) { return NotFound("not found records for the user " + Id); }
            return Ok(records);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecordRequestDto request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            var dependent = await _recordService.Add(request);
            return Ok("Create new records successfully ");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response= await _recordService.Delete(id);
            if(response == null) return NotFound("Not found record with id: "+id);
            return Ok("Delete successfully the record vaccine with id "+id);
        }
    }
}
