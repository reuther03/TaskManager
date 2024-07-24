﻿using Microsoft.Extensions.DependencyInjection;
using TaskManager.Abstractions.Kernel;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;

namespace TaskManager.Infrastructure.Postgres.Decorators;

[Decorator]
internal class TransactionalCommandHandlerDecorator<T> : ICommand<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;
    private readonly IServiceProvider _serviceProvider;

    public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler, UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
    {
        _handler = handler;
        _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        _serviceProvider = serviceProvider;
    }

    public async Task<Result> Handle(T request, CancellationToken cancellationToken)
    {
        var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve<T>();
        if (unitOfWorkType is null)
        {
            await _handler.Handle(request, cancellationToken);
            return Result.Ok();
        }

        var unitOfWork = (IBaseUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok();
    }
}