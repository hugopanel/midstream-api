using MediatR;
namespace Application.Files.Queries;

public record GetFilesQuery(string Id) : IRequest<GetFilesResult>;