using Envelope.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting;

public static class HostExtensions
{
	public static async Task RunWithTasksAsync(this IHost host, CancellationToken cancellationToken = default)
	{
		var serviceProvider = host.Services;
		var startupTasks = serviceProvider.GetServices<IStartupTask>();

		using(var scope = serviceProvider.CreateScope())
		{
			var sp = scope.ServiceProvider;

			foreach (var startupTask in startupTasks)
				await startupTask.ExecuteAsync(sp, cancellationToken);
		}

		await host.RunAsync(cancellationToken);
	}
}
