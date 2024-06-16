using MediatR;

namespace Application.Teams.Commands;

public record CreateProjectCommand(string Name, string Description) : IRequest<ProjectResult>;