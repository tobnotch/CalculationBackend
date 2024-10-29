namespace CalculationBackend.DTOs
{
  public class CalculationRequestDTO
  {
    public double NumberOne { get; set; }
    public double NumberTwo { get; set; }
    public string Operation { get; set; } = null!;
  }
}
