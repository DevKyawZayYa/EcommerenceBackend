using EcommerenceBackend.Application.UseCases.Commands.RegisterUser;
using EcommerenceBackend.Application.UseCases.Configurations;
using EcommerenceBackend.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext before building the app
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("NorthwindConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)),
        b => b.MigrationsAssembly("EcommerenceBackend.Infrastructure")
    ));

// Register repositories
builder.Services.ConfigureRepositories();
builder.Services.AddFluentValidationServices();
builder.Services.AddJwtAuthentication(builder.Configuration);

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
