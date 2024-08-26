using Microsoft.AspNetCore.Http;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Users.Application.Abstractions;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;

namespace TaskManager.Modules.Users.Application.Users.Commands;

public record UpdateUserCommand(string FullName, string Email, IFormFile File) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<UpdateUserCommand, Guid>
    {
        private readonly IUserService _userService;
        private readonly IImgUploader _imgUploader;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserService userService, IImgUploader imgUploader, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _imgUploader = imgUploader;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            if (user is null)
                return Result<Guid>.Unauthorized("User not found");

            var imgUrl = await _imgUploader.UploadImg(request.File);
            user.Update(request.FullName, request.Email, imgUrl, request.File.FileName);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<Guid>.Ok(user.Id.Value);
        }
    }
}