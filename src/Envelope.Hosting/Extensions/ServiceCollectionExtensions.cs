using Envelope.Identity;
using Envelope.Localization;
using Envelope.Trace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Envelope.Hosting.Extensions;

public static partial class ServiceCollectionExtensions
{
	public static IServiceCollection AddHostApplicationContext(
		this IServiceCollection services,
		string systemName,
		Func<EnvelopePrincipal?>? principalGetter)
	{
		services.TryAddScoped<IApplicationContext>(sp =>
		{
			var traceFrame = TraceFrame.Create();
			var traceInfo =
				new TraceInfoBuilder(systemName, traceFrame, null)
					.CorrelationId(Guid.NewGuid())
					.ExternalCorrelationId(Guid.NewGuid().ToString("D"))
					.Principal(principalGetter != null ? principalGetter() : null)
					.Build();

			var appResources = sp.GetRequiredService<IApplicationResources>();
			var appCtx = new ApplicationContext(traceInfo, appResources, null);
			return appCtx;
		});

		return services;
	}
}
