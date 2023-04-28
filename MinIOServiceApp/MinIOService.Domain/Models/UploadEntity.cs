using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinIOService.Domain.Models
{
    [Index(nameof(Id), IsUnique = false, Name = "Index_Id")]
    public class UploadEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public string FileName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ContentType { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Etag { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string FileType { get; set; }
    }
}
