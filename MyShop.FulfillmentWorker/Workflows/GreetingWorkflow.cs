using MyShop.Contracts.Workflows;
using Temporalio.Activities;
using Temporalio.Workflows;

namespace MyShop.FulfillmentWorker.Workflows;

[Workflow]
public class GreetingWorkflow : IGreetingWorkflow
{
	[WorkflowRun]
	public async Task<string> RunAsync(string name)
	{
		return await Workflow.ExecuteActivityAsync<GreetingActivities, string>(
			activity => activity.SayHello(name),
			options: new()
			{
				StartToCloseTimeout = TimeSpan.FromMinutes(5),
			});
	}
}

public class GreetingActivities
{
	[Activity]
	public string SayHello(string name)
	{
		return $"Hello, {name}!";
	}
}
