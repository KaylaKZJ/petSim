using Microsoft.EntityFrameworkCore;
using PetSim.Server.Data;
using PetSim.Server.Services;
using PetSim.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


 // Add services to the container.
builder.Services.AddDbContext<PetSimContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PetSimContext")).EnableSensitiveDataLogging().LogTo(Console.WriteLine)); // Logs to console for debugging
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<StatsService>();
builder.Services.AddScoped<StatsRepository>();
builder.Services.AddScoped<PetRepository>();
builder.Services.AddAutoMapper(typeof(PetProfile));
builder.Services.AddAutoMapper(typeof(StatsProfile));

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<PetSimContext>();
        context.Database.CanConnect(); // Test the DB connection
        Console.WriteLine("Connected to DB successfully!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect to DB: {ex.Message}");
}


app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
