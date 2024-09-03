

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
// Add Services to Container

builder.Services.AddCarter();

builder.Services.AddMediatR(opt =>
{
	opt.RegisterServicesFromAssembly(assembly);
	opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
	opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();
// Configure the HTTP Request Pipeline

app.UseExceptionHandler(opts => { });

app.Run();
