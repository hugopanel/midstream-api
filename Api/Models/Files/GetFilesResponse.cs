using Domain.Entities;
namespace Api.Models.Files;

public record GetFilesResponse(List<FileApp> Files);
