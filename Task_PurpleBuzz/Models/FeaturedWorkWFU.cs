namespace Task_PurpleBuzz.Models
{
	public class FeaturedWorkWFU
	{
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public ICollection <FeaturedWorkPhotoWFU> FeaturedWorkPhotoWFUs { get; set; }
    }
}
