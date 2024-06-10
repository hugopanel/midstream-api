using MediatR;
namespace Application.Files.Queries;

public record GetAllFilesQuery() : IRequest<GetFilesResult>;