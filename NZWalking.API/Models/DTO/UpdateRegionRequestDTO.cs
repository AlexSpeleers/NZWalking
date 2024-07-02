using System.ComponentModel.DataAnnotations;

namespace NZWalking.API.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must contain only 3 chars.")]
        [MaxLength(3, ErrorMessage = "Code must contain only 3 chars.")]
        public string Code { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name must contain at least 2 chars.")]
        [MaxLength(100, ErrorMessage = "Name can't contain more than 100 chars.")]
        public string? RegionImageUrl { get; set; }
    }
}
