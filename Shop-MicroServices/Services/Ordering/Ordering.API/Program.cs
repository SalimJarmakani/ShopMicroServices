using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.
    AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();


//Add Services to Container
var app = builder.Build();
// Configure the HTTP Request Pipeline


app.UseApiServices();




if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatanaseAsync();
}

app.Run();
