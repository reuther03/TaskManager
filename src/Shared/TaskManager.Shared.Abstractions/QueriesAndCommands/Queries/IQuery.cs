using MediatR;
using TaskManager.Abstractions.Kernel.Primitives.Result;

namespace TaskManager.Abstractions.QueriesAndCommands.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;