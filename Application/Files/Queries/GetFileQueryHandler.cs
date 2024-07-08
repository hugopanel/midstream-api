using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Files.Queries;

public class GetFileQueryHandler(IFileRepository fileRepository)
    : IRequestHandler<GetFileQuery, GetFileResult>
{
    private IFileRepository _fileRepository = fileRepository;

    public async Task<GetFileResult> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {

        var fileApp = _fileRepository.GetFile(request.Id);

        var path = fileApp.path;
        var file = File.ReadAllBytes(path);
        return new GetFileResult(fileApp,file);
    }

}
