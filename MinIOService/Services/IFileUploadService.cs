namespace MinIOService.Services
{
    public interface IFileUploadService
    {
        public Task<string> PutObject(IFormFile file);
    }
}
