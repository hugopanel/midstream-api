
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Files.Queries;

public class GetAllFilesQueryHandler(IFileRepository fileRepository)
    :IRequestHandler<GetAllFilesQuery, GetFilesResult>
{
    private IFileRepository _fileRepository = fileRepository;

    public async Task<GetFilesResult> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        var files = _fileRepository.GetAllFiles();
        // trier du plus récent au plus ancien
        files.Sort((a, b) => b.Modified_date.CompareTo(a.Modified_date));

        
        return new GetFilesResult(files);
    }

}
