using EcommerenceBackend.Application.UseCases.Configurations;
using EcommerenceBackend.Infrastructure.Configurations;
using EcommerenceBackend.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom DbContexts
builder.Services.AddCustomDbContexts(builder.Configuration);

// Register repositories
builder.Services.ConfigureRepositories(builder.Configuration);
builder.Services.AddFluentValidationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS 
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
