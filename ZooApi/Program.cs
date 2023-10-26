using Microsoft.EntityFrameworkCore;
using Zoo.Entities;
using Zoo.Services;
using ZooApi.Data;
using ZooApi.Interface;
using ZooApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IService<Animal>, AnimalService>();
builder.Services.AddScoped<IService<Caretaker>, CaretakerService>();
builder.Services.AddScoped<IRepository<Caretaker>, CaretakerRepository>();
builder.Services.AddScoped<IRepository<Animal>, AnimalRepository>();
builder.Services.AddDbContext<ZooContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("Default"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
