using CalculationBackend.Factories.Operations;
using CalculationBackend.Interfaces;

namespace CalculationBackend.Factories
{
  public class OperationFactory
  {
    public static IOperation CreateOperation(string operation)
    {
      return operation switch
      {
        "+" => new AdditionOperation(),
        "-" => new SubtractionOperation(),
        "×" => new MultiplicationOperation(),
        "/" => new DivisionOperation(),
        _ => throw new InvalidOperationException("Invalid operation")
      };
    }
  }
}
