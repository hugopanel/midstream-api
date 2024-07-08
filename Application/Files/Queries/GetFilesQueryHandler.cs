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
        
        List<FileApp> files = _fileRepository.GetFiles(Guid.Parse(request.Id).ToString());
        
        return new GetFilesResult(files);
    }

}
