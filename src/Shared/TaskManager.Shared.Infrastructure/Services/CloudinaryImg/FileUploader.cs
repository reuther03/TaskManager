using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TaskManager.Abstractions.Services;

namespace TaskManager.Infrastructure.Services.CloudinaryImg;

public class FileUploader : IFileUploader
{
    private readonly Cloudinary _cloudinary;

    public FileUploader(IOptions<CloudinaryOptions> options)
    {
        var account = new Account(
            options.Value.cloud_name,
            options.Value.api_key,
            options.Value.api_secret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        if (file.Length is <= 0 or > 20 * 1024 * 1024)
            throw new ArgumentException("Invalid file size");


        List<string> validTypes =
        [
            "text/plain",
            "text/csv",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/pdf",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "image/jpeg",
            "image/png",
            "image/jpg"
        ];
        if (!validTypes.Contains(file.ContentType))
            throw new ArgumentException("Invalid file type");

        await using var stream = file.OpenReadStream();
        var uploadParams = new RawUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            UseFilename = true
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.Url.ToString();
    }
}