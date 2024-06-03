using Microsoft.EntityFrameworkCore;
using UserModule.Service;
using Serilog;
using UserModule.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepository, UserRepository>();
var provider = builder.Services.BuildServiceProvider();
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
builder.Services.AddDbContext<BankingServiceContext>(item => item.UseSqlServer(config.GetConnectionString("Scaffold-DbContext")));
builder.Services.AddTransient<IUserService, UserService>();
// Apply logging 
builder.Logging.ClearProviders();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7022");

                      });
});
var path = config.GetValue<string>("Logging:FilePath");
var logger = new LoggerConfiguration()
    .WriteTo.File(path)
    .CreateLogger();
builder.Logging.AddSerilog(logger);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("_myAllowSpecificOrigins");


app.UseAuthorization();

app.MapControllers();

app.Run();
