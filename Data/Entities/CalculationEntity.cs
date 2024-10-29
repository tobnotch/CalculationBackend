namespace CalculationBackend.Data.Entities
{
  public class CalculationEntity
  {
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public double NumberOne { get; set; }
    public double NumberTwo { get; set; }
    public string Operation { get; set; } = null!;
    public double Result { get; set; }
  }
}
