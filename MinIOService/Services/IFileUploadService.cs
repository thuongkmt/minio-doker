using MinIOService.Models;

namespace MinIOService.Services
{
    public interface IFileUploadService
    {
        public Task<UploadedResponse> PutObject(IFormFile file, string bucketName);

        public Task<string> GetObject(string objectName, string bucketName);
    }
}
