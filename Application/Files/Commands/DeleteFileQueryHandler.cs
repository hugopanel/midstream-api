using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;


namespace Application.Files.Commands;
public class DeleteFileQueryHandler(IFileRepository fileRepository)
    : IRequestHandler<DeleteFileCommand, DeleteFileResult>
{
    private IFileRepository _fileRepository = fileRepository;
    public async Task<DeleteFileResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var file = _fileRepository.GetFile(request.Id);
        if (file == null)
        {
            return new DeleteFileResult(null, "File not found");
        }
        File.Delete(file.path);
        _fileRepository.DeleteFile(file);

        return new DeleteFileResult(file, "File deleted successfully");
    }
}
