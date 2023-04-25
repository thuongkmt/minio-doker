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
        public async Task<string> PutObject(string objectName, MemoryStream filestream)
        {
            var fileName = "";
            bool found = await _minioClient.BucketExistsAsync(bucketName);

            PutObjectArgs poa = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(filestream)
                .WithObjectSize(filestream.Length)
                .WithContentType("application/octet-stream");
            
            await _minioClient.PutObjectAsync(poa);

            StatObjectArgs statObjectArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);
            ObjectStat objectStat = await _minioClient.StatObjectAsync(statObjectArgs);
            
            if(objectStat != null)
            {
                return fileName;
            }
            return "";
        }
    }
}
