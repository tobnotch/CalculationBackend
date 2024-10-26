using MiscBackend.Data.Entities;
using MongoDB.Driver;

namespace MiscBackend.Data.Repositories
{
  public class CalculationRepository
  {
    private readonly IMongoCollection<CalculationEntity> _calculations;

    public CalculationRepository(IMongoClient mongoClient)
    {
      // Använd IMongoClient för att hämta databasen
      var database = mongoClient.GetDatabase("calcdb");
      _calculations = database.GetCollection<CalculationEntity>("calculations");
    }

    public async Task CreateAsync(CalculationEntity calculation)
    {
      await _calculations.InsertOneAsync(calculation);
    }

    public async Task<List<CalculationEntity>> GetAllAsync()
    {
      return await _calculations.Find(c => true).ToListAsync();
    }
  }
}
