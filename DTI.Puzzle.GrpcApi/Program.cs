using DTI.Puzzle.GrpcApi.Services;
using DTI.Puzzle.Application;
using DTI.Puzzle.Persistence;
using Microsoft.OpenApi.Models;
using Google.Api;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureGlossaryApplicationServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcReflectionService();
app.MapGrpcService<PuzzleService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
