using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Users.Application.Abstractions;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;

namespace TaskManager.Modules.Users.Application.Users.Commands;

public class DeleteUserCommand : ICommand
{
    internal sealed class Handler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserService userService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            if (user is null)
                return Result.Unauthorized("User not found");

            _userRepository.Remove(user);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok(user.Id);
        }
    }
}