namespace Onker.Models {
	public class PostTagCommentsUser {
		public Post Post { get; set; }
		public Tag Tag { get; set; }
		public ApplicationUser User { get; set; }
		public IEnumerable<Comment> Comments {get; set;}
		public string URLMetadataImage { get; set; }
		public string HumanizedDate { get; set; }
		public List<string> HumanizedDateComments { get; set; }
	}
}
