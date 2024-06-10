
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Files.Queries;

public class GetAllFilesQueryHandler(IFileRepository fileRepository)
    :IRequestHandler<GetAllFilesQuery, GetFilesResult>
{
    private IFileRepository _fileRepository;

    public async Task<GetFilesResult> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        var files = _fileRepository.GetAllFiles();
        return new GetFilesResult(files);
    }

}
