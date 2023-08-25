using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onker.Models {
	public class Post {
		public Guid PostId { get; set; }
		[Required]
		public string Title { get; set; } = null!;
		public string UrlLink { get; set; }
		public string PostText { get; set; }
		public int PostKarma { get; set; } = 0;

		public ICollection<Comment> Comments { get; set; }

		public Guid TagId { get; set; }
		public Tag Tag { get; set; }

		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public ApplicationUser OriginalPoster { get; set; }

	}
}
