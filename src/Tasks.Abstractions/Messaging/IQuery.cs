using MediatR;
using Tasks.Domain.Shared;

namespace Tasks.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
