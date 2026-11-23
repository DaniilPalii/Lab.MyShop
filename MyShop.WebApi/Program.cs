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
	app.UseOpenApi();

	const string swaggerRoute = "/swagger";
	app.UseSwaggerUi(config => config.Path = swaggerRoute);

	app.Lifetime.ApplicationStarted.Register(() =>
	{
		var logger = app.Services.GetRequiredService<ILoggerFactory>()
			.CreateLogger("Swagger");

		foreach (var appUrl in app.Urls)
			logger.LogInformation("Swagger UI: {appUrl}{swaggerRoute}", appUrl, swaggerRoute);
	});
}

await app.RunAsync();
