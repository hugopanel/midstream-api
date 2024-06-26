using Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Application.Files;
public record GetFileResult(FileApp FileApp, byte[] File);

