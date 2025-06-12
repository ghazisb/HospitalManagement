using HospitalManagement.Application.DTOs;
using HospitalManagement.Application.IServices;
using HospitalManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(IAppointmentService service, ILogger<AppointmentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        //public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        public async Task<IActionResult> GetAll()
        {
            //_logger.LogInformation("AppointmentsController endpoint called at {Time}", DateTime.UtcNow);
            IEnumerable<AppointmentDto> lstAppointments = null;// new IEnumerable<AppointmentDto>();

            try
            {
                //// Simulate something
                //var a = 10;
                //var b = 0;
                //int x = a / b; // Will cause exception
                lstAppointments = await _service.GetAllAsync();
                //return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AppointmentsController at {Time}", DateTime.UtcNow);

            }
            return lstAppointments is null ? NotFound() : Ok(lstAppointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            AppointmentDto appointment = null;
            try
            {
                appointment = await _service.GetByIdAsync(id);
                //var appointment = await _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AppointmentsController at {Time}", DateTime.UtcNow);
            }

            return appointment is null ? NotFound() : Ok(appointment);
        }

        [HttpPost]
        //public async Task<IActionResult> Create(AppointmentDto dto) => Ok(await _service.CreateAsync(dto));
        public async Task<IActionResult> Create(AppointmentDto dto)
        {
            AppointmentDto appointment = null;
            try
            {
                appointment = await _service.CreateAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AppointmentsController at {Time}", DateTime.UtcNow);
            }

            return appointment is null ? NotFound() : Ok(appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AppointmentDto dto)
        {
            AppointmentDto appointment = null;
            try
            {
                if (id != dto.Id) return BadRequest();
                appointment = await _service.UpdateAsync(dto);
                //return result == null ? NotFound() : Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AppointmentsController at {Time}", DateTime.UtcNow);
            }
            return appointment is null ? NotFound() : Ok(appointment);
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
                _logger.LogError(ex, "An error occurred in AppointmentsController at {Time}", DateTime.UtcNow);
            }
            return deleted ? NoContent() : NotFound();
        }
    }
}
