using CalculationBackend.Interfaces;

namespace CalculationBackend.Factories.Operations
{
  public class AdditionOperation : IOperation
  {
    public double Calculate(double numberOne, double numberTwo) => numberOne + numberTwo;
  }
}
