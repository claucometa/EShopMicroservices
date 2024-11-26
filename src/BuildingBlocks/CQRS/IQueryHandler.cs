using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : ICommand<TResponse> where TResponse : notnull;

    public interface IQueryHandler<in TQuery> :
       ICommandHandler<TQuery, Unit> where TQuery : ICommand<Unit>;


}


