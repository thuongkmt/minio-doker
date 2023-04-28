using Minio;
using Minio.DataModel;
using MinIOService.Domain.Interfaces;
using MinIOService.Domain.Models;
using System.Text;

namespace MinIOService.Services
{
    public class FileUploadService : IFileUploadService
    {
        private MinioClient _minioClient;
        //private IStorageService _storageService;

        public FileUploadService(MinioClient minioClient/*, IStorageService storageService*/) {
            _minioClient = minioClient;
            //_storageService = storageService;
        }

        [Obsolete]
        public async Task<UploadedResponse> PutObject(IFormFile file, string bucketName)
        {
            try
            {
                //change the file name
                var fileExtension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
                string fileName = $"{Guid.NewGuid()}.{fileExtension}";

                //check the content-type of file
                //TODO: Check the content-type that allowed by our system later
                var contentType = file.ContentType;
                
                if (file.Length > 0)
                {
                    //check exist or create bucket 
                    bool isFoundBucketName = await _minioClient.BucketExistsAsync(bucketName);
                    if (!isFoundBucketName)
                    {
                        await _minioClient.MakeBucketAsync(bucketName);
                    }
                    //upload file to MINIO
                    PutObjectArgs poa;
                    StatObjectArgs statObjectArgs;
                    ObjectStat objectStat;
                    var fileStream = file.OpenReadStream();
                    poa = new PutObjectArgs()
                             .WithBucket(bucketName)
                             .WithObject(fileName)
                             .WithStreamData(fileStream)
                             .WithObjectSize(fileStream.Length)
                             .WithContentType("application/octet-stream");
                    await _minioClient.PutObjectAsync(poa);

                    statObjectArgs = new StatObjectArgs()
                       .WithBucket(bucketName)
                       .WithObject(fileName);
                    objectStat = await _minioClient.StatObjectAsync(statObjectArgs);
                    
                    /*//insert into db
                    var inserted = await _storageService.AddUpload(new UploadEntity
                    {
                        ContentType = contentType,
                        Etag = objectStat.ETag,
                        FileName = fileName,
                        FileType = file.ContentType
                    });*/

                    return new UploadedResponse
                    {
                        Data = new UploadedFileDto
                        {
                            Etag = objectStat.ETag,
                            FileName= fileName,
                            FileSize = objectStat.Size
                        }
                    };
                }
                else
                {
                    return new UploadedResponse
                    {
                        Data = new UploadedFileDto
                        {
                            Etag = "",
                            FileName = "",
                            FileSize = 0
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new UploadedResponse
                {
                    Data = null,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<string> GetObject(string objectName, string bucketName)
        {
            //check object
            var statObjectArgs = new StatObjectArgs()
                       .WithBucket(bucketName)
                       .WithObject(objectName);
            var objectStat = await _minioClient.StatObjectAsync(statObjectArgs);

            var getObjectArgs = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithFile(objectName);
            var objectBack = await _minioClient.GetObjectAsync(getObjectArgs);

            return "";
        }

        public async Task<UploadedResponse> UploadLocalServer(IFormFile file, string folder)
        {
            var fileExtension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
            string fileName = $"{Guid.NewGuid()}.{fileExtension}";
            //check exist the uploadedfile folder or create
            var currentDirectory = Environment.CurrentDirectory;
            string rootFolderPath = Path.GetFullPath(Path.Combine(currentDirectory, "UploadedFiles"));
            if (!Directory.Exists(rootFolderPath))
            {
                Directory.CreateDirectory(rootFolderPath);
            }
            //check the folder
            string folderPath = Path.GetFullPath(Path.Combine(rootFolderPath, folder));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            //create file on the server
            var rootPathFile = Path.Combine(folderPath, fileName);
            using (var fileStream = new FileStream(rootPathFile, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return new UploadedResponse
            {
                Data = new UploadedFileDto
                {
                    Etag = "",
                    FileName = "",
                    FileSize = 0
                }
            };
        }
    }
}
