using MinIOService.Domain.Interfaces;
using MinIOService.Domain.Models;

namespace MinIOService.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository _storageRepository;

        public StorageService(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task<UploadEntity> GetUpload(string fileName)
        {
            return await _storageRepository.GetUpload(fileName);
        }

        public async Task<bool> AddUpload(UploadEntity uploadEntity)
        {
            return await _storageRepository.AddUpload(uploadEntity);
        }
    }
}
