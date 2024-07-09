using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalking.API.Models.Domain;
using NZWalking.API.Models.DTO;
using NZWalking.API.Repositories;

namespace NZWalking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IImageRepository imageRepository) : ControllerBase
    {
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var imageDomainModel = new Image
            {
                File = request.File,
                FileExtention = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                FileDescription = request.FileDescription
            };
            await imageRepository.Upload(imageDomainModel);
            return Ok(imageRepository);

        }
        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtentions = new string[] { ".jpg", "jpeg", ".png" };
            if (!allowedExtentions.Contains(Path.GetExtension(request.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extention.");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please choose smaller size file.");
            }
        }
    }
}