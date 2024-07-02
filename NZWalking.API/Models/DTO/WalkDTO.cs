using NZWalking.API.Models.Domain;

namespace NZWalking.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public DifficultyDTO Difficulty { get; set; }
        public RegionDTO Region { get; set; }
    }
}
