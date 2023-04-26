namespace MinIOService.Models
{
    public class UploadedFileDto
    {
        public string Etag { get; set; } = "";
        public string FileName { get; set; } = "";
        public long FileSize { get; set; }
    }
}
