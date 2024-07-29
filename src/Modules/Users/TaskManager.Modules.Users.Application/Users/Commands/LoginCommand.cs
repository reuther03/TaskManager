using MediatR;
using TaskManager.Abstractions.Auth;
using TaskManager.Abstractions.Events;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;

namespace TaskManager.Modules.Users.Application.Users.Commands;

public record LoginCommand(string Email, string Password) : ICommand<AccessToken>
{
    internal sealed class Handler : ICommandHandler<LoginCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPublisher _publisher;

        public Handler(IUserRepository userRepository, IJwtProvider jwtProvider, IPublisher publisher)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _publisher = publisher;
        }

        public async Task<Result<AccessToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return Result.Unauthorized<AccessToken>("Authentication failed");

            if (!user.Password.Verify(request.Password))
                return Result.Unauthorized<AccessToken>("Authentication failed");

            var token = AccessToken.Create(_jwtProvider.GenerateToken(user.Id.ToString(), user.Email), user.Id, user.Email);
            await _publisher.Publish(new UserLoggedInEvent(user.Id, user.Email), cancellationToken);

            return Result.Ok(token);
        }
    }
}