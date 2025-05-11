using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
    AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();


//Add Services to Container
var app = builder.Build();
// Configure the HTTP Request Pipeline

app.Run();
