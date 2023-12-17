using AzureStorageService.Repositories;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<FormOptions>(options =>
    options.MultipartBodyLengthLimit = long.MaxValue);

builder.Services.AddSingleton<IBlobRepository, BlobRepository>();
builder.Services.AddSingleton<IContainerRepository, ContainerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () => "Hello, World!");
app.MapControllers();

app.Run();
