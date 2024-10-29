using Microsoft.AspNetCore.Mvc;
using CalculationBackend.Data.Entities;
using CalculationBackend.DTOs;
using CalculationBackend.Services.Calculation;

namespace CalculationBackend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CalculationController : ControllerBase
  {
    private readonly ICalculationService _service;

    public CalculationController(ICalculationService service)
    {
      _service = service;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult<CalculationResultDTO>> Calculate([FromBody] CalculationRequestDTO request)
    {
      try
      {
        var result = await _service.CalculateAsync(request);
        return CreatedAtAction(nameof(Calculate), new { id = result.Id }, result);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { error = "Internal server error", details = ex.Message });
      }
    }

    [HttpGet("history")]
    public async Task<ActionResult<List<CalculationResultDTO>>> GetCalculations()
    {
      var calculations = await _service.GetAllCalculationsAsync();
      return Ok(calculations);
    }
  }
}
