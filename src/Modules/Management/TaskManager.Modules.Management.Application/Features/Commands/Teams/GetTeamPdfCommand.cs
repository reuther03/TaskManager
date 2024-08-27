using iTextSharp.text;
using iTextSharp.text.pdf;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database.Repositories;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record GetTeamPdfCommand(Guid TeamId) : ICommand<string>
{
    internal sealed class Handler : ICommandHandler<GetTeamPdfCommand, string>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IManagementUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserService _userService;

        public Handler(ITeamRepository teamRepository, IManagementUserRepository userRepository,ITaskRepository taskRepository, IUserService userService)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _userService = userService;
        }

        public async Task<Result<string>> Handle(GetTeamPdfCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByIdAsync(_userService.UserId, cancellationToken).Result;
            if (user is null)
                return Result<string>.NotFound("User not found");

            var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken);
            if (team is null)
                return Result<string>.NotFound("Team not found");

            var pdfPath = Path.Combine(Path.GetTempPath(), $"{team.Name}.pdf");

            using (var stream = new FileStream(pdfPath, FileMode.Create))
            {
                var document = new iTextSharp.text.Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();
                document.Add(new Paragraph($"Team Name: {team.Name}"));
                document.Add(new Paragraph($"Description: {team.TeamMembers}"));
                document.Add(new Paragraph($"Description: {team.Progress}"));
                foreach (var taskId in team.TaskItemIds)
                {
                    var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken);
                    document.Add(new Paragraph($"Task: {task}"));
                }

                document.Close();
            }

            return Result<string>.Ok(pdfPath);
        }
    }
}