using Microsoft.EntityFrameworkCore;
using Minio.AspNetCore;
using MinIOService.Domain.Interfaces;
using MinIOService.Infrastructure;
using MinIOService.Infrastructure.Repositories;
using MinIOService.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
var configuration = builder.Configuration;
// Url based configuration
builder.Services.AddMinio(
    options =>
    {
        options.Endpoint = configuration["MinIOService:Endpoint"];
        options.AccessKey = configuration["MinIOService:AccessKey"];
        options.SecretKey = configuration["MinIOService:SecretKey"];
    });
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MinIOServiceDBContext>(options => options.UseMySql(connectionString, serverVersion));

builder.Services.AddTransient<IFileUploadService, FileUploadService>();
builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddTransient<IStorageRepository, StorageRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
