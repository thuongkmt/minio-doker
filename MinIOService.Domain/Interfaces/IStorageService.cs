using MinIOService.Domain.Models;

namespace MinIOService.Domain.Interfaces
{
    public interface IStorageService
    {
        public Task<UploadEntity> GetUpload(string fileName);

        public Task<bool> AddUpload(UploadEntity uploadEntity);
    }
}
