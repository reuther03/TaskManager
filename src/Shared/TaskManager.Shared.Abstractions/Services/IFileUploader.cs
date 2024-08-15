using Microsoft.AspNetCore.Http;

namespace TaskManager.Abstractions.Services;

public interface IFileUploader
{
    Task<string> UploadFile(IFormFile file);
    void DeleteFile(string publicId);
}