using CalculationBackend.Data.Entities;
using CalculationBackend.Data.Repositories;

namespace CalculationBackend.Services.Calculation
{
  public class CalculationService : ICalculationService
  {
    private readonly CalculationRepository _repository;

    public CalculationService(CalculationRepository repository)
    {
      _repository = repository;
    }

    public async Task<CalculationEntity> CalculateAsync(double numberOne, double numberTwo, string operation)
    {
      double result;

      switch (operation)
      {
        case "+":
          result = numberOne + numberTwo;
          break;
        case "-":
          result = numberOne - numberTwo;
          break;
        case "×":
          result = numberOne * numberTwo;
          break;
        case "/":
          if (numberTwo != 0)
            result = numberOne / numberTwo;
          else
            throw new InvalidOperationException("Division by zero is not allowed");
          break;
        default:
          throw new InvalidOperationException("Invalid operation");
      }

      // Skapa en ny Calculation-objekt för att spara i databasen
      var calculation = new CalculationEntity
      {
        NumberOne = numberOne,
        NumberTwo = numberTwo,
        Operation = operation,
        Result = result
      };

      // Använd _repository för att spara beräkningen i databasen
      await _repository.CreateAsync(calculation);

      return calculation; // Returnera den beräknade Calculation
    }

    public async Task<List<CalculationEntity>> GetAllCalculationsAsync()
    {
      return await _repository.GetAllAsync();
    }
  }
}
