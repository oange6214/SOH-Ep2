using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SohatNotebook.DataService.Data;
using SohatNotebook.DataService.IConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddApiVersioning(opt =>
{
    // Provides to the client the different Api versions that we have. 
    opt.ReportApiVersions = true;

    // This will allow the api to automatically provide a default version.
    opt.AssumeDefaultVersionWhenUnspecified = true;

    opt.DefaultApiVersion = ApiVersion.Default;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
