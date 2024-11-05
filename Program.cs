using CalculationBackend.Data.Repositories;
using CalculationBackend.Interfaces;
using CalculationBackend.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MongoDb");
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICalculationRepository, CalculationRepository>();
builder.Services.AddTransient<ICalculationService, CalculationService>();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowFrontend",
      policy =>
      {
        policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
      });
});

builder.WebHost.ConfigureKestrel(options =>
{
  options.ListenAnyIP(8080); // HTTP port (kommer endast använda mig av denna i utvecklingsläge)
  //options.ListenAnyIP(8081, listenOptions =>
  //{
  //  // Läs lösenord från docker-compose.yml
  //  var certPassword = Environment.GetEnvironmentVariable("CERT_PASSWORD");
  //  listenOptions.UseHttps("/root/certs/localhostcert.pfx", certPassword); // HTTPS port med certfikatet
  //});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();
