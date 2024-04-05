using IMediator = MassTransit.Mediator.IMediator;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MediatorsBenchmark.MassTransit;
using MediatorsBenchmark.MediatR;

namespace MediatorsBenchmark.Services;

public class MediatorsService
{
    private static readonly IRequestClient<MassTransitRequest> _massTransitMediator;
    private static readonly ISender _mediatRMediator;

    static MediatorsService()
    {
        IServiceCollection services = new ServiceCollection();

        // MassTransit
        services
            .AddMediator(cfg => cfg.AddConsumersFromNamespaceContaining<MassTransitConsumer>());

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IMediator mediator = serviceProvider.GetRequiredService<IMediator>();
        _massTransitMediator = mediator.CreateRequestClient<MassTransitRequest>();

        // MediatR
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        serviceProvider = services.BuildServiceProvider();

        _mediatRMediator = serviceProvider.GetRequiredService<ISender>();
    }

    public static MassTransitResponse MassTransitMediator(MassTransitRequest request)
    {
        return _massTransitMediator.GetResponse<MassTransitResponse>(request).Result.Message;
    }

    public static MediatRResponse MediatRMediator(MediatRRequest request)
    {
        return _mediatRMediator.Send(request).Result;
    }

    public static DirectResponse Direct(DirectRequest directRequest) => new(directRequest.Message);
}

public sealed record DirectRequest(string Message);
public sealed record DirectResponse(string Message);
