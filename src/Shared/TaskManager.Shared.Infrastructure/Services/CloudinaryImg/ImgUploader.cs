using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TaskManager.Abstractions.Services;

namespace TaskManager.Infrastructure.Services.CloudinaryImg;

public class ImgUploader : IImgUploader
{
    private readonly Cloudinary _cloudinary;

    public ImgUploader(IOptions<CloudinaryOptions> options)
    {
        var account = new Account(
            options.Value.cloud_name,
            options.Value.api_key,
            options.Value.api_secret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImg(IFormFile file)
    {
        if (file.Length is <= 0 or > 5 * 1024 * 1024) // 5 MB limit
            throw new ArgumentException("Invalid file size");

        List<string> validTypes = ["image/jpeg", "image/png", "image/jpg"];
        if (!validTypes.Contains(file.ContentType))
            throw new ArgumentException("Invalid file type");

        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream)
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.Url.ToString();
    }
}