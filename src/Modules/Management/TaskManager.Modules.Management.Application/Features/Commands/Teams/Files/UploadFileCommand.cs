using Microsoft.AspNetCore.Http;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams.Files;

public record UploadFileCommand(IFormFile File, Guid TeamId) : ICommand<string>
{
    internal sealed class Handler : ICommandHandler<UploadFileCommand, string>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMemberRepository _memberRepository;
        private readonly IUserService _userService;
        private readonly IFileUploader _fileUploader;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeamRepository teamRepository, ITeamMemberRepository memberRepository, IUserService userService, IFileUploader fileUploader,
            IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
            _userService = userService;
            _fileUploader = fileUploader;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            if (user is null)
                return Result<string>.Unauthorized("User not found");

            var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken);
            if (team is null)
                return Result<string>.NotFound("Team not found");

            var fileUrl = await _fileUploader.UploadFile(request.File);

            team.AddFile(fileUrl);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<string>.Ok(fileUrl);
        }
    }
}