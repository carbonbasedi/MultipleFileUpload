using Task_PurpleBuzz.Models;

namespace Task_PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkWFU
{
	public class FeaturedWorkWFUUpdateVM
	{
        public FeaturedWorkWFUUpdateVM()
        {
            Photos = new List<IFormFile>();
        }
        public string Description { get; set; }
        public List<FeaturedWorkPhotoWFU> FeaturedWorkPhotoWFUs { get; set; }
        public List<IFormFile>? Photos { get; set; }
    }
}
