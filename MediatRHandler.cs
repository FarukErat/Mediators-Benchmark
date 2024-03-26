using MediatR;

namespace MediatorsBenchmark.MediatR;

public sealed record class MediatRRequest(string? Message)
    : IRequest<MediatRResponse>;
public sealed record class MediatRResponse(string? Message);

public sealed class MediatRHandler : IRequestHandler<MediatRRequest, MediatRResponse>
{
    public Task<MediatRResponse> Handle(MediatRRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new MediatRResponse(request.Message));
    }
}
