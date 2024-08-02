using MediatR;
using TaskManager.Abstractions.Events;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Modules.Users.Application.Abstractions;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;
using TaskManager.Modules.Users.Domain.Users.Entities;
using UserPassword = TaskManager.Modules.Users.Domain.Users.ValueObjects.Password;

namespace TaskManager.Modules.Users.Application.Users.Commands;

public record SignUpCommand(string FullName, string Email, string Password, string ProfilePictureUrl) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<SignUpCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IPublisher publisher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsWithEmailAsync(new Email(request.Email), cancellationToken))
                return Result<Guid>.BadRequest("User with this email already exists.");


            var user = User.Create(
                new Name(request.FullName),
                new Email(request.Email),
                UserPassword.Create(request.Password),
                request.ProfilePictureUrl);

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            await _publisher.Publish(new UserCreatedEvent(
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.ProfilePictureUrl
                ),
                cancellationToken);

            return Result.Ok(user.Id.Value);
        }
    }
}