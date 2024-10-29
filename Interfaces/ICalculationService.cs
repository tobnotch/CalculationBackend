using CalculationBackend.Data.Entities;
using CalculationBackend.DTOs;

namespace CalculationBackend.Interfaces
{
    public interface ICalculationService
    {
        Task<CalculationResultDTO> CalculateAsync(CalculationRequestDTO request);
        Task<List<CalculationResultDTO>> GetAllCalculationsAsync();
    }
}
