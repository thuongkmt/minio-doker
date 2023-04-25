using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinIOService.Services;
using System.IO.Compression;

namespace MinIOService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : Controller
    {
        private IFileUploadService _fileUploadService;
        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost(Name = "Upload")]
        public async Task<List<string>> Upload(IFormFile file)
        {
            var arrayFileName = new List<string>();
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                if (file.Length > 0)
                {
                    var filename = await _fileUploadService.PutObject(file.Name, memoryStream);
                    arrayFileName.Add(filename);
                }
            }
            return arrayFileName;
        }
    }
}
