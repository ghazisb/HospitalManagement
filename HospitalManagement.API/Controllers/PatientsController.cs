using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService service, ILogger<PatientsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {           
            IEnumerable<PatientDto> lstPatients = null;

            try
            {
                lstPatients = await _service.GetAllAsync();
                //return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in PatientsController at {Time}", DateTime.UtcNow);

            }
            return lstPatients is null ? NotFound() : Ok(lstPatients);
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            PatientDto patient = null;
            try
            {
                patient = await _service.GetByIdAsync(id);
                //var patient = await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in PatientsController at {Time}", DateTime.UtcNow);
            }

            return patient is null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        //public async Task<IActionResult> Create(PatientDto dto) => Ok(await _service.CreateAsync(dto));
        public async Task<IActionResult> Create(PatientDto dto)
        {
            PatientDto patient = null;
            try
            {
                patient = await _service.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in PatientsController at {Time}", DateTime.UtcNow);
            }

            return patient is null ? NotFound() : Ok(patient);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatientDto dto)
        {
            PatientDto patient = null;
            try
            {
                if (id != dto.Id) return BadRequest();
                patient = await _service.UpdateAsync(dto);
                //return result == null ? NotFound() : Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in PatientsController at {Time}", DateTime.UtcNow);
            }
            return patient is null ? NotFound() : Ok(patient);
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
                _logger.LogError(ex, "An error occurred in PatientsController at {Time}", DateTime.UtcNow);
            }
            return deleted ? NoContent() : NotFound();
        }
    }
}
