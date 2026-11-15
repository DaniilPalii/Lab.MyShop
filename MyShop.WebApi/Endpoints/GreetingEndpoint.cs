using FastEndpoints;

namespace MyShop.WebApi.Endpoints;

public class GreetingEndpoint : Endpoint<GreetingRequest>
{
	public override void Configure()
	{
		Post("/greeting");
		AllowAnonymous();
	}

	public override async Task HandleAsync(GreetingRequest request, CancellationToken ct)
	{
		await Send.OkAsync(
			new GreetingResponse($"Hello, {request.Name}!"),
			ct);
	}
}

public record GreetingRequest(string Name);

public record GreetingResponse(string Greeting);
