using Microsoft.AspNetCore.Mvc;
using MiscBackend.Data.Entities;
using MiscBackend.DTOs;
using MiscBackend.Services.Calculation;

namespace MiscBackend.Controllers
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
    public async Task<ActionResult<CalculationEntity>> Calculate([FromBody] CalculationRequest request)
    {
      try
      {
        var result = await _service.CalculateAsync(request.NumberOne, request.NumberTwo, request.Operation);
        return CreatedAtAction(nameof(Calculate), new { id = result.Id }, result);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { error = "Internal server error", details = ex.Message });
      }
    }

    [HttpGet("history")]
    public async Task<ActionResult<List<CalculationEntity>>> GetCalculations()
    {
      var calculations = await _service.GetAllCalculationsAsync();
      return Ok(calculations);
    }
  }
}
