using NuGet.Protocol.Core.Types;
using Task_PurpleBuzz.Models;

namespace Task_PurpleBuzz.Areas.Admin.ViewModels.FeaturedWorkWFU
{
    public class FeaturedWorkWFUDetailsVM
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<FeaturedWorkPhotoWFU> Photos { get; set; }
    }
}
