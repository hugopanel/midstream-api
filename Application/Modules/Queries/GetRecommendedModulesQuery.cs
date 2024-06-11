using MediatR;
namespace Application.Modules.Queries;

public record GetRecommendedModulesQuery(string descriptionRequest) : IRequest<GetModulesResult>;