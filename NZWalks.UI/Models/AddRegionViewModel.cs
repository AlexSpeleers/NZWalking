using System.ComponentModel.DataAnnotations;

namespace NZWalks.UI.Models
{
    public class AddRegionViewModel
    {
        [Display(Name=nameof(Code))]
        public string Code { get; set; }

        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        [Display(Name = nameof(RegionImageUrl))]
        public string RegionImageUrl { get; set; }
    }
}
