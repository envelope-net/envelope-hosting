using Envelope.Extensions;

namespace Microsoft.Extensions.Hosting;

public static class HostExtensions
{
	public static async Task RunWithTasksAsync(this IHost host, CancellationToken cancellationToken = default)
	{
		var serviceProvider = host.Services;
		await serviceProvider.RunStartupTasksAsync(cancellationToken);
		await host.RunAsync(cancellationToken);
	}
}
