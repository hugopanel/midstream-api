using MediatR;
namespace Application.Files.Commands;
public record DeleteFileCommand(string Id) : IRequest<DeleteFileResult>;