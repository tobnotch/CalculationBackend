using CalculationBackend.Data.Entities;
using CalculationBackend.DTOs;

namespace CalculationBackend.Services
{
  public static class CalculationMapper
  {
    public static CalculationEntity ToEntity(CalculationRequestDTO DTO)
    {
      return new CalculationEntity
      {
        NumberOne = DTO.NumberOne,
        NumberTwo = DTO.NumberTwo,
        Operation = DTO.Operation,
      };
    }

    public static CalculationResultDTO ToDTO(CalculationEntity entity)
    {
      return new CalculationResultDTO
      {
        Id = entity.Id,
        NumberOne = entity.NumberOne,
        NumberTwo = entity.NumberTwo,
        Operation = entity.Operation,
        Result = entity.Result,
      };
    }
  }
}
