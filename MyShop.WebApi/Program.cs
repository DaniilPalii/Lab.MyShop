using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddOpenApi();
builder.Services.SwaggerDocument();

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

app.Run();
