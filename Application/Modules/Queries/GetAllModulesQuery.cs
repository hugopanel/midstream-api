using MediatR;
namespace Application.Modules.Queries;

public record GetAllModulesQuery() : IRequest<GetModulesResult>;