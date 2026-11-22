using FastEndpoints;
using MyShop.Contracts.Workflows;
using Temporalio.Client;

namespace MyShop.WebApi.Endpoints;

public class GreetingEndpoint(ITemporalClient temporalClient)
	: Endpoint<GreetingRequest>
{
	public override void Configure()
	{
		Post("/greeting");
		AllowAnonymous();
	}

	public override async Task HandleAsync(GreetingRequest request, CancellationToken cancellationToken)
	{
		var handle = await temporalClient.StartWorkflowAsync<IGreetingWorkflow, string>(
			workflow => workflow.RunAsync(request.Name),
			options: new(
				id: GreetingWorkflowIds.Create(),
				taskQueue: TaskQueueIds.MyShop));

		var result = await handle.GetResultAsync<string>(
			rpcOptions: new() { CancellationToken = cancellationToken});

		await Send.OkAsync(
			new GreetingResponse(result),
			cancellationToken);
	}
}

public record GreetingRequest(string Name);

public record GreetingResponse(string Greeting);
