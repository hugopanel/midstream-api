using MediatR;
using Microsoft.AspNetCore.Http;
namespace Application.Files.Commands;

public record UploadFileCommand(
    IFormFile File,
    string Description,
    string Belong
    ) : IRequest<UploadFileResult>;