using MediatR;

namespace TaskManager.Modules.Management.Application.Workflows;

public interface IWorkflowEngine
{
    public Task<Guid> AssignMemberToTaskAsync(Guid teamId, CancellationToken cancellationToken = default);
}