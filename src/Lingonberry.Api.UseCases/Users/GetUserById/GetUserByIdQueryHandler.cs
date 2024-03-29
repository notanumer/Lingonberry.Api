using AutoMapper;
using MediatR;
using Lingonberry.Api.Domain.Users;
using Lingonberry.Api.Infrastructure.Abstractions.Interfaces;
using Lingonberry.Api.UseCases.Handlers;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.EntityFrameworkCore;
using Saritasa.Tools.Common.Utils;

namespace Lingonberry.Api.UseCases.Users.GetUserById;

/// <summary>
/// Handler for <see cref="GetUserByIdQuery" />.
/// </summary>
internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    internal class GetUserByIdQueryMappingProfile : Profile
    {
        public GetUserByIdQueryMappingProfile()
        {
            CreateMap<User, UserDetailsDto>();
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="dbContext">Database context.</param>
    /// <param name="mapper">Automapper instance.</param>
    public GetUserByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.Location)
            .GetAsync(u => u.Id == request.UserId, cancellationToken: cancellationToken);

        var result = mapper.Map<UserDetailsDto>(user);
        result.Location = user.Location?.Name;
        result.WorkType = EnumUtils.GetDescription(user.WorkType);
        return result;
    }
}
