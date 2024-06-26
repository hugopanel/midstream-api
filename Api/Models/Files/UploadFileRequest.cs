namespace Api.Models.Files;

public record UploadFileRequest(
    IFormFile File,
    string Description,
    string Belong
    );
