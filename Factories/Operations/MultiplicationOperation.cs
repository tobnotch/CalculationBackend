using CalculationBackend.Interfaces;

namespace CalculationBackend.Factories.Operations
{
  public class MultiplicationOperation : IOperation
  {
    public double Calculate(double numberOne, double numberTwo) => numberOne * numberTwo;
  }
}
