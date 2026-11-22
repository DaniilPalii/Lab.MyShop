using Temporalio.Workflows;

namespace MyShop.Contracts.Workflows;

[Workflow]
public interface IGreetingWorkflow
{
	[WorkflowRun]
	Task<string> RunAsync(string name);
}

public static class GreetingWorkflowIds
{
	public static string Create()
		=> $"greeting-{Guid.CreateVersion7()}";
}
