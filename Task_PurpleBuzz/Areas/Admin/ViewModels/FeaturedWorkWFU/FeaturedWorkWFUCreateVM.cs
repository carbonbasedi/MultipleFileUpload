using System.ComponentModel.DataAnnotations;

namespace Task_PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkWFU
{
	public class FeaturedWorkWFUCreateVM
	{
        public FeaturedWorkWFUCreateVM()
        {
            Photos = new List<IFormFile>();
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
