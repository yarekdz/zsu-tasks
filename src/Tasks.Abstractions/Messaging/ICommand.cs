using MediatR;
using Tasks.Domain.Shared;

namespace Tasks.Application.Abstractions.Messaging
{
    public interface ICommand : IBaseCommand, IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IBaseCommand, IRequest<Result<TResponse>>
    {

    }

    public interface IBaseCommand
    {

    }
}
