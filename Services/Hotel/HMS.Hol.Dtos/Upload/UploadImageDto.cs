using Microsoft.AspNetCore.Http;

namespace HMS.Hol.Dtos.Upload
{
    public class UploadImageDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
