using MediatR;
using TaskManager.Abstractions.Events;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Users.Application.Abstractions;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;

namespace TaskManager.Modules.Users.Application.Users.Commands;

public class UserDeletedCommand : ICommand
{
    internal sealed class Handler : ICommandHandler<UserDeletedCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IImgUploader _imgUploader;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public Handler(IUserService userService, IUserRepository userRepository, IImgUploader imgUploader, IUnitOfWork unitOfWork, IPublisher publisher)
        {
            _userService = userService;
            _userRepository = userRepository;
            _imgUploader = imgUploader;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Result> Handle(UserDeletedCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            if (user is null)
                return Result.Unauthorized("User not found");


            if (user.ProfilePicture is not null && user.ProfilePicturePublicId is not null)
                _imgUploader.DeleteImg(user.ProfilePicturePublicId);

            _userRepository.Remove(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            await _publisher.Publish(new UserDeletedEvent(user.Id), cancellationToken);

            return Result.Ok(user.Id);
        }
    }
}