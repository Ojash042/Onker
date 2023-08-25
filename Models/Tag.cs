namespace Onker.Models {
	public class Tag {
		public Guid TagId { get; set; }
		public string TagName { get; set; } = null!;
		public ICollection<Post> Posts { get; set; }
	}
}
