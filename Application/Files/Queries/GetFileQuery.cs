using MediatR;
namespace Application.Files.Queries;

public record GetFileQuery(string Id) : IRequest<GetFileResult>;