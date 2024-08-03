using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Queries;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Users.Application.Abstractions.Database;
using TaskManager.Modules.Users.Application.Users.Dto;

namespace TaskManager.Modules.Users.Application.Users.Queries;

public class GetCurrentUserQuery : IQuery<UserDto>
{
    internal sealed class Handler : IQueryHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(IUserDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);

            return user is null
                ? Result.NotFound<UserDto>("User not found")
                : Result.Ok(UserDto.AsDto(user));
        }
    }
}