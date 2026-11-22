using MyShop.Contracts.Workflows;
using MyShop.FulfillmentWorker.Workflows;
using Temporalio.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
	.AddTemporalClient(options =>
	{
		options.TargetHost = builder.Configuration["Temporal:TargetHost"];
	})
	.AddHostedTemporalWorker(TaskQueueIds.MyShop)
	.AddWorkflow<GreetingWorkflow>()
	.AddScopedActivities<GreetingActivities>();

var host = builder.Build();
await host.RunAsync();
