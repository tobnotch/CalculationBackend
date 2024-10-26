using MiscBackend.Data.Entities;

namespace MiscBackend.Services.Calculation
{
  public interface ICalculationService
  {
    Task<CalculationEntity> CalculateAsync(double numberOne, double numberTwo, string operation);
    Task<List<CalculationEntity>> GetAllCalculationsAsync();
  }
}
