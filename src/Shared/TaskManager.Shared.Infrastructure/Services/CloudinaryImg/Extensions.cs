using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Abstractions.Services;

namespace TaskManager.Infrastructure.Services.CloudinaryImg;

public static class Extensions
{
    public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudinaryOptions>(configuration.GetRequiredSection(CloudinaryOptions.SectionName));
        services.AddSingleton<IFileUploader, FileUploader>();
        services.AddSingleton<IImgUploader, ImgUploader>();

        return services;
    }
}