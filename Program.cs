using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

using IMediator = MassTransit.Mediator.IMediator;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

using MediatorsBenchmark.MediatR;
using MediatorsBenchmark.MassTransit;

namespace MediatorsBenchmark;

public class Program
{
    private const string _message = "Hello, World!";
    private static void Main()
    {
        ManualConfig config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);

        BenchmarkRunner.Run<Program>(config);
    }

    [Benchmark]
    public void MassTransit() => MassTransitMediator(new MassTransitRequest(_message));

    [Benchmark]
    public void MediatR() => MediatRMediator(new MediatRRequest(_message));

    public static MassTransitResponse MassTransitMediator(MassTransitRequest request)
    {
        return _massTransitMediator.GetResponse<MassTransitResponse>(request).Result.Message;
    }

    public static MediatRResponse MediatRMediator(MediatRRequest request)
    {
        return _mediatRMediator.Send(request).Result;
    }

    private static readonly IRequestClient<MassTransitRequest> _massTransitMediator;
    private static readonly ISender _mediatRMediator;
    static Program()
    {
        IServiceCollection services = new ServiceCollection();

        services
            .AddMediator(cfg => cfg.AddConsumersFromNamespaceContaining<MassTransitConsumer>())
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IMediator mediator = serviceProvider.GetRequiredService<IMediator>();
        _massTransitMediator = mediator.CreateRequestClient<MassTransitRequest>();

        _mediatRMediator = serviceProvider.GetRequiredService<ISender>();
    }
}
