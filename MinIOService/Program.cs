using Microsoft.Extensions.FileProviders;
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
        options.AccessKey = "9BoVgCwfKSc7PjM5";
        options.SecretKey = "IsorQF1iotuGVRho3wVLViHwz6GqfMTc";
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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "storage")),
    RequestPath = "/staticfile"
});

app.UseAuthorization();

app.MapControllers();

app.Run();
