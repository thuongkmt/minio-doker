using Microsoft.EntityFrameworkCore;
using MinIOService.Domain.Interfaces;
using MinIOService.Domain.Models;

namespace MinIOService.Infrastructure.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly MinIOServiceDBContext _dbContext;

        public StorageRepository(MinIOServiceDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UploadEntity> GetUpload(string fileName)
        {
            var uploadData = await _dbContext.Uploads.FirstOrDefaultAsync(x => x.FileName == fileName);
            return uploadData;
        }

        public async Task<bool> AddUpload(UploadEntity uploadEntity)
        {
            _dbContext.Add(uploadEntity);
            var created = await _dbContext.SaveChangesAsync();

            return created > 0;
        }
    }
}
