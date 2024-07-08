using MediatR;
namespace Application.Modules.Queries;

public record GetRecommendedModulesQuery(string DescriptionRequest) : IRequest<GetModulesResult>;