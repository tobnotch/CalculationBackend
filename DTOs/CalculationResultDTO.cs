namespace CalculationBackend.DTOs
{
  public class CalculationResultDTO
  {
    public string Id { get; set; } = null!;
    public double Result { get; set; }
    public string Operation { get; set; } = null!;
  }
}
