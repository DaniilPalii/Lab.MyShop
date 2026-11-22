using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddOpenApi();
builder.Services.SwaggerDocument();

builder.Services.AddTemporalClient(options =>
{
	options.TargetHost = builder.Configuration["Temporal:TargetHost"];
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseFastEndpoints(config =>
{
	config.Endpoints.RoutePrefix = "api";
});

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwaggerGen();
}

await app.RunAsync();
