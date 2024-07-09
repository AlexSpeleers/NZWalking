using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalking.API.Models.Domain
{
    public class Image
    {
        public Guid ID { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtention { get; set; }
        public long FileSizeInBytes { get; set; } = 0;
        public string FilePath { get; set; }
    }
}
