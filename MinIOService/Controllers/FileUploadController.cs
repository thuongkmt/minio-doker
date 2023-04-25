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
        public async Task<string> Upload(IFormFile file)
        {
            var filename = await _fileUploadService.PutObject(file);
            return filename;
        }
    }
}
