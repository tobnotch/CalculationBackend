using CalculationBackend.Data.Entities;
using CalculationBackend.Data.Repositories;
using CalculationBackend.DTOs;

namespace CalculationBackend.Services.Calculation
{
  public class CalculationService : ICalculationService
  {
    private readonly CalculationRepository _repository;

    public CalculationService(CalculationRepository repository)
    {
      _repository = repository;
    }

    public async Task<CalculationResultDTO> CalculateAsync(CalculationRequestDTO request)
    {
      double result;

      switch (request.Operation)
      {
        case "+":
          result = request.NumberOne + request.NumberTwo;
          break;
        case "-":
          result = request.NumberOne - request.NumberTwo;
          break;
        case "×":
          result = request.NumberOne * request.NumberTwo;
          break;
        case "/":
          if (request.NumberTwo != 0)
            result = request.NumberOne / request.NumberTwo;
          else
            throw new InvalidOperationException("Division by zero is not allowed");
          break;
        default:
          throw new InvalidOperationException("Invalid operation");
      }

      var entity = CalculationMapper.ToEntity(request);
      entity.Result = result;
      await _repository.CreateAsync(entity);

      var DTO = CalculationMapper.ToDTO(entity);
      return DTO; // Returnera den beräknade Calculation
    }

    public async Task<List<CalculationResultDTO>> GetAllCalculationsAsync()
    {
      var entities = await _repository.GetAllAsync();
      var DTOs = entities.Select(CalculationMapper.ToDTO).ToList();
      return DTOs;
    }
  }
}
