namespace Onker.Models {
	public class PostTagUser {
		public Post Post { get; set; }
		public Tag Tag { get; set; }
		public ApplicationUser User { get; set; }
		public string? UrlMetadataImage { get; set; }
		public string HumanizedDate { get; set; }
	}
}
