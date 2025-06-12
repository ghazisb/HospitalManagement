using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _service;
        private readonly ILogger<MedicalRecordsController> _logger;

        public MedicalRecordsController(IMedicalRecordService service, ILogger<MedicalRecordsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<MedicalRecordDto> lstMedicalRecordDto = null;// new IEnumerable<AppointmentDto>();

            try
            {

                lstMedicalRecordDto = await _service.GetAllAsync();
                //return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in MedicalRecordsController at {Time}", DateTime.UtcNow);

            }
            return lstMedicalRecordDto is null ? NotFound() : Ok(lstMedicalRecordDto);
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MedicalRecordDto medicalRecordDto = null;
            try
            {
                medicalRecordDto = await _service.GetByIdAsync(id);
                //var medicalRecord = await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in MedicalRecordsController at {Time}", DateTime.UtcNow);
            }

            return medicalRecordDto is null ? NotFound() : Ok(medicalRecordDto);
        }

        [HttpPost]
       //public async Task<IActionResult> Create(MedicalRecordDto dto) => Ok(await _service.CreateAsync(dto));
        public async Task<IActionResult> Create(MedicalRecordDto dto)
        {
            MedicalRecordDto medicalRecord = null;
            try
            {
                medicalRecord = await _service.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in MedicalRecordsController at {Time}", DateTime.UtcNow);
            }

            return medicalRecord is null ? NotFound() : Ok(medicalRecord);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicalRecordDto dto)
        {
            MedicalRecordDto medicalRecord = null;
            try
            {
                if (id != dto.Id) return BadRequest();
                medicalRecord = await _service.UpdateAsync(dto);
                //return result == null ? NotFound() : Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in MedicalRecordsController at {Time}", DateTime.UtcNow);
            }
            return medicalRecord is null ? NotFound() : Ok(medicalRecord);

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
                _logger.LogError(ex, "An error occurred in MedicalRecordsController at {Time}", DateTime.UtcNow);
            }
            return deleted ? NoContent() : NotFound();
        }
    }
}
