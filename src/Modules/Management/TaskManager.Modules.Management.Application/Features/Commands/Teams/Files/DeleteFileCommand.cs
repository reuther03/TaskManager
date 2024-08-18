using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams.Files;

public record DeleteFileCommand(Guid FileId, Guid TeamId) : ICommand<string>
{
    internal sealed class Handler : ICommandHandler<DeleteFileCommand, string>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMemberRepository _memberRepository;
        private readonly IUserService _userService;
        private readonly IFileUploader _fileUploader;
        private readonly ITeamFileRepository _teamFileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            ITeamRepository teamRepository,
            ITeamMemberRepository memberRepository,
            IUserService userService,
            IFileUploader fileUploader,
            IUnitOfWork unitOfWork,
            ITeamFileRepository teamFileRepository)
        {
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
            _userService = userService;
            _fileUploader = fileUploader;
            _unitOfWork = unitOfWork;
            _teamFileRepository = teamFileRepository;
        }

        public async Task<Result<string>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var user = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            if (user is null)
                return Result<string>.Unauthorized("User not found");

            var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken);
            if (team is null)
                return Result<string>.NotFound("Team not found");

            if (!await _memberRepository.MemberInTeamAsync(user.UserId, team.Id, cancellationToken))
                return Result<string>.BadRequest("User is not team member");

            var file = await _teamFileRepository.GetByIdAsync(request.FileId, cancellationToken);
            if (file is null)
                return Result<string>.NotFound("File not found");

            if (!team.TeamFiles.Contains(file))
                return Result<string>.BadRequest("File not found in team");

            _fileUploader.DeleteFile(file.FileName);
            _teamFileRepository.Remove(file);
            team.RemoveFile(file);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<string>.Ok("File deleted");
        }
    }
}