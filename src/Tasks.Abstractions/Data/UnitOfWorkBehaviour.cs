using System.Transactions;
using MediatR;

namespace Tasks.Application.Abstractions.Data
{
    public sealed class UnitOfWorkBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where  TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (IsNotCommand())
            {
                return await next();
            }

            using var transactionScope = new TransactionScope();

            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            transactionScope.Complete();

            return response;
        }

        private static bool IsNotCommand()
        {
            return !typeof(TRequest).Name.EndsWith("Command");
        }
    }
}
