using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorsController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _service.GetByIdAsync(id);
            return doctor is null ? NotFound() : Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorDto dto) => Ok(await _service.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DoctorDto dto)
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
