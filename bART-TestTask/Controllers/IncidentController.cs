using bART_TestTask.Core.DTOs;
using bART_TestTask.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bART_TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentRequestDTO incidentRequestDto)
        {
            try
            {
                var createdIncident = await _incidentService.CreateIncidentAsync(incidentRequestDto);
                return Ok(createdIncident);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
