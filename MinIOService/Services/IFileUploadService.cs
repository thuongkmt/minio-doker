﻿namespace MinIOService.Services
{
    public interface IFileUploadService
    {
        public Task<string> PutObject(string objectName, MemoryStream filestream);
    }
}
