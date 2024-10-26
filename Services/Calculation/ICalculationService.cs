using CalculationBackend.Data.Entities;

namespace CalculationBackend.Services.Calculation
{
  public interface ICalculationService
  {
    Task<CalculationEntity> CalculateAsync(double numberOne, double numberTwo, string operation);
    Task<List<CalculationEntity>> GetAllCalculationsAsync();
  }
}
