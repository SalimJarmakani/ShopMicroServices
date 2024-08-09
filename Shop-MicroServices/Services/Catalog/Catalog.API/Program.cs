var builder = WebApplication.CreateBuilder(args);


// Add Services to Container

builder.Services.AddCarter();

builder.Services.AddMediatR(opt =>
{
	opt.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
var app = builder.Build();

app.MapCarter();
// Configure the HTTP Request Pipeline
app.Run();
