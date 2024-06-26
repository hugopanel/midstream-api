
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Files.Queries;

public class GetFilesQueryHandler(IFileRepository fileRepository)
    :IRequestHandler<GetFilesQuery, GetFilesResult>
{
    private IFileRepository _fileRepository = fileRepository;

    public async Task<GetFilesResult> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine(Guid.Parse(request.Id).ToString());
        var files = _fileRepository.GetFiles(Guid.Parse(request.Id).ToString());
        
        return new GetFilesResult(files);
    }

}
