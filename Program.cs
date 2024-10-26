using MiscBackend.Data.Repositories;
using MiscBackend.Services.Calculation;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoClient>(s =>
{
  var settings = builder.Configuration.GetConnectionString("MongoDb");
  return new MongoClient(settings);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CalculationRepository>();
builder.Services.AddTransient<ICalculationService, CalculationService>();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowFrontend",
      policy =>
      {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174") // Lokal frontend och Hub frontend
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

//builder.WebHost.ConfigureKestrel(options =>
//{
//  options.ListenAnyIP(8080); // HTTP port
//  //options.ListenAnyIP(8081); // HTTPS port
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
  app.UseHttpsRedirection();
}

app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();
