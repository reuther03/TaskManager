using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;
using TaskManager.Modules.Users.Domain.Users.Entities;
using TaskManager.Modules.Users.Domain.Users.ValueObjects;
using UserPassword = TaskManager.Modules.Users.Domain.Users.ValueObjects.Password;

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
            if (await _userRepository.ExistsWithEmailAsync(new Email(request.Email), cancellationToken))
            {
                return Result<Guid>.BadRequest("User with this email already exists.");
            }

            var fullName = new FullName(request.FullName);
            var email = new Email(request.Email);
            var password = UserPassword.Create(request.Password);

            var user = User.Create(fullName, email, password);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Ok(user.Id.Value);
        }
    }
}