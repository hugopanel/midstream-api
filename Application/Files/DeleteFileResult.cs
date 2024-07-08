using Domain.Entities;
namespace Application.Files;

public record DeleteFileResult(FileApp fileDeleted, string message);