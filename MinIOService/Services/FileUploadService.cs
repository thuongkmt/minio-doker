using Minio;
using Minio.DataModel;

namespace MinIOService.Services
{
    public class FileUploadService : IFileUploadService
    {
        private MinioClient _minioClient;
        private string bucketName = "images";
        //private string contentType = "image/png";

        public FileUploadService(MinioClient minioClient) {
            _minioClient = minioClient;
        }

        [Obsolete]
        public async Task<string> PutObject(IFormFile file)
        {
            var objectName = "mat-ong-3.jpg";
            var fileName = "mat-ong-3.jpg";
            PutObjectArgs poa = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithFileName(fileName)
                    .WithContentType("image/png");

            await _minioClient.PutObjectAsync(poa);

            StatObjectArgs statObjectArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName);
            ObjectStat objectStat = await _minioClient.StatObjectAsync(statObjectArgs);

            if (objectStat != null)
            {
                return fileName;
            }
            return "";

        }
    }
}
