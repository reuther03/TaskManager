﻿using Microsoft.AspNetCore.Http;

namespace TaskManager.Abstractions.Services;

public interface IImgUploader
{
    Task<string> UploadFile(IFormFile file);
}