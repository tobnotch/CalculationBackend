using CalculationBackend.Data.Entities;

namespace CalculationBackend.Interfaces
{
  public interface ICalculationRepository
  {
    Task CreateAsync(CalculationEntity calculation);
    Task<List<CalculationEntity>> GetAllAsync();
  }
}
