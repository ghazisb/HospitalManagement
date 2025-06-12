using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorService service, ILogger<DoctorsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        //public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<DoctorDto> lstDocotors = null;
            try
            {
                lstDocotors = await _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DoctorsController at {Time}", DateTime.UtcNow);
            }
            return lstDocotors is null ? NotFound() : Ok(lstDocotors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            DoctorDto doctor = null;
            try
            {
                doctor = await _service.GetByIdAsync(id);
                //var doctor = await _service.GetByIdAsync(id);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DoctorsController at {Time}", DateTime.UtcNow);
            }

            return doctor is null ? NotFound() : Ok(doctor);
        }

        [HttpPost]
        //public async Task<IActionResult> Create(DoctorDto dto) => Ok(await _service.CreateAsync(dto));
        public async Task<IActionResult> Create(DoctorDto dto)
        {
            DoctorDto doctor = null;
            try
            {
                doctor = await _service.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DoctorsController at {Time}", DateTime.UtcNow);
            }

            return doctor is null ? NotFound() : Ok(doctor);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DoctorDto dto)
        {
            DoctorDto doctor = null;
            try
            {
                if (id != dto.Id) return BadRequest();
                doctor = await _service.UpdateAsync(dto);
                //return result == null ? NotFound() : Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DoctorsController at {Time}", DateTime.UtcNow);
            }
            return doctor is null ? NotFound() : Ok(doctor);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = false;
            try
            {
                deleted = await _service.DeleteAsync(id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in DoctorsController at {Time}", DateTime.UtcNow);
            }
            return deleted ? NoContent() : NotFound();

        }
    }
}
