using Microsoft.EntityFrameworkCore;
using PetSim.Server.Data;
using PetSim.Server.Services;
using PetSim.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add services to the container.
builder.Services.AddDbContext<PetSimContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PetSimContext")).EnableSensitiveDataLogging().LogTo(Console.WriteLine));
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<StatsService>();
builder.Services.AddScoped<PetStatsRepository>();
builder.Services.AddScoped<PetRepository>();
builder.Services.AddScoped<StatsActionService>();
builder.Services.AddScoped<StatsActionRepository>();
builder.Services.AddAutoMapper(typeof(PetProfile));
builder.Services.AddAutoMapper(typeof(StatsProfile));

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<PetSimContext>();
        var env = services.GetRequiredService<IWebHostEnvironment>();
        context.Database.CanConnect(); // Test the DB connection
        Console.WriteLine("Connected to DB successfully!");
        DataSeeder.Seed(context, env);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect to DB: {ex.Message}");
}


app.UseDefaultFiles();
app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
