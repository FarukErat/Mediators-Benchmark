using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

using MediatorsBenchmark.MediatR;
using MediatorsBenchmark.MassTransit;
using MediatorsBenchmark.Services;

namespace MediatorsBenchmark;

[MemoryDiagnoser]
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
    public void MassTransit() => MediatorsService.MassTransitMediator(new MassTransitRequest(_message));

    [Benchmark]
    public void MediatR() => MediatorsService.MediatRMediator(new MediatRRequest(_message));

    [Benchmark]
    public void DirectResponse() => MediatorsService.Direct(new DirectRequest(_message));
}
