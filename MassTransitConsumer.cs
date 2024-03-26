using MassTransit;

namespace MediatorsBenchmark.MassTransit;

public sealed record class MassTransitRequest(string? Message);
public sealed record class MassTransitResponse(string? Message);

public sealed class MassTransitConsumer : IConsumer<MassTransitRequest>
{
    public Task Consume(ConsumeContext<MassTransitRequest> context)
    {
        return context.RespondAsync(
            new MassTransitResponse(context.Message.Message));
    }
}
