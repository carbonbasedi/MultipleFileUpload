namespace Task_PurpleBuzz.Models
{
	public class FeaturedWorkPhotoWFU
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int FeaturedWorkWFUId { get; set; }
        public DateTime CreatedAt { get; set; }
        public FeaturedWorkWFU FeaturedWorkWFU { get; set; }
    }
}
