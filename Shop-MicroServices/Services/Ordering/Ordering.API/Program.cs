using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add Services to Container
var app = builder.Build();

builder.Services.
    AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);
// Configure the HTTP Request Pipeline

app.Run();
