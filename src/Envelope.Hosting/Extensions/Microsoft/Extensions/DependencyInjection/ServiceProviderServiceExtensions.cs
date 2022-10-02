using Envelope.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Envelope.Extensions;

public static class ServiceProviderServiceExtensions
{
	public static async Task RunStartupTasksAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
	{
		if (serviceProvider == null)
			throw new ArgumentNullException(nameof(serviceProvider));

		var startupTasks = serviceProvider.GetServices<IStartupTask>();

		foreach (var startupTask in startupTasks)
			await startupTask.ExecuteAsync(serviceProvider, cancellationToken);
	}
}
