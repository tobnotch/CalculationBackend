using CalculationBackend.Interfaces;

namespace CalculationBackend.Factories.Operations
{
  public class SubtractionOperation : IOperation
  {
    public double Calculate(double numberOne, double numberTwo) => numberOne - numberTwo;
  }
}
