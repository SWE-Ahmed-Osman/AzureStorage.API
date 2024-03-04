using AzureStorage.Repositories;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<FormOptions>(options =>
    options.MultipartBodyLengthLimit = long.MaxValue);

builder.Services.AddSingleton<IBlobRepository, BlobRepository>();
builder.Services.AddSingleton<IContainerRepository, ContainerRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();