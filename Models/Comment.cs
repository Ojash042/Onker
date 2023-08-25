using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onker.Models {
	public class Comment {
		public Guid CommentId { get; set; }

		[Required]
		public string CommentText { get; set; } = null!;

		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public ApplicationUser OrigianalCommenter { get; set; } = null!;

		public Guid PostId { get; set; }
		public Post Post { get; set; } = null!;
	}
}
