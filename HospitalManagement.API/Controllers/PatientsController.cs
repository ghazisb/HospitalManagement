using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        //public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllAsync();

            return Ok(results);
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            return patient is null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientDto dto) => Ok(await _service.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatientDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var result = await _service.UpdateAsync(dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
