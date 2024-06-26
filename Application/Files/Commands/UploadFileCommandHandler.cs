
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Files.Commands;

public class UploadFileCommandHandler(IFileRepository fileRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<UploadFileCommand, UploadFileResult>
{
    private IFileRepository _FileRepository = fileRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<UploadFileResult> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        string name = command.File.FileName;
        string belong = Guid.Parse(command.Belong).ToString();
        string path = belong;
        long size = command.File.Length;
        path = Path.Combine("../Infrastructure/Files", path);
        
        // Créer le dossier s'il n'existe pas
        if (!Directory.Exists(path)){
            Directory.CreateDirectory(path);
        }
        path = Path.Combine(path, name);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await command.File.CopyToAsync(stream);
        }

        var newFile = new FileApp(
            name: name,
            path: path,
            extension: Path.GetExtension(name),
            description: command.Description,
            size: size,
            belong: belong,
            created_date: DateTime.Now,
            modified_date: DateTime.Now
            );
        // Add new FileApp
        _FileRepository.AddFile(newFile);

        return new UploadFileResult(newFile);
    }
}