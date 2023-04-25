using Minio.AspNetCore;
using MinIOService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Url based configuration
builder.Services.AddMinio(
    options =>
    {
        options.Endpoint = "localhost:9000";
        options.AccessKey = "xpT7Kmp1ZGi2l35F";
        options.SecretKey = "ssNg5yoz0h7pqnzFeT9z1hP8zCBIMjYw";

    });

builder.Services.AddTransient<IFileUploadService, FileUploadService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
