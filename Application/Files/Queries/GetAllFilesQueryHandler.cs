
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Files.Queries;

public class GetAllFilesQueryHandler(/*IFileRepository fileRepository*/)
    :IRequestHandler<GetAllFilesQuery, GetFilesResult>
{
    private IFileRepository _fileRepository;

    public async Task<GetFilesResult> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        // var files = _fileRepository.GetAllFiles();

        // charger le fichier files.json dans la variable files "../Application/Files/Queries/files.json"
        var files = JsonSerializer.Deserialize<List<FileApp>>(File.ReadAllText("../Application/Files/Queries/files.json"));
        
        return new GetFilesResult(files);
    }

}
