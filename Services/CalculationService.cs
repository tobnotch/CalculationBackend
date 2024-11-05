using CalculationBackend.Data.Entities;
using CalculationBackend.Data.Repositories;
using CalculationBackend.DTOs;
using CalculationBackend.Factories;
using CalculationBackend.Interfaces;

namespace CalculationBackend.Services
{
  public class CalculationService : ICalculationService
  {
    private readonly ICalculationRepository _repository;

    public CalculationService(ICalculationRepository repository)
    {
      _repository = repository;
    }

    public async Task<CalculationResultDTO> CalculateAsync(CalculationRequestDTO request)
    {
      var operation = OperationFactory.CreateOperation(request.Operation);

      double result = operation.Calculate(request.NumberOne, request.NumberTwo);

      var entity = CalculationMapper.ToEntity(request);
      entity.Result = result;
      await _repository.CreateAsync(entity);

      var DTO = CalculationMapper.ToDTO(entity);
      return DTO;
    }

    public async Task<List<CalculationResultDTO>> GetAllCalculationsAsync()
    {
      var entities = await _repository.GetAllAsync();
      var DTOs = entities.Select(CalculationMapper.ToDTO).ToList();
      return DTOs;
    }
  }
}
