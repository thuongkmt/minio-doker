using Microsoft.AspNetCore.Http;
using MinIOService.Domain.Models;

namespace MinIOService.Domain.Interfaces
{
    public interface IFileUploadService
    {
        public Task<UploadedResponse> PutObject(IFormFile file, string bucketName);

        public Task<string> GetObject(string objectName, string bucketName);
    }
}
