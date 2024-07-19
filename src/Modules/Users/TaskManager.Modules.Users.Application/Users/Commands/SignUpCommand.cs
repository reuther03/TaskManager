using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;
using TaskManager.Modules.Users.Domain.Users.Entities;

namespace TaskManager.Modules.Users.Application.Users.Commands;

public record SignUpCommand(string FullName, string Email, string Password) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<SignUpCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsWithEmailAsync(request.Email, cancellationToken))
            {
                return Result<Guid>.BadRequest("User with this email already exists.");
            }

            var user = User.Create(request.FullName, request.Email, request.Password);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok(user.Id.Value);
        }
    }
}