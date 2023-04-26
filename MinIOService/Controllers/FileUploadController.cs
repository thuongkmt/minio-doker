using Microsoft.AspNetCore.Mvc;
using MinIOService.Models;
using MinIOService.Services;

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

        [HttpPost]
        public async Task<UploadedResponse> Upload(IFormFile file, string bucket)
        {
            var uploadedResponse = await _fileUploadService.PutObject(file, bucket);
            return uploadedResponse;
        }

        [HttpGet]
        public async Task<string> GetObject(string objectName, string bucket)
        {
            var uploadedResponse = await _fileUploadService.GetObject(objectName, bucket);
            return uploadedResponse;
        }
    }
}
