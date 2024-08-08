var builder = WebApplication.CreateBuilder(args);


// Add Services to Container

builder.Services.AddCarter();

builder.Services.AddMediatR(opt =>
{
	opt.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
var app = builder.Build();

app.MapCarter();
// Configure the HTTP Request Pipeline
app.Run();
