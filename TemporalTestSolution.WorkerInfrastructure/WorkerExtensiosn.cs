using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Temporalio.Client;

namespace TemporalTestSolution.WorkerInfrastructure;

public static class WorkerExtensions
{
    public static void AddTemporalClient(this IServiceCollection services, TemporalClientConnectOptions options)
    {
        services.AddSingleton(ctx =>
        {
            var loggerFactory = ctx.GetRequiredService<ILoggerFactory>();
            options.LoggerFactory = loggerFactory;

            return TemporalClient.ConnectAsync(options);
        });
    }
}