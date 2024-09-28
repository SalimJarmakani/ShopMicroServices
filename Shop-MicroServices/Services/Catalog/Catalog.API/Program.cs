

using Core.Services.Behaviors;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
// Add Services to Container
builder.Services.AddCarter();

builder.Services.AddMediatR(opt =>
{
	opt.RegisterServicesFromAssembly(assembly);
	opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
	opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

//Initialize Marten Data If Environment is Development

if (builder.Environment.IsDevelopment())
{
	builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
var app = builder.Build();

app.MapCarter();
// Configure the HTTP Request Pipeline

app.UseExceptionHandler(opts => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.Run();
