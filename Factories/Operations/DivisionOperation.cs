using CalculationBackend.Interfaces;

namespace CalculationBackend.Factories.Operations
{
  public class DivisionOperation : IOperation
  {
    public double Calculate(double numberOne, double numberTwo)
    {
      if (numberTwo == 0)
      {
        throw new InvalidOperationException("Division by zero is not allowed");
      }

      return numberOne / numberTwo;
    }
  }
}
