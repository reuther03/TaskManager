using MediatR;
using TaskManager.Abstractions.Kernel.Primitives.Result;

namespace TaskManager.Abstractions.QueriesAndCommands.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;